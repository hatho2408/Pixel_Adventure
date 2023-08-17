using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
   private Animator anim;
    
    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float cooldown;
    private int movePointIndex;
     private float cooldownTimer;


    private void Start()
    {
        anim = GetComponent<Animator>();
        transform.position=movePoint[0].position;
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        cooldownTimer-=Time.deltaTime;
        bool isSawOn = cooldownTimer < 0;
        anim.SetBool("isSawOn", isSawOn);
        if(isSawOn)
        {
           transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);
        }
        
        if (Vector2.Distance(transform.position, movePoint[movePointIndex].position) < 0.15f)
        {
            
            cooldownTimer=cooldown;
            movePointIndex++;
            if (movePointIndex >= movePoint.Length)
            {
                movePointIndex = 0;
            }
        }
    }
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>()!=null)
        {
            other.transform.SetParent(transform);
        }
    }


  private void OnTriggerExit2D(Collider2D other)
  {
      if(other.GetComponent<PlayerController>()!=null)
        {
            other.transform.SetParent(null);
        }
  }
    

}
