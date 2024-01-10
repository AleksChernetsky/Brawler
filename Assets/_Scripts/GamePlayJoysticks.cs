using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class GamePlayJoysticks : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private RectTransform _movementJoystickBackGround;
    [SerializeField] private RectTransform _combatJoystickBackGround;

    [SerializeField] private GameObject _shootDirection;

    private Vector2 _movementJoystickStartPosition;
    private Vector2 _combatJoystickStartPosition;

    private Joystick _joystick;
    private Rigidbody _rigidBody;
    private bool IsAiming;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();

        _joystick = new Joystick();
        _joystick.Enable();

        _movementJoystickStartPosition = _movementJoystickBackGround.anchoredPosition;
        _combatJoystickStartPosition = _combatJoystickBackGround.anchoredPosition;
    }

    private void Update()
    {
        if (IsAiming)
        {
            RotateToShoot();
        }
        else
        {
            MoveCharacter();
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
        EnhancedTouchSupport.Disable();
        ETouch.Touch.onFingerDown -= OnJoystickDown;
        ETouch.Touch.onFingerUp -= OnJoystickUp;
    }

    //-----------------------------------------------------------------------------------------------------------------

    private void OnJoystickDown(Finger touchedFinger)
    {
        if (touchedFinger.screenPosition.x <= Screen.width / 2)
        {
            IsAiming = false;
            _movementJoystickBackGround.anchoredPosition = touchedFinger.screenPosition;
        }
        else
        {
            IsAiming = true;
            _shootDirection.SetActive(true);
            _combatJoystickBackGround.anchoredPosition = touchedFinger.screenPosition;
        }
    }
    private void OnJoystickUp(Finger releasedFinger)
    {
        _movementJoystickBackGround.anchoredPosition = _movementJoystickStartPosition;
        _combatJoystickBackGround.anchoredPosition = _combatJoystickStartPosition;
        _shootDirection.SetActive(false);
    }

    //-----------------------------------------------------------------------------------------------------------------

    private void MoveCharacter()
    {
        var inputDirection = _joystick.GamePlay.Movement.ReadValue<Vector2>();
        _rigidBody.velocity = new Vector3(inputDirection.x * _moveSpeed, 0, inputDirection.y * _moveSpeed);
        transform.LookAt(transform.position + new Vector3(inputDirection.x, 0, inputDirection.y), Vector3.up);
    }
    public void RotateToShoot()
    {
        var inputDirection = _joystick.GamePlay.Combat.ReadValue<Vector2>();
        transform.LookAt(transform.position + new Vector3(inputDirection.x, 0, inputDirection.y), Vector3.up);
    }
}
