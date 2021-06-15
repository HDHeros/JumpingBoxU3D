using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] GameState _gameState;

    private void Start()
    {
        _gameState.OnGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnGameStateChanged()
    {
        Destroy(gameObject);
    }
}
