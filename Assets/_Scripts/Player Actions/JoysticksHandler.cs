using System;

using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class JoysticksHandler : MonoBehaviour
{
    [SerializeField] private RectTransform _moveJoystickBG;
    [SerializeField] private RectTransform _combatJoystickBG;

    private Joystick _joystick;

    private Vector2 _moveJoystickStartPosition;
    private Vector2 _combatJoystickStartPosition;

    public bool IsAiming { get; private set; }
    public Vector2 MoveDirection => _joystick.GamePlay.Movement.ReadValue<Vector2>();
    public Vector2 RotateDirection => _joystick.GamePlay.Combat.ReadValue<Vector2>();

    public event Action OnCombatJoystickUp;

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
        if (touchedFinger.screenPosition.x <= Screen.width / 2)
        {
            _moveJoystickBG.position = touchedFinger.screenPosition;
        }
        else
        {
            _combatJoystickBG.position = touchedFinger.screenPosition;
            IsAiming = true;
        }
    }
    private void OnJoystickUp(Finger releasedFinger)
    {
        _moveJoystickBG.anchoredPosition = _moveJoystickStartPosition;
        _combatJoystickBG.anchoredPosition = _combatJoystickStartPosition;

        if (IsAiming)
        {
            OnCombatJoystickUp?.Invoke();
            IsAiming = false;
        }
    }
}