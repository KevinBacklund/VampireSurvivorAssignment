using UnityEngine;

public class ShieldBehaviour : ProjectileWeaponBehaviour
{
    public float radius;
    private float angle = 0;
    Transform player;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerStats>().transform;
    }

    private void Update()
    {
       if (!GameManager.gamePaused)
        {
            angle += Time.deltaTime * currentSpeed;
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
            transform.position = player.position + direction * radius;
        }
    }
}
