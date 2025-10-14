using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!GameManager.gamePaused)
        {
            transform.position += direction * currentSpeed * Time.deltaTime;
        }
    }
}
