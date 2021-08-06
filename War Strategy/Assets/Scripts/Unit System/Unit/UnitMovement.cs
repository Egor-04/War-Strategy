using UnityEngine.AI;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    [Header("Nav Mesh Agent")]
    [SerializeField] private NavMeshAgent _navMeshAgent;

    [Header("Movement Target")]
    public Transform MovementTarget;
    [SerializeField] private float _minMovementTargetDistance = 100f;

    [Tooltip("Извлеченная информация из AttackBehaviour")]
    [Header("Battle Target")]
    [SerializeField] private float _minAttackDistance;
    [SerializeField] private float _damageForce;

    [Header("Resource Target")]
    [SerializeField] private float _minWorkDistance;
    [SerializeField] private int _toolDamageForce;

    [Header("Comand Center")]
    [SerializeField] private float _minComandCenterDistance;

    [Header("Fix Target")]
    [SerializeField] private float _minFixDistance;

    [Header("Collected Resources")]
    [SerializeField] private float _collectedCrystalsCount;
    [SerializeField] private float _collectedGasCount;

    private AttackBehaviour _attackBehavour;
    private WorkingBehaviour _workingBehaviour;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _attackBehavour = GetComponent<AttackBehaviour>();
        _workingBehaviour = GetComponent<WorkingBehaviour>();
    }

    private void Update()
    {
        MoveToTarget();
        MoveToBattleTarget();
        MoveToResourceTarget();
        MoveToComandCenter();
        MoveToFixTarget();
    }

    // Нужно попробовать дать юнитам одну цель которая будет меняться взависимости от выбранной цели
    public void SetMovementTarget(Transform movementTarget)
    {
        MovementTarget = movementTarget;
    }

    public void SetBattleTarget(Transform battleTarget, float minAttackDistance, float damageForce)
    {
        Debug.Log("I Set battle Target");
        MovementTarget = battleTarget;
        _minAttackDistance = minAttackDistance;
        _damageForce = damageForce;
    }

    public void SetResourceTarget(Transform resourceTarget, float minWorkDistance, int toolDamageForce) // CollectThisResourceTarget ===> MoveToResourceTarget ===> Work ===> GetCrystal
    {
        MovementTarget = resourceTarget;
        Debug.Log("I Set Resource Target ===> " + resourceTarget);
        _minWorkDistance = minWorkDistance;
        _toolDamageForce = toolDamageForce;
    }

    public void SetComandCenterTarget(Transform comandCenterDeliveryPoint, float minComandCenterDistance, float collectedCrystalsCount, float collectedGasCount)
    {
        MovementTarget = comandCenterDeliveryPoint;
        _minComandCenterDistance = minComandCenterDistance;
        _collectedCrystalsCount = collectedCrystalsCount;
        _collectedGasCount = collectedGasCount;
    }

    public void SetFixTarget(Transform fixTarget)
    {
        MovementTarget = fixTarget;
    }

    private void MoveToResourceTarget()
    {
        if (MovementTarget)
        {
            if (MovementTarget.GetComponent<ResourceSource>())
            {
                float currentResourceTargetDistance = Vector3.SqrMagnitude(MovementTarget.position - transform.position);
                _distance = currentResourceTargetDistance;

                if (currentResourceTargetDistance > _minWorkDistance)
                {
                    _navMeshAgent.enabled = true;
                    transform.LookAt(MovementTarget);
                    _navMeshAgent.SetDestination(MovementTarget.position);
                    transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
                }
                else if (currentResourceTargetDistance <= _minWorkDistance)
                {
                    _navMeshAgent.enabled = false;
                    transform.LookAt(MovementTarget);
                    transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
                }
            }
        }
    }

    private void MoveToComandCenter()
    {
        if (MovementTarget)
        {
            if (MovementTarget.GetComponent<ComandCenter>())
            {
                float currentDistanceToComandCenter = Vector3.SqrMagnitude(MovementTarget.position - transform.position);
                _distance = currentDistanceToComandCenter;
                
                if (currentDistanceToComandCenter > _minComandCenterDistance)
                {
                    Debug.Log("I Move to Comand Center");
                    _navMeshAgent.enabled = true;
                    transform.LookAt(MovementTarget);
                    _navMeshAgent.SetDestination(MovementTarget.position);
                    transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
                }
                else if (currentDistanceToComandCenter <= _minComandCenterDistance)
                {
                    _navMeshAgent.enabled = false;
                    ComandCenter comandCenter = MovementTarget.gameObject.GetComponent<ComandCenter>();

                    if (_collectedCrystalsCount !<= 0f || _collectedGasCount !<= 0f)
                    {
                        comandCenter.GiveResourcesToComandCenter(_collectedCrystalsCount, _collectedGasCount);
                        _collectedCrystalsCount = 0f;
                        _collectedGasCount = 0f;
                    }

                    if (_collectedCrystalsCount <= 0f || _collectedGasCount <= 0f)
                    {
                        Debug.Log("Not Enough Resources");
                        _workingBehaviour.ResourcesDelivered();
                    }
                }
            }
        }
    }

    private void MoveToBattleTarget()
    {
        if (MovementTarget)
        {
            if (MovementTarget.GetComponent<Unit>() || MovementTarget.GetComponent<Building>())
            {
                float currentTargetDistance = Vector3.SqrMagnitude(MovementTarget.position - transform.position);
                _distance = currentTargetDistance;

                if (currentTargetDistance > _minAttackDistance)
                {
                    _navMeshAgent.enabled = true;
                    transform.LookAt(MovementTarget);
                    _navMeshAgent.SetDestination(MovementTarget.position);
                    transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
                }

                if (currentTargetDistance <= _minAttackDistance)
                {
                    _navMeshAgent.enabled = false;
                    transform.LookAt(MovementTarget);
                    _attackBehavour.SetNearbyBattleTarget(MovementTarget);
                    transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
                    MovementTarget = null;
                }
            }
        }
    }

    private void MoveToFixTarget()
    {
        if (MovementTarget)
        {
            if (MovementTarget.GetComponent<ObjectHealth>())
            {
                float currentTargetDistance = Vector3.SqrMagnitude(MovementTarget.position - transform.position);
                _distance = currentTargetDistance;

                if (currentTargetDistance > _minFixDistance)
                {
                    _navMeshAgent.enabled = true;
                    transform.LookAt(MovementTarget);
                    _navMeshAgent.SetDestination(MovementTarget.position);
                    transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
                }

                if (currentTargetDistance <= _minFixDistance)
                {
                    _navMeshAgent.enabled = false;
                    transform.LookAt(MovementTarget);
                    transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
                    MovementTarget = null;
                }
            }
        }
    }

    private void MoveToTarget()
    {
        if (MovementTarget)
        {
            float currentTargetDistance = Vector3.SqrMagnitude(MovementTarget.position - transform.position);
            _distance = currentTargetDistance;

            if (currentTargetDistance >= _minMovementTargetDistance)
            {
                _navMeshAgent.enabled = true;
                transform.LookAt(MovementTarget);
                _navMeshAgent.SetDestination(MovementTarget.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }
            else if (currentTargetDistance <= _minMovementTargetDistance)
            {
                _navMeshAgent.enabled = false;
                MovementTarget = null;
            }
        }
    }
}
