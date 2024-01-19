using System;

using Unity.VisualScripting;

using UnityEngine;

using Random = UnityEngine.Random;

public class ShotGun : Weapon<ShotGunValues>
{
    private float _forceRandomFactor => _weaponValues.ForceRandomFactor;
    private int _projectileNumber => _weaponValues.ProjectileNumber;
    private float _spreadFactor => _weaponValues.SpreadFactor;

    public override void Shoot()
    {
        _weaponMuzzle.Rotate(new Vector3(0, -_spreadFactor, 0));
        for (int i = 0; i < _projectileNumber; i++)
        {
            _weaponMuzzle.Rotate(new Vector3(0, _spreadFactor / (_projectileNumber / 2), 0)); // only even _projectileNumber required
            GameObject bullet = ObjectPool.Instance.GetFreeElement();

            if (bullet != null)
            {
                bullet.transform.SetPositionAndRotation(_weaponMuzzle.position, _weaponMuzzle.rotation);
                bullet.SetActive(true);
                bullet.TryGetComponent(out Projectile projectile);
                projectile.Damage = Damage;
                projectile.RigidBody.AddForce(_weaponMuzzle.transform.forward * Random.Range(Force, Force * _forceRandomFactor), ForceMode);
            }
        }
        _weaponMuzzle.Rotate(new Vector3(0, -_spreadFactor, 0));
    }
}
