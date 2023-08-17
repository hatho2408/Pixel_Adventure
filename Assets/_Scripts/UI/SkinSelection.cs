using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class SkinSelection : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int skinId;
    [SerializeField] private bool[] skinPurchased;
    [SerializeField] private int[] skinPrice;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;

    [SerializeField] private TextMeshProUGUI banktext;

    private void OnEnable()
    {
        setupSkin();
    }


    private void OnDisable()
    {
        equipButton.SetActive(false);
    }

    public void nextSkin()
    {
        AudioManager.instance.PlaySFX(5);
        skinId++;
        if (skinId > 3) { skinId = 0; }

        setupSkin();
    }


    public void previousSkin()
    {
        AudioManager.instance.PlaySFX(5);
        skinId--;
        if (skinId < 0) { skinId = 3; }

        setupSkin();
    }
    public void Buy()
    {
        if(EnoughMoney())
        {
            PlayerPrefs.SetInt("SkinPurchased"+ skinId,1);
            skinPurchased[skinId] = true;
            setupSkin();
        }
        else
        {
            Debug.Log("Not enough money");
        }
        
    }
    public void selectSkin()
    {
        PlayerManager.instance.choosenskinId = skinId;
    }
    private void setupSkin()
    {
        skinPurchased[0] = true;
        for(int i=1;i<skinPurchased.Length;i++)
        {
            bool skinUnlocked=PlayerPrefs.GetInt("SkinPurchased"+i)==1;
            if(skinUnlocked)
            {
                skinPurchased[i]=true;
            }
        }
        banktext.text = PlayerPrefs.GetInt("TotalFruitsCollected").ToString();
        buyButton.SetActive(!skinPurchased[skinId]);
        equipButton.SetActive(skinPurchased[skinId]);
        if (!skinPurchased[skinId])
        {
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + skinPrice[skinId];
        }

        animator.SetInteger("skinId", skinId);
    }
    public void SwitchSelectButton(GameObject newBtn)
    {

        equipButton = newBtn;

    }
    public bool EnoughMoney()
    {
        int totalFruits=PlayerPrefs.GetInt("TotalFruitsCollected");
        if(totalFruits>skinPrice[skinId])
        {
            totalFruits=totalFruits-skinPrice[skinId];
            PlayerPrefs.SetInt("TotalFruitsCollected",totalFruits);
            AudioManager.instance.PlaySFX(6);
            return true;
        }
        AudioManager.instance.PlaySFX(7);
        return false;
    }



}
