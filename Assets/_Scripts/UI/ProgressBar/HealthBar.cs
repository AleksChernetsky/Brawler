using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] protected Image _progressBar;
    [SerializeField] private Transform _parentCanvas;
    protected Animator _anim;
    protected VitalitySystem _vitalitySystem;

    private Camera _camera;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _vitalitySystem = GetComponentInParent<VitalitySystem>();
        _camera = Camera.main;
        _vitalitySystem.OnTakingHit += ChangeHPBar;
        _vitalitySystem.OnHealing += ChangeHPBar;
        _vitalitySystem.OnDeath += DestroyBar;
    }

    private void LateUpdate()
    {
        if (_camera != null)
            _parentCanvas.LookAt(new Vector3(_parentCanvas.position.x, -_camera.transform.position.y, _parentCanvas.position.z));
    }
    private void ChangeHPBar()
    {
        _progressBar.fillAmount = _vitalitySystem.HealthPercentage;
        _anim.SetTrigger("TakeHit");
    }
    public virtual void DestroyBar() => Destroy(gameObject);

    private void OnDestroy()
    {
        _vitalitySystem.OnTakingHit -= ChangeHPBar;
        _vitalitySystem.OnHealing -= ChangeHPBar;
        _vitalitySystem.OnDeath -= DestroyBar;
    }
}