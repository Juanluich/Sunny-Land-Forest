using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKiller : MonoBehaviour
{
    public Enemies Enemy;


    private void Start()
    {
        Enemy = GetComponentInParent<Enemies>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision) //Tiny box placed below the player to kill enemies when collides
    {
        if (collision.gameObject.CompareTag("Killer"))
        {
            Enemy.GetComponent<BoxCollider2D>().enabled = false;
            Enemy.enemyLifes--;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//Same as before but related to types of enemy with triggered active or not
    {
        if (collision.gameObject.CompareTag("Killer"))
        {
            Enemy.enemyLifes--;
            Enemy.GetComponent<BoxCollider2D>().enabled = false;


        }
    }
}
