
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public bool pcTesting;

    [Header("Move Infor")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float doubleJumpForce = 7f;
    [SerializeField] private float wallCheckDistance = 0.7f;
    [SerializeField] private float groundCheckDistance = 1.12f;
    [SerializeField] private float defaultJumpForce = 15f;
    [SerializeField] private Vector2 wallJumpDirection;
    [SerializeField] private float bufferedJumpTime;
    private float bufferedJumpTimer;
    [SerializeField] private float cayoteJumpTime;
    [Header("Knockback info")]
    [SerializeField] private Vector2 knockbackDirection;
    [SerializeField] private float knockbackProtectionTime;
    [SerializeField] private float enemyCheckRadius;
    [SerializeField] private Transform enemyCheck;
    private bool isKnocked;
    [SerializeField] private float knockbackTime;
    private bool canBeknocked = true;
    private bool canBeControlled;
    private float defaultGravityScale;




    private float cayoteJumpCounter;
    private bool canHaveCayoteJump;
    private Rigidbody2D rb;
    private Animator anim;
    private float movingInput;
    [Header("Colision Info")]
    [SerializeField] private LayerMask Ground;
    [SerializeField] private LayerMask Wall;
    private bool isGrounded;
    private bool isWallDetected;
    private bool canDoubleJump = true;
    private bool canMove;
    private bool facingRight = true;
    private bool canWallSlide;
    private bool isWallSliding;
    private int facingDirection = 1;
    [Header("Particles")]
    [SerializeField] private ParticleSystem dustFx;
    private bool readyToLand;
    private float dustFxTimer;

    [Header("Controls Info")]
    public VariableJoystick variableJoystick;
    private float verticalInput;
    private float horizontalInput;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeAnimSkin();
        
        defaultJumpForce = jumpForce;
        defaultGravityScale=rb.gravityScale;
        rb.gravityScale=0;
    }
    private void Update()
    {
        AnimationController();
        if (isKnocked) return;
        FlipController();
        CollisionChecks();
        InputChecks();
        EnemyCheck();
        bufferedJumpTimer -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;

        if (isGrounded)
        {
            canDoubleJump = true;
            canMove = true;
            if (bufferedJumpTimer > 0)
            {
                bufferedJumpTimer = -1;
                Jump();
            }
            canHaveCayoteJump = true;
            if(readyToLand)
            {
                dustFx.Play();
                readyToLand = false;
            }
        }
        else
        {
            if(!readyToLand)
            {
                readyToLand=true;
            }
            if (canHaveCayoteJump)
            {
                canHaveCayoteJump = false;
                cayoteJumpCounter = cayoteJumpTime;
            }

        }
        if (canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }
        Move();

    }

    private void EnemyCheck()
    {
        Collider2D[] hitedColiders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);

        foreach (var enemy in hitedColiders)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                Enemy newEnemy = enemy.GetComponent<Enemy>();

                if (newEnemy.invincible) return;

                if (rb.velocity.y < 0)
                {
                    AudioManager.instance.PlaySFX(1);
                    newEnemy.Damage();
                    anim.SetBool("flipping",true);
                    Jump();

                }

            }
        }
    }

    private void InputChecks()
    {
        if(!canBeControlled) return;
        if(pcTesting)
        {
            movingInput = Input.GetAxisRaw("Horizontal");
            verticalInput=Input.GetAxisRaw("Vertical");
        }
        else
        {
            movingInput=variableJoystick.Horizontal;
            verticalInput=variableJoystick.Vertical;
        }
        // 

        if (verticalInput< 0)
        {
            canWallSlide = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }
    }
    private void StopFlippingAnimation()
    {
        anim.SetBool("flipping", false);

    }
    public void ReturnControll()
    {
        rb.gravityScale=defaultGravityScale;
        canBeControlled=true;
    }

    public void JumpButton()
    {
        if (!isGrounded) bufferedJumpTimer = bufferedJumpTime;
        if (isWallSliding)
        {
            WallJump();
            canDoubleJump = true;
        }
        else if (isGrounded || cayoteJumpCounter > 0)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            jumpForce = doubleJumpForce;
            Jump();
            jumpForce = defaultJumpForce;
        }
        canWallSlide = false;
    }
    private void CancelKnockback()
    {
        isKnocked = false;
    }
    public void Knockback(Transform damageTransform)
    {
        AudioManager.instance.PlaySFX(10);
       PlayerManager.instance.ShakeScreen(-facingDirection);
        if (!canBeknocked) return;

        if (GameManager.instance.difficulty > 1)
        {
           PlayerManager.instance.OnTakingDamage();
        }

        isKnocked = true;
        canBeknocked = false;
        int hDirection = 0;

        if (transform.position.x > damageTransform.transform.position.x)
        {
            hDirection = 1;
        }
        else if (transform.position.x < damageTransform.transform.position.x)
        {
            hDirection = -1;
        }
        rb.velocity = new Vector2(knockbackDirection.x * hDirection, knockbackDirection.y);
        Invoke("CancelKnockback", knockbackTime);
        Invoke("Allowknockback", knockbackProtectionTime);

    }
    private void Allowknockback()
    {
        canBeknocked = true;
    }

    private void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
        }

        //  FlipController();
    }
    private void WallJump()
    {
       // AudioManager.instance.PlaySFX(13);
        canMove = false;
        rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
        dustFx.Play();
    }

    private void AnimationController()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isMove", isMoving);
        anim.SetBool("isGround", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSlide", isWallSliding);
        anim.SetBool("isWallDetected", isWallDetected);
        anim.SetBool("isKnocked", isKnocked);
        anim.SetBool("canControlled",canBeControlled);
    }
    private void ChangeAnimSkin()
    {
        int skinIndex=PlayerManager.instance.choosenskinId;
        for(int i=0;i<anim.layerCount;i++)
        {
            anim.SetLayerWeight(i,0);

        }
        anim.SetLayerWeight(skinIndex,1);
    }

    private void  CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, Ground);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, Wall);
        if (isWallDetected && rb.velocity.y < 0)
        {
            canWallSlide = true;
        }
        if (!isWallDetected)
        {
            isWallSliding = false;
            canWallSlide = false;
        }
    }

    private void Jump()
    {
        AudioManager.instance.PlaySFX(4);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if(isGrounded) dustFx.Play();
    }
    public void pushJump(float pushForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, pushForce);
    }
    private void FlipController()
    {
        dustFxTimer-=Time.deltaTime;
        if (facingRight && rb.velocity.x < -.1f)
        {
            Flip();
        }
        else if (!facingRight && rb.velocity.x > .1f)
        {
            Flip();
        }
    }
    private void Flip()
    {
        if(dustFxTimer<0)
        {
            dustFx.Play();
            dustFxTimer=.7f;
        }
        dustFx.Play();
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
    }

}

