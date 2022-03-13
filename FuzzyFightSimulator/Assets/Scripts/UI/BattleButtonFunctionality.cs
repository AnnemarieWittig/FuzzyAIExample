using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleButtonFunctionality : MonoBehaviour
{
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Button[] fightButtons;

    public void OnAttackButton()
    {
        Debug.Log("Button Pressed");
        BattleState state = battleSystem.GetBattleState();
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(battleSystem.PlayerAttack());
        }
    }

    public void OnDefenseButton()
    {
        BattleState state = battleSystem.GetBattleState();
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(battleSystem.PlayerDefense());
        }
    }

    public void OnFleeButton()
    {
        BattleState state = battleSystem.GetBattleState();
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(battleSystem.CharacterFlees());
        }
    }

    public void deactivateFightButtons()
    {
        foreach (var button in fightButtons)
        {
            button.interactable = false;
        }
    }

    public void activateFightButtons()
    {
        foreach (var button in fightButtons)
        {
            button.interactable = true;
        }
    }

}
