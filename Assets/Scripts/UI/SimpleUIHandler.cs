using UnityEngine;

public class SimpleUIHandler : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _pauseMenuCanvas;
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private GameObject _winCanvas;

    [SerializeField] private VitalitySystem _playerVitalitySystem;
    [SerializeField] private PlayersCountView _playersCountView;

    private void Start()
    {
        _playerVitalitySystem.OnDeath += LoseState;
        _playersCountView.OnOneEnemyLeft += WinState;
    }
    public void PlayButtonAction()
    {
        GlobalEventsManager.OnBattleState?.Invoke();
        Time.timeScale = 1;
        _pauseCanvas.SetActive(true);
    }
    public void PauseButtonAction()
    {
        GlobalEventsManager.OnPauseState?.Invoke();
        Time.timeScale = 0;
        _pauseMenuCanvas.SetActive(true);
        _pauseCanvas.SetActive(false);
    }
    public void ExitButtonAction()
    {
        Application.Quit();
    }
    private void WinState()
    {
        if (_playerVitalitySystem.CurrentHealth > 0)
        {
            _winCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void LoseState()
    {
        _loseCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}
