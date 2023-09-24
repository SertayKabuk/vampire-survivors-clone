using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    PlayerStats player;

    [HideInInspector]
    public Vector2 moveDirection;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public Vector2 lastMovedVector;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerStats>();
        lastMovedVector = new Vector2 (1, 0f); //default direction, if player doesn't move weapon stay still
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveDirection.x != 0)
        {
            lastHorizontalVector = moveDirection.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f); //last moved x
        }

        if (moveDirection.y != 0)
        {
            lastVerticalVector = moveDirection.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector); // last moved y
        }

        if (moveDirection.x != 0 && moveDirection.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); // while moving

        }
    }

    private void Move()
    {
        rigidBody2D.velocity = new Vector2(moveDirection.x * player.currentMoveSpeed, moveDirection.y * player.currentMoveSpeed);
    }
}
