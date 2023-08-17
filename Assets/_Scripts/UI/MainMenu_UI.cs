using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private VolumeController[] volumeController;
 
    private void Start()
    {
        bool showBtn=PlayerPrefs.GetInt("Level"+2+"Unclocked")==1;
        continueButton.SetActive(showBtn);
        for(int i=0; i<volumeController.Length; i++)
        {
            volumeController[i].GetComponent<VolumeController>().SetupVolume();
        }
        AudioManager.instance.PlayBGM(0);

    }
    public void SwitchMenu(GameObject uiMenu)
    {
        for(int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        AudioManager.instance.PlaySFX(5);
        uiMenu.SetActive(true);
    }
    public void SetGameDifficulty(int i)
    {
        GameManager.instance.difficulty = i;
    }
}
