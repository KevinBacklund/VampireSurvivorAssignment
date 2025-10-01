using UnityEngine;

public class EyeProjectileController : EnemyProjectileController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedProjectile = Instantiate(prefab);
        spawnedProjectile.transform.position = transform.position; 
    }
}
