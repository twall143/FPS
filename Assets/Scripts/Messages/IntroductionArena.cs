﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionArena : MonoBehaviour
{

    bool enter = false;
    public GameObject thisObject;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (enter)
        {
            GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height - 150, 200, 50), "This is the Arena. Only the brave survive. Walk to the podium if you think you're ready.");
        }
    }

    //Activate the Main function when player is near the door
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true;
            thisObject.SetActive(true);
        }
    }

    //Deactivate the Main function when player is go away from door
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = false;
            thisObject.SetActive(false);
        }
    }
}