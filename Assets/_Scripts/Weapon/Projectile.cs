using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectileRigidbody;
    private float _timer;

    public Rigidbody RigidBody => _projectileRigidbody;
    public float DestroyTime = 3;

    public float Damage;

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= DestroyTime)
        {
            gameObject.SetActive(false);
            ResetProjectile();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out VitalitySystem vitalitySystem))
        {
            vitalitySystem.TakeDamage(Damage);
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
        _projectileRigidbody.velocity = new Vector3(0, 0, 0);
    }
}