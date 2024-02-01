using System;
using System.Collections;

using UnityEngine;

public class VitalitySystem : MonoBehaviour, IDamageable
{
    [Header("Health Values")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _healRegenAmount;
    [SerializeField] private float _autoHealDelay;
    [SerializeField] private float _healRegenRate;
    private float _currentHealth;
    public float HealthPercentage { get; private set; }

    [Header("Intake Damage Text")]
    [SerializeField] private Transform _damageSpawnPoint;

    private int _id;

    private Coroutine _autoHealing;

    public event Action OnDeath;
    public event Action OnTakingHit;
    public event Action OnHealing;
    public event Action<int> OnRegister;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _id = CharacterDataManager.instance.CharRegister(this);
        OnRegister?.Invoke(_id);
    }

    public void TakeDamage(DamageInfo info)
    {
        _currentHealth -= info.Damage;

        if (_autoHealing != null)
            StopCoroutine(_autoHealing);
        _autoHealing = StartCoroutine(AutoHealing());

        if (_currentHealth > 0)
        {
            HealthPercentage = _currentHealth / _maxHealth;
            OnTakingHit?.Invoke();
            SpawnDamageText(info.Damage);
            //Debug.Log($"{info.DamagerName} deal {info.Damage} damage to {gameObject.name} by {info.weaponType}");
        }
        else
        {
            OnDeath?.Invoke();
            Die();
            //Debug.Log($"{info.DamagerName} killed {gameObject.name} by {info.weaponType}");
        }
    }

    public void Die()
    {
        gameObject.GetComponent<Collider>().enabled = false;
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
        while (_currentHealth < _maxHealth)
        {
            _currentHealth += _healRegenAmount;
            OnHealing?.Invoke();
            HealthPercentage = _currentHealth / _maxHealth;
            yield return new WaitForSeconds(_healRegenRate); // heal each 0.5 second
        }
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