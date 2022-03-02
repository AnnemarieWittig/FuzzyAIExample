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
    private Character playerChar;

    [Header("Enemy")]
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public Transform enemyPosition;
    private Character enemyChar;

    [Header("UI")]
    [SerializeField] public DialogueObject initialDialogue;
    [SerializeField] public CharacterDescription playerWindow;
    [SerializeField] public CharacterDescription enemyWindow;
    [SerializeField] public DialogueUI dialogueUI;
    private CharacterActions actions;

    void Start()
    {
        state = BattleState.START;
        actions = GetComponent<CharacterActions>();
        InitializeVariables();
        StartCoroutine(InitializeFight());
    }

    private IEnumerator PlayerTurn()
    {
        yield return StartCoroutine(dialogueUI.RunDialogue(playerChar.generalChoice));
    }

    private IEnumerator EnemyTurn()
    {
        yield return StartCoroutine(dialogueUI.RunDialogue(enemyChar.generalChoice));
        if (true)
        {
            StartCoroutine(EnemyAttack());
        }
    }

    public IEnumerator PlayerHeal()
    {
        playerChar.Heal(5);

        playerWindow.SetHP(playerChar.currentHP);
        yield return StartCoroutine(dialogueUI.RunDialogue(playerChar.attackChoice));

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    public IEnumerator PlayerAttack()
    {
        yield return StartCoroutine(actions.InitiateAttack(playerChar, enemyChar, enemyWindow, dialogueUI));
        moveToEnemyTurn();
    }

    private IEnumerator EnemyAttack()
    {
        yield return StartCoroutine(actions.InitiateAttack(enemyChar, playerChar, playerWindow, dialogueUI));
        moveToPlayerTurn();
    }

    private void moveToEnemyTurn()
    {
        bool end = CheckWinCondition(playerChar, enemyChar);
        if (!end)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    private void moveToPlayerTurn()
    {
        Debug.Log(playerChar.GetIsDead());
        bool end = CheckWinCondition(playerChar, enemyChar);
        if (!end)
        {
            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }
    }

    private bool CheckWinCondition(Character player, Character enemy)
    {
        if (player.GetIsDead())
        {
            StartCoroutine(EndBattle(enemy.winMessage));
            return true;
        }
        else if (enemy.GetIsDead())
        {
            StartCoroutine(EndBattle(player.winMessage));
            return true;
        }
        else { return false; }

    }

    private IEnumerator EndBattle(DialogueObject dialogueObject)
    {
        state = BattleState.END;
        yield return StartCoroutine(dialogueUI.RunDialogue(dialogueObject));
    }

    private void InitializeVariables()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerPosition);
        playerChar = playerGO.GetComponent<Character>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyPosition);
        enemyChar = enemyGO.GetComponent<Character>();
    }

    private IEnumerator InitializeFight()
    {
        playerWindow.Initialize(playerChar);
        enemyWindow.Initialize(enemyChar);

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
