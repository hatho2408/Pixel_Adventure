 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : Enemy
{
    [Header("Bee Specification")]
    [SerializeField] private Transform[] idlePoint;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float yOffset;
    [SerializeField] private float aggroSpeed;


    [Header("Bullet Specification")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed;

    private bool detectedPlayer;
    private int idlePointIndex;

   
    private float defaultSpeed;

    protected override void Start() 
    {
        base.Start();
        defaultSpeed = speed;
      
       
    }
    private void Update()
    {
        CollisionChecks();
        bool idle=idleTimeCounter>0;
      
        anim.SetBool("idle",idle);
        idleTimeCounter -= Time.deltaTime;

          if (idle)
            return;


        if (player == null)
            return;

        if (detectedPlayer && !aggresive)
        {
            aggresive = true;
            speed = aggroSpeed;
        }

        if (!aggresive)
        {
            transform.position = Vector2.MoveTowards(transform.position, idlePoint[idlePointIndex].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, idlePoint[idlePointIndex].position) < .1f)
            {
                idlePointIndex++;

                if (idlePointIndex >= idlePoint.Length)
                    idlePointIndex = 0;
            }
        }
        else
        {

            Vector2 newPosition = new Vector2(player.transform.position.x, player.transform.position.y + yOffset);
            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

            float xDifference = transform.position.x - player.position.x;

            if (Mathf.Abs(xDifference) < .15f)
            {
                anim.SetTrigger("Attack");
            }

        }

    }
      protected override void CollisionChecks()
    {
        base.CollisionChecks();
        detectedPlayer = Physics2D.OverlapCircle(playerCheck.position, checkRadius, PlayerLayer);
    }

    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.rotation);
        newBullet.GetComponent<Bullet>().SetUpSpeed(0, -speed);
        idleTimeCounter=idleTime;   
        speed=defaultSpeed;
        aggresive=false;
        Destroy(newBullet,3f);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(playerCheck.position,checkRadius);
    }

}
