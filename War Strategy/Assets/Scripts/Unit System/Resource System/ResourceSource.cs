using UnityEngine;

public enum ResourceType {Crystal, Gas}
public class ResourceSource : MonoBehaviour
{
    public ResourceType CurrentResourceType;

    [Header("Resource Count")]
    [SerializeField] int _resourceCount;

    [Header("Interval")]
    [SerializeField] private float _interval = 1f;

    private float _currentInterval;

    private void Update()
    {
        _currentInterval -= Time.deltaTime;
        ChangeState();
    }

    public void ChangeState()
    {
        if (_resourceCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetCrystal(int toolDamageForce, WorkingBehaviour workingBehaviour)
    {
        if (_currentInterval <= 0f)
        {
            _currentInterval = 0f;
            
            _resourceCount -= toolDamageForce;
            workingBehaviour.CollectedResourcesCount += toolDamageForce;

            _currentInterval = _interval;
        }
    }
}
