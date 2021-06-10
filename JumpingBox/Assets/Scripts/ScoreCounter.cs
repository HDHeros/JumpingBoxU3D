using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ScoreChangedEvent : UnityEvent<int> { }

public class ScoreCounter : MonoBehaviour
{
    public ScoreChangedEvent OnScoreChanged;

    private GameObject _mainCube;
    private Text _text;

    private int _score;

    private void Start()
    {
        _mainCube = GameObject.FindGameObjectWithTag("Player");
        _text = GetComponent<Text>();
        _score = 0;
    }

    private void Update()
    {
        if((int)_mainCube.transform.position.y > _score)
        {
            _score = (int)_mainCube.transform.position.y;
            _text.text = _score.ToString();
            OnScoreChanged.Invoke(_score);
        }
    }
}
