using UnityEngine.EventSystems;
using UnityEngine;

public class UnitIcon : MonoBehaviour, IPointerDownHandler, IUnitDeselected
{
    private Unit _currentUnit;

    private UnitSelect _unitSelect;

    private void Start()
    {
        _unitSelect = FindObjectOfType<UnitSelect>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Deselect();
    }

    public void SetCurrentUnit(Unit selectedUnit)
    {
        _currentUnit = selectedUnit;
    }

    public void Deselect()
    {
        if (_unitSelect.TeamGroupUnderControll == TeamGroupControll.Blue)
        {
            _unitSelect.RemoveBlueUnit(_currentUnit);
            Destroy(gameObject);
        }
        else
        {
            _unitSelect.RemoveRedUnit(_currentUnit);
            Destroy(gameObject);
        }
    }
}