using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UnitSelect : MonoBehaviour
{
    public static UnitSelect StaticUnitSelect;

    [Header("Selected Groups")]
    public int _minSelectedUnits = 0;
    public int _maxSelectedUnits = 5;

    [Header("Movement Target Prefab")]
    [SerializeField] Transform _movementTarget;

    [Header("Units")]
    public List<Unit> SelectedUnits;

    [Header("Buildings")]
    public List<Building> SelectedBuilds;

    [Header("UI")]
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _selectedUnitsUI;

    private bool _noPlace;

    private void Start()
    {
        StaticUnitSelect = this;
    }

    public void DefineTaskType(RaycastHit hitinfo)
    {
        if (hitinfo.collider)
        {
            CreateMovementTask(hitinfo);
        }

        if (hitinfo.collider.gameObject.GetComponent<ObjectTarget>())
        {
            ObjectTarget objectTarget = hitinfo.transform.gameObject.GetComponent<ObjectTarget>();

            if (objectTarget.CurrentObjectType == ObjectType.Player)
            {
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].DefineBehaviourType(objectTarget);
                }
            }
            else if (objectTarget.CurrentObjectType == ObjectType.Enemy)
            {
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].DefineBehaviourType(objectTarget);
                }
            }
            else if (objectTarget.CurrentObjectType == ObjectType.Resources)
            {
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].DefineBehaviourType(objectTarget);
                }
            }
        }
    }

    // Task Creator
    public void CreateMovementTask(RaycastHit hitInfo)
    {
        Transform movementTarget = Instantiate(_movementTarget, hitInfo.point, Quaternion.identity);
        GiveUnitMovementTask(movementTarget);
    }

    // Giving Behaviour Tasks
    public void GiveUnitMovementTask(Transform movementTarget)
    {
        for (int i = 0; i < SelectedUnits.Count; i++)
        {
            SelectedUnits[i].UnitMovement.SetMovementTarget(movementTarget);
        }
    }

    public void GiveFixTask(ObjectTarget fixTarget)
    {
        for (int i = 0; i < SelectedUnits.Count; i++)
        {
            if (SelectedUnits[i] != null)
            {
                SelectedUnits[i].UnitBehaviour.CurrentBehaviour(fixTarget);
            }
        }
    }

    public void GiveWorkTask(ObjectTarget workTarget)
    {
        for (int i = 0; i < SelectedUnits.Count; i++)
        {
            if (SelectedUnits[i] != null)
            {
                SelectedUnits[i].UnitBehaviour.CurrentBehaviour(workTarget);
            }
            else
            {
                RemoveUnit(SelectedUnits[i]);
            }
        }
    }

    public void GiveUnitDeliveryTask(ObjectTarget comandCenter)
    {
        for (int i = 0; i < SelectedUnits.Count; i++)
        {
            if (SelectedUnits[i] != null)
            {
                SelectedUnits[i].UnitBehaviour.CurrentBehaviour(comandCenter);
            }
            else
            {
                RemoveUnit(SelectedUnits[i]);
            }
        }
    }


    // Отображение выбранных юнитов
    private void ShowUnitIcon(Unit selectedUnit)
    {
        GameObject cell = Instantiate(_cellPrefab, _selectedUnitsUI);
        cell.transform.GetChild(2).GetComponent<Image>().sprite = selectedUnit.UnitIcon;
        cell.GetComponent<UnitIcon>().SetCurrentUnit(selectedUnit);
        return;
    }

    // Система добавления юнитов в список выбранных
    public void AddUnit(Unit selectedUnit)
    {
        if (SelectedUnits.Count >= _minSelectedUnits && SelectedUnits.Count < _maxSelectedUnits)
        {
            _noPlace = false;
        }
        else
        {
            _noPlace = true;
        }

        if (!_noPlace)
        {
            SelectedUnits.Add(new Unit());

            for (int i = 0; i < SelectedUnits.Count; i++)
            {
                if (SelectedUnits[i] == null)
                {
                    if (SelectedUnits[i].UnitID != selectedUnit.UnitID)
                    {
                        SelectedUnits[i] = selectedUnit;
                        SelectedUnits[i].IsSelected = true;
                        ShowUnitIcon(selectedUnit);
                        return;
                    }
                }
            }
        }
    }

    // Система удаления юнитов из списка выбранных
    public void RemoveUnit(Unit deselectedUnit)
    {
        for (int i = 0; i < SelectedUnits.Count; i++)
        {
            if (SelectedUnits[i].UnitID == deselectedUnit.UnitID)
            {
                Debug.Log("I Remove Unit");
                SelectedUnits[i].IsSelected = false;
                SelectedUnits.RemoveAt(i);
                return;
            }
        }
    }
}