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

    [Header("Audio Shot")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _shotSound;

    [Header("Hit Effect Random Spawn")]
    [SerializeField] private float _X = 3f;
    [SerializeField] private float _Y = 3f;
    [SerializeField] private float _Z = 3f;

    [Header("Min Attack Distance")]
    [SerializeField] private float _minAttackDistance = 500f;

    [Header("Search Area")]
    [SerializeField] private Transform _searchArea;
    [SerializeField] private float _searchRadius = 100f;

    [Header("Gizmos Color")]
    [SerializeField] private float _red = 1f, _green = 0f, _blue = 0f;

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
        
        FindNearbyEnemies();
        AttackNearbyTarget();
    }
    
    private void FindNearbyEnemies()
    {
        if (!_unitMovement.MovementTarget)
        {
            //if (!_enemyTarget)
            //{
                Collider[] colliders = Physics.OverlapSphere(_searchArea.position, _searchRadius);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].GetComponent<Unit>() || colliders[i].GetComponent<Building>())
                    {
                        if (colliders[i].GetComponent<Unit>())
                        {
                            if (colliders[i].GetComponent<Unit>().CurrentTeamGroup != _currentUnit.CurrentTeamGroup)
                            {
                                FollowTarget(colliders[i].transform);
                            }
                        }
                        else if (colliders[i].GetComponent<Building>())
                        {
                            if (colliders[i].GetComponent<Building>().CurrentBuildingTeamGroup != _currentUnit.CurrentTeamGroup)
                            {
                                FollowTarget(colliders[i].transform);
                            }
                        }
                    }
                }
            //}
        }
    }

    public void FollowTarget(Transform currentTarget)
    {
        if (currentTarget.gameObject.GetComponent<Unit>() || currentTarget.gameObject.GetComponent<Building>())
        {
            _unitMovement.SetBattleTarget(currentTarget, _minAttackDistance, _damageForce);
        }
    }

    public void SetNearbyBattleTarget(Transform battleTarget)
    {
        _enemyTarget = battleTarget;
    }

    public void AttackNearbyTarget()
    {
        if (!_unitMovement.MovementTarget)
        {
            if (_enemyTarget)
            {
                float currentNearbyTarget = Vector3.SqrMagnitude(_enemyTarget.position - transform.position);

                if (_enemyTarget.gameObject.GetComponent<ObjectHealth>())
                {
                    if (currentNearbyTarget <= _minAttackDistance)
                    {
                        if (_currentTimeInterval <= 0f)
                        {
                            _currentTimeInterval = 0f;

                            float randomPositionX = Random.Range(_enemyTarget.position.x + _X, _enemyTarget.position.x - _X);
                            float randomPositionY = Random.Range(_enemyTarget.position.y + _Y, _enemyTarget.position.y - _Y);
                            float randomPositionZ = Random.Range(_enemyTarget.position.z + _Z, _enemyTarget.position.z - _Z);

                            Instantiate(_hitEffect, new Vector3(randomPositionX, randomPositionY, randomPositionZ), Quaternion.identity);
                            Instantiate(_muzzleFlash, _muzzleFlashSapwn.position, _muzzleFlashSapwn.rotation);
                            AudioSource source = Instantiate(_source, transform.position, Quaternion.identity);
                            source.clip = _shotSound;
                            source.Play();

                            ObjectHealth targetHealth = _enemyTarget.gameObject.GetComponent<ObjectHealth>();
                            targetHealth.DamageHit(_damageForce);
                            _currentTimeInterval = _shootTimeInterval;
                        }
                    }
                }
            }
        }
    }

    public void AttackThisTarget(Transform currentBattleTarget) // Срабатывает один раз, надо исправить
    {
        Debug.Log("Select Target For Attack " + currentBattleTarget.name);
        if (currentBattleTarget.gameObject.GetComponent<Unit>() || currentBattleTarget.gameObject.GetComponent<Building>())
        {
            _enemyTarget = currentBattleTarget;
        }

        _unitMovement.SetBattleTarget(currentBattleTarget, _minAttackDistance, _damageForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(_red, _green, _blue);
        Gizmos.DrawWireSphere(_searchArea.position, _searchRadius);
    }
}
