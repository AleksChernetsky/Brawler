using System;
using System.Collections;

using UnityEngine;

public enum WeaponType { Shotgun, MachineGun, SniperRifle, SMG};
public abstract class RangeWeapon : MonoBehaviour
{
    [Header("Basic values")]
    [SerializeField] protected WeaponType typeOfGun;
    [SerializeField] protected Transform weaponMuzzle;
    [SerializeField] protected Projectile projectilePrefab;
    //[SerializeField] protected AudioClip shotSound = null;
    //[SerializeField] protected Renderer muzzleFlash = null;
    protected string weaponOwner;

    [Header("Basic stats")]
    [SerializeField] protected int damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float baseSpread;
    [SerializeField] protected int bulletSpeed;

    [Header("Reload stats")]
    [SerializeField] protected int bulletsPerShot;
    [SerializeField] protected int numberOfClips;
    [SerializeField] protected float clipReloadTime;
    protected int clipsLeft;
    protected bool isReloading;
    protected bool isFiring;

    protected DamageInfo _damageInfo;
    protected float ShootTimer;

    public event Action OnShootEvent;
    public void CallOnShootEvent() => OnShootEvent?.Invoke();

    protected void Awake()
    {
        clipsLeft = numberOfClips;
        weaponOwner = gameObject.name;
        GetComponent<VitalitySystem>().OnRegister += ShooterInfo;
    }
    private void Update()
    {
        ShootTimer += Time.deltaTime;
    }
    private void ShooterInfo(int id, string weaponOwner)
    {
        _damageInfo = new DamageInfo(damage, id, weaponOwner);
    }

    public abstract void Shoot();

    public IEnumerator ReloadWeapon()
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
        }
        isReloading = false;
    }
}