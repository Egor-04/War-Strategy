using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [Header("Current Target")]
    [SerializeField] private Transform _enemyTarget;

    [SerializeField] private float _currentDistance;

    [Header("Damage Force")]
    [SerializeField] private float _damageForce;

    [Header("Min Attack Distance")]
    [SerializeField] private float _minAttackDistance = 500f;

    [Header("Search Area")]
    [SerializeField] private Transform _searchArea;
    [SerializeField] private float _searchRadius = 100f;

    [Header("Gizmos Color")]
    [SerializeField] private float _red = 1f, _green = 0f, _blue = 0f;

    private Unit _currentUnit;
    private UnitMovement _unitMovement;

    private void Start()
    {
        _unitMovement = GetComponent<UnitMovement>();
    }

    private void Update()
    {
        if (_enemyTarget)
        {
            _currentDistance = Vector3.SqrMagnitude(_enemyTarget.position - transform.position);
        }

        FindNearbyEnemies();
    }

    private void FindNearbyEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(_searchArea.position, _searchRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Unit>())
            {
                if (colliders[i].GetComponent<Unit>().CurrentTeamGroup != _currentUnit.CurrentTeamGroup)
                {
                    Debug.Log(colliders[i].name);
                    FollowTarget(colliders[i].transform);
                }
            }
        }
    }

    public void FollowTarget(Transform currentTarget)
    {
        float currentTargetDistance = Vector3.SqrMagnitude(currentTarget.position - transform.position);

        Debug.Log("Nav Mesh Target" + currentTarget.name);
        _unitMovement.SetTarget(currentTarget);

        if (currentTargetDistance <= _minAttackDistance)
        {
            AttackNearbyTarget(currentTarget);
        }
    }

    private void AttackNearbyTarget(Transform nearbyTarget)
    {
        Debug.Log("Attacked " + nearbyTarget.name);
        ObjectHealth targethealth = nearbyTarget.gameObject.GetComponent<ObjectHealth>();
        targethealth.DamageHit(_damageForce * Time.deltaTime);
    }

    public void AttackThisTarget(Transform currentBattleTarget) // Срабатывает один раз, надо исправить
    {
        _enemyTarget = currentBattleTarget;

        _unitMovement.SetTarget(currentBattleTarget);

        if (_currentDistance <= _minAttackDistance)
        {
            _unitMovement.NavMeshAgent.enabled = false;
            
            ObjectHealth targethealth = currentBattleTarget.gameObject.GetComponent<ObjectHealth>();
            targethealth.DamageHit(_damageForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(_red, _green, _blue);
        Gizmos.DrawWireSphere(_searchArea.position, _searchRadius);
    }
}
