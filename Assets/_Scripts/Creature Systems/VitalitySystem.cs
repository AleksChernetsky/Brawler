using System;

using UnityEngine;

public class VitalitySystem : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public float CurrentHealthPercent { get; private set; }

    private int _id;
    private string _name;

    public event Action OnDeath;
    public event Action OnTakingHit;
    public event Action<int, string> OnRegister;

    public void CallOnDeathEvent() => OnDeath?.Invoke();
    public void CallOnTakingHitEvent() => OnTakingHit?.Invoke();

    private void Start()
    {
        _currentHealth = _maxHealth;
        _id = CharacterDataManager.instance.CharRegister(_name, this);
        _name = gameObject.name;
        OnRegister?.Invoke(_id, _name);
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
            CallOnDeathEvent();
            Die();
            //Debug.Log($"{info.ShooterName} kill {gameObject.name}");
        }
        else
        {
            CallOnTakingHitEvent();
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
    public int Damage;
    public int ShooterID;
    public string ShooterName;

    public DamageInfo(int damage, int shooterID, string shooterName)
    {
        Damage = damage;
        ShooterID = shooterID;
        ShooterName = shooterName;
    }
}