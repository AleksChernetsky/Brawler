using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidBody;
    private float speed;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    public void UpdateMovement()
    {
        speed = _rigidBody.velocity.magnitude;
        _animator.SetFloat("Speed", speed);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger("Shoot");
    }
}
