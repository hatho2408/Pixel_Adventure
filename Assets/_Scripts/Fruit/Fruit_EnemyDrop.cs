using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_EnemyDrop : Fruit_PlayerDrop
{
    private Rigidbody2D rb;
    [SerializeField]private Vector2[] dropDirection;
    [SerializeField] private float force;

    protected override void Start()
    {
        rb=GetComponentInParent<Rigidbody2D>();
        base.Start();
        int rand=Random.Range(0,dropDirection.Length);
        rb.velocity=dropDirection[rand]*force;
    }
    protected override IEnumerator BlinkImage()
    {
         anim.speed = 0;
        spriteRenderer.color = transparentColor;
        
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = Color.white;
        
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = transparentColor;
      
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = Color.white;
       
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = transparentColor;
       
        yield return new WaitForSeconds(.2f);
        spriteRenderer.color = Color.white;
      
        yield return new WaitForSeconds(.2f);
        spriteRenderer.color=transparentColor;
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = Color.white;
        
        anim.speed = 1;
        canPickup = true;
    }
   
}

