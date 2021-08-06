using UnityEngine;

public enum TeamGroup {Blue, Red}
public class Unit : MonoBehaviour, IUnitSelected
{
    [Header("Info")]
    public TeamGroup CurrentTeamGroup;
    public int UnitID;
    public Sprite UnitIcon;
    public bool IsSelected;

    [Header("Unit Movement")]
    public UnitMovement UnitMovement;
    
    [Header("Unit Behaviour")]
    public UnitBehaviour UnitBehaviour;

    private int _minID = 1;
    private int _maxID = 1000;

    [HideInInspector]
    public float CurrentUnitHealth; 

    private UnitSelect _unitSelect;
    private ObjectHealth _objectHealth;


    private void Start()
    {
        UnitID = Random.Range(_minID, _maxID);
        UnitMovement = GetComponent<UnitMovement>();
        UnitBehaviour = GetComponent<UnitBehaviour>();
        _unitSelect = FindObjectOfType<UnitSelect>();
        _objectHealth = GetComponent<ObjectHealth>();
    }

    private void OnMouseDown()
    {
        Select();
    }

    public void Select()
    {
        if (!IsSelected)
        {
            if (_unitSelect.TeamGroupUnderControll == TeamGroupControll.Blue)
            {
                if (CurrentTeamGroup == TeamGroup.Blue)
                {
                    _unitSelect.AddBlueUnit(GetComponent<Unit>());
                }
            }
            else
            {
                if (CurrentTeamGroup == TeamGroup.Red)
                {
                    _unitSelect.AddRedUnit(GetComponent<Unit>());
                }
            }
        }
    }

    public void SetCurrentHealthvalue(float currentUnitHealth)
    {
        CurrentUnitHealth = currentUnitHealth;
    }

    public void RemoveUnitIcon()
    {
        if (CurrentUnitHealth <= _objectHealth.MaxObjectHealth)
        {
            if (CurrentTeamGroup == TeamGroup.Blue)
            {
                _unitSelect.RemoveBlueUnit(GetComponent<Unit>());
            }
            else
            {
                _unitSelect.RemoveRedUnit(GetComponent<Unit>());
            }
        }
    }
}
