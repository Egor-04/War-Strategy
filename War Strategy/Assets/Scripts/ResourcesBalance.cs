using UnityEngine.UI;
using UnityEngine;

public class ResourcesBalance : MonoBehaviour
{
    [Header("Crystal Balance")]
    public float _crystalsCount;
    [SerializeField] private Text _crystalsTextCount;

    [Header("Gas Balance")]
    public float _gasCount;
    [SerializeField] private Text _gasTextCount;

    [Header("Storage Space")]
    public int _storageSpaceMaxCount = 200;
    public int _storageSpaceCurrentCount = 0;
    [SerializeField] private Text _storageSpaceCurrentCountText;

    public void AddResourcesToSystem(float crystalsCount, float gasCount)
    {
        _crystalsCount += crystalsCount;
        _gasCount += gasCount;

        _crystalsTextCount.text = _crystalsCount.ToString();
        _gasTextCount.text = _gasCount.ToString();
    }
}
