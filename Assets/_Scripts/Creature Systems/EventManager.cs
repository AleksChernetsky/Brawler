using UnityEngine.Events;

public class EventManager
{
    public static readonly UnityEvent OnCharRegister = new UnityEvent();
    public static readonly UnityEvent OnCharDelete = new UnityEvent();

    static public void CallOnCharRegister() => OnCharRegister?.Invoke();
    static public void CallOnCharDelete() => OnCharDelete?.Invoke();
}
