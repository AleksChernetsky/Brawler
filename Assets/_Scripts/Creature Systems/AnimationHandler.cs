using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private Vector3 _previousPosition;
    private float _speed;
    public float SpeedLerp;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    public void UpdateMovement()
    {
        _speed = Mathf.Lerp(_speed, (transform.position - _previousPosition).magnitude / Time.deltaTime, SpeedLerp);
        _previousPosition = transform.position;

        _animator.SetFloat("Speed", _speed);
        StopAttackAnimation();
    }

    public void PlayAttackAnimation() => _animator.SetBool("Shoot", true);
    public void StopAttackAnimation() => _animator.SetBool("Shoot", false); // set event in attack animation on last frame
    public void PlayAimingAnimation() => _animator.SetBool("Aiming", true);
    public void StopAimingAnimation() => _animator.SetBool("Aiming", false);
}
