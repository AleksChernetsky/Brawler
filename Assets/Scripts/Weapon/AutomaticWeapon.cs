using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AutomaticWeapon : WeaponMain
{
    [Header("Basic Weapon values")]
    [SerializeField] private Transform weaponMuzzle;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Weapon settings")]
    [SerializeField] private int bulletsPerShot;
    [SerializeField] private float timeBetweenBullets;
    [SerializeField] private float baseSpread;
    [SerializeField] private int bulletSpeed;

    private float clipReloadTime = 4;
    private int numberOfClips = 3;

    [Header("Sounds")]
    [SerializeField] private AudioClip _fire;
    private AudioSource _audioSource;

    private int clipsLeft;
    private bool isReloading;

    private void Start()
    {
        clipsLeft = numberOfClips;
        CurrentAmmo = (float)clipsLeft / (float)numberOfClips;
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Shoot()
    {
        if (clipsLeft > 0)
        {
            CallOnShootEvent();
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
        var Spread = Random.Range(-baseSpread, baseSpread);

        bullet.transform.SetPositionAndRotation(weaponMuzzle.position, weaponMuzzle.rotation);
        bullet.transform.Rotate(new Vector3(Spread / 2, Spread, 0));
        bullet.SetActive(true);
        bullet.TryGetComponent(out Projectile projectile);
        projectile.DamageInfo = damageInfo;
        projectile.RigidBody.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    private IEnumerator Fire()
    {
        int shotCounter = 0;

        if (timeBetweenBullets == 0) // burst spawn
        {
            SingleFireEffect();
            for (int i = 0; i < bulletsPerShot; i++)
                SpawnProjectile();
            yield break;
        }

        while (shotCounter < bulletsPerShot)
        {
            if (timeBetweenBullets > 0)
                StartCoroutine(RapidFireEffect());

            SpawnProjectile();
            shotCounter++;
            yield return new WaitForSeconds(timeBetweenBullets);
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