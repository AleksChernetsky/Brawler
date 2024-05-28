using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioSource _menuSound;
    [SerializeField] private AudioSource _battleSound;

    private void Start()
    {
        GlobalEventsManager.OnButtonClick.AddListener(ButtonClickSound);
        GlobalEventsManager.OnBattleState.AddListener(PlayBattleSound);
        GlobalEventsManager.OnPauseState.AddListener(PlayMenuSound);

        _battleSound.Play();
    }
    private void PlayMenuSound()
    {
        _battleSound.Pause();
        _menuSound.Play();
    }

    private void PlayBattleSound()
    {
        _menuSound.Pause();
        _battleSound.UnPause();
    }

    private void ButtonClickSound()
    {
        _menuSound.PlayOneShot(_buttonClick);
    }
}
