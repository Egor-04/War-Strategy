using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum TeamGroupControll {Blue, Red}
public class UnitSelect : MonoBehaviour
{
    [Header("Team Under Controll")]
    public TeamGroupControll TeamGroupUnderControll;

    [Header("Selected Groups")]
    public int _minSelectedUnits = 0;
    public int _maxSelectedUnits = 5;
    
    [Header("Units")]
    public List<Unit> SelectedBlueUnits;
    public List<Unit> SelectedRedUnits;


    [Header("Buildings")]
    public List<Building> _selectedBlueBuilds;
    public List<Building> _selectedRedBuilds;

    [Header("UI")]
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _selectedUnitsUI;

    private bool _noPlace;

    // Behaviour Tasks
    public void GiveBattleTask(Transform battleTarget)
    {
        if (TeamGroupUnderControll == TeamGroupControll.Blue)
        {
            for (int i = 0; i < SelectedBlueUnits.Count; i++)
            {
                if (SelectedBlueUnits[i] != null)
                {
                    SelectedBlueUnits[i].UnitBehaviour.CurrentBehaviour(battleTarget);
                }
                else
                {
                    RemoveBlueUnit(SelectedBlueUnits[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < SelectedRedUnits.Count; i++)
            {
                if (SelectedRedUnits[i] != null)
                {
                    SelectedRedUnits[i].UnitBehaviour.CurrentBehaviour(battleTarget);
                }
                else
                {
                    RemoveRedUnit(SelectedRedUnits[i]);
                }
            }
        }
    }

    public void GiveFixTask(Transform fixTarget)
    {
        if (TeamGroupUnderControll == TeamGroupControll.Blue)
        {
            for (int i = 0; i < SelectedBlueUnits.Count; i++)
            {
                if (SelectedBlueUnits[i] != null)
                {
                    SelectedBlueUnits[i].UnitBehaviour.CurrentBehaviour(fixTarget);
                }
            }
        }
        else
        {
            for (int i = 0; i < SelectedRedUnits.Count; i++)
            {
                if (SelectedRedUnits[i] != null)
                {
                    SelectedRedUnits[i].UnitBehaviour.CurrentBehaviour(fixTarget);
                }
            }
        }
    }

    public void GiveWorkTask(Transform workTarget)
    {
        if (TeamGroupUnderControll == TeamGroupControll.Blue)
        {
            for (int i = 0; i < SelectedBlueUnits.Count; i++)
            {
                if (SelectedBlueUnits[i] != null)
                {
                    SelectedBlueUnits[i].UnitBehaviour.CurrentBehaviour(workTarget);
                }
                else
                {
                    RemoveBlueUnit(SelectedBlueUnits[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < SelectedRedUnits.Count; i++)
            {
                if (SelectedRedUnits[i] != null)
                {
                    SelectedRedUnits[i].UnitBehaviour.CurrentBehaviour(workTarget);
                }
                else
                {
                    RemoveRedUnit(SelectedRedUnits[i]);
                }
            }
        }
    }

    public void GiveUnitDeliveryTask(Transform comandCenter)
    {
        if (TeamGroupUnderControll == TeamGroupControll.Blue)
        {
            for (int i = 0; i < SelectedBlueUnits.Count; i++)
            {
                if (SelectedBlueUnits[i] != null)
                {
                    SelectedBlueUnits[i].UnitBehaviour.CurrentBehaviour(comandCenter);
                }
                else
                {
                    RemoveBlueUnit(SelectedBlueUnits[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < SelectedRedUnits.Count; i++)
            {
                if (SelectedRedUnits[i] != null)
                {
                    SelectedRedUnits[i].UnitBehaviour.CurrentBehaviour(comandCenter);
                }
                else
                {
                    RemoveRedUnit(SelectedRedUnits[i]);
                }
            }
        }
    }

    public void GiveUnitMovementTask(Transform movementTarget)
    {
        if (TeamGroupUnderControll == TeamGroupControll.Blue)
        {
            for (int i = 0; i < SelectedBlueUnits.Count; i++)
            {
                SelectedBlueUnits[i].UnitMovement.SetMovementTarget(movementTarget);
            }
        }
        else
        {
            for (int i = 0; i < SelectedRedUnits.Count; i++)
            {
                SelectedRedUnits[i].UnitMovement.SetMovementTarget(movementTarget);
            }
        }
    }

    // Отображение выбранных юнитов
    private void ShowBlueUnitIcon(Unit selectedUnit)
    {
        GameObject cell = Instantiate(_cellPrefab, _selectedUnitsUI);
        cell.transform.GetChild(2).GetComponent<Image>().sprite = selectedUnit.UnitIcon;
        cell.GetComponent<UnitIcon>().SetCurrentUnit(selectedUnit);
        return;
    }

    private void ShowRedUnitIcon(Unit selectedUnit)
    {
        GameObject cell = Instantiate(_cellPrefab, _selectedUnitsUI);
        cell.transform.GetChild(2).GetComponent<Image>().sprite = selectedUnit.UnitIcon;
        cell.GetComponent<UnitIcon>().SetCurrentUnit(selectedUnit);
        return;
    }

    // Система добавления юнитов в список выбранных
    public void AddBlueUnit(Unit selectedUnit)
    {
        if (SelectedBlueUnits.Count >= _minSelectedUnits && SelectedBlueUnits.Count < _maxSelectedUnits)
        {
            _noPlace = false;
        }
        else
        {
            _noPlace = true;
        }

        if (!_noPlace)
        {
            SelectedBlueUnits.Add(new Unit());

            for (int i = 0; i < SelectedBlueUnits.Count; i++)
            {
                if (SelectedBlueUnits[i] == null)
                {
                    if (SelectedBlueUnits[i].UnitID != selectedUnit.UnitID)
                    {
                        SelectedBlueUnits[i] = selectedUnit;
                        SelectedBlueUnits[i].IsSelected = true;
                        ShowBlueUnitIcon(selectedUnit);
                        return;
                    }
                }
            }
        }
    }

    public void AddRedUnit(Unit selectedUnit)
    {
        if (SelectedRedUnits.Count >= _minSelectedUnits && SelectedRedUnits.Count <= _maxSelectedUnits)
        {
            _noPlace = false;
        }
        else
        {
            _noPlace = true;
        }

        if (!_noPlace)
        {
            SelectedRedUnits.Add(new Unit());

            for (int i = 0; i < SelectedRedUnits.Count; i++)
            {
                if (SelectedRedUnits[i] == null)
                {
                    if (SelectedRedUnits[i].UnitID != selectedUnit.UnitID)
                    {
                        SelectedRedUnits[i] = selectedUnit;
                        SelectedRedUnits[i].IsSelected = true;
                        ShowRedUnitIcon(selectedUnit);
                        return;
                    }
                }
            }
        }
    }

    // Система удаления юнитов из списка выбранных
    public void RemoveBlueUnit(Unit deselectedUnit)
    {
        for (int i = 0; i < SelectedBlueUnits.Count; i++)
        {
            if (SelectedBlueUnits[i].UnitID == deselectedUnit.UnitID)
            {
                Debug.Log("I Remove Blue Unit");
                SelectedBlueUnits[i].IsSelected = false;
                SelectedBlueUnits.RemoveAt(i);
                return;
            }
        }
    }

    public void RemoveRedUnit(Unit deselectedUnit)
    {
        for (int i = 0; i < SelectedRedUnits.Count; i++)
        {
            if (SelectedRedUnits[i].UnitID == deselectedUnit.UnitID)
            {
                Debug.Log("I Remove Red Unit");
                SelectedRedUnits[i].IsSelected = false;
                SelectedRedUnits.RemoveAt(i);
                return;
            }
        }
    }
}