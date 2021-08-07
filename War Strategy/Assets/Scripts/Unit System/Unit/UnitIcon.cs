using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class UnitIcon : MonoBehaviour, IPointerDownHandler, IUnitDeselected
{
    [SerializeField] private Image _iconState;
    [SerializeField] private Unit _currentUnit;
    [SerializeField] private Color _color;
    [SerializeField] private float _currentHealth;

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
        _currentHealth = _currentUnit.CurrentUnitHealth;
        _iconState.color = Color.Lerp(Color.red, Color.green, _currentHealth / 100);

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
        _unitSelect.RemoveUnit(_currentUnit);
        Destroy(gameObject);
    }
}
