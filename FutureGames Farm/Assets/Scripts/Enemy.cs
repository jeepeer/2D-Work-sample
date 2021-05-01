using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    float playerPercentage;
    int RandomNumber;
    float newPlayerPercentage;

    public GameObject battleMenu;

    public GameObject gameControllerObject;
    GameController game;
    Boss boss;
    LevelGenerator generator;
    Spawner spawner;

    public Text currentPowerTextFight;
    public Text currentEnemyPowerText;

    // Start is called before the first frame update
    void Start()
    {
        boss = FindObjectOfType<Boss>();
        game = FindObjectOfType<GameController>();
        spawner = FindObjectOfType<Spawner>();
        generator = FindObjectOfType<LevelGenerator>();
        game.totalPower += 100;
        game.enemyCurrentPower += 100;
    }

    // Update is called once per frame
    void Update()
    {
        currentPowerTextFight.text = game.totalPower.ToString();
        currentEnemyPowerText.text = game.enemyCurrentPower.ToString();
    }
    // calculates the percentage chance to win
    public void CalculatePossibilityToWin()
    {
        playerPercentage = (game.totalPower - game.enemyCurrentPower) / ((game.totalPower + game.enemyCurrentPower) / 2) * 100;
        if (playerPercentage <= 0)
        {
            newPlayerPercentage = (50 + (0 + playerPercentage)) / 10;
        }
        else { newPlayerPercentage = (playerPercentage + 50) / 10; }

        RandomNumber = Random.Range(1, 11);
        if (RandomNumber <= newPlayerPercentage)
        {
            Die();
        }
        else if (newPlayerPercentage < RandomNumber)
        {
            game.totalPower -= (game.totalPower / 3);
            ExitBattleMenu();
        }
        else if (RandomNumber <= newPlayerPercentage && boss.isBoss)
        {
            game.WinGame();
        }
    }
    public void ExitBattleMenu()
    {
        battleMenu.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // so that other gameobjects can't trigger
        if (collision.gameObject.tag == "Player")
        {
            battleMenu.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            battleMenu.SetActive(false);
        }
    }
    private void Die()
    {
        CalculateScalingEnemyPower();
        generator.GenerateNewRoom();
        spawner.AttackerSpawner();
        gameObject.SetActive(false);
    }

    public void CalculateScalingEnemyPower()
    {
        game.futureSpendableFood = ((game.farmTotal * 60) + game.totalFood) / 5;
        game.futureSpendableMoney = ((game.mineTotal * 60) + game.totalMoney) / 5;
        if(game.futureSpendableFood < game.futureSpendableMoney)
        {
            game.futureSpendableFood = game.futureCastles;
        }
        else if (game.futureSpendableMoney < game.futureSpendableFood)
        {
            game.futureSpendableMoney = game.futureCastles;
        }

        game.enemyCurrentPower = ((game.castleTotal + game.futureCastles) * 60) + game.totalPower;
    }    
}
