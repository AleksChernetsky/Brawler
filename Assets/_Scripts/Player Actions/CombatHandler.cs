using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class CombatHandler : JoystickHandler
{
    [SerializeField] private GameObject _shootDirection;
    [SerializeField] protected Weapon _weapon;
    public CombatJoystick Joystick;

    protected override void HandleFingerDown(Finger touchedFinger)
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
    protected override void HandleFingerMove(Finger movedFinger)
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
    protected override void HandleFingerUp(Finger lostFinger)
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
