using UnityEngine;
using UnityEngine.Events;

public enum GameStates
{
    Menu,
    GameIsOn
}

public class GameState : MonoBehaviour {

    [SerializeField] private GameStates _state;

    public UnityEvent OnGameStateChanged;

    public GameStates State
    {
        get { return _state; }
        set
        {
            if (value != _state)
            {
                _state = value;
                OnGameStateChanged.Invoke();
            }
        }
    }

    private void Start()
    {
        _state = GameStates.Menu;
    }

   
    public void OnButtonPlayClick()
    {
        GetComponent<Animation>().Play();
        State = GameStates.GameIsOn;
    }
}