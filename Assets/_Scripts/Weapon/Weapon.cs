using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _weaponMuzzle;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;
    [SerializeField] private float _force;
    [SerializeField, Range(1, 2)] private float _forceRandomFactor;
    [SerializeField] private int _projectileCount;
    [SerializeField] private float _spreadFactor;

    public void Shoot()
    {
        _weaponMuzzle.Rotate(new Vector3(0, -_spreadFactor, 0));
        for (int i = 0; i < _projectileCount; i++)
        {
            _weaponMuzzle.Rotate(new Vector3(0, _spreadFactor / (_projectileCount / 2), 0));
            GameObject bullet = ObjectPool.Instance.GetFreeElement();

            if (bullet != null)
            {
                bullet.transform.SetPositionAndRotation(_weaponMuzzle.position, _weaponMuzzle.rotation);
                bullet.SetActive(true);
                bullet.TryGetComponent(out Projectile projectile);
                projectile.RigidBody.AddForce(_weaponMuzzle.transform.forward * Random.Range(_force, _force * _forceRandomFactor), _forceMode);
            }
        }
        _weaponMuzzle.Rotate(new Vector3(0, -_spreadFactor, 0));
    }
}
