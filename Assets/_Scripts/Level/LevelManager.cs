using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject LevelButton;
    [SerializeField] Transform levelBtnParent;
    [SerializeField] private bool[] levelOpen;


    private void Start()
    {
        PlayerPrefs.SetInt("Level" + 1 + "Unlocked", 1);
        assignLevelBoolean();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (!levelOpen[i]) return;
            string sceneName = "Level " + i;
            GameObject newBtn = Instantiate(LevelButton, levelBtnParent);
            newBtn.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
            newBtn.GetComponent<LevelButton_UI>().UpdateTextInfo(i);
        }
    }
    public void LoadLevel(string sceneName)
    {
        AudioManager.instance.PlaySFX(5);
        GameManager.instance.SaveGameDifficulty();
        SceneManager.LoadScene(sceneName);
    }
    public void LoadNewGame()
    {
        AudioManager.instance.PlaySFX(5);
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;
            if (unlocked)
            {
                PlayerPrefs.SetInt("Level" + i + "Unlocked", 0);
            }
            else
            {
                SceneManager.LoadScene("Level 1");
                return;
            }
        }
    }
    private void assignLevelBoolean()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;
            if (unlocked)
            {
                levelOpen[i] = true;
            }
            else
            {
                return;
            }
        }
    }
    public void LoadContinue()
    {
        AudioManager.instance.PlaySFX(5);
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;
            if (!unlocked)
            {
                SceneManager.LoadScene("Level" + (i - 1));
                return;
            }
        }
    }
}