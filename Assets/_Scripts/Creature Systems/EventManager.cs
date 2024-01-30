using UnityEngine.Events;

public class EventManager
{
    public static readonly UnityEvent<int> CoinPickedUo = new UnityEvent<int>();

    static public void CallCoinPickedUp(int reward)
    {
        CoinPickedUo.Invoke(reward);
    }
}
