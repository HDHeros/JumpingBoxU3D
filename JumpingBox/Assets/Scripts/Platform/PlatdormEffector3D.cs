using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatdormEffector3D : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private Transform _transform;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _transform = GetComponent<Transform>();
        _boxCollider.isTrigger = true;
    }
    public void OnTriggerEnter(Collider trigger)
    {
        if (trigger.attachedRigidbody.velocity.y < 0 && trigger.GetComponent<Transform>().position.y > _transform.position.y + 0.19)//если объект движется вниз отключаем триггер
            _boxCollider.isTrigger = false;
    }

    public void OnCollisionExit(Collision collis)
    {
        _boxCollider.isTrigger = true;
    }
}
