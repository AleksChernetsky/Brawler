using System;
using System.Collections;

using UnityEngine;

public abstract class Weapon<T> : MonoBehaviour, IWeapon where T : WeaponDataScriptableObject
{
    [SerializeField] protected T _weaponData;
    protected bool CanAttack = true;
    protected int _maxAmmo = 3;
    protected int _currentAmmo;
    protected DamageInfo _damageInfo;

    protected int Damage => _weaponData.Damage;
    protected float AttackDelay => _weaponData.AttackDelay;
    protected int ReloadTime => _weaponData.ReloadTime;

    public event Action OnShootEvent;
    public event Action OnFistAttack;
    public event Action OnClawAttack;

    public void CallOnShootEvent() => OnShootEvent?.Invoke();
    public void CallOnFistAttack() => OnFistAttack?.Invoke();
    public void CallOnClawAttack() => OnClawAttack?.Invoke();

    private void Awake()
    {
        GetComponent<VitalitySystem>().OnRegister += ShooterInfo;
        _currentAmmo = _maxAmmo;
    }

    private void ShooterInfo(int id, string name)
    {
        _damageInfo = new DamageInfo(Damage, id, gameObject.name);
    }

    public abstract void Shoot();

    public IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackDelay);
        print("ResetAttackCooldown, currentAmmo =  " + _currentAmmo);
        CanAttack = true;
    }
    public IEnumerator ReloadWeapon()
    {
        while (_currentAmmo < _maxAmmo)
        {
            if (_currentAmmo >= 3)
            {
                break;
            }
            yield return new WaitForSeconds(ReloadTime);
            _currentAmmo++;
            print("ReloadWeapon, currentAmmo = " + _currentAmmo);
        }
        CanAttack = true;
    }
}

public interface IWeapon
{
    void Shoot();
    public event Action OnShootEvent;
    public event Action OnFistAttack;
    public event Action OnClawAttack;
}
