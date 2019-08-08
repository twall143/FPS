using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameNade : MonoBehaviour
{

    public float BlastRadius = 10f;
    public float ExplosionPower = 700f;
    public int MaxDMG = 60;
    public Vector3 explosion_position;
    public float delay = 3f;
    float countdown;
    public GameObject Smoke;
    bool hasExploded = false;
    public Transform player;
    public GameObject grenadePin;
    //public Transform explosionPosition;

    //public Transform player;
    //public GameObject player1;

    Player currentHealth;

    //Collider[] hitColliders;

    public GameObject explosionEffect;

    //float countdown;
   // bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        //Smoke = GetComponent<ParticleSystem>();
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
        Instantiate(Smoke, transform.position, transform.rotation);
        // smoke.Play();
        // Destroy(gameObject, t: smoke.duration);
        //Destroy(gameObject);        
    }
}
