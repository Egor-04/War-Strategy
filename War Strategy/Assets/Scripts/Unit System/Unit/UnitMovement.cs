using UnityEngine.AI;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    [Header("Nav Mesh Agent")]
    public NavMeshAgent NavMeshAgent;

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

    private AttackBehaviour _attackBehavour;
    private WorkingBehaviour _workingBehaviour;

    private void Start()
    {
        _attackBehavour = GetComponent<AttackBehaviour>();

        _workingBehaviour = GetComponent<WorkingBehaviour>();
    }

    private void Update()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();

        MoveToTarget();
        AttackTarget();
        MoveToResourceTarget();
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

        AttackTarget();
    }

    public void SetResourceTarget(Transform resourceTarget, float minWorkDistance, int toolDamageForce) // CollectThisResourceTarget ===> MoveToResourceTarget ===> Work ===> GetCrystal
    {
        MovementTarget = null;
        ResourceTarget = resourceTarget;
        _minWorkDistance = minWorkDistance;
        _toolDamageForce = toolDamageForce;

        MoveToResourceTarget();
    }

    private void MoveToResourceTarget()
    {
        if (ResourceTarget)
        {
            float currentResourceTargetDistance = Vector3.SqrMagnitude(ResourceTarget.position - transform.position);
            _distance = currentResourceTargetDistance;

            if (currentResourceTargetDistance > _minWorkDistance)
            {
                NavMeshAgent.enabled = true;
                transform.LookAt(ResourceTarget);
                NavMeshAgent.SetDestination(ResourceTarget.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }

            if (currentResourceTargetDistance <= _minWorkDistance)
            {
                NavMeshAgent.enabled = false;
                _workingBehaviour.Work();
            }
        }
    }

    private void AttackTarget()
    {
        if (BattleTarget)
        {
            MovementTarget = null;

            float currentTargetDistance = Vector3.SqrMagnitude(BattleTarget.position - transform.position);
            _distance = currentTargetDistance;

            if (currentTargetDistance > _minAttackDistance)
            {
                NavMeshAgent.enabled = true;
                transform.LookAt(BattleTarget);
                NavMeshAgent.SetDestination(BattleTarget.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }
            if (currentTargetDistance <= _minAttackDistance)
            {
                NavMeshAgent.enabled = false;
                _attackBehavour.AttackNearbyTarget(BattleTarget);
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

    private void MoveToTarget()
    {
        if (MovementTarget)
        {
            BattleTarget = null;

            float currentTargetDistance = Vector3.SqrMagnitude(MovementTarget.position - transform.position);
            _distance = currentTargetDistance;

            if (currentTargetDistance >= 20f)
            {
                NavMeshAgent.enabled = true;
                transform.LookAt(MovementTarget);
                NavMeshAgent.SetDestination(MovementTarget.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }
            else
            {
                MovementTarget = null;
                NavMeshAgent.enabled = false;
            }
        }
    }
}
