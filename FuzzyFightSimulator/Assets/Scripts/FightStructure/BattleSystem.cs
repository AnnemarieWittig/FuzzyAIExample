using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [Header("UI")]
    [SerializeField] public DialogueObject initialDialogue;
    [SerializeField] public CharacterDescription playerWindow;
    [SerializeField] public CharacterDescription enemyWindow;
    [SerializeField] public DialogueUI dialogueUI;
    private CharacterActions actions;

    void Start()
    {
        state = BattleState.START;
        InitializeVariables();
        StartCoroutine(InitializeFight());
    }

    private IEnumerator PlayerTurn()
    {
        playerCharacter.StopDefense();
        yield return StartCoroutine(dialogueUI.RunDialogue(playerCharacter.generalChoice));
    }

    public IEnumerator PlayerAttack()
    {
        yield return StartCoroutine(actions.InitiateAttack(playerCharacter, enemyCharacter, enemyWindow, dialogueUI));
        moveToEnemyTurn();
    }

    public IEnumerator PlayerDefense()
    {
        yield return StartCoroutine(actions.InitiateDefense(playerCharacter, dialogueUI));
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
        yield return StartCoroutine(dialogueUI.RunDialogue(enemyCharacter.generalChoice));
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
        yield return StartCoroutine(actions.InitiateAttack(enemyCharacter, playerCharacter, playerWindow, dialogueUI));
        moveToPlayerTurn();
    }

    private IEnumerator EnemyDefense()
    {
        yield return StartCoroutine(actions.InitiateDefense(enemyCharacter, dialogueUI));
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
            StartCoroutine(EndBattle(enemyCharacter.winMessage));
            return true;
        }
        else if (enemyCharacter.GetIsDead())
        {
            StartCoroutine(EndBattle(playerCharacter.winMessage));
            return true;
        }
        else { return false; }

    }

    public IEnumerator CharacterFlees()
    {
        if (state == BattleState.PLAYERTURN)
        {
            yield return StartCoroutine(dialogueUI.RunDialogue(playerCharacter.flightChoice));
            StartCoroutine(EndBattle(enemyCharacter.winMessage));
        }
        else if (state == BattleState.ENEMYTURN)
        {
            yield return StartCoroutine(dialogueUI.RunDialogue(enemyCharacter.flightChoice));
            StartCoroutine(EndBattle(playerCharacter.winMessage));
        }
    }

    private IEnumerator EndBattle(DialogueObject dialogueObject)
    {
        state = BattleState.END;
        yield return StartCoroutine(dialogueUI.RunDialogue(dialogueObject));
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

    private IEnumerator InitializeFight()
    {
        playerWindow.Initialize(playerCharacter);
        enemyWindow.Initialize(enemyCharacter);

        yield return StartCoroutine(dialogueUI.RunDialogue(initialDialogue));
        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

    private IEnumerator playDialogueUi(DialogueObject dialogueObject)
    {
        Debug.Log("playDialogueUi");
        dialogueUI.showDialogue(dialogueObject);
        yield return 0;
    }

    public BattleState GetBattleState()
    {
        return state;
    }
}
