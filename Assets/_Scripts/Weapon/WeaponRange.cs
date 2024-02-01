using System.Collections;

using UnityEngine;

public class WeaponRange : WeaponMain
{
    [Header("Range Weapon values")]
    [SerializeField] protected Transform weaponMuzzle;
    [SerializeField] protected Projectile projectilePrefab;
    //[SerializeField] protected AudioClip weaponSound = null;
    //[SerializeField] protected Renderer muzzleFlash = null;

    [Header("Bullet stats")]
    [SerializeField] protected float baseSpread;
    [SerializeField] protected int bulletSpeed;

    [Header("Reload stats")]
    [SerializeField] protected int bulletsPerShot;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] protected int numberOfClips;
    [SerializeField] protected float clipReloadTime;

    protected int clipsLeft;
    protected bool isReloading;
    protected bool isFiring;

    protected void Start() => clipsLeft = numberOfClips;

    public override void Shoot()
    {
        if (attackTimer >= fireRate && clipsLeft > 0)
        {
            StartCoroutine(Fire());
            clipsLeft--;
            CurrentAmmo = clipsLeft / numberOfClips;
            attackTimer = 0;
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
    private IEnumerator Fire()
    {
        int shotCounter = 0;

        while (shotCounter < bulletsPerShot)
        {
            isFiring = true;
            CallOnShootEvent();
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
    private IEnumerator ReloadWeapon()
    {
        if (isReloading)
        {
            yield break;
        }
        while (clipsLeft < numberOfClips)
        {
            isReloading = true;
            yield return new WaitForSeconds(clipReloadTime);
            clipsLeft++;
            CurrentAmmo = clipsLeft / numberOfClips;
        }
        isReloading = false;
    }
}