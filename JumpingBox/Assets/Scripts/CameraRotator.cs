using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Vector3 _point;
    [SerializeField] private float _rotationSpeed;


    private float _angleForRotate;
    private float _currentRotateProgress;//текущий прогресс поворота
    private Transform _transform;


    private void Start()
    {
        _rotationSpeed = _rotationSpeed == 0 ? 0.5f : Math.Abs(_rotationSpeed % 1);
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if(_angleForRotate != 0)
        {
            _transform.RotateAround(_point, Vector3.up, _angleForRotate * _rotationSpeed);
            _currentRotateProgress += _rotationSpeed;

            if (Math.Round((decimal)_currentRotateProgress, 1) == 1 - (decimal)_rotationSpeed)
            {

                float remainingAngle = _angleForRotate < 0 ? 0 : 90;
                remainingAngle -= transform.rotation.eulerAngles.y % 90;
                _transform.RotateAround(_point, Vector3.up, remainingAngle);
                _currentRotateProgress = 0;
                _angleForRotate = 0;
               
                // ПОСЛЕ ПОВОРОТА ГУЛЯЕТ ПОЗИЦИЯ, ИСПРАВИТЬ
            }
        }

    }

    public void OnSwipe(PointerEventData eventData)
    {
        if(Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y)) //если свайп горизонтальный - действуем, веритакальные не интересуют
        {
            _angleForRotate += eventData.delta.x > 0 ? 90f : -90f;
        }
    }
}