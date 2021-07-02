using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _resourceTarget;

    [Header("Tool Damage")]
    [SerializeField] private float _toolDamageForce = 1f;

    [Header("Work Time")]
    [SerializeField] private float _workTime = 8f;

    private float _currentworkTime;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
