using System;
using UnityEngine;

public class VitalitySystem : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float CurrentHealth { get; set; }

    public static Action OnTakingDamage;
    public static Action OnEnemyDied;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        OnTakingDamage.Invoke();

        if (CurrentHealth <= 0)
        {
            OnEnemyDied.Invoke();
            this.gameObject.layer = 0;
            Die();
        }
    }

    public void Die()
    {
        
        Destroy(gameObject, 5f);
    }
}
