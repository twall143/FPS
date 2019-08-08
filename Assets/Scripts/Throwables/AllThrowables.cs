using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class AllThrowables : MonoBehaviour
{


    public GameObject flashBang;
    public GameObject fragGrenade;
    public GameObject flameGrenade;
    public GameObject smokeGrenade;

    GameObject flash;
    GameObject frag;
    GameObject flame;
    GameObject smoke;

    public int maxammoFlash = 1;
    public int maxammoFrag = 1;
    public int maxammoSmoke = 1;
    public int maxammoFlame = 1;

    //Sniper HUD
    public Image smokeTrue;
    public Image smokeFalse;
    public Image flameTrue;
    public Image flameFalse;

    //Gunner HUD
    public Image flashTrue;
    public Image flashFalse;
    public Image fragTrue;
    public Image fragFalse;

    public bool isSniper;
    public bool isGunner;

    private int currentammoFlash;
    private int currentammoFrag;
    private int currentammoSmoke;
    private int currentammoFlame;

    public float force = 20;

    
    // Start is called before the first frame update
    void Start()
    {
        currentammoFlash = maxammoFlash;
        currentammoFrag = maxammoFrag;
        currentammoSmoke = maxammoSmoke;
        currentammoFlame = maxammoFlame;
       

        if (isGunner == true)
        {
            flashTrue.gameObject.SetActive(true);
            fragTrue.gameObject.SetActive(true);
        }

        if (isSniper == true)
        {
            smokeTrue.gameObject.SetActive(true);
            flameTrue.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame

    void Update()
    {
           if (isGunner == true)
        {
            if (currentammoFlash <= 0)
            {
                flashFalse.gameObject.SetActive(true);
                flashTrue.gameObject.SetActive(false);

            }
            if (currentammoFlash > 0)
            {
                flashFalse.gameObject.SetActive(false);
                flashTrue.gameObject.SetActive(true);
            }
            if (currentammoFrag <= 0)
            {
                fragFalse.gameObject.SetActive(true);
                fragTrue.gameObject.SetActive(false);
            }
            if (currentammoFrag > 0)
            {
                fragFalse.gameObject.SetActive(false);
                fragTrue.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown("4"))
            {
                if (currentammoFrag > 0)
                    frag = Instantiate(fragGrenade, transform.position + transform.forward, Quaternion.identity) as GameObject;
                frag.AddComponent<Rigidbody>();
                frag.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
                frag.AddComponent<FragGrenade>();
                frag.GetComponent<FragGrenade>().player = transform;
                frag.AddComponent<AudioSource>();
                currentammoFrag--;
                if (currentammoFrag <= 0)
                {
                    fragFalse.gameObject.SetActive(true);
                    fragTrue.gameObject.SetActive(false);
                }
            }
            if (Input.GetKeyDown("5"))
            {
                if (currentammoFlash > 0)
                    flash = Instantiate(flashBang, transform.position + transform.forward, Quaternion.identity) as GameObject;
                flash.AddComponent<Rigidbody>();
                flash.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
                flash.AddComponent<FlashBang>();
                flash.GetComponent<FlashBang>().player = transform;
                flash.AddComponent<AudioSource>();
                currentammoFlash--;

                if (currentammoFlash <= 0)
                {
                    flashFalse.gameObject.SetActive(true);
                    flashTrue.gameObject.SetActive(false);
                }
            }
        }
        if (isSniper == true)
        {
            if (currentammoSmoke <= 0)
            {
                smokeFalse.gameObject.SetActive(true);
                smokeTrue.gameObject.SetActive(false);
            }
            if (currentammoSmoke > 0)
            {
                smokeFalse.gameObject.SetActive(false);
                smokeTrue.gameObject.SetActive(true);
            }
            if (currentammoFlame <= 0)
            {
                flameFalse.gameObject.SetActive(true);
                flameTrue.gameObject.SetActive(false);
            }
            if (currentammoFlame > 0)
            {
                flameFalse.gameObject.SetActive(false);
                flameTrue.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown("4"))
            {
                if (currentammoSmoke > 0)
                    smoke = Instantiate(smokeGrenade, transform.position + transform.forward, Quaternion.identity) as GameObject;
                smoke.AddComponent<Rigidbody>();
                smoke.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
                smoke.AddComponent<SmokeGrenade>();
                smoke.GetComponent<SmokeGrenade>().player = transform;
                smoke.AddComponent<AudioSource>();
                currentammoSmoke--;
                if (currentammoSmoke <= 0)
                {
                    smokeFalse.gameObject.SetActive(true);
                    smokeTrue.gameObject.SetActive(false);
                }
            }
            if (Input.GetKeyDown("5"))
            {
                if (currentammoFlame > 0)
                    flame = Instantiate(flameGrenade, transform.position + transform.forward, Quaternion.identity) as GameObject;
                flame.AddComponent<Rigidbody>();
                flame.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
                flame.AddComponent<FlameNade>();
                flame.GetComponent<FlameNade>().player = transform;
                flame.AddComponent<AudioSource>();
                currentammoFlame--;
                if (currentammoFlame <= 0)
                {
                    flameFalse.gameObject.SetActive(true);
                    flameTrue.gameObject.SetActive(false);
                }
            }
        }
    }
}