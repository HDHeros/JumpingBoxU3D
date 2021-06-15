using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryAxisCorrector : MonoBehaviour
{
    [SerializeField] private CameraRotator _cameraRotator;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Platform") return;
        if(_cameraRotator.CameraTurnToX)
        {
            _transform.position = new Vector3(collision.transform.position.x, _transform.position.y, _transform.position.z);
        }
        else
        {
            _transform.position = new Vector3(_transform.position.x, _transform.position.y, collision.transform.position.z);
        }

    }
}
