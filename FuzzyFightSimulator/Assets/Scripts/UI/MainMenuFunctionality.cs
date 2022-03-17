using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuFunctionality : MonoBehaviour
{
    [SerializeField] public GameObject MainMenuUI;
    [SerializeField] public GameObject PlayerMenu;
    [SerializeField] public GameObject EnemyMenu;
    [SerializeField] public GameObject EndScreen;

    public void OnPlayerMenuButton()
    {
        SetActiveMenus(false, true, false, false);
    }

    public void OnEnemyMenuButton()
    {
        SetActiveMenus(false, false, true, false);
    }

    public void ReturnToMainMenuFromOtherMenu()
    {
        SetActiveMenus(true, false, false, false);
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void EndGameScreen(DialogueObject dialogue)
    {
        TMP_Text endText = EndScreen.GetComponentInChildren<TMP_Text>();
        endText.text = dialogue.Dialogue[0];
        SetActiveMenus(false, false, false, true);
    }

    private void SetActiveMenus(bool main, bool player, bool enemy, bool endScreen)
    {
        MainMenuUI.SetActive(main);
        PlayerMenu.SetActive(player);
        EnemyMenu.SetActive(enemy);
        EndScreen.SetActive(endScreen);
    }

    public void SetCharacterInMenus(Character player, Character enemy, CharacterDescription playerWin, CharacterDescription enemyWin)
    {
        CharacterMenu playerMenu = PlayerMenu.GetComponent<CharacterMenu>();
        playerMenu.Char = player;
        playerMenu.CharWindow = playerWin;
        CharacterMenu enemyMenu = EnemyMenu.GetComponent<CharacterMenu>();
        enemyMenu.Char = enemy;
        enemyMenu.CharWindow = enemyWin;
    }

}
