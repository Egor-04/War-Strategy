using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingBehaviour : MonoBehaviour
{
    [Header("Collected Resources")]
    public float CollectedCrystalsCount;
    public float CollectedGasCount;

    [Header("Resources View")]
    [SerializeField] private GameObject _crystals;
    [SerializeField] private GameObject _gasBarrel;

    [Header("Work Target (Point A)")]
    [SerializeField] private Transform _resourceTarget;

    [Header("Comand Center (Point B)")]
    [SerializeField] private Transform _ÒomandCenterDeliveryPoint;
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

    [Header("Gizmos Color")]
    [SerializeField] private float _red = 0f, _green = 1f, _blue = 0f;

    private Unit _currentUnit;
    private UnitMovement _unitMovement;
    [SerializeField] private float _currentWorkTime;
    private Transform _cachedResourceTarget;

    private void Start()
    {
        _currentUnit = GetComponent<Unit>();
        _currentWorkTime = 8f;
        _unitMovement = GetComponent<UnitMovement>();

        // Õ‡‰Ó ‰ÂÎ‡Ú¸ ÚÓ„‰‡ ÔÓ‚ÂÍÛ Í‡ÍÓÈ ÍÓÏ‡Ì‰˚ ‡·Ó˜ËÈ Ë ÚÓ„‰‡ ÔËÒ‚‡Ë‚‡Ú¸ ÂÏÛ Â„Ó ·‡ÁÛ
        if (_currentUnit.CurrentTeamGroup == TeamGroup.Blue)
        {
            ComandCenter findedComandCenter = FindObjectOfType<ComandCenter>();
            
            if (findedComandCenter.CurrentTeamGroup == TeamGroup.Blue)
            {
                _ÒomandCenterDeliveryPoint = findedComandCenter.transform;
            }
        }
        else if (_currentUnit.CurrentTeamGroup == TeamGroup.Red)
        {
            ComandCenter findedComandCenter = FindObjectOfType<ComandCenter>();

            if (findedComandCenter.CurrentTeamGroup == TeamGroup.Red)
            {
                _ÒomandCenterDeliveryPoint = findedComandCenter.transform;
            }
        }
    }

    private void Update()
    {
        Work();
        CheckHaveResources();
    }

    public void DefineTypeTarget(Transform target)
    {
        if (target.gameObject.GetComponent<ResourceSource>())
        {
            CollectThisResourceTarget(target);
        }
        else if (target.gameObject.GetComponent<ComandCenter>())
        {
            DeliveryResourcesToComandCenter();
        }
    }

    public void DeliveryResourcesToComandCenter()
    {
        FinishedWork();
    }

    private void CollectThisResourceTarget(Transform resourceTarget)
    {
        if (resourceTarget.gameObject.GetComponent<ResourceSource>())
        {
            Debug.Log("I Selected Resource Target ===> " + resourceTarget);
            _resourceTarget = resourceTarget;
            _cachedResourceTarget = resourceTarget;
            _unitMovement.SetResourceTarget(resourceTarget, _minWorkDistance, _toolDamageForce);
        }
    }

    public void CheckHaveResources()
    {
        if (CollectedCrystalsCount > 0f)
        {
            _crystals.SetActive(true);
        }
        else
        {
            _crystals.SetActive(false);
        }

        if (CollectedGasCount > 0f)
        {
            _gasBarrel.SetActive(true);
        }
        else
        {
            _gasBarrel.SetActive(false);
        }
    }

    public void Work()
    {
        if (_resourceTarget)
        {
            if (_resourceTarget.gameObject.GetComponent<ResourceSource>())
            {
                Collider[] colliders = Physics.OverlapSphere(_searchArea.position, _radius);

                for (int i = 0; i < colliders.Length; i++)
                {
                    ResourceSource findedResource = colliders[i].transform.gameObject.GetComponent<ResourceSource>();

                    if (findedResource)
                    {
                        Debug.Log(_resourceTarget);
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
            else
            {
                CheckCollectedResources();
            }
        }
    }

    public void CheckCollectedResources()
    {
        if (CollectedCrystalsCount > 0f || CollectedGasCount > 0f)
        {
            FinishedWork();
        }
    }

    public void FinishedWork()
    {
        _resourceTarget = null;
        _unitMovement.SetComandCenterTarget(_ÒomandCenterDeliveryPoint, _minComandCeneterDistance, CollectedCrystalsCount, CollectedGasCount);
    }

    public void ResourcesDelivered()
    {
        Debug.Log("I Delivered Resources. I go to ==> " + _cachedResourceTarget);
        CollectedCrystalsCount -= CollectedCrystalsCount;
        CollectedGasCount -= CollectedGasCount;
        _unitMovement.MovementTarget = _cachedResourceTarget;
        _resourceTarget = _cachedResourceTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(_red, _green, _blue);
        Gizmos.DrawWireSphere(_searchArea.position, _radius);
    }
}
