using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire_Switcher : MonoBehaviour
{
    public Trap_Fire trap_Fire;
    private Animator anim;
   [SerializeField] private float timeNoActive=3;
    private void Start()
    {
        anim=GetComponent<Animator>();  
    }
    
    private  void OnTriggerEnter2D(Collider2D other)
    {
        // if(countdown>0) return; 
        if(other.GetComponent<PlayerController>() != null)
        {
           
            anim.SetTrigger("isPressed");
            trap_Fire.FireSwitchAfter(timeNoActive);
        }
    }   
}
