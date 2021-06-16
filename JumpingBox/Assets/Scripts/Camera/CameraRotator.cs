using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[System.Serializable]
public class CameraCompletedRotateEvent : UnityEvent<bool> { }

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Vector3 _pivotPoint;
    [SerializeField] private float _rotationSpeed;

    public CameraCompletedRotateEvent CameraCompletedRotate;
    public bool CameraIsTurnToX { get; private set; }
   
    private Transform _transform;
    private float _angleForRotate;
    private float _currentRotateProgress;


    public void OnSwipeScreen(PointerEventData eventData)
    {
        if (Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y)) //если свайп горизонтальный - действуем, веритакальные не интересуют
        {
            _angleForRotate = CameraIsTurnToX ? -90f : 90f;
        }
    }

    private void SubscribeOnSwipes()
    {
        GameObject swipeListenerContainer = GameObject.FindGameObjectWithTag("SwipeListner");
        SwipeListner swipeListnerComponent = swipeListenerContainer.GetComponent<SwipeListner>();
        swipeListnerComponent.SwipeScreen.AddListener(OnSwipeScreen);
    }

    private void Start()
    {
        _rotationSpeed = _rotationSpeed == 0 ? 0.1f : Math.Abs(_rotationSpeed % 1);
        _transform = GetComponent<Transform>();
        SubscribeOnSwipes();
    }

    private void CompleteRotation()
    {
        float remainingAngle = _angleForRotate < 0 ? 0 : Math.Abs(_angleForRotate);//высчитываем угол, на который осталось довернуть
        remainingAngle -= transform.rotation.eulerAngles.y % Math.Abs(_angleForRotate);
        _transform.RotateAround(_pivotPoint, Vector3.up, remainingAngle);//доворачиваем
        _currentRotateProgress = 0;
        _angleForRotate = 0;
        CameraIsTurnToX = !CameraIsTurnToX;
        CameraCompletedRotate.Invoke(CameraIsTurnToX);
    }

    private void RotateCamera()
    {
        _transform.RotateAround(_pivotPoint, Vector3.up, _angleForRotate * _rotationSpeed);
        _currentRotateProgress += _rotationSpeed;

        if (_currentRotateProgress >= 1 - 2 * _rotationSpeed)//если до окончания поворота осталось меньше двух проходов
        {
            CompleteRotation();
        }
    }

    private void Update()
    {
        if (_angleForRotate == 0) return;

        RotateCamera();
    }
}