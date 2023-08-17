using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGame_UI : MonoBehaviour
{
   private bool gamePaused;

    [Header("Menu gameobjects")]
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject endLevelUI;

    [Header("Controlls")]
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Button jumpButton;

    [Header("TextComponents")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI currentFruitAmount;

    [SerializeField] private TextMeshProUGUI endTimerText;
    [SerializeField] private TextMeshProUGUI endBestTimeText;
    [SerializeField] private TextMeshProUGUI endFruitsText;

    private void Awake()
    {
        
        PlayerManager.instance.inGameUI = this;
    }

    private void Start()
    {
        GameManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SwitchUI(inGameUI);

    }

    void Update()
    {
        UpdateInGameInfo();


        if (Input.GetKeyDown(KeyCode.Escape))
            CheckIfNotPaused();

        if (endLevelUI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                LoadNextLevel();
        }

    }

    public void AssignPlayerControlls(PlayerController player)
    {
        player.variableJoystick = joystick;

        // if (!player.pcTesting)
        // {
        //     jumpButton.onClick.RemoveAllListeners();
        //     jumpButton.onClick.AddListener(player.JumpButton);
        // }
    }

    public void PauseButton() => CheckIfNotPaused();

    private bool CheckIfNotPaused()
    {
        if (!gamePaused)
        {
            gamePaused = true;
            Time.timeScale = 0;
            SwitchUI(pauseUI);
            return true;
        }
        else
        {
            gamePaused = false;
            Time.timeScale = 1;
            SwitchUI(inGameUI);
            return false;
        }
    }

    public void OnDeath() => SwitchUI(pauseUI);
    

    public void OnLevelFinished()
    {
        endFruitsText.text = "Fruits: " + PlayerManager.instance.fruits;
        endTimerText.text = "Your time: " + GameManager.instance.timer.ToString("00") + " s";
        endBestTimeText.text = "Best time: " + PlayerPrefs.GetFloat("Level" + GameManager.instance.levelNumber + "BestTime",999).ToString("00") + " s";
        SwitchUI(endLevelUI);
    }

    private void UpdateInGameInfo()
    {
        timerText.text = "Timer: " + GameManager.instance.timer.ToString("00") + " s";
        currentFruitAmount.text = PlayerManager.instance.fruits.ToString();
    }

    public void SwitchUI(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        uiMenu.SetActive(true);

        if (uiMenu == inGameUI)
        {
            joystick.gameObject.SetActive(true);
            jumpButton.gameObject.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading main menu");
    }
    public void ReloadCurrentLevel()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      Debug.Log("reload");
    } 
    public void LoadNextLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

}