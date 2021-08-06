using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CraftSystem : MonoBehaviour
{
    [SerializeField] private GameObject _currentCraftedPrefab;

    [Header("For Unit Craft")]
    [SerializeField] private float _craftTime = 10f;
    [SerializeField] private Craft _craftItem;

    private ResourcesBalance _resourcesBalance;

    [Serializable]
    public class Craft 
    {
        public GameObject ItemPrefab;
        public Transform ExitPoint;
        public int CrystalsCount;
        public int GasCount;
        public int SpaceCount;
    }

    public void Update()
    {
        CurrentCraftProcess();
    }

    public void CraftUnit()
    {
        if (_resourcesBalance._storageSpaceCurrentCount < _resourcesBalance._storageSpaceMaxCount)
        {
            if (_resourcesBalance._crystalsCount >= _craftItem.CrystalsCount)
            {
                _resourcesBalance._crystalsCount -= _craftItem.CrystalsCount;
                _resourcesBalance._gasCount -= _craftItem.GasCount;
            }
        }
    }

    public void CraftBuild()
    {

    }

    private void CurrentCraftProcess()
    {

    }
}
