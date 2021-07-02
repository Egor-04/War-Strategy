using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] private float _objectHealth;

    [Header("Die Sound or Destroy Sound")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _killSound;

    private void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_objectHealth <= 0f)
        {
            _objectHealth = 0f;

            AudioSource source = Instantiate(_source, transform.position, Quaternion.identity);
            source.PlayOneShot(_killSound[Random.Range(0, _killSound.Length)]);
            Destroy(gameObject);
        }
    }

    public void DamageHit(float damageForce)
    {
        _objectHealth -= damageForce;
    }
}
