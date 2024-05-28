using UnityEngine;
using UnityEngine.Events;

public class GlobalEventsManager
{
    public static readonly UnityEvent OnCharRegister = new UnityEvent();
    public static readonly UnityEvent OnCharDelete = new UnityEvent();

    public static readonly UnityEvent<WeaponType, Sprite> OnKillInfo = new UnityEvent<WeaponType, Sprite>();

    public static readonly UnityEvent OnButtonClick = new UnityEvent();
    public static readonly UnityEvent OnBattleState = new UnityEvent();
    public static readonly UnityEvent OnPauseState = new UnityEvent();
}
