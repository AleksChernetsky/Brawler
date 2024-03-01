using System;
using System.Collections;

using UnityEngine;

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

public class VitalitySystem : MonoBehaviour, IDamageable
{
    [Header("Health Values")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _healRegenAmount;
    [SerializeField] private float _autoHealDelay;
    [SerializeField] private float _healRegenRate;

    [Header("Intake Damage Text")]
    [SerializeField] private Transform _damageSpawnPoint;

    private int _id;
    private Coroutine _autoHealing;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth { get; private set; }
    public float HealthPercentage { get; private set; }

    public event Action OnDeath;
    public event Action OnTakingHit;
    public event Action OnHealing;
    public event Action<int> OnRegister;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        _id = CharacterDataManager.instance.CharRegister(this);
        OnRegister?.Invoke(_id);
    }

    public void TakeDamage(DamageInfo info)
    {
        CurrentHealth -= info.Damage;

        if (_autoHealing != null)
            StopCoroutine(_autoHealing);
        _autoHealing = StartCoroutine(AutoHealing());

        if (CurrentHealth > 0)
        {
            HealthPercentage = CurrentHealth / MaxHealth;
            OnTakingHit?.Invoke();
            SpawnDamageText(info.Damage);
        }
        else if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
            Die();
        }
    }

    public void Die()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        CharacterDataManager.instance.CharDelete(_id);
        Destroy(gameObject, 5f);
    }
    private void SpawnDamageText(int value)
    {
        GameObject text = ObjectPool.Instance.GetFreeDamageText();
        var randomForce = UnityEngine.Random.Range(3, 5); // numbers configured in playmode
        var randomDirection = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0, 0); // numbers configured in playmode
        if (text != null)
        {
            text.transform.SetPositionAndRotation((_damageSpawnPoint.position + randomDirection), _damageSpawnPoint.rotation);
            text.SetActive(true);
            text.TryGetComponent(out DamageText damageText);
            damageText.textMesh.text = value.ToString();
            damageText.RigidBody.AddForce((text.transform.up + randomDirection) * randomForce, ForceMode.Impulse);
        }
    }
    private IEnumerator AutoHealing()
    {
        yield return new WaitForSeconds(_autoHealDelay); // start healing after 5 second left
        while (CurrentHealth < MaxHealth)
        {
            CurrentHealth += _healRegenAmount;
            OnHealing?.Invoke();
            HealthPercentage = CurrentHealth / MaxHealth;
            yield return new WaitForSeconds(_healRegenRate); // heal each 0.5 second
        }
    }
}