using System.Collections.Generic;
using System.Collections;
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
    [SerializeField] private Transform _movementTarget;

    private void Update()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        MoveToTarget();
    }

    public void SetTarget(Transform movementTarget)
    {
        _movementTarget = movementTarget;
    }

    private void MoveToTarget()
    {
        if (_movementTarget)
        {
            float currentTargetDistance = Vector3.SqrMagnitude(_movementTarget.position - transform.position);
            _distance = currentTargetDistance;

            if (currentTargetDistance >= 20f)
            {
                _navMeshAgent.enabled = true;
                transform.LookAt(_movementTarget);
                _navMeshAgent.SetDestination(_movementTarget.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }
            else
            {
                _movementTarget = null;
                _navMeshAgent.enabled = false;
            }
        }
    }
}
