using UnityEngine;

public enum HealthType {Humaniod, Mechanic}
public class ObjectHealth : MonoBehaviour
{
    [Header("Health Type")]
    public HealthType CurrentHealthType;

    [Header("Health")]
    public float MaxObjectHealth;
    public float CurrentObjectHealth;

    [Header("Die Sound or Destroy Sound")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _killSound;

    private Unit _currentUnit;

    private void Start()
    {
        if (GetComponent<Unit>())
        {
            _currentUnit = GetComponent<Unit>();
        }
    }

    private void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_currentUnit)
        {
            _currentUnit.SetCurrentHealthValue(CurrentObjectHealth);
        }
        
        if (CurrentObjectHealth <= 0f)
        {
            CurrentObjectHealth = 0f;

            AudioSource source = Instantiate(_source, transform.position, Quaternion.identity);
            source.PlayOneShot(_killSound[Random.Range(0, _killSound.Length)]);

            Destroy(gameObject);
        }
    }

    public void DamageHit(float damageForce)
    {
        CurrentObjectHealth -= damageForce;
    }

    public void FixObject(float fixCount)
    {
        if (CurrentObjectHealth < MaxObjectHealth)
        {
            Debug.Log("Fixing");
            CurrentObjectHealth += fixCount;
        }
        else if (CurrentObjectHealth >= MaxObjectHealth)
        {
            CurrentObjectHealth = MaxObjectHealth;
        }
    }
}
