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

    [Tooltip("Извлеченная информация из AttackBehaviour")]
    [Header("Battle Target")]
    public Transform BattleTarget;
    [SerializeField] private float _minAttackDistance;
    [SerializeField] private float _damageForce;

    [Header("Resource Target")]
    public Transform ResourceTarget;
    [SerializeField] private float _minWorkDistance;
    [SerializeField] private int _toolDamageForce;

    [Header("Comand Center")]
    [SerializeField] private Transform ComandCenterDeliveryPoint;
    [SerializeField] private float _minComandCenterDistance;

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
    }

    public void SetMovementTarget(Transform movementTarget)
    {
        MovementTarget = movementTarget;
        ResourceTarget = null;
    }

    public void SetBattleTarget(Transform battleTarget, float minAttackDistance, float damageForce)
    {
        BattleTarget = battleTarget;
        _minAttackDistance = minAttackDistance;
        _damageForce = damageForce;
    }

    public void SetResourceTarget(Transform resourceTarget, float minWorkDistance, int toolDamageForce) // CollectThisResourceTarget ===> MoveToResourceTarget ===> Work ===> GetCrystal
    {
        MovementTarget = null;
        ResourceTarget = resourceTarget;
        _minWorkDistance = minWorkDistance;
        _toolDamageForce = toolDamageForce;
    }

    private void MoveToResourceTarget()
    {
        if (ResourceTarget)
        {
            float currentResourceTargetDistance = Vector3.SqrMagnitude(ResourceTarget.position - transform.position);
            _distance = currentResourceTargetDistance;

            if (currentResourceTargetDistance > _minWorkDistance)
            {
                _navMeshAgent.enabled = true;
                transform.LookAt(ResourceTarget);
                _navMeshAgent.SetDestination(ResourceTarget.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }

            if (currentResourceTargetDistance <= _minWorkDistance)
            {
                _navMeshAgent.enabled = false;
                transform.LookAt(ResourceTarget);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }
        }
        else
        {
            ClearResourceTarget();
        }
    }

    public void SetComandCenterTarget(Transform comandCenterDileveryPoint, float minComandCenterDistance, float collectedCrystalsCount, float collectedGasCount)
    {
        ComandCenterDeliveryPoint = comandCenterDileveryPoint;
        _minComandCenterDistance = minComandCenterDistance;
        _collectedCrystalsCount = collectedCrystalsCount;
        _collectedGasCount = collectedGasCount;
    }

    private void MoveToComandCenter()
    {
        if (ComandCenterDeliveryPoint)
        {
            ResourceTarget = null;

            float currentDistanceToComandCenter = Vector3.SqrMagnitude(ComandCenterDeliveryPoint.position - transform.position);
            _distance = currentDistanceToComandCenter;
            
            if (currentDistanceToComandCenter > _minComandCenterDistance)
            {
                _navMeshAgent.enabled = true;
                transform.LookAt(ComandCenterDeliveryPoint);
                _navMeshAgent.SetDestination(ComandCenterDeliveryPoint.position);
                Debug.Log("I Move to Comand Center");
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }

            if (currentDistanceToComandCenter <= _minComandCenterDistance)
            {
                Debug.Log("I Near Comand Center");
                _navMeshAgent.enabled = false;
                ComandCenter comandCenter = ComandCenterDeliveryPoint.gameObject.GetComponent<ComandCenter>();
                comandCenter.GiveResourcesToComandCenter(_collectedCrystalsCount, _collectedGasCount);
                _workingBehaviour.ResourcesDelivered();
            }
        }
    }

    private void MoveToBattleTarget()
    {
        if (BattleTarget)
        {
            MovementTarget = null;

            float currentTargetDistance = Vector3.SqrMagnitude(BattleTarget.position - transform.position);
            _distance = currentTargetDistance;

            if (currentTargetDistance > _minAttackDistance)
            {
                _navMeshAgent.enabled = true;
                transform.LookAt(BattleTarget);
                _navMeshAgent.SetDestination(BattleTarget.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }

            if (currentTargetDistance <= _minAttackDistance)
            {
                _navMeshAgent.enabled = false;
                transform.LookAt(BattleTarget);
                _attackBehavour.AttackNearbyTarget(BattleTarget);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }
        }
        else
        {
            ClearBattleTarget();
        }
    }

    private void ClearBattleTarget()
    {
        BattleTarget = null;
        _minAttackDistance = 0f;
        _damageForce = 0f;
    }

    private void ClearResourceTarget()
    {
        ResourceTarget = null;
        _minWorkDistance = 0f;
        _toolDamageForce = 0;
    }

    private void MoveToTarget()
    {
        if (MovementTarget)
        {
            BattleTarget = null;
            ComandCenterDeliveryPoint = null;

            float currentTargetDistance = Vector3.SqrMagnitude(MovementTarget.position - transform.position);
            _distance = currentTargetDistance;

            if (currentTargetDistance >= 20f)
            {
                _navMeshAgent.enabled = true;
                transform.LookAt(MovementTarget);
                _navMeshAgent.SetDestination(MovementTarget.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }
            else
            {
                MovementTarget = null;
                _navMeshAgent.enabled = false;
            }
        }
    }
}
