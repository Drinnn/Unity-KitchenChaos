using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }
    
    public static KitchenGameManager Instance { private set; get; }

    public event EventHandler OnStateChanged;

    [SerializeField] private float waitingToStartTimer = 1f;
    [SerializeField] private float countdownToStartTimer = 3f;
    [SerializeField] private float gamePlayingTimerMax = 10f;

    private State _state;
    private float _gamePlayingTimer;

    private void Awake()
    {
        Instance = this;
        
        _state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    _state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    _state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                    _gamePlayingTimer = gamePlayingTimerMax;
                }

                break;
            case State.GamePlaying:
                _gamePlayingTimer -= Time.deltaTime;
                if (_gamePlayingTimer < 0f)
                {
                    _state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.GameOver:
                break;
        }
        
        Debug.Log(_state);
    }

    public bool IsGamePlaying()
    {
        return _state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return _state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return _state == State.GameOver;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetPlayingTimeNormalized()
    {
        return 1 - _gamePlayingTimer / gamePlayingTimerMax;
    }
}
