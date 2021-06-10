using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _speed = _speed == 0 ? 10 : _speed;
    }

    private void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.acceleration.x;

        print(direction.x);



        direction *= Time.deltaTime;

        _transform.Translate(direction * _speed);
    }
}
