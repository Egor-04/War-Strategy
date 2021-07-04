using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingBehaviour : MonoBehaviour
{
    [Header("Collected Resources")]
    public float CollectedCrystalsCount;
    public float CollectedGasCount;
    
    [Header("Work Target (Point A)")]
    [SerializeField] private Transform _resourceTarget;

    [Header("Comand Center (Point B)")]
    [SerializeField] private Transform _ñomandCenterDeliveryPoint;
    [SerializeField] private float _minComandCeneterDistance;

    [Header("Find Resource Search Area")]
    [SerializeField] private Transform _searchArea;
    [SerializeField] private float _radius;

    [Header("Tool Damage")]
    [SerializeField] private int _toolDamageForce = 1;

    [Header("Work Distance")]
    [SerializeField] private float _minWorkDistance = 100f;

    [Header("Work Time")]
    [SerializeField] private float _workTime = 8f;

    [Header("Delivery Time")]
    [SerializeField] private float _loadResourcesTime = 5f;

    [Header("Gizmos Color")]
    [SerializeField] private float _red = 0f, _green = 1f, _blue = 0f;
    
    private UnitMovement _unitMovement;
    [SerializeField] private float _currentWorkTime;
    private Transform _cachedResourceTarget;

    private void Start()
    {
        _currentWorkTime = 8f;
        _unitMovement = GetComponent<UnitMovement>();

        // Íàäî äåëàòü òîãäà ïğîâåğêó êàêîé êîìàíäû ğàáî÷èé è òîãäà ïğèñâàèâàòü åìó åãî áàçó
        //_ñomandCenterDeliveryPoint = FindObjectOfType<ComandCenter>().transform;
    }

    private void Update()
    {
        Work();
    }

    public void CollectThisResourceTarget(Transform resourceTarget)
    {
        _resourceTarget = resourceTarget;
        _cachedResourceTarget = resourceTarget;

        _unitMovement.SetResourceTarget(resourceTarget, _minWorkDistance, _toolDamageForce);
        Debug.Log("I Selected Resource Target");
    }

    public void Work()
    {
        if (_resourceTarget)
        {
            Collider[] colliders = Physics.OverlapSphere(_searchArea.position, _radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.gameObject.GetComponent<ResourceSource>())
                {
                    ResourceSource findedResource = colliders[i].transform.gameObject.GetComponent<ResourceSource>();

                    if (findedResource)
                    {
                        Debug.Log("Im Find Resources");
                        _currentWorkTime -= Time.deltaTime;
                        
                        findedResource.GetCrystal(_toolDamageForce, GetComponent<WorkingBehaviour>());
                            
                        if (_currentWorkTime <= 0f)
                        {
                            _currentWorkTime = 0f;
                            FinishedWork();
                            _currentWorkTime = _workTime;
                        }
                    }
                }
            }
        }
    }

    public void FinishedWork()
    {
        _resourceTarget = null;
        _unitMovement.SetComandCenterTarget(_ñomandCenterDeliveryPoint, _minComandCeneterDistance, CollectedCrystalsCount, CollectedGasCount);
    }

    public void ResourcesDelivered()
    {
        CollectedCrystalsCount -= CollectedCrystalsCount;
        CollectedGasCount -= CollectedGasCount;
        _unitMovement.ResourceTarget = _cachedResourceTarget;
        _ñomandCenterDeliveryPoint = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(_red, _green, _blue);
        Gizmos.DrawWireSphere(_searchArea.position, _radius);
    }
}
