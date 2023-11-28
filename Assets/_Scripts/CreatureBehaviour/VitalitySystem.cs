using System;
using UnityEngine;

public class VitalitySystem : MonoBehaviour, IDamageable
{
    public static event Action<VitalitySystem> OnEnemyKilled;
    [field: SerializeField] public int MaxHealth { get ; set; }
    [field: SerializeField] public int CurrentHealth { get; set; }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();            
        }
    }

    public void Die()
    {
        OnEnemyKilled?.Invoke(this);
        Destroy(gameObject);        
    }
}
