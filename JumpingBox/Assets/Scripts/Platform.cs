using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _bounceForce; //сила отскока куба от платформы

    public void OnCollisionEnter2D(Collision2D objectCollission)
    { 
        if (objectCollission.gameObject.GetComponent<MainCube>())
        {
            Rigidbody2D rigidbody = objectCollission.gameObject.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
        }
    }
}
