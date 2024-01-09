using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class MovementHandler : JoystickHandler
{
    [SerializeField] private RectTransform _movementJoystickBG;
    [SerializeField] private RectTransform _movementJoystickKnob;

    private Rigidbody _rigidBody;
    private Vector2 _movementJoystickBGStartPosition;

    public float MoveSpeed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _movementJoystickBGStartPosition = _movementJoystickBG.anchoredPosition;
    }

    private void Update()
    {
        if (MovementFinger != null && IsAiming == false)
        {
            MoveCharacter();
        }
    }

    protected override void HandleFingerDown(Finger touchedFinger)
    {
        if (MovementFinger == null && touchedFinger.screenPosition.x <= Screen.width / 2)
        {
            MovementFinger = touchedFinger;
            MovementDirection = Vector2.zero;
            _movementJoystickBG.sizeDelta = JoystickSize;
            _movementJoystickBG.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
        }
    }
    protected override void HandleFingerMove(Finger movedFinger)
    {
        if (movedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxKnobMovement = JoystickSize.x / 2f;
            ETouch.Touch currentTouch = movedFinger.currentTouch;

            if (Vector2.Distance(currentTouch.screenPosition, _movementJoystickBG.anchoredPosition) > maxKnobMovement)
                knobPosition = (currentTouch.screenPosition - _movementJoystickBG.anchoredPosition).normalized * maxKnobMovement;
            else
                knobPosition = currentTouch.screenPosition - _movementJoystickBG.anchoredPosition;

            _movementJoystickKnob.anchoredPosition = knobPosition;
            MovementDirection = knobPosition / maxKnobMovement;
        }
    }
    protected override void HandleFingerUp(Finger lostFinger)
    {
        if (lostFinger == MovementFinger)
        {
            MovementFinger = null;
            _movementJoystickBG.position = _movementJoystickBGStartPosition;
            _movementJoystickKnob.anchoredPosition = Vector2.zero;
            MovementDirection = Vector2.zero;
        }
    }

    private void MoveCharacter()
    {
        var direction = new Vector3(MovementDirection.x, 0, MovementDirection.y);
        _rigidBody.velocity = new Vector3(direction.x * MoveSpeed, 0, direction.z * MoveSpeed);
        transform.LookAt(transform.position + direction, Vector3.up);
    }
}
