using UnityEngine;

public class EnemyMovementKeepDistance : EnemyMovement
{
    public float distanceToKeep;
    float distanceToPlayer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        CalculateDistanceToPlayer();

        if (knockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else if (distanceToPlayer > distanceToKeep)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
        }
        else if (distanceToPlayer < distanceToKeep + 0.1)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -enemy.currentMoveSpeed * Time.deltaTime);
        }
    }

    public override void Knockback(Vector2 velocity, float duration)
    {
        base.Knockback(velocity, duration);
    }

    void CalculateDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
    }

}
