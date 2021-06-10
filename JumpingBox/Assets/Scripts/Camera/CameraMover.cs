using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private ScoreCounter _scoreCouner;
    private float _speed;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _startSpeed = _startSpeed == 0 ? 0.01f : _startSpeed;
        _speed = _startSpeed;
        _scoreCouner.OnScoreChanged.AddListener(OnScoreChanged);
    }

    private void Update()
    {
        _transform.Translate(Vector3.up * _speed);
    }

    public void OnScoreChanged(int _score)
    {
        _speed = _startSpeed + (float)_score / 1000;
    }

}
