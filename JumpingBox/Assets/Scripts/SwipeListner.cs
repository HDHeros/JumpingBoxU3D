using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[System.Serializable]
public class SwipeScreenEvent : UnityEvent<PointerEventData> { }

public class SwipeListner : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public SwipeScreenEvent SwipeScreen;
    
    private GameState _gameState;

    private void Start()
    {
        _gameState = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameState>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_gameState.State == GameStates.Menu) return;
        SwipeScreen.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
