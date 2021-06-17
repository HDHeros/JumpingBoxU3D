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
        float triggerVelocityY = trigger.attachedRigidbody.velocity.y;
        float triggerPositionY = trigger.GetComponent<Transform>().position.y;
        if (triggerVelocityY < 0 && triggerPositionY > _transform.position.y)//если объект движется вниз отключаем триггер
        {
            _boxCollider.isTrigger = false;
        }
    }

    public void OnCollisionExit(Collision collis)
    {
        _boxCollider.isTrigger = true;
    }
}
