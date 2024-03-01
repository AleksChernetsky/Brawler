using System.Collections;

using UnityEngine;

public class AutomaticWeapon : WeaponMain
{
    [Header("Range Weapon values")]
    [SerializeField] private Transform weaponMuzzle;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Bullet stats")]
    [SerializeField] private float baseSpread;
    [SerializeField] private int bulletSpeed;

    [Header("Reload stats")]
    [SerializeField] private int bulletsPerShot;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float clipReloadTime;
    private int numberOfClips = 3;

    [Header("Sounds")]
    [SerializeField] private AudioClip _fire;
    private AudioSource _audioSource;

    private int clipsLeft;
    private bool isReloading;

    private void Start()
    {
        clipsLeft = numberOfClips;
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Shoot()
    {
        if (clipsLeft > 0)
        {
            StartCoroutine(Fire());
            clipsLeft--;
            CurrentAmmo = (float)clipsLeft / (float)numberOfClips;
            attackTimer = 0;
            StartCoroutine(ReloadWeapon());
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
        if (timeBetweenShots == 0)
        {
            SingleFireEffect();
        }

        while (shotCounter < bulletsPerShot)
        {
            if (timeBetweenShots > 0)
                StartCoroutine(RapidFireEffect());

            CallOnShootEvent();
            SpawnProjectile();
            shotCounter++;
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
    private void SingleFireEffect()
    {
        _audioSource.PlayOneShot(_fire);
        muzzleFlash.Emit(2);
    }
    private IEnumerator RapidFireEffect()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.3f);
        _audioSource.PlayOneShot(_fire);
        muzzleFlash.Emit(1);
        yield return new WaitForSeconds(_fire.length);
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
            CurrentAmmo = (float)clipsLeft / (float)numberOfClips;
            CallOnReloadEvent();
        }
        isReloading = false;
    }
}