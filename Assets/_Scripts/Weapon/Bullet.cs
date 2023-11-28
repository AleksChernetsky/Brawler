using UnityEngine;

public class Bullet : MonoBehaviour
{
    public DamageConfigScriptableObject DamageConfig;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<VitalitySystem>(out VitalitySystem component))
        {
            component.TakeDamage(DamageConfig.Damage);
            Destroy(gameObject);
        }

        Destroy(gameObject, 1f);
    }
}