using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class FlashBang : MonoBehaviour
{
    //public GameObject player;
    public Transform player;

    public float MaxTimer = 2;
    float flashTimer;

    public AudioClip audioClip;
 

    AudioSource audioSource;
    bool cap;
    CaptureScreen capScr;
   // ScreenCapture capScr;
    bool once;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        capScr = Camera.main.transform.GetComponent<CaptureScreen>();
    }

    // Update is called once per frame
  
    void Update()
    {
        flashTimer += Time.deltaTime;
        if (flashTimer > MaxTimer)
        {
            if (!once)
            {
               if(Vector3.Angle(player.transform.forward, transform.position - player.transform.position)< 110)
                {
                    capScr.startFading = true;
                    capScr.cap = true;
                }
                audioSource.PlayOneShot(capScr.audioClip);

                once = true;
            }
            if(flashTimer >MaxTimer+1)
            {
                Destroy(gameObject);
            }
        }
    }
}