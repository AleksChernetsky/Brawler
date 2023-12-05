using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField, Min(0f)] private float _damage = 10f;

    [Header("Ray")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField, Min(0f)] private float _distance = Mathf.Infinity;
    [SerializeField, Min(0f)] private int _shotCount = 1;

    [Header("Spread")]
    [SerializeField] private bool _useSpread;
    [SerializeField, Min(0f)] private float _spreadFactor = 1f;

    public void Shoot()
    {
        for (int i = 0; i < _shotCount; i++)
        {
            Attack();
            Debug.Log("Shoot");
        }
    }

    private void Attack()
    {
        var direction = _useSpread ? transform.forward + CalculateSpread() : transform.forward;
        var ray = new Ray(transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _layerMask))
        {
            var hitCollider = hitInfo.collider;

            if (hitCollider.TryGetComponent(out VitalitySystem vitalitySystem))
            {
                vitalitySystem.TakeDamage(_damage);
                Debug.Log("Attack");
            }
        }
    }

    private Vector3 CalculateSpread()
    {
        return new Vector3
        {
            x = Random.Range(-_spreadFactor, _spreadFactor),
            y = Random.Range(-_spreadFactor, _spreadFactor),
            z = 0
        };
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        var ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out var hitInfo, _distance, _layerMask))
        {
            DrawRay(ray, hitInfo.point, hitInfo.distance, Color.red);
        }
        else
        {
            var hitPosition = ray.origin + ray.direction * _distance;
            DrawRay(ray, hitPosition, _distance, Color.green);
        }
    }

    private static void DrawRay(Ray ray, Vector3 hitPosition, float distance, Color color)
    {
        const float hitPointRadius = 0.15f;

        Debug.DrawRay(ray.origin, ray.direction * distance, color);

        Gizmos.color = color;
        Gizmos.DrawSphere(hitPosition, hitPointRadius);
    }
}
#endif