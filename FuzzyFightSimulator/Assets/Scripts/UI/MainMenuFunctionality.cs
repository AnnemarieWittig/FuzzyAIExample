using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuFunctionality : MonoBehaviour
{
    [SerializeField] public GameObject MainMenuUI;
    [SerializeField] public GameObject PlayerMenu;
    [SerializeField] public GameObject EnemyMenu;
    [SerializeField] public GameObject EndScreen;
    [SerializeField] public GameObject Credits;
    [SerializeField] public GameObject CalculationOverview;

    public void OnPlayerMenuButton()
    {
        SetActiveMenus(false, true, false, false, false, false);
    }

    public void OnEnemyMenuButton()
    {
        SetActiveMenus(false, false, true, false, false, false);
    }

    public void OnCalculationOverviewButton()
    {
        CalculationOverview.SetActive(true);
    }

    public void OnCalculationOverviewClose()
    {
        CalculationOverview.SetActive(false);
    }

    public void ReturnToMainMenuFromOtherMenu()
    {
        SetActiveMenus(true, false, false, false, false, false);
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnCreditsButton()
    {
        Credits.SetActive(true);
    }

    public void OnCreditClose()
    {
        Credits.SetActive(false);
    }

    public void EndGameScreen(DialogueObject dialogue)
    {
        TMP_Text endText = EndScreen.GetComponentInChildren<TMP_Text>();
        endText.text = dialogue.Dialogue[0];
        SetActiveMenus(false, false, false, true, false, false);
    }

    private void SetActiveMenus(bool main, bool player, bool enemy, bool endScreen, bool credits, bool calculation)
    {
        MainMenuUI.SetActive(main);
        PlayerMenu.SetActive(player);
        EnemyMenu.SetActive(enemy);
        EndScreen.SetActive(endScreen);
        Credits.SetActive(credits);
        CalculationOverview.SetActive(calculation);
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
