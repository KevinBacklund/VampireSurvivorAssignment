using UnityEngine;

public class KnifeController : WeaponController
{
    
    protected override void Start()
    {
        base.Start();   
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(weaponData.Prefab);
        spawnedKnife.transform.position = transform.position; // position parented to player
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(pm.lastMovedDirection);
    }
}
