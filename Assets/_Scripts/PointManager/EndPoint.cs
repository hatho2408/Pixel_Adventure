using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private InGame_UI inGame_UI;
    private void Start()
    {
        inGame_UI=GameObject.Find("Canvas").GetComponent<InGame_UI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            GetComponent<Animator>().SetTrigger("activate");
            AudioManager.instance.PlaySFX(2);
            PlayerManager.instance.PlayerKilled();
            Destroy(other.gameObject);
            inGame_UI.OnLevelFinished();
            PlayerManager.instance.spawnPoint = transform;
            GameManager.instance.saveBestTime();
            GameManager.instance.SaveCollectedFruit();
            GameManager.instance.SaveLevelInfo();
        }
    }
}
