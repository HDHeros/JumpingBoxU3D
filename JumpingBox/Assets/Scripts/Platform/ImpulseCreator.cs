using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseCreator : MonoBehaviour
{
    [SerializeField] private float _bounceForce; //сила отскока куба от платформы

    public void OnCollisionEnter(Collision objectCollission)
    {
        if (objectCollission.gameObject.GetComponent<MainCube>())
        {

            Rigidbody rigidbody = objectCollission.gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(Vector2.up * _bounceForce, ForceMode.Impulse);
        }
    }
}
