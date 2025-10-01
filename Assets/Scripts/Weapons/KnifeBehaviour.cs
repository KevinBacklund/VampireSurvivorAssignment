using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (!GameManager.gamePaused)
        {
            transform.position += direction * currentSpeed * Time.deltaTime;
        }
    }
}
