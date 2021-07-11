using UnityEngine;

public enum ResourceType {Crystal, Gas}
public class ResourceSource : MonoBehaviour
{
    public ResourceType CurrentResourceType;

    [Header("Resource Count")]
    [SerializeField] int _resourceCount;

    private void Update()
    {
        ChangeState();
    }

    public void ChangeState()
    {
        if (_resourceCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetResource(int toolDamageForce, WorkingBehaviour workingBehaviour)
    {
        Debug.Log("Im Getting");
        _resourceCount -= toolDamageForce;

        if (CurrentResourceType == ResourceType.Crystal)
        {
            workingBehaviour.CollectedCrystalsCount += toolDamageForce;
        }
        else if (CurrentResourceType == ResourceType.Gas)
        {
            workingBehaviour.CollectedGasCount += toolDamageForce;
        }
    }
}
