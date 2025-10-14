using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float projSpeed;
    public float damage;
    public int destroyAfterSeconds;
    private float destroyTimer;
    Vector3 direction;
    Transform player;

    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        direction = player.transform.position - transform.position;
        direction.Normalize();
    }

    void Update()
    {
        if (!GameManager.gamePaused)
        {
            destroyTimer += Time.deltaTime;
            transform.position += direction * projSpeed * Time.deltaTime;
            if (destroyTimer >= destroyAfterSeconds)
            {
                Destroy(gameObject);
                destroyTimer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(damage);
            Destroy(gameObject);
        } 
    }
}
