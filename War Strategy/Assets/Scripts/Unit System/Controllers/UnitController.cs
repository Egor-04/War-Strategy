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

    private UnitSelect _unitSelect;

    private void Start()
    {
        _unitSelect = GetComponent<UnitSelect>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_mouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _rayLength))
            {
                Transform movementTarget = Instantiate(_movementTarget, hit.transform.position, Quaternion.identity);
                _unitSelect.GiveUnitTask(movementTarget);
            }
        }
    }
}
