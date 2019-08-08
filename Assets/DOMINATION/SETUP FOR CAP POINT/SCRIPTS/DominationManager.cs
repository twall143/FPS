using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominationManager : MonoBehaviour
{
    //Red Team
    public Text domRedTeam;
    public Text domRedScore;
    //Blue Team
    public Text domBlueTeam;
    public Text domBlueScore;
    //Both Teams
    public Text domTimer;
    public Text domKillFeed;
    //public Text blank;
    public Text m_captureA;
    public Text m_captureB;
    public Text m_captureC;


    public GameObject[] m_Players;
    public GameObject redTeam;
    public GameObject blueTeam;

   // private float m_gameTime = 0;
   // public float GameTime { get { return m_gameTime; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
