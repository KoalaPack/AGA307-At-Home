using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimerDirection { CountUp, CountDown }

public class Timer : Singleton<Timer>
{
    public TimerDirection timerDirection;
    public float startTime = 30;
    float currentTime;
    bool isTiming = false;
    float timeLimit = 0;
    bool hasTimeLimit = false;
    float timeAfterKill = 5f;

    private void Start()
    {
        StartTimer(30, TimerDirection.CountDown);
    }

    void Update()
    {
        if (!isTiming)
            return;

        if (timerDirection == TimerDirection.CountDown)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                currentTime = 0;
                StopTimer();
            }
        }
        else // TimerDirection.CountUp
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeLimit)
            {
                currentTime = timeLimit;
                StopTimer();
            }
        }

        _UI.UpdateTime(currentTime);

        if (currentTime <= 0)
        {
            _UI.UpdateTime(0);
        }
    }

    public void StartTimer(float _startTime = 0, TimerDirection _direction = TimerDirection.CountDown)
    {
        timerDirection = _direction;
        startTime = _startTime;
        currentTime = startTime;
        isTiming = true;
    }

    public void StartTimer(float _startTime = 0, float _timeLimit = 0, bool _hasTimeLimit = true, TimerDirection _direction = TimerDirection.CountDown)
    {
        hasTimeLimit = _hasTimeLimit;
        startTime = _startTime;
        timeLimit = _timeLimit;
        isTiming = true;
    }

    public void ResumeTimer()
    {
        isTiming = true;
    }

    public void PauseTimer()
    {
        isTiming = false;
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public void IncrementTimer(float _increment)
    {
        currentTime += _increment;
    }

    public void DecrementTimer(float _decrement)
    {
        currentTime -= _decrement;
    }

    public bool TimeExpired()
    {
        if (!hasTimeLimit)
            return false;
        return timerDirection == TimerDirection.CountUp ? currentTime > timeLimit : currentTime < timeLimit;
    }

    public float GetTime()
    {
        return currentTime;
    }

    public bool IsTiming()
    {
        return isTiming;
    }

    public void ChangeTimerDirection(TimerDirection _direction)
    {
        timerDirection = _direction;
    }

    public void AddTime()
    {
        currentTime = currentTime + timeAfterKill;
    }
}
