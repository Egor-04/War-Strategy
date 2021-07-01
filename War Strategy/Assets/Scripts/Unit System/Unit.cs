using UnityEngine;

public enum TeamGroup {Blue, Red}
public class Unit : MonoBehaviour, IUnitSelected
{
    [Header("Info")]
    public TeamGroup CurrentTeamGroup; 
    public int UnitID;
    public bool IsSelected;

    [Header("Unit Movement")]
    public UnitMovement _unitMovement;

    private void Start()
    {
        _unitMovement = GetComponent<UnitMovement>();
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
        }
        else
        {
            IsSelected = false;
        }
    }
}
