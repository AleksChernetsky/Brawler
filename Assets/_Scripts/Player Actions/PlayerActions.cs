using UnityEngine;
using UnityEngine.AI;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private JoysticksHandler _joysticks;
    [SerializeField] private GameObject _shootDirection;

    private NavMeshAgent _agent;
    private AnimationHandler _animHandler;
    private WeaponMain _weaponMain;
    private VitalitySystem _vitalitySystem;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animHandler = GetComponent<AnimationHandler>();
        _weaponMain = GetComponent<WeaponMain>();
        _vitalitySystem = GetComponent<VitalitySystem>();

        _vitalitySystem.OnDeath += IsDead;
        _joysticks.OnCombatJoystickUp += Shoot;
    }
    private void Update()
    {
        if (!_joysticks.IsAiming)
        {
            MoveCharacter(_joysticks.MoveDirection);
        }
        else
        {
            RotateToShoot(_joysticks.RotateDirection);
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        _agent.destination = transform.position + new Vector3(direction.x, 0, direction.y);
        _agent.velocity = _agent.desiredVelocity;
        transform.LookAt(_agent.destination);
    }
    public void RotateToShoot(Vector2 direction)
    {
        transform.LookAt(transform.position + new Vector3(direction.x, 0, direction.y), Vector3.up);
        _shootDirection.SetActive(true);
        _animHandler.PlayAimmingAnim();
    }
    private void Shoot()
    {
        _weaponMain.Shoot();
        _shootDirection.SetActive(false);
    }
    private void IsDead()
    {
        _joysticks.gameObject.SetActive(false);
    }
}
