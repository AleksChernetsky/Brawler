using UnityEngine;

public class Score : MonoBehaviour
{
    private int _score;
    [SerializeField] ScoreView _scoreView;

    private void Awake()
    {
        EventManager.CoinPickedUo.AddListener(Increase);
    }
    private void Start()
    {
        _score = 0;
    }
    private void Increase(int value)
    {
        _score += value;
        _scoreView.SetScore(_score);
    }
}