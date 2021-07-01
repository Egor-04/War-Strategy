using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _speed;
    
    [Header("Nav Mesh Agent")]
    [SerializeField] private NavMeshAgent _navMeshAgent;

    [Header("Target")]
    [SerializeField] private Transform _target;

    private Unit _unit;

    private void Start()
    {
        _unit = GetComponent<Unit>();
    }

    private void Update()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (_target)
        {
            float currentTargetDistance = Vector3.SqrMagnitude(_target.position - transform.position);

            if (currentTargetDistance > 0f)
            {
                transform.LookAt(_target);
                _navMeshAgent.SetDestination(_target.position);
                transform.eulerAngles = new Vector3( 0f, transform.eulerAngles.y, 0f);
            }
        }
    }

    private void MoveToEnemy()
    {

    }
}
