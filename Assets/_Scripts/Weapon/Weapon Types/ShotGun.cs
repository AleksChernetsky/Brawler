using System;

using Unity.VisualScripting;

using UnityEngine;

using Random = UnityEngine.Random;

public class ShotGun : Weapon<ShotGunDataScriptableObject>
{
    [SerializeField] protected Transform _weaponMuzzle;
    [SerializeField] protected Projectile _projectilePrefab;
    private float _forceRandomFactor => _weaponData.ForceRandomFactor;
    private float _spreadFactor => _weaponData.SpreadFactor;
    private ForceMode _forceMode => _weaponData.ForceMode;
    private float _force => _weaponData.Force;
    private int _magCapacity => _weaponData.MagCapacity;

    public override void Shoot()
    {
        if (CanAttack && _currentAmmo < _magCapacity)
        {
            CanAttack = false;
            for (int i = 0; i < _magCapacity; i++)
            {
                GameObject bullet = ObjectPool.Instance.GetFreeElement();
                var SpreadX = Random.Range(-_spreadFactor, _spreadFactor);
                var SpreadY = Random.Range(-_spreadFactor, _spreadFactor);

                if (bullet != null)
                {
                    _currentAmmo++;
                    CallOnShootEvent();
                    bullet.transform.SetPositionAndRotation(_weaponMuzzle.position, _weaponMuzzle.rotation);
                    bullet.transform.Rotate(new Vector3(SpreadX, SpreadY, 0));
                    bullet.SetActive(true);
                    bullet.TryGetComponent(out Projectile projectile);
                    projectile._damageInfo = _damageInfo;
                    projectile.RigidBody.AddForce(bullet.transform.forward * Random.Range(_force, _force * _forceRandomFactor), _forceMode);
                }
            }
            if (_currentAmmo < _magCapacity)
                StartCoroutine(ResetAttackCooldown());
            else
                StartCoroutine(ReloadWeapon());
        }
    }
}