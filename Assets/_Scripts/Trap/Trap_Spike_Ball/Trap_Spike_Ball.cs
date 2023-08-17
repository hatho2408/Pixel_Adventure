using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spike_Ball : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField] private Vector2 pushDirection;
    private void Start()
    {
        rb.AddForce(pushDirection, ForceMode2D.Impulse);
    }
}
