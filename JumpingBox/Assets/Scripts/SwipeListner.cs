using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SwipeListner : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] Camera gameCamera;

    public void OnBeginDrag(PointerEventData eventData)
    {
        gameCamera.GetComponent<CameraRotator>().OnSwipe(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
