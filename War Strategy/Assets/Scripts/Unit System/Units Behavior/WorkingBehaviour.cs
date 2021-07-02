using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingBehaviour : MonoBehaviour
{
    [Header("Collected Resources")]
    public float CollectedResourcesCount;
    
    [Header("Work Target (Point A)")]
    [SerializeField] private Transform _workTarget;

    [Header("Comand Center (Point B)")]
    [SerializeField] private Transform _comandCenter;

    [Header("Find Resource Search Area")]
    [SerializeField] private Transform _searchArea;
    [SerializeField] private float _radius;

    [Header("Tool Damage")]
    [SerializeField] private int _toolDamageForce = 1;

    [Header("Work Distance")]
    [SerializeField] private float _minWorkDistance = 100f;

    [Header("Work Time")]
    [SerializeField] private float _workTime = 8f;

    [Header("Gizmos Color")]
    [SerializeField] private float _red = 0f, _green = 1f, _blue = 0f;
    
    private UnitMovement _unitMovement;
    private float _currentworkTime;
    private bool _maxResourcesCollected;

    private void Start()
    {
        _unitMovement = GetComponent<UnitMovement>();
        _comandCenter = FindObjectOfType<ComandCenter>().transform;
    }

    private void Update()
    {
        _currentworkTime -= Time.deltaTime;
    }

    public void CollectThisResourceTarget(Transform workTarget)
    {
        _workTarget = workTarget;

        if (_workTarget)
        {
            _unitMovement.SetResourceTarget(workTarget, _minWorkDistance, _toolDamageForce);  
        }
    }

    public void Work()
    {
        Collider[] colliders = Physics.OverlapSphere(_searchArea.position, _radius);

        if (_currentworkTime <= 0f)
        {
            _currentworkTime = 0f;

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.gameObject.GetComponent<ResourceSource>())
                {
                    ResourceSource takedResource = colliders[i].transform.gameObject.GetComponent<ResourceSource>();

                    if (takedResource)
                    {
                        takedResource.GetCrystal(_toolDamageForce, this);
                        _currentworkTime = _workTime;
                    }
                }
            }
        }
    }

    public void MoveToComandCenter()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(_red, _green, _blue);
        Gizmos.DrawWireSphere(_searchArea.position, _radius);
    }
}
