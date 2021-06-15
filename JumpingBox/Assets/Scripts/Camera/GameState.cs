using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour {

    [SerializeField] private GameStates _gameState = GameStates.Menu;

    public UnityEvent OnGameStateChanged;

    private void Start()
    {
    }

    public GameStates State
    {
        get { return _gameState; }
        set
        {
            if (value != _gameState)
            {
                _gameState = value;
                OnGameStateChanged.Invoke();
            }
        }
    }

    public void OnButtonPlayClick()
    {
        GetComponent<Animation>().Play();
        State = GameStates.GameIsOn;
    }
}

public enum GameStates
{
    Menu,
    GameIsOn
}
