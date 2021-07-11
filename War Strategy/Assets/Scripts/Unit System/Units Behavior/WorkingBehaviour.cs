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

    [Header("Target for Fix")]
    [SerializeField] private Transform _fixTarget;
    [SerializeField] private float _fixTime = 2f;
    [SerializeField] private float _fixCount = 1;
    [SerializeField] private float _minFixDistance = 700f;

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

    [SerializeField] private float _currentWorkTime;
    [SerializeField] private float _currentFixTime;
    [SerializeField] private float _currentFixDistance;
    private Unit _currentUnit;
    private UnitMovement _unitMovement;
    private Transform _cachedResourceTarget;

    private void Start()
    {
        _currentFixTime = _fixTime;
        _currentWorkTime = _workTime;

        _currentUnit = GetComponent<Unit>();
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
        FixTargetNow();
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
            NeedFix(target);
            DeliveryResourcesToComandCenter();
        }
        else if (target.GetComponent<ObjectHealth>())
        {
            NeedFix(target);
        }
    }

    private void NeedFix(Transform fixTarget)
    {
        ObjectHealth objectHealth = fixTarget.gameObject.GetComponent<ObjectHealth>();

        if (objectHealth.CurrentObjectHealth < objectHealth.MaxObjectHealth)
        {
            if (objectHealth.CurrentHealthType == HealthType.Mechanic)
            {
                GoFixTarget(fixTarget);
            }
        }
    }

    public void GoFixTarget(Transform fixTarget)
    {
        _fixTarget = fixTarget;
        _unitMovement.SetFixTarget(fixTarget);
    }

    public void FixTargetNow()
    {
        if (!_unitMovement.MovementTarget)
        {
            if (_fixTarget)
            {
                float currentFixDistance = Vector3.SqrMagnitude(_fixTarget.position - transform.position);
                _currentFixDistance = currentFixDistance;

                ObjectHealth currentObjectHealth = _fixTarget.transform.GetComponent<ObjectHealth>();

                if (currentObjectHealth)
                {
                    if (currentFixDistance <= _minFixDistance)
                    {
                        _currentFixTime -= Time.deltaTime;

                        if (currentObjectHealth.CurrentObjectHealth < currentObjectHealth.MaxObjectHealth)
                        {
                            if (_currentFixTime <= 0f) 
                            { 
                                currentObjectHealth.FixObject(_fixCount);
                                _currentFixTime = _fixTime;
                            }
                        }
                        else
                        {
                            FinishedFix();
                        }
                    }
                }
            }
        }
    }

    public void FinishedFix()
    {
        Debug.Log("I finished Fix");
        _fixTarget = null;
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
                        
                        findedResource.GetResource(_toolDamageForce, GetComponent<WorkingBehaviour>());
                            
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
        else
        {
            CheckCollectedResources();
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
        Debug.Log("I finished Work");
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
