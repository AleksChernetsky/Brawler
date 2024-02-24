using TMPro;

using UnityEngine;

public class PlayersCountView : MonoBehaviour
{
    [SerializeField] private string _template;
    private TextMeshProUGUI _countText;
    private int _count;

    private void Awake()
    {
        EventManager.OnCharRegister.AddListener(Increase);
        EventManager.OnCharDelete.AddListener(Decrease);
        _countText = GetComponent<TextMeshProUGUI>();
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