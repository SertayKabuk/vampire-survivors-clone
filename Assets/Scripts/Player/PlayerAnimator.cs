using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool moving = false;

        if (playerMovement.moveDirection.x != 0 || playerMovement.moveDirection.y != 0)
        {
            moving = true;
            SpriteDirectionChecker();
        }

        animator.SetBool(Enums.AnimationParameters.Move.ToString(), moving);

    }

    void SpriteDirectionChecker()
    {
        bool flipX = false;

        if (playerMovement.lastHorizontalVector < 0)
            flipX = true;

        spriteRenderer.flipX = flipX;
    }
}
