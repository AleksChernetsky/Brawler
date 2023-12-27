using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;

using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerCombatActions : MonoBehaviour
{
    [SerializeField] private GameObject _shootDirection;
    [SerializeField] protected Weapon _weapon;

    private Vector2 JoystickSize = new Vector2(200, 200);
    private Finger CombatFinger;
    private Vector2 AimDirection;

    public CombatJoystick Joystick;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleFingerUp;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }
    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleFingerUp;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void HandleFingerDown(Finger touchedFinger)
    {
        if (CombatFinger == null && touchedFinger.screenPosition.x >= Screen.width / 2)
        {
            CombatFinger = touchedFinger;
            AimDirection = Vector2.zero;
            Joystick.CombatJoystickBG.sizeDelta = JoystickSize;
            Joystick.CombatJoystickBG.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
            _shootDirection.SetActive(true);
        }
    }
    private void HandleFingerMove(Finger movedFinger)
    {
        if (movedFinger == CombatFinger)
        {
            Vector2 knobPosition;
            float maxKnobMovement = JoystickSize.x / 2f;
            ETouch.Touch currentTouch = movedFinger.currentTouch;

            if (Vector2.Distance(currentTouch.screenPosition, Joystick.CombatJoystickBG.anchoredPosition) > maxKnobMovement)
                knobPosition = (currentTouch.screenPosition - Joystick.CombatJoystickBG.anchoredPosition).normalized * maxKnobMovement;
            else
                knobPosition = currentTouch.screenPosition - Joystick.CombatJoystickBG.anchoredPosition;

            VisualShootDirection(new Vector3(knobPosition.x, 0, knobPosition.y));
            Joystick.CombatJoystickKnob.anchoredPosition = knobPosition;
            AimDirection = knobPosition / maxKnobMovement;
        }
    }
    private void HandleFingerUp(Finger lostFinger)
    {
        if (lostFinger == CombatFinger)
        {
            CombatFinger = null;
            Joystick.CombatJoystickBG.position = Joystick.CombatJoystickBGStartPosition;
            Joystick.CombatJoystickKnob.anchoredPosition = Vector2.zero;
            ShootDirection();
            AimDirection = Vector2.zero;
            _shootDirection.SetActive(false);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        if (startPosition.x < JoystickSize.x / 2)
        {
            startPosition.x = JoystickSize.x / 2;
        }

        if (startPosition.y < JoystickSize.y / 2)
        {
            startPosition.y = JoystickSize.y / 2;
        }
        else if (startPosition.y > Screen.height - JoystickSize.y / 2)
        {
            startPosition.y = Screen.height - JoystickSize.y / 2;
        }

        return startPosition;
    }
    private void VisualShootDirection(Vector3 shootDirection)
    {
        if (Vector3.Angle(_shootDirection.transform.forward, shootDirection) > 0)
        {
            _shootDirection.transform.eulerAngles = new Vector3(90, 0, Mathf.Atan2(shootDirection.x, shootDirection.z) * -180 / Mathf.PI);
        }
    }
    public void ShootDirection()
    {
        var direction = new Vector3(AimDirection.x, 0, AimDirection.y).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        _weapon.Shoot();
    }
}
