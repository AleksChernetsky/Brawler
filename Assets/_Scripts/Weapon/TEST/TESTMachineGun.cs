using System.Collections;

using UnityEngine;

public class TESTMachineGun : TESTWeapon
{
    //[Header("Automatic Rifle values")]
    //[SerializeField] private int roundsPerBurst;
    //[SerializeField] private float lagBetweenShots;
    //[SerializeField] private bool isFiring;

    //private void Start()
    //{
    //    typeOfGun = WeaponType.MachineGun;
    //}
    public override void Shoot()
    {
        //if (bulletsLeft <= 0)
        //{
        //    StartCoroutine(Burst_Fire());
        //    return;
        //}
    }

    //IEnumerator Burst_Fire()
    //{
    //    int shotCounter = 0;

    //    if (bulletsLeft <= 0)
    //    {
    //        StartCoroutine(ReloadWeapon());
    //        yield break;
    //    }

    //    if (Time.time - fireRate > nextFireTime)
    //        nextFireTime = Time.time - Time.deltaTime;

    //    while (nextFireTime < Time.time)
    //    {
    //        while (shotCounter < roundsPerBurst)
    //        {
    //            isFiring = true;

    //            shotCounter++;
    //            bulletsLeft--;
    //            yield return new WaitForSeconds(lagBetweenShots);
    //        }
    //        nextFireTime += fireRate;
    //    }
    //    isFiring = false;
    //}
}
