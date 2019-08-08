using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
   // public GameObject player;

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    public int currentHealth;

    private void Awake()
    {
        SetDefaults();   
    }
    public void TakeDamage(int _amount)
    {
        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");
    }

    public void SetDefaults()
        {
        currentHealth = maxHealth;
    }
}
