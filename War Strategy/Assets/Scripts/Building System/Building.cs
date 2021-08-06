using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Building Type")]
    public TeamGroup CurrentBuildingTeamGroup;
    
    [Header("ID")]
    public int BuildingID;

    [Header("Tool Panel")]
    [SerializeField] private GameObject _panelMenu;

    private UnitSelect _unitSelect;
    private int _minID = 1;
    private int _maxID = 1000;

    private void Start()
    {
        BuildingID = Random.Range(_minID, _maxID);
        _unitSelect = FindObjectOfType<UnitSelect>();
    }
    
    private void OnMouseDown()
    {
        Select();
    }

    private void Select()
    {
        if (_unitSelect.TeamGroupUnderControll == TeamGroupControll.Blue)
        {
            if (CurrentBuildingTeamGroup == TeamGroup.Blue)
            {

                if (!PanelManager.PanelManagerStatic.IsActivePanel)
                {
                    _panelMenu.SetActive(true);
                    PanelManager.PanelManagerStatic.IsActivePanel = true;
                }
                else
                {
                    PanelManager.PanelManagerStatic.DisableAllPanels();
                }
            }
        }
        else
        {
            if (CurrentBuildingTeamGroup == TeamGroup.Red)
            {
                PanelManager.PanelManagerStatic.DisableAllPanels();

                if (!PanelManager.PanelManagerStatic.IsActivePanel)
                {
                    _panelMenu.SetActive(true);
                    PanelManager.PanelManagerStatic.IsActivePanel = true;
                }
                else
                {
                    PanelManager.PanelManagerStatic.DisableAllPanels();
                }
            }
        }
    }
}
