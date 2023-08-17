using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trunk : Enemy
{
    [Header("Trunk Specification")]

    [SerializeField]private float moveBackTime;
        private float moveBackTimeCounter;

    private bool wallBehind;
    private bool groundBehind;

    private bool detectedPlayer;
    [Header("Bullet Specific")]
    [SerializeField] private float attackCooldown;
    private float attackCooldownCounter;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed;
    [Header("Collision Specification")]
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private Transform groundBehindCheck;


    protected override void Start()
    {
        base.Start();


    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        CollisionChecks();
        anim.SetFloat("xVelocity", rb.velocity.x);
        if(!playerDetection)
        {
            WalkAround();
            return;
        }

        if (!canMove) rb.velocity = Vector2.zero;
        attackCooldownCounter -= Time.deltaTime;
        moveBackTimeCounter-=Time.deltaTime;
        if(detectedPlayer&&moveBackTimeCounter<0)
        {
            moveBackTimeCounter=moveBackTime;
        }
        if (playerDetection.collider.GetComponent<PlayerController>() != null)
        {
            if (attackCooldownCounter < 0)
            {
                attackCooldownCounter = attackCooldown;
                anim.SetTrigger("Attack");
                canMove = false;
            }else if(playerDetection.distance<3)
            {
                MoveBackWards(1.5f);
            }
        }
        else
        {
            if(moveBackTimeCounter>0)
            {
                MoveBackWards(3);
            }
            else
            {
                    WalkAround();
            }
            
        }
    }
    private void MoveBackWards(float multiplier)
    {
        if(wallBehind)return;
        if(!groundBehind) return;
        rb.velocity=new Vector2(speed*multiplier*-facingDirection,rb.velocity.y);
    }
    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.rotation);
        newBullet.GetComponent<Bullet>().SetUpSpeed(bulletSpeed * facingDirection, 0);
        Destroy(newBullet, 3f);
    }
    private void ReturnMovement()
    {
        canMove = true;
    }
    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        detectedPlayer = Physics2D.OverlapCircle(transform.position, checkRadius, PlayerLayer);
        groundBehind = Physics2D.Raycast(groundBehindCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        Physics2D.Raycast(wallCheck.position, Vector2.right * (-facingDirection+1), wallCheckDistance, whatIsGround);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position,checkRadius);
        Gizmos.DrawLine(groundBehindCheck.position,new Vector2(groundBehindCheck.position.x,groundBehindCheck.position.y-groundCheckDistance));

    }
}
