using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollFade : MonoBehaviour
{
    public float timer;
 
    public float destroyRagdoll = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > destroyRagdoll)
        {
            Destroy(gameObject);
            timer = timer - destroyRagdoll;
        }
    }
}
