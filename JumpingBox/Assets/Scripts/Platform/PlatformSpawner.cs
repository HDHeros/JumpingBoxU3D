using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlatformProbWrap
{
    [SerializeField] public GameObject _gameObject;
    [SerializeField] public int _probability;
}

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private bool _isActive = false;
    [SerializeField] private float _period;
    [SerializeField] private CameraRotator _cameraRotator;
    [SerializeField] private GameState _gameState;
    [SerializeField] private List<PlatformProbWrap> _platformsTypes;

    private Transform _transform;
    private Transform _lastPlatformTransform;
    private GameObject _lastPlatformGameObject;
    private List<string> _lastPlatformsName;//хранит имена последних _numberOfStored платформ

    const int _numberOfStored = 5;


    private Quaternion GetPlatformRotation()
    {
        return _cameraRotator.CameraIsTurnToX ? Quaternion.AngleAxis(90f, new Vector3(0f, 90f, 0f)) : Quaternion.identity;
    }

    private int GetSumProbabilities()
    {
        int sumProbabilitys = 0;
        foreach (PlatformProbWrap element in _platformsTypes)
        {
            sumProbabilitys += element._probability;
        }
        return sumProbabilitys;
    }

    private GameObject GetRandomPlatformType()//возвращает случайную платформу с учетом вероятности ее появления
    {
        int sumProbabilitys = GetSumProbabilities();
        int randomValue = Random.Range(0, sumProbabilitys);
        foreach (PlatformProbWrap element in _platformsTypes)
        {
            if (element._probability >= randomValue)
            {
                return element._gameObject;
            }
            else
            {
                randomValue -= element._probability;
            }
        }
        return _platformsTypes[0]._gameObject;

    }

    private void SetLastPlatform(GameObject lastPlatform)
    {
        _lastPlatformGameObject = lastPlatform;
        _lastPlatformTransform = lastPlatform.GetComponent<Transform>();
        _lastPlatformsName.Insert(0, _lastPlatformGameObject.name);

        if (_lastPlatformsName.Count > _numberOfStored)
            _lastPlatformsName.RemoveAt(_numberOfStored);
    }

    private GameObject GetPlatformType()
    {
        GameObject newPlatform = GetRandomPlatformType();

        if (newPlatform.name != "Platform")
            foreach (string platform in _lastPlatformsName)
            {
                if(platform == newPlatform.name)
                {
                    newPlatform = _platformsTypes[0]._gameObject;
                } 
            }

        SetLastPlatform(newPlatform);
        return newPlatform;
    }

    private float GetPseudoRandomAxis(float _lastPosition)
    {
        if (_lastPosition < 1) return Random.Range(1, 2.5f);
        if (_lastPosition > -1) return Random.Range(-2.5f, -1);
        float result = Random.Range(_lastPosition - 1.5f, _lastPosition + 1.5f);
        result = result > 2.5f ? 2.5f : result;
        result = result < -2.5f ? -2.5f : result;
        return result;

    }

    private Vector3 GetPlatformPosition()
    {
        bool isXRandom = Random.Range(0, 2) % 2 == 1;//выбор оси, которая будет случайное. true - x, false - z
        float posX = isXRandom ? Random.Range(-2.5f, 2.5f) : GetPseudoRandomAxis(_lastPlatformTransform.position.x);
        float posY = Random.Range(_transform.position.y - 0.5f, _transform.position.y);
        float posZ = !isXRandom ? Random.Range(-2.5f, 2.5f) : GetPseudoRandomAxis(_lastPlatformTransform.position.z);
        return new Vector3(posX, posY, posZ);
    }

    private void CreatePlatform()
    {
        GameObject instPlatformType = GetPlatformType();
        Vector3 instPlatformPosition = GetPlatformPosition();
        Quaternion instPlatformRotation = GetPlatformRotation();

        GameObject newPlatform = Instantiate(instPlatformType, instPlatformPosition, instPlatformRotation);
        _lastPlatformTransform = newPlatform.GetComponent<Transform>();
    }
    private void OnGameStateChanged()
    {
        if (_gameState.State == GameStates.GameIsOn)
        {
            _isActive = true;
            CreatePlatform();
        }
        else
        {
            _isActive = false;
        }
    }

    private void Awake()
    {
        _period = _period == 0 ? 2 : _period;
        _transform = GetComponent<Transform>();
        _lastPlatformTransform = _transform;
        _lastPlatformGameObject = gameObject;
        _gameState.OnGameStateChanged.AddListener(OnGameStateChanged);
        _lastPlatformsName = new List<string>();
        _lastPlatformsName.Capacity = 5;
        OnGameStateChanged();
    }
    private void Update()
    {
        if (!_isActive) return;
        if(_transform.position.y < 8)
        {
            _transform.position = new Vector3(_transform.position.x, _transform.position.y + 1f, _transform.position.z);
        }
        if(_transform.position.y - _lastPlatformTransform.position.y >= _period)
        {
            CreatePlatform();
        }
    }

}