using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGrenade : MonoBehaviour
{
    public float delay = 3f;
    float countdown;
    public GameObject Smoke;
    bool hasExploded = false;
    public Transform player;
    public GameObject grenadePin;

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
