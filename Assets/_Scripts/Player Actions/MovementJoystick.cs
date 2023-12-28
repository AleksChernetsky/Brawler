using UnityEngine;

public class MovementJoystick : MonoBehaviour
{
    [HideInInspector] public Vector2 MovementJoystickBGStartPosition;
    [HideInInspector] public RectTransform MovementJoystickBG;
    public RectTransform MovementJoystickKnob;

    private void Awake()
    {
        MovementJoystickBG = GetComponent<RectTransform>();
        MovementJoystickBGStartPosition = MovementJoystickBG.anchoredPosition;
    }
}