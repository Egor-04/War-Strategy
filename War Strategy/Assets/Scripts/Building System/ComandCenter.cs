using UnityEngine;

public class ComandCenter : MonoBehaviour
{
    [Header("Resources Balance")]
    [SerializeField] private ResourcesBalance _resourcesBalance;

    private void Start()
    {
        _resourcesBalance = FindObjectOfType<ResourcesBalance>();
    }

    private void Update()
    {
        
    }

    public void GiveResourcesToComandCenter(float collectedCrystalsCount, float collectedGasCount)
    {
        _resourcesBalance.AddResourcesToSystem(collectedCrystalsCount, collectedGasCount);
    }
}
