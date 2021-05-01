using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int money = 1;
    private int totalMineHealth = 3;
    private int currentMineHealth;
    private float generationTimer;

    public LayerMask castleLayer, farmLayer;

    private int happiness = 1;
    private int happiness2 = 1;
    private int finalEmotion;
    public Sprite[] Emotions;
    public GameObject EmotionObject;

    GameController game;
    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameController>();
        currentMineHealth = totalMineHealth;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateFood();
        LookForBuidlingCollisions();
        generationTimer += Time.deltaTime;
    }

    private void LookForBuidlingCollisions()
    {
        Collider2D[] castleCollision = Physics2D.OverlapCircleAll(transform.position, 1, castleLayer);
        happiness2 = castleCollision.Length;

        Collider2D[] farmCollision = Physics2D.OverlapCircleAll(transform.position, 1, farmLayer);
        happiness = farmCollision.Length;

        finalEmotion = (happiness + happiness2);
        AmountOfPowerGenerated(finalEmotion);
    }

    private void GenerateFood()
    {
        if (generationTimer >= 2)
        {
            game.totalMoney += money;
            generationTimer = 0;
        }
    }
    private void AmountOfPowerGenerated(int amount)
    {
        switch (amount)
        {
            case 1:
                money = 1;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[1];
                break;
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                money = 2;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[2];
                break;
            case 8:
                money = 3;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[3];
                break;
            default: // incase the finalEmotion goes negative
                money = 1;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[1];
                break;
        }
    }

    public void TakeDamage()
    {
        if (currentMineHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            currentMineHealth--;
        }
    }
}
