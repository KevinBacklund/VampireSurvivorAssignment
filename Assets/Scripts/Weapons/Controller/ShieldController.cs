using UnityEngine;
using UnityEngine.Rendering;

public class ShieldController : WeaponController
{
    int shields;
    protected override void Start()
    {
        SpawnShields();
    }

    protected override void Attack()
    {
        if (shields <= 3)
        {
            base.Attack();
            SpawnShields();
        }
    }

    private void SpawnShields()
    {
        GameObject spawnedShield = Instantiate(weaponData.Prefab);
        shields += 1;
    }
}
