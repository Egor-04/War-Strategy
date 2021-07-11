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
            Unit unitTarget = hitInfo.transform.gameObject.GetComponent<Unit>();

            NeedFix(hitInfo.transform);

            UnitBattleTarget(unitTarget);
        }
        else if (hitInfo.transform.gameObject.GetComponent<Building>())
        {
            Building buildingTarget = hitInfo.transform.gameObject.GetComponent<Building>();

            NeedFix(hitInfo.transform);

            BuildingBattleTarget(buildingTarget);

            DeliveryResourceTarget(buildingTarget);
        }
        else if (hitInfo.transform.gameObject.GetComponent<ResourceSource>())
        {
            ResourceSource resourceTarget = hitInfo.transform.gameObject.GetComponent<ResourceSource>();

            ResourceTarget(resourceTarget);
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

    // Targets

    private void UnitBattleTarget(Unit unitTarget)
    {
        if (unitTarget)
        {
            if (unitTarget.CurrentTeamGroup == TeamGroup.Blue)
            {
                for (int i = 0; i < _unitSelect.SelectedRedUnits.Count; i++)
                {
                    CreateBattleTask(unitTarget.transform);
                }
            }
            else if (unitTarget.CurrentTeamGroup == TeamGroup.Red)
            {
                for (int i = 0; i < _unitSelect.SelectedBlueUnits.Count; i++)
                {
                    CreateBattleTask(unitTarget.transform);
                }
            }
        }
    }

    private void BuildingBattleTarget(Building buildingTarget)
    {
        if (buildingTarget.CurrentBuildingTeamGroup == TeamGroup.Blue)
        {
            for (int i = 0; i < _unitSelect.SelectedRedUnits.Count; i++)
            {
                CreateBattleTask(buildingTarget.transform);
            }
        }
        else if (buildingTarget.CurrentBuildingTeamGroup == TeamGroup.Red)
        {
            for (int i = 0; i < _unitSelect.SelectedBlueUnits.Count; i++)
            {
                CreateBattleTask(buildingTarget.transform);
            }
        }
    }
    
    private void ResourceTarget(ResourceSource resourceTarget)
    {
        if (resourceTarget.CurrentResourceType == ResourceType.Crystal)
        {
            if (_unitSelect.TeamGroupUnderControll == TeamGroupControll.Blue)
            {
                for (int i = 0; i < _unitSelect.SelectedBlueUnits.Count; i++)
                {
                    CreateWorkingTask(resourceTarget.transform);
                }
            }
            else
            {
                if (_unitSelect.TeamGroupUnderControll == TeamGroupControll.Red)
                {
                    for (int i = 0; i < _unitSelect.SelectedRedUnits.Count; i++)
                    {
                        CreateWorkingTask(resourceTarget.transform);
                    }
                }
            }
        }
    }

    private void DeliveryResourceTarget(Building buildingTarget)
    {
        if (buildingTarget.GetComponent<ComandCenter>())
        {
            if (buildingTarget.CurrentBuildingTeamGroup == TeamGroup.Blue)
            {
                for (int i = 0; i < _unitSelect.SelectedBlueUnits.Count; i++)
                {
                    CreateDeliveryTask(buildingTarget.transform);
                }
            }
            else if (buildingTarget.CurrentBuildingTeamGroup == TeamGroup.Red)
            {
                for (int i = 0; i < _unitSelect.SelectedRedUnits.Count; i++)
                {
                    CreateDeliveryTask(buildingTarget.transform);
                }
            }
        }
        else
        {
            return;
        }
    }

    private void NeedFix(Transform fixTarget)
    {
        ObjectHealth objectHealth = fixTarget.GetComponent<ObjectHealth>();

        if (objectHealth.CurrentObjectHealth < objectHealth.MaxObjectHealth)
        {
            FixTarget(objectHealth);
        }
    }

    private void FixTarget(ObjectHealth fixTarget)
    {
        if (fixTarget.GetComponent<ObjectHealth>())
        {
            ObjectHealth objectHealth = fixTarget.GetComponent<ObjectHealth>();

            if (objectHealth.CurrentTeamGroup == TeamGroup.Blue)
            {
                for (int i = 0; i < _unitSelect.SelectedBlueUnits.Count; i++)
                {
                    CreateFixTask(fixTarget.transform);
                }
            }
            else if (objectHealth.CurrentTeamGroup == TeamGroup.Red)
            {
                for (int i = 0; i < _unitSelect.SelectedRedUnits.Count; i++)
                {
                    CreateFixTask(fixTarget.transform);
                }
            }
        }
        else
        {
            return;
        }
    }

    // Tasks

    private void CreateFixTask(Transform fixTarget)
    {
        _unitSelect.GiveFixTask(fixTarget);
    }

    private void CreateDeliveryTask(Transform comandCenter)
    {
        _unitSelect.GiveUnitDeliveryTask(comandCenter);
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
