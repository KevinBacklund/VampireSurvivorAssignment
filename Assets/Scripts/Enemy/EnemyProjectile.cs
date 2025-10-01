using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float projSpeed;
    public float damage;
    public int destroyAfterSeconds;
    Vector3 direction;
    Transform player;

    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        Destroy(gameObject, destroyAfterSeconds);
        direction = player.transform.position - transform.position;
        direction.Normalize();
    }

    void Update()
    {
        if (!GameManager.gamePaused)
        {
            transform.position += direction * projSpeed * Time.deltaTime;
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
