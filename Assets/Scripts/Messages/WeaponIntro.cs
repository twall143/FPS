using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIntro : MonoBehaviour
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
            GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height - 150, 200, 50), "These are your weapons. Kill enemies to upgrade them and make them more powerful.");
            GUI.Label(new Rect(Screen.width / 3 - 80, Screen.height - 150, 200, 50), "The Podium is a safe area. Enemies can not attack you here.");
            GUI.Label(new Rect(Screen.width / 1 - 450, Screen.height - 150, 200, 50), "1= Handgun. 2= Shotgun. 3= Assault Rifle. 4= ...");
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