using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public bool canDie = true;                  // Whether or not this health can die

    public float startingHealth = 100.0f;       // The amount of health to start with
    public float maxHealth = 100.0f;            // The maximum amount of health
    public float currentHealth;		            // The current ammount of health

    public bool replaceWhenDead = false;        // Whether or not a dead replacement should be instantiated.  (Useful for breaking/shattering/exploding effects)
    public GameObject deadReplacement;          // The prefab to instantiate when this GameObject dies

    public GameObject enemyPrefab;



    public bool damaged;

    public bool dead = false;					// Used to make sure the Die() function isn't called twice

    public bool respawning;

    /*
    //Prefabs for respawning
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject friendlyPrefab;
    */

    //Respawn Floats
    public float respawnTime = 25f;
    public float respawnEnemy = 5f;
    public float timer = 0.0f;

    void Start()
    {
        currentHealth = startingHealth;
      
    }
    // Use this for initialization
    void Update()
    {
       


        if (damaged)
        {
           
        }
       
        else
        {
          
        }

      
        damaged = false;
    }

    public void TakeDamage(float amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // If the player has lost all it's health
        if (currentHealth <= 0)
        {
            GameManager.blueKills += 1;
            GetComponent<GameManager>().spawnEnemy();
            if (replaceWhenDead)
            {
                dead = true;
            }
            Destroy(gameObject);

        }
    }
    public void Death()
    {
        // This GameObject is officially dead.  This is used to make sure the Die() function isn't called again
        
        //GameManager.blueKills += 1;
       
        GetComponent<GameManager>().spawnEnemy();
        if (replaceWhenDead)
        {
            dead = true;
        }
        Destroy(gameObject);

      
    }


}


