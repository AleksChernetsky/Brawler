using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadHandler : MonoBehaviour
{
    private string Tutorial = "Tutorial";
    private string BattleScene = "BattleScene";
    private string MainMenu = "MainMenu";

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void StartBattle()
    {
        SceneManager.LoadScene(BattleScene);
    }
    public void TutorialButton()
    {
        SceneManager.LoadScene(Tutorial);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(MainMenu);
    }
    public void RestartTutorialButton()
    {
        SceneManager.LoadScene(Tutorial);
    }
    public void ExitButtonAction()
    {
        Application.Quit();
    }
}
