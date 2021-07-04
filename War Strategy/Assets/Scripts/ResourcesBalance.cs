using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesBalance : MonoBehaviour
{
    [Header("Crystal Balance")]
    [SerializeField] private float _crystalsCount;

    [Header("Gas Balance")]
    [SerializeField] private float _gasCount;

    public void AddResourcesToSystem(float crystalsCount, float gasCount)
    {
        _crystalsCount += crystalsCount;
        _gasCount += gasCount;
    }

    public void Pay()
    {

    }
}
