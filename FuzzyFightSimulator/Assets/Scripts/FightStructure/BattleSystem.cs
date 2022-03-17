using System.Collections;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, END }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    [Header("Player")]
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
    [SerializeField] public DialogueObject initialDialogue;
    [SerializeField] public CharacterDescription PlayerWindow;
    [SerializeField] public CharacterDescription EnemyWindow;
    [SerializeField] public DialogueUI Dialogue;
    [SerializeField] public BattleButtonFunctionality ButtonActions;

    [Header("Pause Menu UI")]
    [SerializeField] public PausePlaySwitcher PausePlay;
    [SerializeField] public MainMenuFunctionality MainMenu;
    private CharacterActions actions;

    void Start()
    {
        ButtonActions.deactivateFightButtons();
        initializeGame();
        PausePlay.Pause();
    }

    private void initializeGame()
    {
        state = BattleState.START;
        InitializeVariables();
        InitializeMenus();
        StartCoroutine(InitializeFight());
    }

    private void removeCharacters()
    {
        Destroy(playerCharacter);
        Destroy(enemyCharacter);
    }

    private IEnumerator PlayerTurn()
    {
        playerCharacter.StopDefense();
        yield return StartCoroutine(Dialogue.RunDialogue(playerCharacter.generalChoice));
        ButtonActions.activateFightButtons();
    }

    public IEnumerator PlayerAttack()
    {
        yield return StartCoroutine(actions.InitiateAttack(playerCharacter, enemyCharacter, EnemyWindow, Dialogue));
        moveToEnemyTurn();
    }

    public IEnumerator PlayerDefense()
    {
        yield return StartCoroutine(actions.InitiateDefense(playerCharacter, Dialogue, PlayerWindow));
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

    private IEnumerator EnemyTurn()
    {
        if (state != BattleState.ENEMYTURN)
            Debug.Log("Error");
        enemyCharacter.StopDefense();
        yield return StartCoroutine(Dialogue.RunDialogue(enemyCharacter.generalChoice));
        BattleChoices choice = enemyCharacterAI.makeBattleDecision();
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
        yield return StartCoroutine(actions.InitiateAttack(enemyCharacter, playerCharacter, PlayerWindow, Dialogue));
        moveToPlayerTurn();
    }

    private IEnumerator EnemyDefense()
    {
        yield return StartCoroutine(actions.InitiateDefense(enemyCharacter, Dialogue, EnemyWindow));
        moveToPlayerTurn();
    }

    private void moveToPlayerTurn()
    {
        Debug.Log(playerCharacter.GetIsDead());
        bool end = CheckWinCondition();
        if (!end)
        {
            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }
    }

    private bool CheckWinCondition()
    {
        if (playerCharacter.GetIsDead())
        {
            StartCoroutine(EndBattle(enemyCharacter.WinMessage, enemyCharacter.WinScreenMessage));
            return true;
        }
        else if (enemyCharacter.GetIsDead())
        {
            StartCoroutine(EndBattle(playerCharacter.WinMessage, playerCharacter.WinScreenMessage));
            return true;
        }
        else { return false; }

    }

    public IEnumerator CharacterFlees()
    {
        if (state == BattleState.PLAYERTURN)
        {
            yield return StartCoroutine(Dialogue.RunDialogue(playerCharacter.flightChoice));
            StartCoroutine(EndBattle(enemyCharacter.WinMessage, enemyCharacter.WinScreenMessage));
        }
        else if (state == BattleState.ENEMYTURN)
        {
            yield return StartCoroutine(Dialogue.RunDialogue(enemyCharacter.flightChoice));
            StartCoroutine(EndBattle(playerCharacter.WinMessage, playerCharacter.WinScreenMessage));
        }
    }

    private IEnumerator EndBattle(DialogueObject winText, DialogueObject endScreenText)
    {
        state = BattleState.END;
        yield return StartCoroutine(Dialogue.RunDialogue(winText));
        PausePlay.EndGame(endScreenText, MainMenu);
    }

    private void InitializeVariables()
    {
        actions = GetComponent<CharacterActions>();
        GameObject playerGO = Instantiate(playerPrefab, playerPosition);
        playerCharacter = playerGO.GetComponent<Character>();
        playerCharacterAI.aiCharacter = playerCharacter;

        GameObject enemyGO = Instantiate(enemyPrefab, enemyPosition);
        enemyCharacter = enemyGO.GetComponent<Character>();
        enemyCharacterAI.aiCharacter = enemyCharacter;
    }

    private void InitializeMenus()
    {
        MainMenu.SetCharacterInMenus(playerCharacter, enemyCharacter, PlayerWindow, EnemyWindow);
    }

    private IEnumerator InitializeFight()
    {
        PlayerWindow.Initialize(playerCharacter);
        EnemyWindow.Initialize(enemyCharacter);

        yield return StartCoroutine(Dialogue.RunDialogue(initialDialogue));
        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

    private IEnumerator playDialogueUi(DialogueObject dialogueObject)
    {
        Debug.Log("playDialogueUi");
        Dialogue.showDialogue(dialogueObject);
        yield return 0;
    }

    public BattleState GetBattleState()
    {
        return state;
    }
}
