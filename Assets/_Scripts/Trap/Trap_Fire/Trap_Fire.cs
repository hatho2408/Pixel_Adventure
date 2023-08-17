using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire : Trap
{
  
    public bool isWorking;
    private Animator anim;
    [SerializeField] protected float repeatRate = 3f;
    private void Start()
    {
        anim = GetComponent<Animator>();
        if(transform.parent == null)
        {
            InvokeRepeating("FireSwitch",0,repeatRate);
        }
       
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        anim.SetBool("isWorking", isWorking);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (isWorking)
        {
            base.OnTriggerEnter2D(other);
        }

    }
    public void FireSwitchAfter(float time)
    {
        CancelInvoke(); 
        isWorking=false;
        Invoke("FireSwitch",time);
    }
    public void FireSwitch()
    {
        isWorking = !isWorking;

    }
}

