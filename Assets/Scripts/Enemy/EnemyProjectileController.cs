using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    float currentCooldown;
    public GameObject prefab;
    public float cooldownDuration;

    protected virtual void Start()
    {
        currentCooldown = cooldownDuration;
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
        currentCooldown = cooldownDuration;
        SoundManager.PlaySfx(SfxType.enemyAttack);
    }
}
