using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BlueBird : Enemy
{
    
    private RaycastHit2D groundAboveDetected;
    [Header("Blue Bird Specific")]
    [SerializeField] private float groundAboveDistance;
    [SerializeField] private float groundBellowDistance;
    [SerializeField] private float flyUpForce;
    [SerializeField] private float flyDownForce;

    // [SerializeField] private Transform movePoint;
    // [SerializeField] private float xMultiplier;
    //  [SerializeField] private float yMultiplier;
    private float flyForce;
    private bool canFly=true;
    protected override void Start()
    {
        base.Start();
        flyForce=flyUpForce;
    }
    private void Update()
    {
        CollisionChecks();
        if(groundAboveDetected)
        {
            flyForce=flyDownForce;
        }
        else if(groundDetected)
        {
            flyForce=flyUpForce;
        }
        if(wallDetected)
        {
            Flip();
        }
       
    }
    public override void Damage()
    {
        canFly=false;
        rb.velocity=Vector2.zero;
        rb.gravityScale=0;
        base.Damage();
    }
    public void FlyUpEvent()
    {
        if(canFly)
        {
            rb.velocity = new Vector2(speed * facingDirection, flyForce);   
        }
        // if(canFly)
        // {
        //     Vector2 direction = transform.position -movePoint.position;
        //       rb.velocity = new Vector2(-direction.x*xMultiplier,-direction.y*yMultiplier); 
        //       if(direction.x<0)
        //       {
        //         Flip();
        //       }
        // }
        
        
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + groundAboveDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundBellowDistance));
    }
    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        groundAboveDetected = Physics2D.Raycast(transform.position, Vector2.up, groundAboveDistance, whatIsGround);
       
    }
}
