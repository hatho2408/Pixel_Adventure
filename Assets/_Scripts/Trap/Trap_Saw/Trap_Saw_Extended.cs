using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw_Extended : Trap
{
    private Animator anim;

    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed = 5f;

    private int movePointIndex;
    private bool moveForward = true;


    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isSawOn", true);
        transform.position=movePoint[0].position;
        Flip();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);


        if (Vector2.Distance(transform.position, movePoint[movePointIndex].position) < 0.15f)
        {
            if(movePointIndex==0)
            {
                Flip();
                moveForward=true;
            }

            if (moveForward)
            {
                movePointIndex++;
            }
            else
            {
                movePointIndex--;
            }
            if (movePointIndex >= movePoint.Length)
            {

                movePointIndex = movePoint.Length - 1;
                moveForward=false;
                Flip();
            }
        }
    }
    private void Flip()
    {
        transform.localScale = new Vector3(1, transform.localScale.y * -1);
    }
}
