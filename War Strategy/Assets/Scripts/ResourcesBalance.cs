using UnityEngine.UI;
using UnityEngine;

public class ResourcesBalance : MonoBehaviour
{
    [Header("Crystal Balance")]
    [SerializeField] private float _crystalsCount;
    [SerializeField] private Text _crystalsTextCount;

    [Header("Gas Balance")]
    [SerializeField] private float _gasCount;
    [SerializeField] private Text _gasTextCount;

    public void AddResourcesToSystem(float crystalsCount, float gasCount)
    {
        _crystalsCount += crystalsCount;
        _gasCount += gasCount;

        _crystalsTextCount.text = _crystalsCount.ToString();
        _gasTextCount.text = _gasCount.ToString();
    }

    public void Pay()
    {

    }
}
