using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float _timeDestroy = 1f;

    private void Start()
    {
        Destroy(gameObject, _timeDestroy);
    }
}
