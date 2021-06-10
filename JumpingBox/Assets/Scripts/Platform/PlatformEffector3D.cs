using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffector3D : MonoBehaviour
{
    [SerializeField] private BoxCollider _platformCollider;

    private BoxCollider _effectorCollider;

    private void Start()
    {
        _effectorCollider = GetComponent<BoxCollider>();
        _platformCollider.GetComponent<ColliderRotator>().OnColliderSizeChanged.AddListener(OnPlatformColliderChanged);
        _platformCollider.isTrigger = true;
    }

    private void OnDestroy()
    {
        _platformCollider.GetComponent<ColliderRotator>().OnColliderSizeChanged.RemoveListener(OnPlatformColliderChanged);
    }

    private void OnTriggerEnter(Collider triggerCollider)
    { 
        if(triggerCollider.GetComponent<MainCube>())
        {
            _platformCollider.isTrigger = false;
        }
    } 

    private void OnTriggerExit(Collider triggerCollider)
    {
        if (triggerCollider.GetComponent<MainCube>())
        {
            StartCoroutine(MakePlatformTrigger());
        }
    }

    private void OnPlatformColliderChanged()
    {
        _effectorCollider.size = _platformCollider.size;
        _effectorCollider.isTrigger = true;
    }

    IEnumerator MakePlatformTrigger()
    {
        yield return new WaitForSeconds(1f);
        _platformCollider.isTrigger = true;
    }
}
