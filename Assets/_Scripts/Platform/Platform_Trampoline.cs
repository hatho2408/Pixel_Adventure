using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Trampoline : MonoBehaviour
{
    [SerializeField] private float pushForce = 20f;
    [SerializeField] private bool canBeUsed=true;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.GetComponent<PlayerController>() != null && canBeUsed)
        {
            canBeUsed = false;
            GetComponent<Animator>().SetTrigger("activate");
            other.GetComponent<PlayerController>().pushJump(pushForce);
        }

    }
    private void CanUseAgain() => canBeUsed = true;


}
