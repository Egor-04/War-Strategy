using UnityEngine.UI;
using UnityEngine;

public class ResourcesBalance : MonoBehaviour
{
    [Header("Crystal Balance")]
    public float CrystalsCount;
    [SerializeField] private Text _crystalsTextCount;

    [Header("Gas Balance")]
    public float GasCount;
    [SerializeField] private Text _gasTextCount;

    [Header("Storage Space")]
    public int StorageSpaceMaxCount = 200;
    public int StorageSpaceCurrentCount = 0;
    [SerializeField] private Text _storageSpaceCurrentCountText;

    public void Update()
    {
        _crystalsTextCount.text = CrystalsCount.ToString();
        _gasTextCount.text = GasCount.ToString();
    }

    public void AddResourcesToSystem(float crystalsCount, float gasCount)
    {
        CrystalsCount += crystalsCount;
        GasCount += gasCount;

        _crystalsTextCount.text = CrystalsCount.ToString();
        _gasTextCount.text = GasCount.ToString();
    }
}
