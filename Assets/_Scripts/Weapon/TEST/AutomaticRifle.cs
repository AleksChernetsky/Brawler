using System.Collections;

using UnityEngine;

public class AutomaticRifle : RangeWeapon
{
    [SerializeField] private float lagBetweenShots;

    public override void Shoot()
    {
        if (ShootTimer >= fireRate)
        {
            if (clipsLeft > 0)
            {
                CallOnShootEvent();
                StartCoroutine(BurstFire());
                clipsLeft--;
                ShootTimer = 0;
            }
        }
    }

    IEnumerator BurstFire()
    {
        int shotCounter = 0;

        while (shotCounter < bulletsPerShot)
        {
            isFiring = true;
            SpawnProjectile();
            shotCounter++;
            yield return new WaitForSeconds(lagBetweenShots);
        }
        if (clipsLeft < numberOfClips)
        {
            StartCoroutine(ReloadWeapon());
            isFiring = false;
            yield break;
        }
    }
    private void SpawnProjectile()
    {
        GameObject bullet = ObjectPool.Instance.GetFreeElement();
        var SpreadX = Random.Range(-baseSpread, baseSpread);
        var SpreadY = Random.Range(-baseSpread, baseSpread);

        if (bullet != null)
        {
            CallOnShootEvent();
            bullet.transform.SetPositionAndRotation(weaponMuzzle.position, weaponMuzzle.rotation);
            bullet.transform.Rotate(new Vector3(SpreadX / 2, SpreadY, 0));
            bullet.SetActive(true);
            bullet.TryGetComponent(out Projectile projectile);
            projectile._damageInfo = _damageInfo;
            projectile.RigidBody.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}
