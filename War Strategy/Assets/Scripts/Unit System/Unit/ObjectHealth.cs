using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] private float _objectHealth;

    private void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_objectHealth <= 0f)
        {
            _objectHealth = 0f;
            Destroy(gameObject);
        }
    }

    public void DamageHit(float damageForce)
    {
        _objectHealth -= damageForce;
    }
}
