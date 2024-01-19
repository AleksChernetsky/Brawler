using UnityEngine;
using UnityEngine.AI;

public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;
    private float _speed;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        VitalitySystem.OnTakingDamage += PlayTakingHitAnim;
        VitalitySystem.OnEnemyDied += PlayDeathAnim;
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

    public void PlayAimmingAnim() => _animator.Play("Base Layer.Rifle Aiming Idle", 0);
    public void PlayFiringAnim() => _animator.Play("Base Layer.Firing Rifle", 0);
    public void PlayTakingHitAnim()
    {
        if (_speed >= 0.05)
            _animator.Play("Base Layer.Hit Reaction Run", 0);
        else
            _animator.Play("Base Layer.Hit Reaction Idle", 0);
    }
    public void PlayDeathAnim()
    {
        if (_speed >= 0.05)
            _animator.Play("Base Layer.Rifle Run To Dying", 0);
        else
            _animator.Play("Base Layer.Death From Back Headshot", 0);
    }
}
