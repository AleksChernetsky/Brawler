using System;

using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class JoysticksHandler : MonoBehaviour
{
    [SerializeField] private RectTransform _movementArea;
    [SerializeField] private RectTransform _combatArea;

    [SerializeField] private RectTransform _moveJoystickBG;
    [SerializeField] private RectTransform _combatJoystickBG;

    private float joystickShootPosition = 0.98f; // calibrated - shoot on joystich up on edge
    private Joystick _joystick;
    private Vector2 _moveJoystickStartPosition;
    private Vector2 _combatJoystickStartPosition;
    private bool _isCombatFingerActive;

    public Vector2 MoveDirection => _joystick.GamePlay.Movement.ReadValue<Vector2>();
    public Vector2 RotateDirection => _joystick.GamePlay.Combat.ReadValue<Vector2>();

    public event Action OnJoystickUpToShoot;
    public event Action OnJoystickUpToRelease;

    private void Awake()
    {
        _joystick = new Joystick();
        _joystick.Enable();

        _moveJoystickStartPosition = _moveJoystickBG.anchoredPosition;
        _combatJoystickStartPosition = _combatJoystickBG.anchoredPosition;
    }
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
    private void OnJoystickDown(Finger touchedFinger)
    {
        if (touchedFinger.screenPosition.x <= _movementArea.anchorMax.x * Screen.width && touchedFinger.screenPosition.y <= _movementArea.anchorMax.y * Screen.height)
        {
            _moveJoystickBG.position = touchedFinger.screenPosition;
            _isCombatFingerActive = false;
        }

        if (touchedFinger.screenPosition.x >= _combatArea.anchorMin.x * Screen.width && touchedFinger.screenPosition.y <= _combatArea.anchorMax.y * Screen.height)
        {
            _combatJoystickBG.position = touchedFinger.screenPosition;
            _isCombatFingerActive = true;
        }
    }
    private void OnJoystickUp(Finger releasedFinger)
    {
        if (RotateDirection.magnitude >= joystickShootPosition)
        {
            OnJoystickUpToShoot?.Invoke();
        }
        if (_isCombatFingerActive && RotateDirection.magnitude < joystickShootPosition)
        {
            OnJoystickUpToRelease?.Invoke();
        }
        _moveJoystickBG.anchoredPosition = _moveJoystickStartPosition;
        _combatJoystickBG.anchoredPosition = _combatJoystickStartPosition;
    }
}