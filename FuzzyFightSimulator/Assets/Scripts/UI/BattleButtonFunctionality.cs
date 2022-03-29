using UnityEngine;
using UnityEngine.UI;

public class BattleButtonFunctionality : MonoBehaviour
{
    [SerializeField] BattleSystem BattleSystem;
    [SerializeField] Button[] FightButtons;

    public void OnAttackButton()
    {
        Debug.Log("Button Pressed");
        BattleState state = BattleSystem.GetBattleState();
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(BattleSystem.PlayerAttack());
        }
    }

    public void OnDefenseButton()
    {
        BattleState state = BattleSystem.GetBattleState();
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(BattleSystem.PlayerDefense());
        }
    }

    public void OnFleeButton()
    {
        BattleState state = BattleSystem.GetBattleState();
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(BattleSystem.CharacterFlees());
        }
    }

    public void DeactivateFightButtons()
    {
        foreach (var button in FightButtons)
        {
            button.interactable = false;
        }
    }

    public void ActivateFightButtons()
    {
        foreach (var button in FightButtons)
        {
            button.interactable = true;
        }
    }

}
