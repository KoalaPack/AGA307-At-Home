using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { Title, Playing, Paused, GameOver}
public enum Difficulty { Easy, Medium, Hard}


public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;
    public int score = 0;
    int scoreMultiplier = 1;


    private void Start()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                scoreMultiplier = 1;
                break;
            case Difficulty.Medium:
                scoreMultiplier = 2;
                break;
            case Difficulty.Hard:
                scoreMultiplier = 3;
                break;
        }
    }

    public void AddScore(int _points)
    {
        score += _points * scoreMultiplier;
        _UI.UpdateScore(score);
    }

    void OnTargetHit(GameObject _enemy)
    {
        int _score = _enemy.GetComponent<Enemy>().scoreBonus;
        AddScore(_score);
    }

    private void OnEnable()
    {
        Enemy.OnTargetHit += OnTargetHit;
        Enemy.OnTargetDie += OnTargetHit;
    }

    private void OnDisable()
    {
        Enemy.OnTargetHit -= OnTargetHit;
        Enemy.OnTargetDie -= OnTargetHit;
    }

}
