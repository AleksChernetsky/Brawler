using UnityEngine.Events;

public class GlobalEventsManager
{
    public static readonly UnityEvent OnCharRegister = new UnityEvent();
    public static readonly UnityEvent OnCharDelete = new UnityEvent();
    public static readonly UnityEvent<string, string> OnCharDeath = new UnityEvent<string, string>();
}
