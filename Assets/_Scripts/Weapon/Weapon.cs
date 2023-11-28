using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    public DamageConfigScriptableObject DamageConfig;

    public void ShootRifle()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * DamageConfig.ProjectileSpeed;
        Destroy(bullet, 1.5f);
    }

    public void ShootShotGun()
    {
        for (int i = 0; i < DamageConfig.BulletsAmount; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

            Vector3 dir = transform.forward + new Vector3(Random.Range(DamageConfig.HorizontalBulletsSpread, DamageConfig.HorizontalBulletsSpread),
                Random.Range(-DamageConfig.VerticalBulletsSpread, DamageConfig.VerticalBulletsSpread), 0);
            
            bullet.GetComponent<Rigidbody>().AddForce(dir * DamageConfig.ProjectileSpeed, ForceMode.Impulse);
        }
    }
}
