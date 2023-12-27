using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;

    private Vector2 JoystickSize = new Vector2(200, 200);
    private Finger MovementFinger;
    private Vector2 MovementDirection;
    private Rigidbody _rigidBody;

    public MovementJoystick Joystick;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

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
        if (MovementFinger == null && touchedFinger.screenPosition.x <= Screen.width / 2)
        {
            MovementFinger = touchedFinger;
            MovementDirection = Vector2.zero;
            Joystick.MovementJoystickBG.sizeDelta = JoystickSize;
            Joystick.MovementJoystickBG.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
        }
    }
    private void HandleFingerMove(Finger movedFinger)
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
    private void HandleFingerUp(Finger lostFinger)
    {
        if (lostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.MovementJoystickBG.position = Joystick.MovementJoystickBGStartPosition;
            Joystick.MovementJoystickKnob.anchoredPosition = Vector2.zero;
            MovementDirection = Vector2.zero;
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
    private void MoveCharacter()
    {
        var direction = new Vector3(MovementDirection.x, 0, MovementDirection.y);
        _rigidBody.velocity = new Vector3(direction.x * MoveSpeed, 0, direction.z * MoveSpeed);
        transform.LookAt(transform.position + direction, Vector3.up);
    }
}