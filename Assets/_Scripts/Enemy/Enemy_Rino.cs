using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rino : Enemy
{
    [Header("Rino Specific Info")]

    [SerializeField] private float aggressiveSpeed ;
    [SerializeField] private float shockTime;
    private float shockTimeCounter ;

  

    protected override void Start()
    {
        base.Start();
        invincible = true;
    }
    private void Update()
    {
        CollisionChecks();
        AnimatorController();
        if(!playerDetection)
        {
            WalkAround();
            return;
        }
        
        if (playerDetection.collider.GetComponent<PlayerController>() != null&& playerDetection) aggresive = true;
        if (!aggresive)
        {
           WalkAround();
        }
        else
        {
            if(!groundDetected)
            {
                aggresive=false;
                Flip();

            }
            rb.velocity = new Vector2(aggressiveSpeed * facingDirection, rb.velocity.y);
            if (wallDetected && invincible)
            {
                invincible = false;
                shockTimeCounter = shockTime;

            }
            if (shockTimeCounter <= 0 && !invincible)
            {
                invincible = true;
                Flip();
                aggresive = false;
            }
        }

        shockTimeCounter -= Time.deltaTime;

       
    }

    private void AnimatorController()
    {
        anim.SetBool("invincible", invincible);
        anim.SetFloat("xVelocity", rb.velocity.x);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
      
    }
}
