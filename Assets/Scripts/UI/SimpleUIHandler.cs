using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleUIHandler : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _pauseMenuCanvas;
    [SerializeField] private GameObject _loseCanvas;

    [SerializeField] private VitalitySystem _playerVitalitySystem;

    private void Start()
    {
        _playerVitalitySystem.OnDeath += LoseState;
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
    private void LoseState()
    {
        _loseCanvas.SetActive(true);
    }
}
