using System;

using UnityEngine;

public enum WeaponType { Shotgun, MachineGun, SniperRifle, SMG, Fist };
public abstract class WeaponMain : MonoBehaviour
{
    [Header("Basic values")]
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected int damage;
    [SerializeField] protected float fireRate;

    protected DamageInfo damageInfo;
    protected float attackTimer;

    public float CurrentAmmo { get; protected set; }

    public event Action OnShootEvent;
    public event Action OnFistAttack;
    public event Action OnClawAttack;

    public void CallOnShootEvent() => OnShootEvent?.Invoke();
    public void CallOnFistAttack() => OnFistAttack?.Invoke();
    public void CallOnClawAttack() => OnClawAttack?.Invoke();

    protected void Awake() => GetComponent<VitalitySystem>().OnRegister += DamagerInfo;
    private void Update() => attackTimer += Time.deltaTime;
    public abstract void Shoot();
    private void DamagerInfo(int id) => damageInfo = new DamageInfo(id, gameObject.name, weaponType, damage);
}
