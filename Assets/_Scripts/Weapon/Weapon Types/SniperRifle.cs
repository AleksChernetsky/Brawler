using UnityEngine;
using UnityEngine.Events;

using Random = UnityEngine.Random;

public class SniperRifle : Weapon<SniperRifleDataScriptableObject>
{
    [SerializeField] protected Transform _weaponMuzzle;
    [SerializeField] protected Projectile _projectilePrefab;
    private ForceMode _forceMode => _weaponData.ForceMode;
    private float _force => _weaponData.Force;
    private int _magCapacity => _weaponData.MagCapacity;

    public override void Shoot()
    {
        if (CanAttack && _currentAmmo < _magCapacity)
        {
            CanAttack = false;
            GameObject bullet = ObjectPool.Instance.GetFreeElement();
            if (bullet != null)
            {
                _currentAmmo++;
                CallOnShootEvent();
                bullet.transform.SetPositionAndRotation(_weaponMuzzle.position, _weaponMuzzle.rotation);
                bullet.SetActive(true);
                bullet.TryGetComponent(out Projectile projectile);
                projectile._damageInfo = _damageInfo;
                projectile.RigidBody.AddForce(_weaponMuzzle.transform.forward * _force, _forceMode);
            }
            if (_currentAmmo < _magCapacity)
                StartCoroutine(ResetAttackCooldown());
            else
                StartCoroutine(ReloadWeapon());
        }
    }
}