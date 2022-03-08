using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonFunctionality : MonoBehaviour
{
    [SerializeField] BattleSystem battleSystem;

    public void OnAttackButton()
    {
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

}
