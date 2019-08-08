using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyCollisionDamage : MonoBehaviour
{

    // The amount of damage done if the enemy touches the player
    public float m_MaxDamage = 34f;
    // The maximum distance away from the player for the enemy to do damage
    public float m_DamageRadius = 1;
    // The amount of force added to a player on contact
    public float m_EnemyForce = 100f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        // find the rigidbody of the collision object
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
        // only tanks will have rigidbody scripts
        if (targetRigidbody != null)
        {
            // Add an explosion force
            targetRigidbody.AddExplosionForce(m_EnemyForce,
           transform.position, m_DamageRadius);
            // find the TankHealth script associated with the rigidbody
            Health currentHealth = targetRigidbody.GetComponent<Health>();
            if (currentHealth != null)
            {
                // Calculate the amount of damage the target should take
                // based on it's distance from the shell.
                float damage = CalculateDamage(targetRigidbody.position);
                // Deal this damage to the tank
                currentHealth.TakeDamage(damage);
            }
        }
    }
    private float CalculateDamage(Vector3 targetPosition)
    {
        // Create a vector from the shell to the target
        Vector3 explosionToTarget = targetPosition - transform.position;
        // Calculate the distance from the shell to the target
        float explosionDistance = explosionToTarget.magnitude;
        // Calculate the proportion of the maximum distance (the explosionRadius)
        // the target is away
        float relativeDistance =
       (m_DamageRadius - explosionDistance) / m_DamageRadius;
        // Calculate damage as this proportion of the maximum possible damage
        float damage = relativeDistance * m_MaxDamage;
        // Make sure that the minimum damage is always 0
        damage = Mathf.Max(0f, damage);
        return damage;
    }
}
