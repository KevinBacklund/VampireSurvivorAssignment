using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    public GameObject prefab;
    float currentCooldown;
    protected PlayerMovement pm;
    protected virtual void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
        currentCooldown = weaponData.CooldownDuration;
    }

    protected virtual void Update()
    {
        if (!GameManager.gamePaused)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0f)
            {
                Attack();
            }
        }
    }
 
    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
