using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Trap
{
    private Rigidbody2D rb;
    private float xSpeed;
    private float ySpeed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }
    public void SetUpSpeed(float x, float y)
    {
        xSpeed = x;
        ySpeed = y;
    }
      protected override void OnTriggerEnter2D(Collider2D other)
    {

        base.OnTriggerEnter2D(other);
        Destroy(gameObject);
    }

}
