using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _projectileSpeed;

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * _projectileSpeed;
        Destroy(bullet, 1.5f);
    }

    public void ShootRifle()
    {
        Shoot();
    }

    public void ShootShotGun()
    {
        var bulletCount = 0;
        while (bulletCount < 10)
        {
            Shoot();
            bulletCount++;
        }
    }
}
