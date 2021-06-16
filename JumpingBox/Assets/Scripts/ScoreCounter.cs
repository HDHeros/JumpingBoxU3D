using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ScoreChangedEvent : UnityEvent<int> { }

public class ScoreCounter : MonoBehaviour
{
    public ScoreChangedEvent ScoreChanged;

    private GameObject _mainCube;
    private Text _labelText;
    private int _score;

    private void CheckBestScore()
    {
        if (PlayerPrefs.GetInt("BestScore") < _score)
            PlayerPrefs.SetInt("BestScore", _score);
    }

    public void OnCubeBecameInvisible()
    {
        CheckBestScore();
    }

    private void Start()
    {
        _mainCube = GameObject.FindGameObjectWithTag("Player");
        _mainCube.GetComponent<MainCube>().CubeBecameInvisible.AddListener(OnCubeBecameInvisible);
        _labelText = GetComponent<Text>();

        if (!PlayerPrefs.HasKey("BestScore"))
            PlayerPrefs.SetInt("BestScore", 0);

        _labelText.text = "Best score: " + PlayerPrefs.GetInt("BestScore").ToString();
    }

    private void Update()
    {
        if((int)_mainCube.transform.position.y > _score)
        {
            _score = (int)_mainCube.transform.position.y;
            _labelText.text = _score.ToString();
            ScoreChanged.Invoke(_score);
        }
    }


}
