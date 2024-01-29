using UnityEngine;

using Random = UnityEngine.Random;

public class MachineGun : Weapon<MachineGunDataScriptableObject>
{
    [SerializeField] protected Transform _weaponMuzzle;
    [SerializeField] protected Projectile _projectilePrefab;
    private float _spreadFactor => _weaponData.SpreadFactor;
    private ForceMode _forceMode => _weaponData.ForceMode;
    private float _force => _weaponData.Force;
    private int _magCapacity => _weaponData.MagCapacity;

    public override void Shoot()
    {
        if (CanAttack && _currentAmmo < _magCapacity)
        {
            CanAttack = false;
            GameObject bullet = ObjectPool.Instance.GetFreeElement();
            var SpreadX = Random.Range(-_spreadFactor, _spreadFactor);
            var SpreadY = Random.Range(-_spreadFactor, _spreadFactor);
            if (bullet != null)
            {
                _currentAmmo++;
                CallOnShootEvent();
                bullet.transform.SetPositionAndRotation(_weaponMuzzle.position, _weaponMuzzle.rotation);
                bullet.transform.Rotate(new Vector3(SpreadX / 2, SpreadY, 0));
                bullet.SetActive(true);
                bullet.TryGetComponent(out Projectile projectile);
                projectile._damageInfo = _damageInfo;
                projectile.RigidBody.AddForce(bullet.transform.forward * _force, _forceMode);
            }
            if (_currentAmmo < _magCapacity)
                StartCoroutine(ResetAttackCooldown());
            else
                StartCoroutine(ReloadWeapon());
        }
    }
}
