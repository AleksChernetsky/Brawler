using TMPro;

using UnityEngine;

public class PlayersCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private string _template;
    private int _count;

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
    }
    public void SetCount() => _countText.text = string.Format(_template, _count);
}