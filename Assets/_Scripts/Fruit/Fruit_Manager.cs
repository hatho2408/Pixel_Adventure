using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fruit_Manager : MonoBehaviour
{
    [SerializeField] private Transform[] fruitPosition;
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private bool randomFruit;
    private int fruitIndex;

    private void Start()
    {
        setUpFruits();
    }

    private void setUpFruits()
    {
        fruitPosition = GetComponentsInChildren<Transform>();
        int levelNumber = GameManager.instance.levelNumber;

        for (int i = 1; i < fruitPosition.Length; i++)
        {
            GameObject newFruit = Instantiate(fruitPrefab, fruitPosition[i]);
            if (randomFruit)
            {
                fruitIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(FruitType)).Length);
                newFruit.GetComponent<Fruit_Item>().FruitSetup(fruitIndex);
            }
            else
            {
                newFruit.GetComponent<Fruit_Item>().FruitSetup(fruitIndex);
                fruitIndex++;
                if (fruitIndex > Enum.GetNames(typeof(FruitType)).Length)
                {
                    fruitIndex = 0;
                }
            }
            fruitPosition[i].GetComponent<SpriteRenderer>().sprite = null;

        }
        int totalAmountFruits = PlayerPrefs.GetInt("Level" + levelNumber + "TotalFruits");
        if (totalAmountFruits != fruitPosition.Length - 1)
        {
            PlayerPrefs.SetInt("Level" + levelNumber + "TotalFruits", fruitPosition.Length - 1);
        }
    }

    private void Update()
    {

    }
}