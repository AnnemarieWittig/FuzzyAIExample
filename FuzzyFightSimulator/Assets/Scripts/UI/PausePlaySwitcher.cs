using UnityEngine;

public class PausePlaySwitcher : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] public GameObject PauseMenuUI;
    [SerializeField] public GameObject GameInterface;
    [SerializeField] public GameObject PlayerField;
    [SerializeField] public GameObject EnemyField;

    public void Resume()
    {
        changeActiveOfGame(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        changeActiveOfGame(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void EndGame(DialogueObject dialogue, MainMenuFunctionality menu)
    {
        Pause();
        menu.EndGameScreen(dialogue);
    }

    private void changeActiveOfGame(bool active)
    {
        PauseMenuUI.SetActive(!active);
        GameInterface.SetActive(active);
        PlayerField.SetActive(active);
        EnemyField.SetActive(active);
    }
}
