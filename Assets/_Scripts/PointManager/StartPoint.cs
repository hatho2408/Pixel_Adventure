using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform resPoint;



    private void Start()
    {
        PlayerManager.instance.spawnPoint = resPoint;
        PlayerManager.instance.PlayerRespawn();
        PlayerManager.instance.fruits=0;
        GameManager.instance.timer=0;
        AudioManager.instance.PlayRandomBGM();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            if(!GameManager.instance.startTime)
            {
                GameManager.instance.startTime=true;
            }
            if (other.transform.position.x > transform.position.x)
            {
                GetComponent<Animator>().SetTrigger("touch");
            }

        }
    }

}
