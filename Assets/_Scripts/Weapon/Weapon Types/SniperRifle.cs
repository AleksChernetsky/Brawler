using UnityEngine;

using Random = UnityEngine.Random;

public class SniperRifle : Weapon<WeaponValues>
{
    public override void Shoot()
    {
        GameObject bullet = ObjectPool.Instance.GetFreeElement();

        if (bullet != null)
        {
            bullet.transform.SetPositionAndRotation(_weaponMuzzle.position, _weaponMuzzle.rotation);
            bullet.SetActive(true);
            bullet.TryGetComponent(out Projectile projectile);
            projectile.Damage = Damage;
            projectile.RigidBody.AddForce(_weaponMuzzle.transform.forward * Force, ForceMode);
        }
    }
}