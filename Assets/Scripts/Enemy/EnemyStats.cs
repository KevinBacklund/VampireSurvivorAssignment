using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    public float currentHealth;
    public float currentMoveSpeed;
    public float currentDamage;
    public int xpDrop;
    float dmgCooldown;

    [Header("Damage Feedback")]
    EnemyMovement movement;

    void Awake()
    {
        currentHealth = enemyData.MaxHealth;
        currentMoveSpeed = enemyData.MoveSpeed;
        currentDamage = enemyData.Damage;
        xpDrop = enemyData.XpDrop;
    }

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
    }

    public void TakeDamage(float dmg, Vector2 sourcePosittion, float knockbackForce = 5f, float knockbackDuration = 0.2f)
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        currentHealth -= dmg * player.CurrentDmgMultiplier;
        SoundManager.PlaySfx(SfxType.enemyDamaged, 0.5f);
        if(knockbackForce > 0)
        {
            Vector2 direction = (Vector2)transform.position - sourcePosittion;
            movement.Knockback(direction.normalized * knockbackForce, knockbackDuration);
        }
        if (currentHealth <= 0 )
        {
            Kill();
        }
    }
    void Kill()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
        player.IncreaseExperience(xpDrop);
        GameManager.currentScore++;
        SoundManager.PlaySfx(SfxType.enemyDeath);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (dmgCooldown > 0)
        {
            dmgCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collider) 
    {
        if (dmgCooldown <= 0)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerStats player = collider.gameObject.GetComponent<PlayerStats>();
                player.TakeDamage(currentDamage);
                dmgCooldown = 1;
                Vector2 direction = (Vector2)transform.position - (Vector2)player.transform.position;
                movement.Knockback(direction.normalized, 0.5f);
            }
        }
    }
}
