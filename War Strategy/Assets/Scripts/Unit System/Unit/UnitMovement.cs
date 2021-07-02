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

    private AttackBehaviour _attackBehavour;

    private void Start()
    {
        _attackBehavour = GetComponent<AttackBehaviour>();
    }

    private void Update()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();

        MoveToTarget();
        AttackTarget();
    }

    public void SetMovementTarget(Transform movementTarget)
    {
        MovementTarget = movementTarget;
    }

    public void SetBattleTarget(Transform battleTarget, float minAttackDistance, float damageForce)
    {
        BattleTarget = battleTarget;
        _minAttackDistance = minAttackDistance;
        _damageForce = damageForce;

        AttackTarget();
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
