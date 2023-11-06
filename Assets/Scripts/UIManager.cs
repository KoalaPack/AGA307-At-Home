using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;


public class UIManager : Singleton<UIManager>
{
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text enemyCountText;

    public GameObject spikeBall;
    public GameObject atomBall;
    public GameObject wheelBall;

    public TMP_Text gameDifficulty;

    private GameManager gameManager;

    public GameObject startMenu;
    public GameObject EndMenu;
    public GameObject InGamePanel;

    private void Start()
    {
        startMenu.SetActive(true);
        EndMenu.SetActive(false);
        InGamePanel.SetActive(false );

        spikeBall.SetActive(false);
        atomBall.SetActive(false);
        wheelBall.SetActive(false);


    }

    public void PlayGame()
    {
        startMenu.SetActive(false);
        EndMenu.SetActive(false);
        InGamePanel.SetActive(true);

        gameManager = GameObject.FindObjectOfType<GameManager>(); // Find the GameManager in the scene
        UpdateScore(0);
        UpdateTime(0);
        UpdateTargetCount(0);

        //Ball Selector
        spikeBall.SetActive(true);
        atomBall.SetActive(false);
        wheelBall.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spikeBall.SetActive(true);
            atomBall.SetActive(false);
            wheelBall.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            spikeBall.SetActive(false);
            atomBall.SetActive(true);
            wheelBall.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spikeBall.SetActive(false);
            atomBall.SetActive(false);
            wheelBall.SetActive(true);
        }
    }

    public void UpdateScore(int _score)
    {
        scoreText.text = "Score: " + _score.ToString();
    }

    public void UpdateTime(float _time)
    {
        timeText.text = _time.ToString("F2");
    }

    public void UpdateTargetCount(int _enemyCount)
    {
        enemyCountText.text = "Enemy Count: " + _enemyCount.ToString();
    }

    //public void EasyButton()
    //{
    //    // Change the game difficulty to Easy
    //    gameManager.difficulty = Difficulty.Easy;
    //    UpdateGameDifficultyEasy(); // Update the game difficulty text
    //}

    //public void MediumButton()
    //{
    //    // Change the game difficulty to Easy
    //    gameManager.difficulty = Difficulty.Medium;
    //    UpdateGameDifficultyMedium(); // Update the game difficulty text
    //}

    //public void HardButton()
    //{
    //    // Change the game difficulty to Easy
    //    gameManager.difficulty = Difficulty.Hard;
    //    UpdateGameDifficultyHard(); // Update the game difficulty text
    //}


    //public void UpdateGameDifficultyEasy()
    //{
    //    gameDifficulty.text = "Game Difficulty: Easy";
    //    StartGame();
    //}

    //public void UpdateGameDifficultyMedium()
    //{
    //    gameDifficulty.text = "Game Difficulty: Medium";
    //    StartGame();
    //}

    //public void UpdateGameDifficultyHard()
    //{
    //    gameDifficulty.text = "Game Difficulty: Hard";
    //    StartGame();
    //}

    public void QuitGame()
    {
        Application.Quit();
    }
}
