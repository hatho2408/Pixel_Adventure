using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton_UI : MonoBehaviour, IPointerDownHandler
{
    private PlayerController player;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(PlayerManager.instance.currentPlayer!=null)
        {
            player=PlayerManager.instance.currentPlayer.GetComponent<PlayerController>();
            player.JumpButton();
        }
    }
}
