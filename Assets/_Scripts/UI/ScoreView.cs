using TMPro;

using UnityEngine;

public class ScoreView : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    [SerializeField] private string _template;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }
    public void SetScore(int value)
    {
        _scoreText.text = string.Format(_template, value);
    }
}