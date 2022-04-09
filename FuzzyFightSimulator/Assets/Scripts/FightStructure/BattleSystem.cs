using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, END }

public class BattleSystem : MonoBehaviour
{
    #region Variables

    public BattleState state;

    [Header("Player")]
    [SerializeField] public Toggle PlayerAutomaticToggle;
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public Transform playerPosition;
    [SerializeField] public CharacterAI playerCharacterAI;
    private Character playerCharacter;

    [Header("Enemy")]
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public Transform enemyPosition;
    [SerializeField] public CharacterAI enemyCharacterAI;
    private Character enemyCharacter;

    [Header("Battle UI")]
    [SerializeField] public CharacterDescription PlayerWindow;
    [SerializeField] public CharacterDescription EnemyWindow;
    [SerializeField] public BattleButtonFunctionality ButtonActions;

    [Header("Pause Menu UI")]
    [SerializeField] public PausePlaySwitcher PausePlay;
    [SerializeField] public MainMenuFunctionality MainMenu;

    [Header("Dialogue")]
    [SerializeField] public DialogueUI DialoguePlayer;
    [SerializeField] public DialogueObject initialDialogue;
    private CharacterActions actions;

    #endregion

    void Start()
    {
        ButtonActions.DeactivateFightButtons();
        initializeGame();
        PausePlay.Pause();
    }

    #region Player Turn Functions
    private IEnumerator PlayerTurn()
    {
        if (state != BattleState.PLAYERTURN)
            throw new NotCharacterTurnException("It's not the player's turn.");
        playerCharacter.StopDefense();
        yield return StartCoroutine(DialoguePlayer.RunDialogue(playerCharacter.GeneralChoice));
        if (PlayerAutomaticToggle.isOn == false)
        {
            ButtonActions.ActivateFightButtons();
        }
        else
        {
            switch (playerCharacterAI.MakeBattleDecision())
            {
                case BattleChoices.ESCAPE:
                    StartCoroutine(CharacterFlees());
                    break;

                case BattleChoices.BLOCK:
                    StartCoroutine(PlayerDefense());
                    break;

                case BattleChoices.ATTACK:
                    StartCoroutine(PlayerAttack());
                    break;
            }
        }
    }

    public IEnumerator PlayerAttack()
    {
        yield return StartCoroutine(actions.InitiateAttack(playerCharacter, enemyCharacter, EnemyWindow, DialoguePlayer));
        moveToEnemyTurn();
    }

    public IEnumerator PlayerDefense()
    {
        yield return StartCoroutine(actions.InitiateDefense(playerCharacter, DialoguePlayer, PlayerWindow));
        moveToEnemyTurn();
    }

    private void moveToEnemyTurn()
    {
        bool end = CheckWinCondition();
        if (!end)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    #endregion

    #region Enemy Turn functions
    private IEnumerator EnemyTurn()
    {
        if (state != BattleState.ENEMYTURN)
            throw new NotCharacterTurnException("It's not the enemy's turn.");

        enemyCharacter.StopDefense();

        yield return StartCoroutine(DialoguePlayer.RunDialogue(enemyCharacter.GeneralChoice));

        BattleChoices choice = enemyCharacterAI.MakeBattleDecision();
        switch (choice)
        {
            case BattleChoices.ESCAPE:
                StartCoroutine(CharacterFlees());
                break;

            case BattleChoices.BLOCK:
                StartCoroutine(EnemyDefense());
                break;

            case BattleChoices.ATTACK:
                StartCoroutine(EnemyAttack());
                break;
        }
    }

    private IEnumerator EnemyAttack()
    {
        yield return StartCoroutine(actions.InitiateAttack(enemyCharacter, playerCharacter, PlayerWindow, DialoguePlayer));
        moveToPlayerTurn();
    }

    private IEnumerator EnemyDefense()
    {
        yield return StartCoroutine(actions.InitiateDefense(enemyCharacter, DialoguePlayer, EnemyWindow));
        moveToPlayerTurn();
    }

    private void moveToPlayerTurn()
    {
        bool end = CheckWinCondition();
        if (!end)
        {
            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }
    }
    #endregion

    #region Game-Ending Functions

    public IEnumerator CharacterFlees()
    {
        if (state == BattleState.PLAYERTURN)
        {
            playerCharacter.StartEscapeAnimation();
            yield return StartCoroutine(DialoguePlayer.RunDialogue(playerCharacter.EscapeChoice));
            StartCoroutine(EndBattle(enemyCharacter.WinMessage, enemyCharacter.WinScreenMessage));
        }
        else if (state == BattleState.ENEMYTURN)
        {
            enemyCharacter.StartEscapeAnimation();
            yield return StartCoroutine(DialoguePlayer.RunDialogue(enemyCharacter.EscapeChoice));
            StartCoroutine(EndBattle(playerCharacter.WinMessage, playerCharacter.WinScreenMessage));
        }
    }

    private bool CheckWinCondition()
    {
        if (playerCharacter.GetIsDead())
        {
            enemyCharacter.StartWinAnimation();
            playerCharacter.StartDeathAnimation();
            StartCoroutine(EndBattle(enemyCharacter.WinMessage, enemyCharacter.WinScreenMessage));
            return true;
        }
        else if (enemyCharacter.GetIsDead())
        {
            playerCharacter.StartWinAnimation();
            enemyCharacter.StartDeathAnimation();
            StartCoroutine(EndBattle(playerCharacter.WinMessage, playerCharacter.WinScreenMessage));
            return true;
        }
        else { return false; }

    }

    private IEnumerator EndBattle(DialogueObject winText, DialogueObject endScreenText)
    {
        state = BattleState.END;
        yield return StartCoroutine(DialoguePlayer.RunDialogue(winText));
        PausePlay.EndGame(endScreenText, MainMenu);
    }
    #endregion

    #region Initializors

    private void initializeGame()
    {
        state = BattleState.START;
        InitializeVariables();
        InitializeMenus();
        StartCoroutine(InitializeFight());
    }

    private void InitializeVariables()
    {
        actions = GetComponent<CharacterActions>();
        GameObject playerGO = Instantiate(playerPrefab, playerPosition);
        playerCharacter = playerGO.GetComponent<Character>();
        playerCharacterAI.AiCharacter = playerCharacter;

        GameObject enemyGO = Instantiate(enemyPrefab, enemyPosition);
        enemyCharacter = enemyGO.GetComponent<Character>();
        enemyCharacterAI.AiCharacter = enemyCharacter;
    }

    private void InitializeMenus()
    {
        MainMenu.SetCharacterInMenus(playerCharacter, enemyCharacter, PlayerWindow, EnemyWindow);
    }

    private IEnumerator InitializeFight()
    {
        PlayerWindow.Initialize(playerCharacter);
        EnemyWindow.Initialize(enemyCharacter);

        yield return StartCoroutine(DialoguePlayer.RunDialogue(initialDialogue));
        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }
    #endregion

    public BattleState GetBattleState()
    {
        return state;
    }
}
