using UnityEngine;

public abstract class Weapon<T> : MonoBehaviour, IWeapon where T : WeaponValues
{
    [SerializeField] protected T _weaponValues;
    [SerializeField] protected Transform _weaponMuzzle;
    [SerializeField] protected Projectile _projectilePrefab;

    protected float Damage => _weaponValues.Damage;
    protected ForceMode ForceMode => _weaponValues.ForceMode;
    protected float Force => _weaponValues.Force;

    public abstract void Shoot();
}

public interface IWeapon
{
    void Shoot();
}