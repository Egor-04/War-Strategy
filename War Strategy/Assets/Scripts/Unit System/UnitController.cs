using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum TeamGroupControll {Blue, Red}
public class UnitController : MonoBehaviour
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

    private void ShowBlueUnitIcon(Unit selectedUnit)
    {
        for (int i = 0; i < _selectedBlueUnits.Count; i++)
        {
            Transform cell = Instantiate(_cellPrefab, _selectedUnitsUI).transform;
            cell.GetChild(0).GetComponent<Image>().sprite = selectedUnit.UnitIcon;
            return;
        }
    }

    private void ShowRedUnitIcon(Unit selectedUnit)
    {
        for (int i = 0; i < _selectedRedUnits.Count; i++)
        {
            Transform cell = Instantiate(_cellPrefab, _selectedUnitsUI).transform;
            cell.GetChild(0).GetComponent<Image>().sprite = selectedUnit.UnitIcon;
            return;
        }
    }

    private void RemoveBlueUnitIcon(Unit deselected)
    {
        for (int i = 0; i < _selectedBlueUnits.Count; i++)
        {
            if (!deselected.IsSelected)
            {
                Destroy(_selectedUnitsUI.GetChild(i));
                return;
            }
        }
    }
    private void RemoveRedUnitIcon(Unit deselected)
    {
        for (int i = 0; i < _selectedRedUnits.Count; i++)
        {
            if (!deselected.IsSelected)
            {
                Destroy(_selectedUnitsUI.GetChild(i));
                return;
            }
        }
    }

    public void AddBlueUnit(Unit selectedUnit)
    {
        if (_selectedBlueUnits.Count >= _minSelectedUnits && _selectedBlueUnits.Count <= _maxSelectedUnits)
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
        if (_selectedBlueUnits.Count >= _minSelectedUnits && _selectedBlueUnits.Count <= _maxSelectedUnits)
        {
            _selectedRedUnits.Add(new Unit());

            for (int i = 0; i < _selectedRedUnits.Count; i++)
            {
                if (_selectedRedUnits[i] == null)
                {
                    if (_selectedRedUnits[i].UnitID != selectedUnit.UnitID)
                    {
                        _selectedBlueUnits[i].IsSelected = true;
                        _selectedRedUnits[i] = selectedUnit;
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
                RemoveBlueUnitIcon(_selectedBlueUnits[i]);
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
                _selectedBlueUnits[i].IsSelected = false;
                RemoveRedUnitIcon(_selectedBlueUnits[i]);
                _selectedRedUnits.RemoveAt(i);
                return;
            }
        }
    }
}