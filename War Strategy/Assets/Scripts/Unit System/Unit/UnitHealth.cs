using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private float _unitHealth;

    private void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_unitHealth <= 0f)
        {
            _unitHealth = 0f;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageForce)
    {
        _unitHealth -= damageForce;
    }
}
