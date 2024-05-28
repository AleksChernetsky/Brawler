using UnityEngine;
using UnityEngine.AI;

public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;
    private WeaponMain _weaponMain;
    private VitalitySystem _vitalitySystem;
    private float _speed;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _vitalitySystem = GetComponent<VitalitySystem>();
        _weaponMain = GetComponent<WeaponMain>();

        _weaponMain.OnShoot += PlayShootingAnim;
        _weaponMain.OnFistAttack += FistAttack;
        _weaponMain.OnClawAttack += ClawAttack;

        _vitalitySystem.OnTakingHit += PlayTakingHitAnim;
        _vitalitySystem.OnDeath += PlayDeathAnim;
    }

    private void Update()
    {
        UpdateMovement();
    }

    public void UpdateMovement()
    {
        _speed = _agent.velocity.magnitude;
        _animator.SetFloat("Speed", _speed);
    }
    public void PlayAimmingAnim() => _animator.Play("Base Layer.Rifle Aiming Idle");
    public void PlayShootingAnim() => _animator.Play("Base Layer.Firing Rifle");
    public void PlayTakingHitAnim()
    {
        if (_speed >= 0.05)
            _animator.Play("Base Layer.Hit Reaction Run");
    }
    public void PlayDeathAnim()
    {
        if (_speed >= 0.05)
            _animator.Play("Base Layer.Rifle Run To Dying");
        else
            _animator.Play("Base Layer.Death From Back Headshot");
    }

    public void FistAttack() => _animator.SetTrigger("FistAttack");
    public void ClawAttack() => _animator.SetTrigger("ClawAttack");
}
