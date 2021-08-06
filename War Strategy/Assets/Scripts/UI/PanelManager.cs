using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager PanelManagerStatic;

    public bool IsActivePanel; 
    [SerializeField] private GameObject[] _panels;

    private void Start()
    {
        PanelManagerStatic = this;
    }

    public void DisableAllPanels()
    {
        if (IsActivePanel)
        {
            for (int i = 0; i < _panels.Length; i++)
            {
                if (_panels[i].activeSelf)
                {
                    IsActivePanel = false;
                    _panels[i].SetActive(false);
                }
            }
        }
    }
}
