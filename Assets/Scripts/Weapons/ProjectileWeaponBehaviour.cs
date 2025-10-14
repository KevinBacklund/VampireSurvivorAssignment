using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    public float destroyAfterSeconds;
    private Vector3 rotation;
    private float rotationDegrees;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;
    protected float currentKnockbackforce;
    protected float currentRotationOffset;
    protected float destroyTimer;

    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentPierce = weaponData.Pierce;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentKnockbackforce = weaponData.KnockbackForce;
        currentRotationOffset = weaponData.RotationOffset;
    }
    protected virtual void Start()
    {
        //Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void Update()
    {
        if (!GameManager.gamePaused)
        {
            destroyTimer += Time.deltaTime;
            if (destroyTimer >= destroyAfterSeconds)
            {
                Destroy(gameObject);
                destroyTimer = 0;
            }
        }
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
        rotationDegrees = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        rotation = new Vector3(0, 0, currentRotationOffset - rotationDegrees);
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage, gameObject.transform.position, currentKnockbackforce);
            if (weaponData.Pierce >= 0)
            {
                Pierce();
            }
        }

    }

    void Pierce()
    {
        currentPierce--;
        if (currentPierce < 0)
        {
            Destroy(gameObject);
        }
    }
}
