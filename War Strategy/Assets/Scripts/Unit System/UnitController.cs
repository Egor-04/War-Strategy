using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private List<Unit> _selectedUnits;

    [SerializeField] private UnitMovement _unitMovement;
    
    private void Update()
    {
        
    }

    public void AddUnit(Unit selectedUnit)
    {
        _selectedUnits.Add(new Unit());

        for (int i = 0; i < _selectedUnits.Count; i++)
        {
            if (_selectedUnits[i] == null)
            {
                if (_selectedUnits[i].UnitID != selectedUnit.UnitID)
                {
                    _selectedUnits[i] = selectedUnit;
                    return;
                }
            }
        }
    }

    public void RemoveUnit(int unitID)
    {
        for (int i = 0; i < _selectedUnits.Count; i++)
        {
            if (_selectedUnits[i].UnitID == unitID)
            {
                _selectedUnits.RemoveAt(i);
                return;
            }
        }
    }
}
