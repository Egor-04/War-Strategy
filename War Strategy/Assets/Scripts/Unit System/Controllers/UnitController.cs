using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [Header("Movement Target Prefab")]
    [SerializeField] Transform _movementTarget;

    [Header("Tap Count for Movement")]
    [SerializeField] private int _tapCount = 2;

    [Header("Ray Length")]
    [SerializeField] private float _rayLength = 5000f;

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
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(_mouseButton))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, _rayLength))
                {
                    CreateTask(hit);
                }
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetTouch(0).tapCount == _tapCount)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, _rayLength))
                {
                    CreateTask(hit);
                }
            }
        }
    }

    private void CreateTask(RaycastHit hitInfo)
    {
        if (hitInfo.transform.gameObject.GetComponent<Unit>())
        {
            Unit targetUnit = hitInfo.transform.gameObject.GetComponent<Unit>();
            if (targetUnit)
            {
                if (targetUnit.CurrentTeamGroup == TeamGroup.Blue)
                {
                    for (int i = 0; i < _unitSelect.SelectedRedUnits.Count; i++)
                    {
                        CreateBattleTask(targetUnit.transform);
                    }
                }
                else if (targetUnit.CurrentTeamGroup == TeamGroup.Red)
                {
                    for (int i = 0; i < _unitSelect.SelectedBlueUnits.Count; i++)
                    {
                        CreateBattleTask(targetUnit.transform);
                    }
                }
            }
        }
        else if (hitInfo.transform.gameObject.GetComponent<Building>())
        {
            Building targetBuilding = hitInfo.transform.gameObject.GetComponent<Building>();

            if (targetBuilding.CurrentBuildingTeamGroup == TeamGroup.Blue)
            {
                for (int i = 0; i < _unitSelect.SelectedRedUnits.Count; i++)
                {
                    CreateBattleTask(targetBuilding.transform);
                }
            }
            else if (targetBuilding.CurrentBuildingTeamGroup == TeamGroup.Red)
            {
                for (int i = 0; i < _unitSelect.SelectedBlueUnits.Count; i++)
                {
                    CreateBattleTask(targetBuilding.transform);
                }
            }
        }
        else if (hitInfo.transform.gameObject.GetComponent<ResourceSource>())
        {
            ResourceSource targetResource = hitInfo.transform.gameObject.GetComponent<ResourceSource>();

            if (targetResource.CurrentResourceType == ResourceType.Crystal)
            {
                if (_unitSelect.TeamGroupUnderControll == TeamGroupControll.Blue)
                {
                    for (int i = 0; i < _unitSelect.SelectedBlueUnits.Count; i++)
                    {
                        CreateWorkingTask(targetResource.transform);
                    }
                }
                else
                {
                    if (_unitSelect.TeamGroupUnderControll == TeamGroupControll.Red)
                    {
                        for (int i = 0; i < _unitSelect.SelectedRedUnits.Count; i++)
                        {
                            CreateWorkingTask(targetResource.transform);
                        }
                    }
                }
            }
        }
        else if (hitInfo.transform)
        {
            Transform movementTarget = hitInfo.transform;
            
            if (movementTarget)
            {
                CreateMovementTask(hitInfo);
            }
        }
    }

    private void CreateBattleTask(Transform battleTarget)
    {
        _unitSelect.GiveBattleTask(battleTarget);
    }

    private void CreateWorkingTask(Transform resourceTarget)
    {
        _unitSelect.GiveWorkTask(resourceTarget);
    }

    private void CreateMovementTask(RaycastHit hitInfo)
    {
        Transform movementTarget = Instantiate(_movementTarget, hitInfo.point, Quaternion.identity);
        _unitSelect.GiveUnitMovementTask(movementTarget);
    }
}
