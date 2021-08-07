using UnityEngine;

public class Unit : MonoBehaviour, IUnitSelected
{
    [Header("Info")]
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
    private ObjectHealth _objectHealth;
    private UnitBehaviour _unitBehaviour; 

    private void Start()
    {
        UnitID = Random.Range(_minID, _maxID);
        UnitMovement = GetComponent<UnitMovement>();
        UnitBehaviour = GetComponent<UnitBehaviour>();
        _objectHealth = GetComponent<ObjectHealth>();
        _unitBehaviour = GetComponent<UnitBehaviour>();
    }

    private void OnMouseDown()
    {
        Select();
    }

    public void Select()
    {
        if (!IsSelected)
        {
            UnitSelect.StaticUnitSelect.AddUnit(GetComponent<Unit>());
        }
    }

    public void SetCurrentHealthValue(float currentUnitHealth)
    {
        CurrentUnitHealth = currentUnitHealth;
    }

    public void RemoveUnitIcon()
    {
        if (CurrentUnitHealth <= _objectHealth.MaxObjectHealth)
        {
            UnitSelect.StaticUnitSelect.RemoveUnit(GetComponent<Unit>());
        }
    }

    public void DefineBehaviourType(ObjectTarget objectTarget)
    {
        _unitBehaviour.CurrentBehaviour(objectTarget);
    }
}
