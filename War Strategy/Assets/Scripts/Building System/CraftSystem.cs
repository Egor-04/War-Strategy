using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CraftSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _unitQueue;

    [Header("For Unit Craft")]
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private float _craftTime = 10f;
    [SerializeField] private Craft _craftItem;

    private ResourcesBalance _resourcesBalance;
    [SerializeField] private float _currentCraftTime;
    private bool _itemIsCrafting;

    [Serializable]
    public class Craft 
    {
        public GameObject ItemPrefab;
        public int CrystalsCount;
        public int GasCount;
        public int SpaceCount;
    }

    private void Start()
    {
        _resourcesBalance = FindObjectOfType<ResourcesBalance>();
    }

    public void Update()
    {
        CurrentCraftProcess();
    }

    public void CraftUnit()
    {
        if (_resourcesBalance.CrystalsCount >= _craftItem.CrystalsCount && _resourcesBalance.GasCount >= _craftItem.GasCount)
        {
            _resourcesBalance.CrystalsCount -= _craftItem.CrystalsCount;
            _resourcesBalance.GasCount -= _craftItem.GasCount;
            _currentCraftTime += _craftTime;
            _itemIsCrafting = true;
        }
    }

    public void CraftBuild()
    {

    }

    private void CurrentCraftProcess()
    {
        _currentCraftTime -= Time.deltaTime;

        if (_itemIsCrafting)
        {
            if (_currentCraftTime <= 0f)
            {
                _currentCraftTime = 0f;
                Instantiate(_craftItem.ItemPrefab, _exitPoint.position, _craftItem.ItemPrefab.transform.rotation);
                _itemIsCrafting = false;
            }
        }
    }
}
