using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamGroupControll {Blue, Red}
public class UnitController : MonoBehaviour
{
    [Header("Team Under Controll")]
    public TeamGroupControll TeamGroupUnderControll;

    [SerializeField] private List<Unit> _selectedBlueUnits;
    [SerializeField] private List<Unit> _selectedRedUnits;

    [SerializeField] private UnitMovement _unitMovement;
    
    private void Update()
    {
        
    }

    public void AddBlueUnit(Unit selectedUnit)
    {
        _selectedBlueUnits.Add(new Unit());

        for (int i = 0; i < _selectedBlueUnits.Count; i++)
        {
            if (_selectedBlueUnits[i] == null)
            {
                if (_selectedBlueUnits[i].UnitID != selectedUnit.UnitID)
                {
                    _selectedBlueUnits[i] = selectedUnit;
                    return;
                }
            }
        }
    }

    public void AddRedUnit(Unit selectedUnit)
    {
        _selectedRedUnits.Add(new Unit());

        for (int i = 0; i < _selectedRedUnits.Count; i++)
        {
            if (_selectedRedUnits[i] == null)
            {
                if (_selectedRedUnits[i].UnitID != selectedUnit.UnitID)
                {
                    _selectedRedUnits[i] = selectedUnit;
                    return;
                }
            }
        }
    }

    public void RemoveBlueUnit(int unitID)
    {
        for (int i = 0; i < _selectedBlueUnits.Count; i++)
        {
            if (_selectedBlueUnits[i].UnitID == unitID)
            {
                _selectedBlueUnits.RemoveAt(i);
                return;
            }
        }
    }

    public void RemoveRedUnit(int unitID)
    {
        for (int i = 0; i < _selectedRedUnits.Count; i++)
        {
            if (_selectedRedUnits[i].UnitID == unitID)
            {
                _selectedRedUnits.RemoveAt(i);
                return;
            }
        }
    }
}
