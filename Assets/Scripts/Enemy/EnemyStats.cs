using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    public float currentHealth;
    public float currentMoveSpeed;
    public float currentDamage;
    public int xpDrop;
    float dmgCooldown;
    Animator animator;

    [Header("Damage Feedback")]
    public Color damageColor = new Color(1,0,0,1);
    public float damageFlashDuration = 0.1f;
    public float deathFadeTime = 0.3f;
    Color originalColor;
    public SpriteRenderer spriteRenderer;
    EnemyMovement movement;
    public GameObject deathParticles;

    void Awake()
    {
        currentHealth = enemyData.MaxHealth;
        currentMoveSpeed = enemyData.MoveSpeed;
        currentDamage = enemyData.Damage;
        xpDrop = enemyData.XpDrop;
    }
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }


    IEnumerator DamageFlash()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageFlashDuration);
        spriteRenderer.color = originalColor;
    }   

    IEnumerator DeathFade()
    {
        float elapsed = 0f;
        while (elapsed < deathFadeTime)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / deathFadeTime);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void TakeDamage(float dmg, Vector2 sourcePosittion, float knockbackForce = 5f, float knockbackDuration = 0.2f)
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        currentHealth -= dmg * player.CurrentDmgMultiplier;
        SoundManager.PlaySfx(SfxType.enemyDamaged, 0.5f);
        StartCoroutine(DamageFlash());
        if (knockbackForce > 0)
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
        DroprateManager drops = GetComponent<DroprateManager>();
        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
        drops.DropItems();
        deathParticles = Instantiate(deathParticles,transform.position, Quaternion.identity);
        Destroy(deathParticles,0.3f);
        GameManager.currentScore++;
        SoundManager.PlaySfx(SfxType.enemyDeath);
        StartCoroutine(DeathFade());
    }

    private void Update()
    {
        if (!GameManager.gamePaused)
        {
            if (animator != null)
            {
                animator.enabled = true;
            }
            if (dmgCooldown > 0)
            {
                dmgCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (animator != null)
            {
                animator.enabled = false;
            }
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
