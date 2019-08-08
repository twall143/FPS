using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction3 : MonoBehaviour
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
            GUI.Label(new Rect(Screen.width / 1 - 450, Screen.height - 150, 200, 50), "Enter the Arena, if you think you can handle it.");
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