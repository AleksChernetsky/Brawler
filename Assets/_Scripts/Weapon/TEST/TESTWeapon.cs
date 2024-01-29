using System;
using System.Collections;

using UnityEngine;

public enum WeaponType { Shotgun, MachineGun, SniperRifle, SMG, Melee };
public abstract class TESTWeapon : MonoBehaviour
{
    [Header("Basic values")]
    [SerializeField] protected Transform weaponMuzzle;
    [SerializeField] protected Projectile projectilePrefab;
    //[SerializeField] protected AudioClip shotSound = null;
    //[SerializeField] protected Renderer muzzleFlash = null; 
    protected WeaponType typeOfGun;
    protected string weaponOwner;

    [Header("Basic stats")]
    [SerializeField] protected int damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float baseSpread;
    [SerializeField] protected int bulletSpeed;

    [Header("Reload stats")]
    [SerializeField] protected int numberOfClips;
    [SerializeField] protected int clipsLeft;
    [SerializeField] protected int bulletsPerClip;
    [SerializeField] protected int bulletsLeft;
    [SerializeField] protected float clipReloadTime;
    [SerializeField] protected bool isReloading;
    [SerializeField] protected bool isFiring;

    protected DamageInfo _damageInfo;
    //private int lastFrameShot = -1;       // last frame a shot was fired
    //public float nextFireTime = 0.0f;      // able to fire again on this frame


    protected void Awake()
    {
        bulletsLeft = bulletsPerClip;
        clipsLeft = numberOfClips;
        weaponOwner = gameObject.name;
        GetComponent<VitalitySystem>().OnRegister += ShooterInfo;
    }
    private void ShooterInfo(int id, string weaponOwner)
    {
        _damageInfo = new DamageInfo(damage, id, weaponOwner);
    }

    public abstract void Shoot();
    public event Action OnShootEvent;
    public void CallOnShootEvent() => OnShootEvent?.Invoke();

    public IEnumerator ReloadWeapon()
    {
        if (isReloading)
        {
            yield break; // if already reloading... exit and wait till reload is finished
        }

        while (bulletsLeft < bulletsPerClip)
        {
            isReloading = true; // we are now reloading
            yield return new WaitForSeconds(clipReloadTime); // wait for set reload time
            bulletsLeft++; // take away a clip
        }
        while (bulletsLeft == bulletsPerClip)
        {
            clipsLeft++;
        }

        isReloading = false; // done reloading
    }
}