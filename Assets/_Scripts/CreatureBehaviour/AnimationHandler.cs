using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger("Shoot");
        _animator.SetBool("IsRunning", false);
    }
    public void PlayRunAnimation()
    {
        _animator.SetBool("IsRunning", true);
        _animator.SetBool("IsIdle", false);
    }
    public void PlayIdleAnimation()
    {
        _animator.SetBool("IsIdle", true);
        _animator.SetBool("IsRunning", false);
    }
}
