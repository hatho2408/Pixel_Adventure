using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int difficulty;
    public float timer;
    public bool startTime;
    public int levelNumber;

    private void Start()
    {
        // int gameDifficulty =PlayerPrefs.GetInt("GameDifficulty");
        if(difficulty==0)
        {
            difficulty=PlayerPrefs.GetInt("GameDifficulty");
        }
    
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if(startTime)
        {
            timer+=Time.deltaTime;
        }
    }
    public void SaveGameDifficulty()
    {
        PlayerPrefs.SetInt("GameDifficulty", difficulty);
    }
    public void saveBestTime()
    {
        startTime=false;
        float lastTime=  PlayerPrefs.GetFloat("Level"+levelNumber+"BestTime",999);
        if(timer<lastTime)
        {
            PlayerPrefs.SetFloat("Level"+levelNumber+"BestTime",timer);
        }
        timer=0;
    }
    public void SaveCollectedFruit()
    {
       int totalFruits = PlayerPrefs.GetInt("TotalFruitsCollected");

        int newTotalFruits = totalFruits + PlayerManager.instance.fruits;

        PlayerPrefs.SetInt("TotalFruitsCollected", newTotalFruits);
        PlayerPrefs.SetInt("Level" + levelNumber + "FruitsCollected", PlayerManager.instance.fruits);

        PlayerManager.instance.fruits = 0;
    }
    public void SaveLevelInfo()
    {
        int nextLevel=levelNumber+1;
        PlayerPrefs.SetInt("Level"+levelNumber+"Unclocked",1);

    }

}
