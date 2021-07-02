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

    private UnitSelect _unitSelect;

    private void Start()
    {
        UnitID = Random.Range(_minID, _maxID);
        UnitMovement = GetComponent<UnitMovement>();
        UnitBehaviour = GetComponent<UnitBehaviour>();
        _unitSelect = FindObjectOfType<UnitSelect>();
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
}
