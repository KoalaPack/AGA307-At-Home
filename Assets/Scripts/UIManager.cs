using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text enemyCountText;

    public GameObject spikeBall;
    public GameObject atomBall;
    public GameObject wheelBall;

    private void Start()
    {
        UpdateScore(0);
        UpdateTime(0);
        UpdateEnemyCount(0);

        spikeBall.SetActive(true);
        atomBall.SetActive(false);
        wheelBall.SetActive(false);
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

    public void UpdateEnemyCount(int _enemyCount)
    {
        enemyCountText.text = "Enemy Count: " + _enemyCount.ToString();
    }
}
