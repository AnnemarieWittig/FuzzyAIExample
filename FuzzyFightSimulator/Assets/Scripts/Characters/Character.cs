using UnityEngine;

public enum DefenseState { DEFENDING, OPEN }

public class Character : MonoBehaviour
{
    #region Variables

    [Header("General Information")]
    [SerializeField] public string CharacterName;
    [SerializeField] public float TrainingHours;
    private DefenseState _defenseState;

    [Header("Life Points")]
    [SerializeField] public int MaxHP;
    [SerializeField] public float CurrentHP;
    private bool isDead;

    [Header("Attack Information")]
    [SerializeField] public float Damage;
    [SerializeField] public float HitChance;

    [Header("Random Events")]
    [SerializeField] public bool CritAllowance = false;
    [SerializeField] public bool HealAllowance = false;
    [SerializeField] public float CritChance;
    [SerializeField] public float HealChance;
    [SerializeField] public float HealValue;

    [Header("Messages - Choices")]
    [SerializeField] public DialogueObject GeneralChoice;
    [SerializeField] public DialogueObject AttackChoice;
    [SerializeField] public DialogueObject DefenseChoice;
    [SerializeField] public DialogueObject EscapeChoice;

    [Header("Messages - Attack")]
    [SerializeField] public DialogueObject AttackAgainstDefender;
    [SerializeField] public DialogueObject AttackAgainstOpen;
    [SerializeField] public DialogueObject AttackMiss;

    [Header("Messages - Random Events")]
    [SerializeField] public DialogueObject CriticalAttack;
    [SerializeField] public DialogueObject SelfHealed;

    [Header("Messages - Other")]
    [SerializeField] public DialogueObject WinMessage;
    [SerializeField] public DialogueObject WinScreenMessage;

    [Header("Organisational")]
    private Animator _animator;

    #endregion

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("IsWinner", false);
        _defenseState = DefenseState.OPEN;
    }

    #region Actions
    public bool Attack(Character enemy, CharacterDescription enemyInfo)
    {
        float damageToApply = Damage;
        bool crit = false;
        if (CritAllowance && CalculateSuccessOfAction(CritChance))
        {
            damageToApply *= 2;
            crit = true;
        }
        _animator.SetTrigger("Attack");
        enemy.TakeDamage(damageToApply);
        enemyInfo.SetHP(enemy.CurrentHP);
        return crit;
    }

    private void TakeDamage(float Damage)
    {
        _animator.SetTrigger("Hurt");
        if (_defenseState == DefenseState.DEFENDING)
            Damage = (float)(Damage / 2);

        CurrentHP -= Damage;

        if (CurrentHP <= 0)
            isDead = true;
        else
            isDead = false;
    }

    public bool StartDefense(CharacterDescription myWindow)
    {
        _defenseState = DefenseState.DEFENDING;
        _animator.SetBool("IsDefending", true);
        if (HealAllowance && CalculateSuccessOfAction(HealChance))
        {
            CurrentHP += HealValue;
            if (CurrentHP > MaxHP)
                CurrentHP = MaxHP;
            myWindow.SetHP(CurrentHP);
            _animator.SetTrigger("Heal");
            return true;
        }

        return false;
    }

    public void StopDefense()
    {
        _animator.SetBool("IsDefending", false);
        _defenseState = DefenseState.OPEN;
    }

    public static bool CalculateSuccessOfAction(float chance)
    {
        float hit = Random.Range(0f, 100 * 100f);
        if (chance > 0 && 0 <= hit && hit <= chance * 100f)
            return true;
        else
            return false;
    }

    #endregion

    public void StartWinAnimation()
    {
        _animator.SetBool("IsWinner", true);
    }

    public void StartDeathAnimation()
    {
        _animator.SetTrigger("Death");
    }

    public bool GetIsDead() { return isDead; }
    public void SetIsDead(bool dead) { isDead = dead; }
    public DefenseState GetDefenseState() => _defenseState;

}
