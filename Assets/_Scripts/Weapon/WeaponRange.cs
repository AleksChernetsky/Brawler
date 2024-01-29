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
    [SerializeField] protected int numberOfClips;
    [SerializeField] protected float clipReloadTime;
    protected int clipsLeft;
    protected bool isReloading;
    protected bool isFiring;

    protected void Start()
    {
        clipsLeft = numberOfClips;
    }
    
    public override void Shoot() { }

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