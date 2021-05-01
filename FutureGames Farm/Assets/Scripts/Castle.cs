using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] public int power;
    private int totalCastleHealth = 3;
    private int currentCastleHealth;
    [SerializeField]private float generationTimer;

    private float attackTimer;

    public LayerMask farmLayer, mineLayer;

    [SerializeField] private int happiness = 1;
    [SerializeField] private int sadness = 0;
    [SerializeField] private int finalEmotion = 1;

    public Sprite[] Emotions;
    public GameObject EmotionObject;

    GameController game;


    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameController>();
        currentCastleHealth = totalCastleHealth;
    }

    // Update is called once per frame
    void Update()
    {
        GeneratePower();
        LookForBuidlingCollisions();

        generationTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
    }

    private void GeneratePower()
    {
        if (generationTimer >= 2)
        {
            game.totalPower += power; 
            generationTimer = 0;
        }
    }

    private void AmountOfPowerGenerated(int amount)
    {
        switch(amount)
        {
            case 1:
                power = 1;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[1];
                break;
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                power = 2;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[2];
                break;
            case 8:
                break;
            case 9:
                power = 3;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[3];
                break;
            default: // incase the finalEmotion goes negative
                power = 0;
                EmotionObject.GetComponent<SpriteRenderer>().sprite = Emotions[0];
                break;
        }
    }


    private void LookForBuidlingCollisions()
    {
        Collider2D[] farmCollision = Physics2D.OverlapCircleAll(transform.position, 1, farmLayer);
        happiness = farmCollision.Length;

        Collider2D[] mineCollision = Physics2D.OverlapCircleAll(transform.position, 1, mineLayer);
        sadness = mineCollision.Length;

        finalEmotion = (happiness - sadness) + 1;
        AmountOfPowerGenerated(finalEmotion);
    }


    public void TakeDamage()
    {
        if (currentCastleHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            currentCastleHealth--;
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attacker" && attackTimer >= 5)
        {
            collision.gameObject.GetComponent<Attacker>().TakeDamage();
            attackTimer = 0;
        }
    }*/

}
