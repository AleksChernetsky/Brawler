using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class MovementHandler : JoystickHandler
{
    public float MoveSpeed;
    public MovementJoystick Joystick;
    private Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    protected override void HandleFingerDown(Finger touchedFinger)
    {
        if (MovementFinger == null && touchedFinger.screenPosition.x <= Screen.width / 2)
        {
            MovementFinger = touchedFinger;
            MovementDirection = Vector2.zero;
            Joystick.MovementJoystickBG.sizeDelta = JoystickSize;
            Joystick.MovementJoystickBG.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
        }
    }
    protected override void HandleFingerMove(Finger movedFinger)
    {
        if (movedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxKnobMovement = JoystickSize.x / 2f;
            ETouch.Touch currentTouch = movedFinger.currentTouch;

            if (Vector2.Distance(currentTouch.screenPosition, Joystick.MovementJoystickBG.anchoredPosition) > maxKnobMovement)
            {
                knobPosition = (currentTouch.screenPosition - Joystick.MovementJoystickBG.anchoredPosition).normalized * maxKnobMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.MovementJoystickBG.anchoredPosition;
            }

            Joystick.MovementJoystickKnob.anchoredPosition = knobPosition;
            MovementDirection = knobPosition / maxKnobMovement;
            MoveCharacter();
        }
    }
    protected override void HandleFingerUp(Finger lostFinger)
    {
        if (lostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.MovementJoystickBG.position = Joystick.MovementJoystickBGStartPosition;
            Joystick.MovementJoystickKnob.anchoredPosition = Vector2.zero;
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
