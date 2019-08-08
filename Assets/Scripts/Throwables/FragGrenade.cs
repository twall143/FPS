using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class FragGrenade : MonoBehaviour
{
    public float delay = 3f;
    public float BlastRadius = 10f;
    public float ExplosionPower = 700f;
    public int MaxDMG = 60;
    public Vector3 explosion_position;


    //public Transform explosionPosition;

    public Transform player;
    //public GameObject player1;

    Player currentHealth;

    //Collider[] hitColliders;

    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;

    }

    // Update is called once per frame
   
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            hasExploded = true;
            if (hasExploded == true)
            {
                Explode();
            }
        }
    }
 public void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
         Collider[] hitColliders = Physics.OverlapSphere(transform.position, BlastRadius);
        //Collider[] hitColliders = Physics.OverlapSphere(location, radius);

        foreach (Collider nearbyObject in hitColliders)
        {

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(ExplosionPower, transform.position, BlastRadius);
                GameObject.FindGameObjectsWithTag("Player");
                if ("Player" != null)
                {
                    RaycastHit hit;
                    var exposed = false;
                    //see if player is exposed
                      if (Physics.Raycast(explosion_position, (player.transform.position-explosion_position), out hit))
                      {
                        exposed = (hit.collider.tag == "Player");
                     }
                       if (exposed)
                       {
                        // Damage Enemy! with a linear falloff of damage amount
                        //var proximity : float = (explosion_position - player.transform.position).magnitude;
                        // var effect : float = 1 - (proximity / BlastRadius);
                        GetComponent<Player>().currentHealth -= (MaxDMG);
                    }
                 

                }

                //Destruction
                Destruction dest = nearbyObject.GetComponent<Destruction>();
                if (dest != null)
                {
                    dest.Destroy();
                }
                //if (player != null)
               // {
               //     // ExplosionDamage();
              //  }

            }

            Destroy(gameObject);

            Debug.Log("Nade out");

        }
    }
    

    /* public void ExplosionDamage(Vector3 center, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            int i = 0;
            while (i < hitColliders.Length)
            {
                hitColliders[i].SendMessage("AddDamage");
                i++;
            }

            Debug.Log(transform.name + " now has " + currentHealth + " health.");
        }

                public void Explode(Vector3 center, float radius)
              {
                  Instantiate(explosionEffect, transform.position, transform.rotation);

               // public void ExplosionDamage(Vector3 center, float radius) {}

                    Collider[] hitColliders = Physics.OverlapSphere(center, radius);
                    int i = 0;
                    while (i < hitColliders.Length)
                    {
                        hitColliders[i].SendMessage("AddDamage");
                        i++;
                    }
                foreach (Collider col in hitColliders)
                {
                    Player player = col.GetComponent<Player>();
                    if (player != null)
                    {

                    }
                        Debug.Log(transform.name + " now has " + currentHealth + " health.");

                }

            */


    //GRENADE USING RAYCAST FOR DAMAGE SEMI WORKING
    /* void Explode()
    {
        //Instantiate our explosion *flame, smoke, trail*
        Instantiate(explosionEffect, transform.position, transform.rotation);


        //Collider[] hitColliders;
        Collider[] hitColliders = Physics.OverlapSphere(explosion_position, BlastRadius);

        foreach (Collider hitcol in hitColliders)
        {

            if (hitcol.GetComponent<Rigidbody>() != null || hitcol.GetComponent<Player>() != null)
            {


                RaycastHit hit;
                bool wallhit = false;
                if (Physics.Raycast(explosion_position, hitcol.transform.position - explosion_position, out hit, BlastRadius))
                {

                    if (hit.transform.GetComponent<Rigidbody>() == null && hit.collider != hitcol && hit.transform.tag != "Player")
                    {
                        wallhit = true;
                    }

                }

                if (wallhit == false)
                {
                    if (hitcol.GetComponent<Rigidbody>() != null)
                    {
                        hitcol.GetComponent<Rigidbody>().AddExplosionForce(ExplosionPower, explosion_position, BlastRadius, 1, ForceMode.Impulse);
                    }

                    if (hitcol.GetComponent<Player>() != null)
                    {
                        Vector3 closespoint = hitcol.ClosestPoint(explosion_position);
                        //MaxDMG * (Vector3.Distance(explosion_position, closespoint) / BlastRadius);
                        hitcol.GetComponent<Player>().currentHealth -= (MaxDMG);
                        Debug.Log(transform.name + " now has " + currentHealth + " health.");

                    }


                }


            }


        }

    }

    */


}