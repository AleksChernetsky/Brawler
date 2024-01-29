using System;

using UnityEngine;

public class VitalitySystem : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public float CurrentHealthPercent { get; private set; }
    private int _id;

    public event Action OnDeath;
    public event Action OnTakingHit;
    public event Action<int> OnRegister;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _id = CharacterDataManager.instance.CharRegister(this);
        OnRegister?.Invoke(_id);
    }

    public void TakeDamage(DamageInfo info)
    {
        CurrentHealthPercent = (float)_currentHealth / _maxHealth;

        if (_currentHealth > 0)
        {
            _currentHealth -= info.Damage;
        }

        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke();
            Die();
            Debug.Log($"{info.DamagerName} killed {gameObject.name} by {info.weaponType}");
        }
        else
        {
            OnTakingHit?.Invoke();
            //Debug.Log($"{info.ShooterName} shoot on {gameObject.name} and deal {info.Damage} damage");
        }
    }

    public void Die()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.layer = 0;
        Destroy(gameObject, 5f);
    }
}

public struct DamageInfo
{
    public int ShooterID;
    public string DamagerName;
    public WeaponType weaponType;
    public int Damage;

    public DamageInfo(int shooterID, string shooterName, WeaponType weaponType, int damage)
    {
        ShooterID = shooterID;
        DamagerName = shooterName;
        this.weaponType = weaponType;
        Damage = damage;
    }
}