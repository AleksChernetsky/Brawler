using System;

using TMPro;

using UnityEngine;

public class PlayersCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private string _template;
    private int _count;

    public event Action OnOneEnemyLeft;

    private void Awake()
    {
        GlobalEventsManager.OnCharRegister.AddListener(Increase);
        GlobalEventsManager.OnCharDelete.AddListener(Decrease);
    }
    private void Increase()
    {
        _count++;
        SetCount();
    }
    private void Decrease()
    {
        _count--;
        SetCount();

        if (_count <= 1)
        {
            OnOneEnemyLeft?.Invoke();
        }
    }
    public void SetCount() => _countText.text = string.Format(_template, _count);
}