using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class UnitIcon : MonoBehaviour, IPointerDownHandler, IUnitDeselected
{
    [SerializeField] private Image _iconState;
    [SerializeField] private Unit _currentUnit;
    [SerializeField] private Color _color;
    [SerializeField] private float _h = 0,
                                   _s = 2,
                                   _v = 1;

    private UnitSelect _unitSelect;

    private void Start()
    {
        _iconState = transform.GetChild(2).GetComponent<Image>();
        _unitSelect = FindObjectOfType<UnitSelect>();
    }

    private void Update()
    {
        CheckUnit();
    }

    private void CheckUnit()
    {
        _h = _currentUnit.CurrentUnitHealth;
        _iconState.color = _color;

        if (_currentUnit.CurrentUnitHealth <= 0f)
        {
            Destroy(gameObject);
        }
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
