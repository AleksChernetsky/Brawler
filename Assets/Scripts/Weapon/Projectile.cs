using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectileRigidbody;
    private float _timer;
    private float _timeToDestroy = 0.5f;

    public DamageInfo DamageInfo;
    public Rigidbody RigidBody => _projectileRigidbody;
    public int Damage { get; set; }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeToDestroy)
        {
            ResetProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out VitalitySystem vitalitySystem))
        {
            vitalitySystem.TakeDamage(DamageInfo);
            ResetProjectile();
        }
        else
        {
            ResetProjectile();
        }
    }

    public void ResetProjectile()
    {
        _timer = 0;
        _projectileRigidbody.velocity = Vector3.zero;
        _projectileRigidbody.position = Vector3.zero;
        _projectileRigidbody.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
}