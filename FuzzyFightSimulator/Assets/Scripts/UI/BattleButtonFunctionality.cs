using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public void deactivateFightButtons()
    {
        foreach (var button in FightButtons)
        {
            button.interactable = false;
        }
    }

    public void activateFightButtons()
    {
        foreach (var button in FightButtons)
        {
            button.interactable = true;
        }
    }

}
