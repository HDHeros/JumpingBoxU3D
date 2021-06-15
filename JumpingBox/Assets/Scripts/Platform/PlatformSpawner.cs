using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private List<PlatformProbWrap> _platformsTypes;
    [SerializeField] private float _period;
    [SerializeField] private CameraRotator _cameraRotator;
    [SerializeField] private GameState _gameState;
    [SerializeField] private bool _isActive = false;


    private Transform _transform;
    private Transform _lastPlatformTransform;
    private GameObject _lastPlatformGameObject;


    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _period = _period == 0 ? 2 : _period;
        _lastPlatformTransform = _transform;
        _lastPlatformGameObject = gameObject;
        _gameState.OnGameStateChanged.AddListener(OnGameStateChanged);
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

    private void OnGameStateChanged()
    {
        if(_gameState.State == GameStates.GameIsOn)
        {
            _isActive = true;
            CreatePlatform();

        }
        else
        {
            _isActive = false;
        }
    }

    private void CreatePlatform()
    {
        Vector3 instPlatformPosition = GetPlatformPosition();
        GameObject instPlatform;

        instPlatform = GetPlatformType();

        Quaternion instPlatformRotation = GetPlatformRotation();
        GameObject newPlatform =  Instantiate(instPlatform, instPlatformPosition, instPlatformRotation);
        _lastPlatformTransform = newPlatform.GetComponent<Transform>();
    }

    private Quaternion GetPlatformRotation()
    {
        return _cameraRotator.CameraTurnToX ? Quaternion.AngleAxis(90f, new Vector3(0f, 90f, 0f)) : Quaternion.identity;
    }

    private GameObject GetPlatformType()
    {
        int sumProbabilitys = 0;
        foreach(PlatformProbWrap element in _platformsTypes)
        {
            sumProbabilitys += element._probability;
        }
        int randomValue = Random.Range(0, sumProbabilitys);
        foreach (PlatformProbWrap element in _platformsTypes)
        {
            if(element._probability >= randomValue)
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

    private Vector3 GetPlatformPosition()
    {
        bool isXRandom = Random.Range(0, 2) % 2 == 1;//выбор оси, которая будет случайное. true - x, false - z
        float posX = isXRandom ? Random.Range(-2.5f, 2.5f) : GetPseudoRandomAxis(_lastPlatformTransform.position.x);
        float posY = Random.Range(_transform.position.y - 0.5f, _transform.position.y);
        float posZ = !isXRandom ? Random.Range(-2.5f, 2.5f) : GetPseudoRandomAxis(_lastPlatformTransform.position.z);
        return new Vector3(posX, posY, posZ);
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
}

[System.Serializable]
public class PlatformProbWrap
{
    [SerializeField] public GameObject _gameObject;
    [SerializeField] public int _probability;
}