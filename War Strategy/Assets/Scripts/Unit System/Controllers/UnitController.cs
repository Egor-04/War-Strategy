using UnityEngine;

public class UnitController : MonoBehaviour
{
    [Header("Tap Count for Movement")]
    [SerializeField] private int _tapCount = 2;

    [Header("Ray Length")]
    [SerializeField] private float _rayLength = 5000f;

    [Header("Button Create Target")]
    [SerializeField] private KeyCode _mouseButton = KeyCode.Mouse1;

    private Camera _camera;
    private UnitSelect _unitSelect;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Camera>();
        _unitSelect = GetComponent<UnitSelect>();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(_mouseButton))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, _rayLength))
                {
                    CreateTask(hit);
                }
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetTouch(0).tapCount == _tapCount)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, _rayLength))
                {
                    CreateTask(hit);
                }
            }
        }
    }

    private void CreateTask(RaycastHit hitInfo)
    {
        _unitSelect.DefineTaskType(hitInfo);
    }
}