using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Plant : Enemy
{
    [Header("Plant Specific")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool facingRight;
    protected override void Start()
    {
        base.Start();
        if (facingRight) Flip();
    }
    private void Update()
    {
        CollisionChecks();
        idleTimeCounter -= Time.deltaTime;
        if(!playerDetection) return;
        
        if (idleTimeCounter < 0 && playerDetection.collider.GetComponent<PlayerController>() != null)
        {
            idleTimeCounter = idleTime;
            anim.SetTrigger("Attack");

        }
    }
    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.rotation);
        newBullet.GetComponent<Bullet>().SetUpSpeed(bulletSpeed * facingDirection, 0);
        Destroy(newBullet,3f);
    }
  
}
