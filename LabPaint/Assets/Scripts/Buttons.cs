using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    //References
    [SerializeField] GameObject scoreboardHolder;
    [SerializeField] GameObject gameOverHolder;
    [SerializeField] GameObject controlsHolder;

    //Variables
    Button button_startGame, button_showScoreboard, button_exit, button_back, button_controls;
    Button button_submitScore, button_restart, button_mainMenu;
    InputField enterName;
    bool mainMenu = true;

    private void Start()
    {
        //Menu
        button_startGame = GameObject.Find("Play Button").GetComponent<Button>();
        button_showScoreboard = GameObject.Find("Scoreboard Button").GetComponent<Button>();
        button_exit = GameObject.Find("Exit Button").GetComponent<Button>();
        button_back = GameObject.Find("Back Button").GetComponent<Button>();
        button_controls = GameObject.Find("Controls Button").GetComponent<Button>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowScoreboard()
    {
        button_startGame.gameObject.SetActive(false);
        button_showScoreboard.gameObject.SetActive(false);
        button_exit.gameObject.SetActive(false);
        button_back.gameObject.SetActive(true);
        scoreboardHolder.SetActive(true);
        button_back.gameObject.SetActive(true);
    }

    public void ShowControls()
    {
        controlsHolder.gameObject.SetActive(true);
        button_startGame.gameObject.SetActive(false);
        button_showScoreboard.gameObject.SetActive(false);
        button_exit.gameObject.SetActive(false);
        button_back.gameObject.SetActive(true);
        button_controls.gameObject.SetActive(false);
    }

    public void Back()
    {
        button_startGame.gameObject.SetActive(true);
        button_showScoreboard.gameObject.SetActive(true);
        button_exit.gameObject.SetActive(true);
        button_back.gameObject.SetActive(false);
        scoreboardHolder.SetActive(false);
        button_back.gameObject.SetActive(false);
        controlsHolder.gameObject.SetActive(false);
        button_controls.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        gameOverHolder.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MaineMenu()
    {
        SceneManager.LoadScene("Menu:");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
