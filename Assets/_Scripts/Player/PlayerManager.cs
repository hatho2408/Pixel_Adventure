using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public int fruits;
    public GameObject currentPlayer;
    public Transform spawnPoint;
    public int choosenskinId;
    [Header("Camera")]
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private Vector3 shakeDirection;
    [SerializeField] private float forceMultiplier;
    [Header("Player Info")]
    [SerializeField] private GameObject deathFX;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject fruitPrefab;

    public InGame_UI inGameUI;



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
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerRespawn();
        }
    }

    public void PlayerRespawn()
    {
        if (currentPlayer == null)
        {
            
            currentPlayer = Instantiate(playerPrefab, spawnPoint.position, transform.rotation);
            AudioManager.instance.PlaySFX(12);
            inGameUI.AssignPlayerControlls(currentPlayer.GetComponent<PlayerController>());
        }
    }
    public void ShakeScreen(int facingDirection)
    {
        impulseSource.m_DefaultVelocity = new Vector3(shakeDirection.x, shakeDirection.y) * forceMultiplier;
        impulseSource.GenerateImpulse();
    }
    public void PlayerKilled()
    {
        AudioManager.instance.PlaySFX(0);
        GameObject newDeathFx = Instantiate(deathFX, currentPlayer.transform.position, currentPlayer.transform.rotation);
        Destroy(newDeathFx, 0.4f);
        Destroy(currentPlayer);

    }
    public bool HaveEnoughFruits()
    {
        if (fruits > 0)
        {
            fruits--;
            if (fruits < 0)
            {
                fruits = 0;
            }
            DropFruits();
            return true;
        }
        return false;
    }
    public void OnTakingDamage()
    {
      if(!HaveEnoughFruits())
      {
        PlayerKilled();
        int difficulty = GameManager.instance.difficulty;
        if (difficulty < 3)
        {
            Invoke("PlayerRespawn", 1);
        }
        else{
             inGameUI.OnDeath();
        }
      }
    }
    public void OnFalling()
    {
        AudioManager.instance.PlaySFX(0);
        PlayerKilled();
        int difficulty = GameManager.instance.difficulty;
        if (difficulty < 3)
        {
            Invoke("PlayerRespawn", 1);
            if (difficulty > 1)
            {
                HaveEnoughFruits();
            }
        }
        else
        {
            inGameUI.OnDeath();
        }

    }
    private void DropFruits()
    {
        GameObject newfruit = Instantiate(fruitPrefab, currentPlayer.transform.position, transform.rotation);
        int fruitIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(FruitType)).Length);
        newfruit.GetComponent<Fruit_PlayerDrop>().FruitSetup(fruitIndex);
        Destroy(newfruit, 18);
    }




}
