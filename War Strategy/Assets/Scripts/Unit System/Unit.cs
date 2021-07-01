using UnityEngine;

public enum TeamGroup { Blue, Red }
public class Unit : MonoBehaviour, IUnitSelected
{
    [Header("Info")]
    public TeamGroup CurrentTeamGroup;
    public int UnitID;
    public Sprite UnitIcon;
    public bool IsSelected;

    [Header("Unit Movement")]
    public UnitMovement UnitMovement;

    private UnitController _unitController;
    private int _minID = 1;
    private int _maxID = 1000;

    private void Start()
    {
        UnitID = Random.Range(_minID, _maxID);
        UnitMovement = GetComponent<UnitMovement>();
        _unitController = FindObjectOfType<UnitController>();
    }

    private void OnMouseDown()
    {
        Select();
    }

    public void Select()
    {
        if (!IsSelected)
        {
            if (_unitController.TeamGroupUnderControll == TeamGroupControll.Blue)
            {
                if (CurrentTeamGroup == TeamGroup.Blue)
                {
                    _unitController.AddBlueUnit(GetComponent<Unit>());
                }
            }
            else
            {
                if (CurrentTeamGroup == TeamGroup.Red)
                {
                    _unitController.AddRedUnit(GetComponent<Unit>());
                }
            }
        }
    }
}
