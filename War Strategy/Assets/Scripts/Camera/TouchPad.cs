using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Camera")]
    [SerializeField] private GameObject _camera;

    [Header("Values")]
    [SerializeField] private float _sensitivity = 5f;

    private bool _isDragged;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragged = true;

        OnDrag(eventData);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragged)
        {
            Transform cameraTransform = _camera.transform;
            Vector3 touchPosition = Input.GetTouch(0).position; 

            cameraTransform.position += new Vector3(touchPosition.x * _sensitivity * Time.deltaTime, 0f, touchPosition.z * _sensitivity * Time.deltaTime);
            Debug.Log("Dragged");
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragged = false;
    }
}
