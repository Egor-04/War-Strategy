using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera _camera;

    [Header("Values")]
    [SerializeField] private float _sensitivity = 5f;

    [Header("Confines")]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    [Space(10)]
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;

    [Header("Camera State")]
    [SerializeField] private bool _isLock = false;
        
    private Vector2 _startPosition;
    private float _targetPositionX;
    private float _targetPositionZ;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (!_isLock)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
                _targetPositionX = transform.position.x;
                _targetPositionZ = transform.position.z;
            }
            
            if (Input.GetMouseButton(0))
            {
                float positionX = _camera.ScreenToViewportPoint(Input.mousePosition).x - _startPosition.x;
                float positionZ = _camera.ScreenToViewportPoint(Input.mousePosition).y - _startPosition.y;
                _targetPositionX = Mathf.Clamp(transform.position.x - positionX * _sensitivity, _minX, _maxX);
                _targetPositionZ = Mathf.Clamp(transform.position.z - positionZ * _sensitivity, _minZ, _maxZ);
            }

            float clampPositionX = Mathf.Lerp(transform.position.x, _targetPositionX, _sensitivity * Time.deltaTime);
            float clampPositionZ = Mathf.Lerp(transform.position.z, _targetPositionZ, _sensitivity * Time.deltaTime);
            transform.position = new Vector3(clampPositionX, transform.position.y, clampPositionZ);
        }
    }
}
