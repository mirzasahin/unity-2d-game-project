using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Transform player;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;
    
    private float dirX = 0f;
    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;
    
    private enum MovementState { idle, running, jumping, falling, doublejumping}

    [SerializeField] private AudioSource jumpSoundEffect;

    private int maxJumps = 1;

    private int _jumpsLeft;

    private bool isDoubleJumping = false;
    private bool jumpInAir;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        player = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        _jumpsLeft = maxJumps;
        jumpInAir = false;
        Debug.Log(jumpInAir);
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); // Player'ın haritadaki konumu, koordinatı. -1 sol, 1 sağ.
        if(rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); // Player'ın hızı.
        }
       
            if(Input.GetKeyDown("space") && IsGrounded() && jumpInAir == false)
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpInAir = true;
            }

            if(Input.GetKeyDown("space") && !IsGrounded() && _jumpsLeft > 0 && jumpInAir == true)
            {
                {
                    jumpSoundEffect.Play();
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce - 3f);
                    _jumpsLeft -= 1;
                    isDoubleJumping = true;
                    jumpInAir = false;
                }
            }
        
        
        if(IsGrounded() && rb.velocity.y < 0)
        {
            _jumpsLeft = maxJumps;
            jumpInAir = false;
        }

    
        UpdateAnimationState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGrounded())
        {
            isDoubleJumping = false;
        }
    }


    private void UpdateAnimationState()
    {

        MovementState state;

        if(dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if(dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            if(isDoubleJumping)
            {
                state = MovementState.doublejumping;
            }
            else
            {
                state = MovementState.jumping;
            }
        }

        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }


        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded() //BoxCast bize boolean döndürüyor.
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
