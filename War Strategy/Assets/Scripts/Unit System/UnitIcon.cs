using UnityEngine.EventSystems;
using UnityEngine;

public class UnitIcon : MonoBehaviour, IPointerDownHandler, IUnitDeselected
{
    private Unit _currentUnit;

    private UnitController _unitController;

    private void Start()
    {
        _unitController = FindObjectOfType<UnitController>();
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
        if (_unitController.TeamGroupUnderControll == TeamGroupControll.Blue)
        {
            _unitController.RemoveBlueUnit(_currentUnit);
            Destroy(gameObject);
        }
        else
        {
            _unitController.RemoveRedUnit(_currentUnit);
            Destroy(gameObject);
        }
    }
}
