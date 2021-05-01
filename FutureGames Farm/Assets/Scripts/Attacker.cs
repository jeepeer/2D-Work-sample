using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    private Vector2 buildingPosition;
    public LayerMask buildingLayer;
    private Vector2 attackerPosition;
    private float maxRange = 20f;
    private float movementSpeed = 5f;

    public LayerMask wall;
    public LayerMask attackerlayer;
    public LayerMask farmLayer, mineLayer;
    [SerializeField]private float attackTimer;

    [SerializeField]private int whichBuilding;

    private int totalAttackerHealth = 3;
    [SerializeField]public int currentAttackerHealth;

    // Start is called before the first frame update
    void Start()
    {
        whichBuilding = Random.Range(1, 3);
        currentAttackerHealth = totalAttackerHealth;

    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        attackerPosition = gameObject.transform.position;

        if(Input.GetKey(KeyCode.L)) { LookForBuilding(Random.Range(1,3)); }
        //GoTowardsBuilding();
        LookForBuilding(whichBuilding);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "farm" && attackTimer >= 3)
        {
            collision.gameObject.GetComponent<Farm>().TakeDamage();
            attackTimer = 0;
            Debug.Log("hit");
        }
        else if (collision.gameObject.tag == "mine" && attackTimer >= 3)
        {
            collision.gameObject.GetComponent<Mine>().TakeDamage();
            attackTimer = 0;
            Debug.Log("hit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ScaredAway();
        }
    }

    private void GoTowardsBuilding()
    {
        //moves attacker towards the nearest building
          transform.position = Vector2.MoveTowards
          (attackerPosition, buildingPosition, movementSpeed * Time.deltaTime);   
    }

    private void LookForBuilding(int whichBuilding)
    {

        switch(whichBuilding)
        {
            case 1:
                Collider2D[] farmCollisions = Physics2D.OverlapCircleAll(attackerPosition, maxRange, farmLayer);
                foreach (var farmCollision in farmCollisions)
                {
                    buildingPosition = farmCollision.transform.position;

                    if (farmCollisions.Length >= 1)
                    {
                        GoTowardsBuilding();
                    }
                    else { whichBuilding = 2; }
                }
                break;
            case 2:
                Collider2D[] mineCollisions = Physics2D.OverlapCircleAll(attackerPosition, maxRange, mineLayer);
                foreach (var mineCollision in mineCollisions)
                {
                    buildingPosition = mineCollision.transform.position;

                    if (mineCollisions.Length >= 1)
                    {
                        GoTowardsBuilding();
                    }
                    else { whichBuilding = 1; }
                }
                break;
        }
    }

    public void TakeDamage()
    {
        if (currentAttackerHealth <= 0)
        {
            ScaredAway();
            currentAttackerHealth = totalAttackerHealth;
        }
        else
        {
            currentAttackerHealth--;
        }
    }

    private void ScaredAway()
    {
        NewLocation();
    }

    private void NewLocation() // teleports to new location after being scared away
    {
        float randomX = Random.Range(10, 31);
        float randomY = Random.Range(10, 31);
        transform.position = new Vector2(randomX, randomY);
    }


}
