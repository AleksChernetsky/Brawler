using UnityEngine;

public class VitalitySystem : MonoBehaviour, IDamageable
{
    //public static event Action<VitalitySystem> OnEnemyKilled;
    [field: SerializeField] public float MaxHealth { get ; set; }
    [field: SerializeField] public float CurrentHealth { get; set; }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();            
        }
    }

    public void Die()
    {
        //OnEnemyKilled?.Invoke(this);
        Destroy(gameObject);        
    }
}
