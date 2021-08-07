using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float MaxEnemyHealth;
    public float CurrentEnemyHealth;

    [Header("Die Sound or Destroy Sound")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _killSound;

    private void Update()
    {
        CheckHealth();
    }

    public void CheckHealth()
    {
        if (CurrentEnemyHealth <= 0f)
        {
            CurrentEnemyHealth = 0f;

            AudioSource source = Instantiate(_source, transform.position, Quaternion.identity);
            source.PlayOneShot(_killSound[Random.Range(0, _killSound.Length - 1)]);

            Destroy(gameObject);
        }
    }

    public void DamageHit(float damageForce)
    {
        CurrentEnemyHealth -= damageForce;
    }
}