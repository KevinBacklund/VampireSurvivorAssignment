using UnityEngine;

public class ShieldController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedShield = Instantiate(weaponData.Prefab);
    }
}
