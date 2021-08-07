using UnityEngine;

public class Building : MonoBehaviour
{
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
