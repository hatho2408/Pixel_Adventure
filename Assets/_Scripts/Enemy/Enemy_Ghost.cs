using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy
{
    [Header("Ghost Specific")]
    [SerializeField] private float activeTime;
    private float activeTimeCounter = 4;
   
    private SpriteRenderer sr;
    [SerializeField] private float[] xOffsetPos;
    protected override void Start()
    {
        base.Start();
      
        sr = GetComponent<SpriteRenderer>();
        aggresive = true;
        invincible = true;

    }
    private void Update()
    {
        if (player == null)
        {
            anim.SetTrigger("Desappear");
            return;
        }
        activeTimeCounter -= Time.deltaTime;
        idleTimeCounter -= Time.deltaTime;
        if (activeTimeCounter > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        }
        if (activeTimeCounter < 0 && idleTimeCounter < 0 && aggresive)
        {
            anim.SetTrigger("Desappear");
            idleTimeCounter = idleTime;
            aggresive = false;
        }
        if (activeTimeCounter < 0 && idleTimeCounter < 0 && !aggresive)
        {
            ChoosePosition();
            anim.SetTrigger("Appear");
            aggresive = true;
            activeTimeCounter = activeTime;
        }
        FlipController();
    }

    private void FlipController()
    {
        if(player==null) return;
        if (facingDirection == -1 && transform.position.x < player.transform.position.x)
        {
            Flip();
        }
        else if (facingDirection == 1 && transform.position.x > player.transform.position.x)
        {
            Flip();
        }
    }

    private void ChoosePosition()
    {
        float yOffSet = Random.Range(-7, 7);
        float xOffset = xOffsetPos[Random.Range(0, xOffsetPos.Length)];
        transform.position = new Vector2(player.transform.position.x + 7, player.transform.position.y + yOffSet);
    }
    public void Desappear()
    {
        // sr.enabled=false;
        sr.color = Color.clear;
    }
    public void Appear()
    {
        // sr.enabled=true;
        sr.color = Color.white;
    }
    // public override void Damage()
    // {
    //     base.Damage();
    // }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (aggresive)
        {
            base.OnTriggerEnter2D(other);
        }

    }
}
