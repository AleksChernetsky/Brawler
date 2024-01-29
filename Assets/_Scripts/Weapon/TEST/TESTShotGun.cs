using UnityEngine;

public class TESTShotGun : TESTWeapon
{
    [Header("ShotGun values")]
    [SerializeField] private int pelletsPerShot;

    private void Start()
    {
        typeOfGun = WeaponType.Shotgun;
    }
    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }    
}
