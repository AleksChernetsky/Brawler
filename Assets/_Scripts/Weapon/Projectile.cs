using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectileRigidbody;
    private float _timer;
    public DamageInfo _damageInfo;

    public Rigidbody RigidBody => _projectileRigidbody;
    public float DestroyTime = 3;

    public int Damage;

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= DestroyTime)
        {
            ResetProjectile();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out VitalitySystem vitalitySystem))
        {
            vitalitySystem.TakeDamage(_damageInfo);
            ResetProjectile();
        }
        else
        {
            ResetProjectile();
        }
    }

    private void ResetProjectile()
    {
        gameObject.SetActive(false);
        _timer = 0;
        _projectileRigidbody.velocity = Vector3.zero;
        _projectileRigidbody.position = Vector3.zero;
        _projectileRigidbody.rotation = Quaternion.identity;
    }
}