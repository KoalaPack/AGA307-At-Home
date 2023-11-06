using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Title, Playing, Paused, GameOver }
public enum Difficulty { Easy, Medium, Hard }

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;
    public int score = 0;
    int scoreMultiplier = 1;

    public void Start()
    {
        DifficultEnum difficultEnum = new DifficultEnum();
        Difficulty currentDifficultyValue = difficultEnum.currentDifficultyValue;

        switch (currentDifficultyValue)
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

    public class DifficultEnum
    {
        public Difficulty currentDifficultyValue = Difficulty.Easy;
    }

    public void AddScore(int _points)
    {
        score += _points * scoreMultiplier;
        _UI.UpdateScore(score);
    }

    void OnTargetHit(GameObject _enemy)
    {
        int _score = _enemy.GetComponent<Target>().scoreBonus;
        AddScore(_score);
    }

    public void OnEnable()
    {
        Target.OnTargetHit += OnTargetHit;
        Target.OnTargetDie += OnTargetHit;
    }

    public void OnDisable()
    {
        Target.OnTargetHit -= OnTargetHit;
        Target.OnTargetDie -= OnTargetHit;
    }
}
