using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public Vector2 moveDirection;
    [HideInInspector]
    public Vector3 lastMovedDirection;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;

    Rigidbody2D rb;
    PlayerStats player;
    
    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMovedDirection = new Vector2(1, 0);
    }

    
    void Update()
    {
        if (!GameManager.gamePaused)
        {
            InputManagement();
        }
    }
    private void FixedUpdate()
    {
           Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2 (moveX, moveY).normalized;

        if (moveDirection.x != 0)
        {
            lastHorizontalVector = moveDirection.x;
            lastMovedDirection = new Vector2 (lastHorizontalVector, 0);
        }
        if (moveDirection.y != 0)
        {
            lastVerticalVector = moveDirection.y;
            lastMovedDirection = new Vector2 (0,lastVerticalVector);
        }
        if (moveDirection.x != 0 && moveDirection.y != 0)
        {
            lastMovedDirection = new Vector2 (lastHorizontalVector, lastVerticalVector);
        }

    }

    void Move()
    {
        if (!GameManager.gamePaused)
        {
            rb.linearVelocity = new Vector2(moveDirection.x * player.CurrentMoveSpeed, moveDirection.y * player.CurrentMoveSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
