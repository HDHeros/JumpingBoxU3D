using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private CameraRotator _cameraRotator;


    private void Start()
    {
        _cameraRotator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraRotator>();
       // _camerRotator.OnCameraRotated.AddListener(OnCameraRotated);

        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _speed = _speed == 0 ? 10 : _speed;
    }

    private void Update()
    {
        SetHorizontalSpeed();
    }

    private void SetHorizontalSpeed()
    {
        float direction = _speed;
        bool cameraDirectionIsX = _cameraRotator.CameraTurnToX;

        if (Application.platform == RuntimePlatform.Android)
        {
            direction *= Input.acceleration.x;
        }
        else
        {
            direction *= Input.GetAxis("Horizontal");
        }
        _rigidbody.velocity = new Vector3(cameraDirectionIsX ? 0 : direction, _rigidbody.velocity.y, cameraDirectionIsX ? -direction : 0);

    }
}
