    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : Enemy
{
    [Header("Bat Specification")]
    [SerializeField] private Transform[] idlePoint;
    [SerializeField] private float checkRadius;
    [SerializeField] protected LayerMask  PlayerLayer;
   

    private float defaultSpeed;
    private bool canAggressive=true;
    private Vector2 destination;
    private bool detectedPlayer;
   
  
    protected override void Start()
    {
        base.Start();
       
        defaultSpeed=speed;
        destination=idlePoint[0].position;
        transform.position=idlePoint[0].position;
     
        for(int i=0;i<idlePoint.Length;i++)
        {
            idlePoint[i].GetComponent<SpriteRenderer>().sprite=null;
        }
    }

    protected void Update()
    {
        anim.SetFloat("speed",speed);
        anim.SetBool("Aggressive",canAggressive);
        idleTimeCounter-=Time.deltaTime;

        if(idleTimeCounter>0) return;
       
        //CollisionChecks();
        if(player!=null)
        {
           float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            detectedPlayer = distanceToPlayer <= checkRadius;
        }
     
        if(detectedPlayer && !aggresive && canAggressive)
        {
            aggresive=true;
            canAggressive=false;
            if(player!=null)
            {
                destination=player.transform.position;
            }
            else
            {
                aggresive=false;
                canAggressive=true;
            }
        }
        if(aggresive)
        {
            transform.position=Vector2.MoveTowards(transform.position,destination,speed*Time.deltaTime);
           if (Vector2.Distance(transform.position,destination)<=0.1f)
           {
                aggresive=false;
                int i=Random.Range(0,idlePoint.Length);
                destination=idlePoint[i] .position;
                speed=speed*.5f;
           }
        }
        else
        {
             transform.position=Vector2.MoveTowards(transform.position,destination,speed*Time.deltaTime);
              if (Vector2.Distance(transform.position,destination)<=0.1f)
              {
                if(!canAggressive)
                {
                    idleTimeCounter=idleTime;
                    
                }
                canAggressive=true;
                speed=defaultSpeed;
              }
        }
        FlipController();
    }
    private void FlipController()
    {
        if(player==null) return;
        if (facingDirection == -1 && transform.position.x < destination.x)
        {
            Flip();
        }
        else if (facingDirection == 1 && transform.position.x > destination.x)
        {
            Flip();
        }
    }
    public override void Damage()
    {
        base.Damage();
        idleTimeCounter=5;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position,checkRadius);
    }
  
}
