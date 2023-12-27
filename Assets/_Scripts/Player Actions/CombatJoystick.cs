using UnityEngine;

public class CombatJoystick : MonoBehaviour
{
    [HideInInspector] public Vector2 CombatJoystickBGStartPosition;
    [HideInInspector] public RectTransform CombatJoystickBG;
    public RectTransform CombatJoystickKnob;

    private void Awake()
    {
        CombatJoystickBG = GetComponent<RectTransform>();
        CombatJoystickBGStartPosition = CombatJoystickBG.anchoredPosition;
    }
}