using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    // The amount of health player/enemy starts with
    public float m_StartingHealth = 100f;
    public float maxHealth = 100;

    // A prefab that will be instantiated in Awake, then used whenever
    public float m_CurrentHealth;

    private bool m_Dead;
   
    public float playerDead = 0;

    // Use this for initialization
    void Start () {
        //HealthBarManager.instance.AddHealthBar(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnEnable()
    {
        // when the tank is enabled, reset the tank's health and whether
        // or not it's dead
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

    }
    public void TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done
        m_CurrentHealth -= amount;
        // Change the UI elements appropriately
        //SetHealthUI();
        // if the current health is at or below zero and it has not yet
        // been registered, call OnDeath

        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }
    private void OnDeath()
    {
        
        m_Dead = true;
        
        gameObject.SetActive(false);
      

    }
}
