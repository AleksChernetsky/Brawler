using System.Collections;

using UnityEngine;

public class AutomaticRifle : WeaponRange
{
    [SerializeField] private float timeBetweenShots;

    public override void Shoot()
    {
        if (attackTimer >= fireRate)
        {
            if (clipsLeft > 0)
            {
                CallOnShootEvent();
                StartCoroutine(BurstFire());
                clipsLeft--;
                attackTimer = 0;
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
            yield return new WaitForSeconds(timeBetweenShots);
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
        GameObject bullet = ObjectPool.Instance.GetFreeProjectile();
        var SpreadX = Random.Range(-baseSpread, baseSpread);
        var SpreadY = Random.Range(-baseSpread, baseSpread);

        if (bullet != null)
        {
            CallOnShootEvent();
            bullet.transform.SetPositionAndRotation(weaponMuzzle.position, weaponMuzzle.rotation);
            bullet.transform.Rotate(new Vector3(SpreadX / 2, SpreadY, 0));
            bullet.SetActive(true);
            bullet.TryGetComponent(out Projectile projectile);
            projectile.DamageInfo = damageInfo;
            projectile.RigidBody.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}
