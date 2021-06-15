using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Vector3 _point;//точка, вокруг которой вращается камера
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _isActive = true;//предохраняет от вращений во время вращения
    
    private GameState _gameState;

    public UnityEvent OnCameraRotated;//вызывается, когда камера завершила вращение
    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = _angleForRotate == 0 ? value : _isActive; }
    }
    public float AngleForRotate
    {
        get { return _angleForRotate; }
        set {
            if(IsActive && _gameState.State == GameStates.GameIsOn)
            {
                _angleForRotate = value;
                _isActive = false;
            }
        }
    }
    public bool CameraTurnToX
    {
        get { return _cameraTurnToX; }
    }


    private bool _cameraTurnToX = false;
    private float _angleForRotate;
    private float _currentRotateProgress;//текущий прогресс поворота
    private Transform _transform;


    private void Start()
    {
        _rotationSpeed = _rotationSpeed == 0 ? 0.1f : Math.Abs(_rotationSpeed % 1);
        _transform = GetComponent<Transform>();
        _gameState = GetComponent<GameState>();
    }

    private void FixedUpdate()
    {
        if (_angleForRotate == 0) return;

        _transform.RotateAround(_point, Vector3.up, _angleForRotate * _rotationSpeed);
        _currentRotateProgress += _rotationSpeed;

        if (Math.Round((decimal)_currentRotateProgress, 1) == 1 - (decimal)_rotationSpeed)//если до окончания поворота осталось меньше двух проходов
        {
            float remainingAngle = _angleForRotate < 0 ? 0 : Math.Abs(_angleForRotate);//высчитываем угол, на который осталось довернуть
            remainingAngle -= transform.rotation.eulerAngles.y % Math.Abs(_angleForRotate);
            _transform.RotateAround(_point, Vector3.up, remainingAngle);//доворачиваем
            _currentRotateProgress = 0;//обнуляем прогресс и угол поворота
            _angleForRotate = 0;
            _isActive = true;
            _cameraTurnToX = !_cameraTurnToX;
            OnCameraRotated.Invoke();

                // TODO: ПОСЛЕ ПОВОРОТА ГУЛЯЕТ ПОЗИЦИЯ, ИСПРАВИТЬ
        }

    }

    public void OnSwipe(PointerEventData eventData)
    {
        if(Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y)) //если свайп горизонтальный - действуем, веритакальные не интересуют
        {
            //if (_cameraTurnToX)
            //{
            //    AngleForRotate = eventData.delta.x > 0 ? 0f : -90f;
            //}
            //else
            //{
            //    AngleForRotate = eventData.delta.x > 0 ? 90f : 0f;
            //}
            AngleForRotate = _cameraTurnToX ? -90f : 90f;

        }
    }
}