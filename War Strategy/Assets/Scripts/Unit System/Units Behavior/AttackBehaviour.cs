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

    [Header("Shot Effect")]
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private Transform _muzzleFlashSapwn;

    [Header("Attack Interval")]
    [SerializeField] private float _shootTimeInterval = 1f;

    [Header("Min Attack Distance")]
    [SerializeField] private float _minAttackDistance = 500f;

    [Header("Search Area")]
    [SerializeField] private Transform _searchArea;
    [SerializeField] private float _searchRadius = 100f;

    [Header("Gizmos Color")]
    [SerializeField] private float _red = 1f, _green = 0f, _blue = 0f;

    [SerializeField] private bool _hasBattleTarget;
    [SerializeField] private bool _hasMovementTarget;

    private float _currentTimeInterval;

    private Unit _currentUnit;
    private UnitMovement _unitMovement;

    private void Start()
    {
        _currentUnit = GetComponent<Unit>();
        _unitMovement = GetComponent<UnitMovement>();
    }

    private void Update()
    {
        _currentTimeInterval -= Time.deltaTime;

        if (_enemyTarget)
        {
            _currentDistance = Vector3.SqrMagnitude(_enemyTarget.position - transform.position);
        }

        CheckMovementTargets();
        CheckBattleTarget();
        FindNearbyEnemies();
    }

    private void CheckMovementTargets()
    {
        if (_unitMovement.MovementTarget)
        {
            _hasMovementTarget = true;
        }
        else
        {
            _hasMovementTarget = false;
        }
    }

    private void CheckBattleTarget()
    {
        if (_unitMovement.BattleTarget)
        {
            _hasBattleTarget = true;
        }
        else
        {
            _hasBattleTarget = false;
        }
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
                    FollowTarget(colliders[i].transform);
                }
            }
        }
    }

    public void FollowTarget(Transform currentTarget)
    {
        if (!_hasMovementTarget && !_hasBattleTarget)
        {
            _unitMovement.SetBattleTarget(currentTarget, _minAttackDistance, _damageForce);
        }
        else
        {
            return;
        }
    }

    public void AttackNearbyTarget(Transform battleTarget)
    {
        if (_currentTimeInterval <= 0f)
        {
            _currentTimeInterval = 0f;

            Instantiate(_hitEffect, new Vector3(Random.Range(battleTarget.position.x + 5f, battleTarget.position.x - 5f), Random.Range(battleTarget.position.y + 5f, battleTarget.position.y - 5f), Random.Range(battleTarget.position.z + 5f, battleTarget.position.z - 5f)), Quaternion.identity);
            Instantiate(_muzzleFlash, _muzzleFlashSapwn.position, _muzzleFlash.transform.rotation);

            ObjectHealth targetHealth = battleTarget.gameObject.GetComponent<ObjectHealth>();
            targetHealth.DamageHit(_damageForce);
            _currentTimeInterval = _shootTimeInterval;
        }
    }

    public void AttackThisTarget(Transform currentBattleTarget) // ����������� ���� ���, ���� ���������
    {
        _enemyTarget = currentBattleTarget;
        _unitMovement.SetBattleTarget(currentBattleTarget, _minAttackDistance, _damageForce);
        Debug.Log("Select Target For Attack");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(_red, _green, _blue);
        Gizmos.DrawWireSphere(_searchArea.position, _searchRadius);
    }
}
