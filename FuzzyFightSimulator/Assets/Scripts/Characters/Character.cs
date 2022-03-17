using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public enum DefenseState { DEFENDING, OPEN }

public class Character : MonoBehaviour
{
    [Header("General Information")]
    [SerializeField] public string characterName;
    [SerializeField] public double trainingHours;
    private DefenseState defenseState;

    [Header("Life Points")]
    [SerializeField] public int maxHP;
    [SerializeField] public float currentHP;
    private bool isDead;

    [Header("Attack Information")]
    [SerializeField] public float damage;
    [SerializeField] public float hitChance;

    [Header("Random Events")]
    [SerializeField] public bool CritAllowance = false;
    [SerializeField] public bool HealAllowance = false;
    [SerializeField] public float CritChance;
    [SerializeField] public float HealChance;
    [SerializeField] public float HealValue;

    [Header("Messages - Choices")]
    [SerializeField] public DialogueObject generalChoice;
    [SerializeField] public DialogueObject attackChoice;
    [SerializeField] public DialogueObject defenseChoice;
    [SerializeField] public DialogueObject flightChoice;

    [Header("Messages - Attack")]
    [SerializeField] public DialogueObject attackAgainstDefender;
    [SerializeField] public DialogueObject attackAgainstOpen;
    [SerializeField] public DialogueObject attackMiss;

    [Header("Messages - Random Events")]
    [SerializeField] public DialogueObject CriticalAttack;
    [SerializeField] public DialogueObject SelfHealed;

    [Header("Messages - Other")]
    [SerializeField] public DialogueObject WinMessage;
    [SerializeField] public DialogueObject WinScreenMessage;

    [Header("Organisational")]
    [SerializeField] public Animator animator;


    private void Start()
    {
        defenseState = DefenseState.OPEN;
    }

    public bool Attack(Character enemy, CharacterDescription enemyInfo)
    {
        float damageToApply = damage;
        bool crit = false;
        if (CritAllowance && CalculateSuccessOfAction(CritChance))
        {
            damageToApply *= 2;
            crit = true;
        }
        enemy.TakeDamage(damageToApply);
        enemyInfo.SetHP(enemy.currentHP);
        return crit;
    }

    private void TakeDamage(float damage)
    {
        if (defenseState == DefenseState.DEFENDING)
            damage = (float)(damage / 2);

        currentHP -= damage;

        if (currentHP <= 0)
            isDead = true;
        else
            isDead = false;
    }

    public bool StartDefense(CharacterDescription myWindow)
    {
        defenseState = DefenseState.DEFENDING;
        if (HealAllowance && CalculateSuccessOfAction(HealChance))
        {
            currentHP += HealValue;
            if (currentHP > maxHP)
                currentHP = maxHP;
            myWindow.SetHP(currentHP);
            return true;
        }

        //animator.SetBool("defending", true);
        return false;
    }

    public void StopDefense()
    {
        defenseState = DefenseState.OPEN;
        //animator.SetBool("defending", false);
    }

    public static bool CalculateSuccessOfAction(float chance)
    {
        float hit = Random.Range(0f, 100 * 100f);
        if (chance > 0 && 0 <= hit && hit <= chance * 100f)
            return true;
        else
            return false;
    }

    public bool GetIsDead() { return isDead; }
    public void SetIsDead(bool dead) { isDead = dead; }
    public DefenseState GetDefenseState() => defenseState;

}
