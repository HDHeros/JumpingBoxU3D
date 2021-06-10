using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Компонент изменяет Z и X  коллайдеров в зависимости от поворота камеры
/// </summary>
public class ColliderRotator : MonoBehaviour
{
    public UnityEvent OnColliderSizeChanged;

    private GameObject _camera;
    private BoxCollider _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _camera.GetComponent<CameraRotator>().OnCameraRotated.AddListener(OnCameraRotated);
        ChangePlatformCollider();
    }
    
    private void OnDestroy()
    {
        try
        {
            _camera.GetComponent<CameraRotator>().OnCameraRotated.RemoveListener(OnCameraRotated);
        }
        catch
        {

        }
    }

    public void OnCameraRotated()
    {
        ChangePlatformCollider();
    }

    private void ChangePlatformCollider()
    {
        if (_camera.GetComponent<CameraRotator>().CameraTurnToX)
        {
            _collider.size = new Vector3(10f, 1f, 1f);
        }
        else
        {
            _collider.size = new Vector3(1f, 1f, 10f);
        }
        OnColliderSizeChanged.Invoke();
    }
}
