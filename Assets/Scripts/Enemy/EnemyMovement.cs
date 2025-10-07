using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    protected EnemyStats enemy;
    protected Transform player;

    protected Vector2 knockbackVelocity;
    protected float knockbackDuration;

    protected virtual void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    protected virtual void Update()
    {
        if (!GameManager.gamePaused)
        {
            if (knockbackDuration > 0)
            {
                transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
                knockbackDuration -= Time.deltaTime;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
            }
        }
    }

    public virtual void Knockback(Vector2 velocity, float duration)
    {
       knockbackVelocity = velocity;
       knockbackDuration = duration;
    }
}
