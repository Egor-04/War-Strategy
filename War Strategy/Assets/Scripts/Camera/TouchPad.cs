using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class TouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Camera")]
    [SerializeField] private GameObject _camera;

    [Header("Values")]
    [SerializeField] private float _sensitivity = 5f;

    [SerializeField] private Text _textX;
    [SerializeField] private Text _textY;

    private Image _touchPad;
    private bool _isDragged;

    private void Start()
    {
        _touchPad = GetComponent<Image>();
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
            Vector3 touchPosition = Input.mousePosition;
            Vector3 touchDelta = new Vector3(touchPosition.x / _touchPad.rectTransform.sizeDelta.x / -2f, 0f, touchPosition.y / _touchPad.rectTransform.sizeDelta.y / -2f);
            //Vector3 touchPosition = Input.GetTouch(0).position;

            cameraTransform.position += new Vector3(touchDelta.x * _sensitivity, 0f, touchDelta.z * _sensitivity);
            _textX.text = string.Format("{0:X:0}", touchDelta.x);
            _textY.text = string.Format("{0:Y:0}", touchDelta.z);
            Debug.Log("Dragged");
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragged = false;
    }
}
