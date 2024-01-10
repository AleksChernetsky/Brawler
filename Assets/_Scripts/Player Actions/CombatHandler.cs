using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class CombatHandler : JoystickHandler
{
    [SerializeField] private RectTransform _combatJoystickBG;
    [SerializeField] private RectTransform _combatJoystickKnob;
    [SerializeField] private GameObject _shootDirection;
    [SerializeField] private Weapon _weapon;

    private AnimationHandler _animationHandler;
    private Vector2 _combatJoystickBGStartPosition;

    private void Awake()
    {
        _animationHandler = GetComponent<AnimationHandler>();
        _combatJoystickBGStartPosition = _combatJoystickBG.anchoredPosition;
    }
    private void Update()
    {
        if (CombatFinger != null)
        {
            RotateToShoot();
        }
    }

    // -----------------------------------------------------------------------------------------------------------------

    protected override void HandleFingerDown(Finger touchedFinger)
    {
        if (CombatFinger == null && touchedFinger.screenPosition.x >= Screen.width / 2)
        {
            IsAiming = true;
            CombatFinger = touchedFinger;
            AimDirection = Vector2.zero;
            _combatJoystickBG.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
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

            if (Vector2.Distance(currentTouch.screenPosition, _combatJoystickBG.anchoredPosition) > maxKnobMovement)
                knobPosition = (currentTouch.screenPosition - _combatJoystickBG.anchoredPosition).normalized * maxKnobMovement;
            else
                knobPosition = currentTouch.screenPosition - _combatJoystickBG.anchoredPosition;

            _combatJoystickKnob.anchoredPosition = knobPosition;
            AimDirection = knobPosition / maxKnobMovement;
        }
    }
    protected override void HandleFingerUp(Finger lostFinger)
    {
        if (lostFinger == CombatFinger)
        {
            Shoot();
            IsAiming = false;
            CombatFinger = null;
            _combatJoystickBG.position = _combatJoystickBGStartPosition;
            _combatJoystickKnob.anchoredPosition = Vector2.zero;
            AimDirection = Vector2.zero;
            _shootDirection.SetActive(false);
        }
    }

    // -----------------------------------------------------------------------------------------------------------------

    public void RotateToShoot()
    {
        var direction = new Vector3(AimDirection.x, 0, AimDirection.y).normalized;
        transform.LookAt(transform.position + direction, Vector3.up);
        _animationHandler.PlayAimingAnimation();
    }
    private void Shoot()
    {
        _weapon.Shoot();
        _animationHandler.StopAimingAnimation();
        _animationHandler.PlayAttackAnimation();
    }
}
