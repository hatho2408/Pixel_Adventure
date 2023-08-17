using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Radish : Enemy
{
    private RaycastHit2D groundBellowDetected;
    private RaycastHit2D groundAboveDetected;
    [Header("Radish Specific")]
    [SerializeField] private float groundAboveDistance;
    [SerializeField] private float groundBellowDistance;
    [SerializeField] private float aggressiveTime;
    protected float aggroTimeCounter;
    [SerializeField] private float flyForce;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        aggroTimeCounter-=Time.deltaTime;
        if(aggroTimeCounter<0&& !groundAboveDetected)
        {
            rb.gravityScale=1;
            aggresive=false;
        }
        if(!aggresive)
        {
            rb.gravityScale=12;
            if(groundBellowDetected&&!groundAboveDetected)
            {
                rb.velocity=new Vector2(0,flyForce);
            }
        }
        else
        {
            if(groundBellowDetected.distance<1.25f)
            {
                WalkAround();

            }
            
        }
        CollisionChecks();
       anim.SetFloat("xVelocity",rb.velocity.x);
       anim.SetBool("aggressive",aggresive);
    }
    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        groundAboveDetected=Physics2D.Raycast(transform.position,Vector2.up,groundAboveDistance,whatIsGround);
        groundBellowDetected=Physics2D.Raycast(transform.position,Vector2.down,groundBellowDistance,whatIsGround);
    }
    public override void Damage()
    {
        if(!aggresive)
        {
            aggroTimeCounter=aggressiveTime;
            aggresive=true;
        }
        else
        {
              base.Damage();
        }
      
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x,transform.position.y+groundAboveDistance));
         Gizmos.DrawLine(transform.position,new Vector2(transform.position.x,transform.position.y-groundBellowDistance));
    }
   

}
