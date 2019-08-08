using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{

    public Button BeginGame;
    public Button ExitGame;
    public Button Settings;
    public Button HighScores;

    public Text welcomeText;
    public Text menuText;

    public bool isStart;
    public bool isQuit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    public void StartSinglePlayer()
    {
        SceneManager.LoadScene("FPS Tutorial Scene");
    }

    public void StartMultiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}


