using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX = 0f;
    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private BoxCollider2D collide;
    [SerializeField] private LayerMask JumpableGround;

    private enum MovementStatus { Idle, Running, Jumping, Falling }

    [SerializeField] private AudioSource JumpSoundEffect;
    

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collide = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && Grounded())
        {
            JumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        MovementStatus status;


        if (dirX > 0f)
        {
            status = MovementStatus.Running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            status = MovementStatus.Running;
            sprite.flipX = true;
        }
        else
        {
            status = MovementStatus.Idle;
        }

        if (rb.velocity.y > .1f)
        {
            status = MovementStatus.Jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            status = MovementStatus.Falling;
        }

        anim.SetInteger("status", (int)status);
    }

    private bool Grounded()
    {
        return Physics2D.BoxCast(collide.bounds.center, collide.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }
}
