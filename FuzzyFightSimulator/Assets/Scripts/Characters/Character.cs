using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public enum DefenseState { DEFENDING, OPEN }

public class Character : MonoBehaviour
{
    [Header("General Information")]
    [SerializeField] public string characterName;
    [SerializeField] public int level;
    private DefenseState defenseState;

    [Header("Life Points")]
    [SerializeField] public int maxHP;
    [SerializeField] public int currentHP;
    private bool isDead;

    [Header("Attack Information")]
    [SerializeField] public int damage;
    [SerializeField] public float hitChance;

    [Header("Messages")]
    [SerializeField] public DialogueObject winMessage;
    [SerializeField] public DialogueObject attackAgainstDefender;
    [SerializeField] public DialogueObject attackAgainstOpen;
    [SerializeField] public DialogueObject generalChoice;
    [SerializeField] public DialogueObject attackChoice;
    [SerializeField] public DialogueObject defenseChoice;
    [SerializeField] public DialogueObject flightChoice;

    [Header("Organisational")]
    [SerializeField] public Animator animator;


    private void Start()
    {
        defenseState = DefenseState.OPEN;
    }

    public void Attack(Character enemy, CharacterDescription enemyInfo)
    {
        enemy.TakeDamage(damage);
        enemyInfo.SetHP(enemy.currentHP);
    }

    private void TakeDamage(int damage)
    {
        if (defenseState == DefenseState.DEFENDING)
            damage = (damage / 2);

        currentHP -= damage;

        if (currentHP <= 0)
            isDead = true;
        else
            isDead = false;
    }

    public void StartDefense()
    {
        defenseState = DefenseState.DEFENDING;
        //animator.SetBool("defending", true);
    }

    public void StopDefense()
    {
        defenseState = DefenseState.OPEN;
        //animator.SetBool("defending", false);
    }

    public bool GetIsDead() { return isDead; }
    public void SetIsDead(bool dead) { isDead = dead; }
    public DefenseState GetDefenseState() => defenseState;

}
