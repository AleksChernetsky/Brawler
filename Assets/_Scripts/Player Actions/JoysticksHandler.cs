using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;

using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class JoysticksHandler : MonoBehaviour
{
    [SerializeField] private RectTransform _movementJoystickBackGround;
    [SerializeField] private RectTransform _combatJoystickBackGround;
    [SerializeField] private GameObject _shootDirection;

    //private IWeapon _weapon;
    private RangeWeapon _weapon;

    private Vector2 _movementJoystickStartPosition;
    private Vector2 _combatJoystickStartPosition;

    private AnimationHandler _animHandler;
    private Joystick _joystick;
    private NavMeshAgent _agent;

    private bool IsAiming;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animHandler = GetComponent<AnimationHandler>();
        //_weapon = GetComponent<IWeapon>();
        _weapon = GetComponent<RangeWeapon>();

        _joystick = new Joystick();
        _joystick.Enable();

        _movementJoystickStartPosition = _movementJoystickBackGround.anchoredPosition;
        _combatJoystickStartPosition = _combatJoystickBackGround.anchoredPosition;
    }

    private void Update()
    {
        if (!IsAiming)
        {
            MoveCharacter();
        }
        else
        {
            RotateToShoot();
        }
    }

    //-----------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += OnJoystickDown;
        ETouch.Touch.onFingerUp += OnJoystickUp;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= OnJoystickDown;
        ETouch.Touch.onFingerUp -= OnJoystickUp;
        EnhancedTouchSupport.Disable();
    }

    //-----------------------------------------------------------------------------------------------------------------

    private void OnJoystickDown(Finger touchedFinger)
    {
        if (touchedFinger.screenPosition.x <= Screen.width / 2)
        {
            _movementJoystickBackGround.anchoredPosition = touchedFinger.screenPosition;
        }
        else
        {
            IsAiming = true;
            _combatJoystickBackGround.anchoredPosition = touchedFinger.screenPosition;
        }
    }
    private void OnJoystickUp(Finger releasedFinger)
    {
        _movementJoystickBackGround.anchoredPosition = _movementJoystickStartPosition;
        _combatJoystickBackGround.anchoredPosition = _combatJoystickStartPosition;

        if (IsAiming)
        {
            Shoot();
        }
    }

    //-----------------------------------------------------------------------------------------------------------------

    private void MoveCharacter()
    {
        var inputDirection = _joystick.GamePlay.Movement.ReadValue<Vector2>();
        _agent.destination = transform.position + new Vector3(inputDirection.x, 0, inputDirection.y);
        transform.LookAt(_agent.destination);
    }
    public void RotateToShoot()
    {
        var inputDirection = _joystick.GamePlay.Combat.ReadValue<Vector2>();
        transform.LookAt(transform.position + new Vector3(inputDirection.x, 0, inputDirection.y), Vector3.up);
        _shootDirection.SetActive(true);
        _animHandler.PlayAimmingAnim();
    }
    private void Shoot()
    {
        _weapon.Shoot();
        _shootDirection.SetActive(false);
        IsAiming = false;
    }
}
