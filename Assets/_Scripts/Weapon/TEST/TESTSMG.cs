using System.Collections;

using UnityEngine;

public class TESTSMG : TESTWeapon
{
    [Header("Automatic Rifle values")]
    [SerializeField] private int roundsPerBurst;
    [SerializeField] private float lagBetweenShots;

    private void Start()
    {
        typeOfGun = WeaponType.SMG;
    }
    public override void Shoot()
    {
        if (clipsLeft > 0)
        {
            StartCoroutine(Burst_Fire());
            clipsLeft--;
            //bulletsLeft = bulletsPerClip;
        }

    }

    IEnumerator Burst_Fire()
    {
        int shotCounter = 0;

        while (shotCounter < roundsPerBurst)
        {
            isFiring = true;
            SpawnProjectile();
            shotCounter++;
            bulletsLeft--;
            yield return new WaitForSeconds(lagBetweenShots);
        }
        if (bulletsLeft <= 0)
        {
            StartCoroutine(ReloadWeapon());
            yield break;
        }
        isFiring = false;
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