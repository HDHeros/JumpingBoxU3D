using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private ScoreCounter _scoreCouner;
    [SerializeField] private Transform _mainCubeTransform;
    [SerializeField] private GameState _gameState;
    [SerializeField] private bool _isActive = false;

    private float _speed;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _startSpeed = _startSpeed == 0 ? 0.01f : _startSpeed;
        _speed = _startSpeed;
        _scoreCouner.OnScoreChanged.AddListener(OnScoreChanged);
        _gameState = GetComponent<GameState>();
        _gameState.OnGameStateChanged.AddListener(OnGameStateChanged);
        OnGameStateChanged();
    }

    private void Update()
    {
        if (!_isActive) return;
        CameraMove();
    }

    private void OnGameStateChanged()
    {
        if (_gameState.State == GameStates.GameIsOn)
        {
            _isActive = true;
        }
        else
        {
            _isActive = false;
        }
    }

    private void CameraMove()
    {
        if(_mainCubeTransform.position.y > _transform.position.y + 2)
            _transform.Translate(Vector3.up * _speed * 4);

        _transform.Translate(Vector3.up * _speed);

    }

    public void OnScoreChanged(int _score)
    {
        _speed = _startSpeed + (float)_score / 10000;
    }

}
