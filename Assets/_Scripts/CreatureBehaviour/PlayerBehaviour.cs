using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] protected Weapon _weapon;
    [SerializeField] protected float _moveSpeed, _rotateSpeed;
    [SerializeField] protected Transform _shootDirection;

    private AnimationHandler _animHandler;

    private float ReloadTime;
    public float FireRate;

    private void Start()
    {
        _animHandler = GetComponent<AnimationHandler>();
    }

    private void FixedUpdate()
    {
        ReloadTime += Time.deltaTime;
    }

    public void MoveCharacter(Vector3 moveDirection)
    {
        var direction = new Vector3(moveDirection.x, 0, moveDirection.z);
        transform.position += direction * _moveSpeed * Time.deltaTime;
        _animHandler.PlayRunAnimation();
    }

    public void RotateCharacter(Vector3 moveDirection)
    {
        if (Vector3.Angle(transform.forward, moveDirection) > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public void VisualShootDirection(Vector3 shootDirection)
    {
        if (Vector3.Angle(_shootDirection.transform.forward, shootDirection) > 0)
        {
            _shootDirection.eulerAngles = new Vector3(90, 0, Mathf.Atan2(shootDirection.x, shootDirection.z) * -180 / Mathf.PI);
        }
    }

    public void IdleStateAnim()
    {
        _animHandler.PlayIdleAnimation(); //заглушка для сброса анимации бега
    }

    public void Shoot()
    {
        if (ReloadTime >= FireRate)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, _shootDirection.eulerAngles.y, transform.eulerAngles.z);
            _animHandler.PlayAttackAnimation();
            _weapon.ShootShotGun();
            ReloadTime = 0;
        }
    }
}