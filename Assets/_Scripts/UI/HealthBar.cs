using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] protected Image _progressBar;
    protected Animator _anim;

    protected VitalitySystem _vitalitySystem;
    private Canvas _parentCanvas;
    private Camera _camera;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _parentCanvas = GetComponentInParent<Canvas>();
        _vitalitySystem = GetComponentInParent<VitalitySystem>();
        _camera = Camera.main;

        _vitalitySystem.OnTakingHit += ChangeHPBar;
        _vitalitySystem.OnHealing += ChangeHPBar;
        _vitalitySystem.OnDeath += DestroyBar;
    }

    private void LateUpdate()
    {
        if (_camera != null)
            _parentCanvas.transform.LookAt(new Vector3(_parentCanvas.transform.position.x, -_camera.transform.position.y, _parentCanvas.transform.position.z));
    }
    private void ChangeHPBar()
    {
        _progressBar.fillAmount = _vitalitySystem.HealthPercentage;
        _anim.SetTrigger("TakeHit");
    }
    public virtual void DestroyBar()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _vitalitySystem.OnTakingHit -= ChangeHPBar;
        _vitalitySystem.OnHealing -= ChangeHPBar;
        _vitalitySystem.OnDeath -= DestroyBar;
    }
}