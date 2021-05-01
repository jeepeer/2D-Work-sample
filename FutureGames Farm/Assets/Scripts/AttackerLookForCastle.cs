using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerLookForCastle : MonoBehaviour
{
    float timeBewteenAttack;
    public GameObject attacker;

    private void Update()
    {
        timeBewteenAttack += Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "castle" && timeBewteenAttack >= 5)
        {
            attacker.GetComponent<Attacker>().TakeDamage();
            timeBewteenAttack = 0;
        }
    }
}
