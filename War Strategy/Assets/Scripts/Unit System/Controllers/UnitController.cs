using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [Header("Movement Target Prefab")]
    [SerializeField] Transform _movementTarget;

    [Header("Ray Length")]
    [SerializeField] private float _rayLength = 500f;

    [Header("Button Create Target")]
    [SerializeField] private KeyCode _mouseButton = KeyCode.Mouse1;

    private Camera _camera;
    private UnitSelect _unitSelect;

    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
        _unitSelect = GetComponent<UnitSelect>();
    }

    private void Update()
    {
        Debug.DrawRay(Input.mousePosition, _camera.transform.forward * _rayLength, Color.red);

        if (Input.GetKeyDown(_mouseButton))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = new Ray(mousePosition, _camera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _rayLength))
            {
                Transform movementTarget = Instantiate(_movementTarget, hit.point, Quaternion.identity);
                _unitSelect.GiveUnitTask(movementTarget);
            }
        }
    }
}
