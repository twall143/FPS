using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominationZone : MonoBehaviour
{
    public bool zonesAlive = false;
    public bool zoneActive = false;
    //public float zoneActive = 0;
    public bool zoneCaptured = false;
    //public float zoneCaptured = 0;
    //public bool zoneCapturing = false;
    //public float zoneCapturing = 0;
    public float zoneCapturing = 0f;

    //Timer
    public float timetoCapture = 10f;

    public float timeinGame = 1;

    public int zoneMultiplier = 1;

    public float[] zonesCaptured;

    public Text captureZone;
    public Text captureTeam;

    DominationManager blueTeam;
    DominationManager redTeam;

    public GameObject capturezoneA;
    public GameObject capturezoneB;
    public GameObject capturezoneC;

    // Start is called before the first frame update
    void Start()
    {
        zonesAlive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        zoneActive = true;
        if (zoneActive = true)
        {
            zoneCapture();
        }
    }
    public void domZone()
    {

    }

    public void zoneCapture()
    {
        

    }

}
