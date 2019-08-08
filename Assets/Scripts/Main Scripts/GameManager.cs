using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Health currentHealth;
    Health dead;

    //Reference to the overlay Text to display winning text, etc
   // public Text m_MessageText;
    public Text m_TimerText;


    //PAUSED Text
   // public Text m_PauseText;



    //Pause Buttons
    public Button m_NewGameButton;
    public Button m_QuitToMenuButton;
    public Button m_ResumeButton;

    //Class Selection
    public Button sniperClass;
    public Button gunnerClass;
    public Button assaultClass;

    //Spawn points
    //Enemies
    //public GameObject[] redSpawn;
    public Transform redSpawn;
    //Friendly + player
    // public GameObject[] blueSpawn;
    public Transform blueSpawn;
    //Instantiate
    public GameObject[] m_Enemies;
    public GameObject[] m_Friendlies;
    public GameObject[] m_Player;
    //BG 
    public Image background;

    //Player Class
    public GameObject[] m_Chars;

    //GameTime
    private float m_gameTime = 600;
    public float GameTime { get { return m_gameTime; } }

    //Pause Menu
   // public GameObject m_pauseMenu;

    //Class selection
    public GameObject classSelection;

    //Scoring for kills
    public static int redKills;
    public static int blueKills;

    //Winning the game
    public bool isblueVictory = false;
    public bool isredVictory = false;

    //Game Over Screen
    public GameObject gameoverScreen;
    public GameObject redvictoryScreen;
    public GameObject bluevictoryScreen;

    //Killcounter
    public Text redkillCount;
    public Text bluekillCount;

    //Final Scores
    public Text finalscoreRed;
    public Text finalscoreBlue;

    //MAX Score, allows for freeze of kills
    public int maxredKill = 3;
    public int maxblueKill = 3;

    /* //Prefabs for respawning
     public GameObject playerPrefab;
     public GameObject enemyPrefab;
     public GameObject friendlyPrefab; */

    //Prefabs for respawning
    public GameObject player;

    public GameObject sniperPrefab;
    public GameObject gunnerPrefab;
    public GameObject assaultPrefab;

    public GameObject enemyPrefab;
    public GameObject friendlyPrefab;

    //Respawn Floats
    public float respawnTime = 5f;
    public float respawnPlayer = 5f;
    public float timer = 0.0f;

    public Text timetoSpawn;
    public GameObject deadScreen;

    //Gamestates
    public enum GameState
    {
        Start,
        Playing,
       // GameOver
    };

    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }
    
    private void Awake()
    {

        m_GameState = GameState.Start;
        classSelection.gameObject.SetActive(true);
    // theGameManager = this;
    //redSpawn = GameObject.FindGameObjectsWithTag("Spawn Enemy");

    // blueSpawn = GameObject.FindGameObjectsWithTag("Spawn Friendly");
    //Instantiate(playerPrefab, blueSpawn.position, Quaternion.identity);
}


    private void Start()
    {
        spawnEnemy();
        spawnFriendly();
        player = GameObject.FindGameObjectWithTag("Player");
        blueKills = 0;

        redKills = 0;

        m_TimerText.gameObject.SetActive(false);
       
        // m_PauseText.gameObject.SetActive(false);
        gameoverScreen.gameObject.SetActive(false);
        deadScreen.gameObject.SetActive(false);
        bluevictoryScreen.gameObject.SetActive(false);
        redvictoryScreen.gameObject.SetActive(false);

        //m_MessageText.text = "Select Class to START";
        //classSelection.gameObject.SetActive(true);

        m_NewGameButton.gameObject.SetActive(false);
        m_QuitToMenuButton.gameObject.SetActive(false);
        // m_ResumeButton.gameObject.SetActive(false);

        // m_pauseMenu.SetActive(false);

       // Instantiate(playerPrefab, blueSpawn.position, Quaternion.identity);

    }

    void Update()
    {
        if (redKills > 2)
        {
            Time.timeScale = 0;
            isredVictory = true;
            redvictoryScreen.gameObject.SetActive(true);
            bluevictoryScreen.gameObject.SetActive(false);
            gameoverScreen.gameObject.SetActive(true);
            
        }
        if (blueKills > 2)
        {
            Time.timeScale = 0;
            isblueVictory = true;
            redvictoryScreen.gameObject.SetActive(false);
            bluevictoryScreen.gameObject.SetActive(true);
            gameoverScreen.gameObject.SetActive(true);
        }

        if (isredVictory == true)
        {
            gameoverScreen.gameObject.SetActive(true);
            // m_GameState = GameState.GameOver;
            m_TimerText.gameObject.SetActive(false);
            m_NewGameButton.gameObject.SetActive(true);
            m_QuitToMenuButton.gameObject.SetActive(true);
            finalscoreBlue.text = " " + blueKills;
            finalscoreRed.text = " " + redKills;
            redvictoryScreen.gameObject.SetActive(true);
            bluevictoryScreen.gameObject.SetActive(false);
            if (redKills > maxredKill)
            {
                redKills = maxredKill;
            }


        }
        if (isblueVictory == true)
        {
            gameoverScreen.gameObject.SetActive(true);
            // m_GameState = GameState.GameOver;
            m_TimerText.gameObject.SetActive(false);
            m_NewGameButton.gameObject.SetActive(true);
            m_QuitToMenuButton.gameObject.SetActive(true);
            finalscoreRed.text = " " + redKills;
            finalscoreBlue.text = " " + blueKills;
            bluevictoryScreen.gameObject.SetActive(true);
            redvictoryScreen.gameObject.SetActive(false);
            if (blueKills > maxblueKill)
            {
                blueKills = maxblueKill;
            }
          
        }

        /* switch (m_GameState)
         {
             case GameState.Start:
                 if (Input.GetKeyUp(KeyCode.Return) == true) */



                     Time.timeScale = 1;
                    m_TimerText.gameObject.SetActive(true);
        // Find a random index between zero and one less than the number of spawn points.


        //classSelection.gameObject.SetActive(true);
        //m_GameState = GameState.Playing;
        /* for (int i = 0; i < m_Player.Length; i++)
         {
             m_Player[i].SetActive(true);
         }


/*
     break;
 case GameState.Playing:
 */

        //bool isGameOver = false;


               
        {

        }
                bluekillCount.text = "BLUE TEAM: " + blueKills;
                redkillCount.text = "RED TEAM: " + redKills;
                m_gameTime -= Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_gameTime);
                m_TimerText.text = string.Format("TIME REMAINING: " + "{0:D2}:{1:D2}",

                (seconds / 60), (seconds % 60));
                //Pause();


               
                /*
                break;
            case GameState.GameOver: */
                /* if (Input.GetKeyUp(KeyCode.Return) == true)
                 {                  
                     m_GameState = GameState.Playing;
                     m_MessageText.text = "";
                     m_TimerText.gameObject.SetActive(true);
                     for (int i = 0; i < m_Chars.Length; i++)
                     {
                         m_Chars[i].SetActive(true);
                     }
                 }
                break; */
        //Kill calculation
      

 //       if (Input.GetKeyUp(KeyCode.Escape))
   //     {
     //       Application.Quit();
       // }
    }

    public void OnNewGame()
    {
        SceneManager.LoadScene("Outside");

    }



   /* public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                //m_pauseMenu.SetActive(true);
                m_QuitToMenuButton.gameObject.SetActive(true);
                m_NewGameButton.gameObject.SetActive(true);
                m_ResumeButton.gameObject.SetActive(true);
               // m_PauseText.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            //  else if (Time.timeScale == 0)
            //     {
            //       Debug.Log("high");
            //       Time.timeScale = 1;    
            //   }
        } */
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu Scene");
    }
    public void ResumeGame()
    {
        m_QuitToMenuButton.gameObject.SetActive(false);
        m_NewGameButton.gameObject.SetActive(false);
        m_ResumeButton.gameObject.SetActive(false);
        //m_PauseText.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("FPS Tutorial Scene");
    }
    public void Scoring()
    {


    }
    public void Sniper()
    {
        Instantiate(sniperPrefab, blueSpawn.position, Quaternion.identity);
            m_GameState = GameState.Playing;
        classSelection.gameObject.SetActive(false);

    }
    public void Gunner()
    {
        Instantiate(gunnerPrefab, blueSpawn.position, Quaternion.identity);
        m_GameState = GameState.Playing;
        classSelection.gameObject.SetActive(false);
    }
    public void Assault()
    {
        Instantiate(assaultPrefab, blueSpawn.position, Quaternion.identity);
        m_GameState = GameState.Playing;
        classSelection.gameObject.SetActive(false);
    }

    public void classSelector()
    {

    }








    /* public void spawnPlayer()
     {
        // Instantiate(playerPrefab, blueSpawn.position, blueSpawn.rotation);

         timer += Time.deltaTime;
         deadScreen.gameObject.SetActive(true);
         timetoSpawn.gameObject.SetActive(true);
         timetoSpawn.text = " " + timer.ToString("f1");

         if (timer > respawnPlayer)
         {
             timer = timer - respawnPlayer;
             timetoSpawn.gameObject.SetActive(false);
             deadScreen.gameObject.SetActive(false);
             Instantiate(playerPrefab, blueSpawn.position, Quaternion.identity);
         } */


    public void spawnFriendly()
    {

        Instantiate(friendlyPrefab, blueSpawn.position, blueSpawn.rotation);
   }
    public void spawnEnemy()
    {
        Instantiate(enemyPrefab, redSpawn.position, redSpawn.rotation);
    }

}









/*
   for (int i = 0; i < m_Enemies.Length; i++)
                    {
                        m_Enemies[i].SetActive(true);
                    }                    if (m_Enemies.Length <5)
                    {
                        Instantiate(enemyPrefab, redSpawn.position, redSpawn.rotation);
                    }
                    for (int i = 0; i < m_Friendlies.Length; i++)
                    {
                        m_Friendlies[i].SetActive(true);
                    }                    if (m_Enemies.Length < 4)
                    {
                        Instantiate(friendlyPrefab, blueSpawn.position, blueSpawn.rotation);
                    }
                    for (int i = 0; i < m_Player.Length; i++)
                    {
                        m_Player[i].SetActive(true);
                    }
                    if (m_Player.Length < 1)
                    {
                        Instantiate(playerPrefab, blueSpawn.position, blueSpawn.rotation);
                    }
                    */