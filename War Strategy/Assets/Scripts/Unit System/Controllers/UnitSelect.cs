using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum TeamGroupControll {Blue, Red}
public class UnitSelect : MonoBehaviour
{
    [Header("Team Under Controll")]
    public TeamGroupControll TeamGroupUnderControll;

    [Header("Selected Groups")]
    [SerializeField] private int _minSelectedUnits = 0;
    [SerializeField] private int _maxSelectedUnits = 5;
    [SerializeField] private List<Unit> _selectedBlueUnits;
    [SerializeField] private List<Unit> _selectedRedUnits;

    [Header("UI")]
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _selectedUnitsUI;

    private bool _noPlace;

    public void GiveUnitTask(Transform movementTarget)
    {
        if (TeamGroupUnderControll == TeamGroupControll.Blue)
        {
            for (int i = 0; i < _selectedBlueUnits.Count; i++)
            {
                _selectedBlueUnits[i].UnitMovement.SetTarget(movementTarget);
            }
        }
        else
        {
            for (int i = 0; i < _selectedRedUnits.Count; i++)
            {
                _selectedRedUnits[i].UnitMovement.SetTarget(movementTarget);
            }
        }
    }

    private void ShowBlueUnitIcon(Unit selectedUnit)
    {
        GameObject cell = Instantiate(_cellPrefab, _selectedUnitsUI);
        cell.transform.GetChild(0).GetComponent<Image>().sprite = selectedUnit.UnitIcon;
        cell.GetComponent<UnitIcon>().SetCurrentUnit(selectedUnit);
        return;
    }

    private void ShowRedUnitIcon(Unit selectedUnit)
    {
        GameObject cell = Instantiate(_cellPrefab, _selectedUnitsUI);
        cell.transform.GetChild(0).GetComponent<Image>().sprite = selectedUnit.UnitIcon;
        cell.GetComponent<UnitIcon>().SetCurrentUnit(selectedUnit);
        return;
    }

    public void AddBlueUnit(Unit selectedUnit)
    {
        if (_selectedBlueUnits.Count >= _minSelectedUnits && _selectedBlueUnits.Count < _maxSelectedUnits)
        {
            _noPlace = false;
        }
        else
        {
            _noPlace = true;
        }

        if (!_noPlace)
        {
            _selectedBlueUnits.Add(new Unit());

            for (int i = 0; i < _selectedBlueUnits.Count; i++)
            {
                if (_selectedBlueUnits[i] == null)
                {
                    if (_selectedBlueUnits[i].UnitID != selectedUnit.UnitID)
                    {
                        _selectedBlueUnits[i] = selectedUnit;
                        _selectedBlueUnits[i].IsSelected = true;
                        ShowBlueUnitIcon(selectedUnit);
                        return;
                    }
                }
            }
        }
    }

    public void AddRedUnit(Unit selectedUnit)
    {
        if (_selectedRedUnits.Count >= _minSelectedUnits && _selectedRedUnits.Count <= _maxSelectedUnits)
        {
            _noPlace = false;
        }
        else
        {
            _noPlace = true;
        }

        if (!_noPlace)
        {
            _selectedRedUnits.Add(new Unit());

            for (int i = 0; i < _selectedRedUnits.Count; i++)
            {
                if (_selectedRedUnits[i] == null)
                {
                    if (_selectedRedUnits[i].UnitID != selectedUnit.UnitID)
                    {
                        _selectedRedUnits[i] = selectedUnit;
                        _selectedRedUnits[i].IsSelected = true;
                        ShowRedUnitIcon(selectedUnit);
                        return;
                    }
                }
            }
        }
    }

    public void RemoveBlueUnit(Unit deselectedUnit)
    {
        for (int i = 0; i < _selectedBlueUnits.Count; i++)
        {
            if (_selectedBlueUnits[i].UnitID == deselectedUnit.UnitID)
            {
                _selectedBlueUnits[i].IsSelected = false;
                _selectedBlueUnits.RemoveAt(i);
                return;
            }
        }
    }

    public void RemoveRedUnit(Unit deselectedUnit)
    {
        for (int i = 0; i < _selectedRedUnits.Count; i++)
        {
            if (_selectedRedUnits[i].UnitID == deselectedUnit.UnitID)
            {
                _selectedRedUnits[i].IsSelected = false;
                _selectedRedUnits.RemoveAt(i);
                return;
            }
        }
    }
}