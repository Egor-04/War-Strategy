using UnityEngine;

public enum TeamGroup { Blue, Red }
public class Unit : MonoBehaviour, IUnitSelected
{
    [Header("Info")]
    public TeamGroup CurrentTeamGroup;
    public int UnitID;
    public bool IsSelected;

    [Header("Unit Movement")]
    public UnitController _unitController;

    private void Start()
    {
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
            IsSelected = true;
            
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
        else
        {
            IsSelected = false;

            if (_unitController.TeamGroupUnderControll == TeamGroupControll.Blue)
            {
                if (CurrentTeamGroup == TeamGroup.Blue)
                {
                    _unitController.RemoveBlueUnit(UnitID);
                }
            }
            else
            {
                if (CurrentTeamGroup == TeamGroup.Red)
                {
                    _unitController.RemoveRedUnit(UnitID);
                }
            }
        }
    }
}
