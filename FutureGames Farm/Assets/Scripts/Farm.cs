using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField] public int food;
    private int totalFarmHealth = 3;
    [SerializeField]private int currentFarmHealth;
    private float generationTimer;
    public Sprite[] Emotions;

    public LayerMask castleLayer, mineLayer, farmLayer;

    [SerializeField] private int happiness = 1;
    [SerializeField] private int sadness;
    [SerializeField] private int finalEmotion = 1;


    public GameObject EmotionObject;
    GameController game;
    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameController>();
        currentFarmHealth = totalFarmHealth;
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
        happiness = castleCollision.Length;

        Collider2D[] farmCollision = Physics2D.OverlapCircleAll(transform.position, 1, farmLayer);
        happiness = farmCollision.Length;

        Collider2D[] mineCollision = Physics2D.OverlapCircleAll(transform.position, 1, mineLayer);
        sadness = mineCollision.Length;

        finalEmotion = happiness - sadness;
        AmountOfPowerGenerated(finalEmotion);
    }

    private void GenerateFood()
    {
        if (generationTimer >= 2)
        {
            game.totalFood += food;
            generationTimer = 0;
        }
    }
    private void AmountOfPowerGenerated(int amount)
    {
        switch (amount)
        {
            case 1:
                food = 1;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[1];
                break;
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                food = 2;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[2];
                break;
            case 8:
                break;
            case 9:
                food = 3;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[3];
                break;
            default: // incase the finalEmotion goes negative
                food = 0;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[0];
                break;
        }
    }

    public void TakeDamage()
    {
        if(currentFarmHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            currentFarmHealth--;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
