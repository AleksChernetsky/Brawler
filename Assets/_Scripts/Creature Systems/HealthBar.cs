using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    private VitalitySystem _vitalitySystem;

    private Camera _camera;

    private void Start()
    {
        _vitalitySystem = GetComponentInParent<VitalitySystem>();
        _camera = Camera.main;
        _vitalitySystem.OnTakingHit += ChangeHPBar;
        _vitalitySystem.OnDeath += DestroyHPBar;
    }

    private void ChangeHPBar() => healthBar.fillAmount = _vitalitySystem.CurrentHealthPercent;
    private void DestroyHPBar() => Destroy(gameObject);

    private void LateUpdate()
    {
        if (_camera != null)
        {
            transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, transform.position.z));
        }
    }

    private void OnDestroy()
    {
        _vitalitySystem.OnTakingHit -= ChangeHPBar;
    }
}