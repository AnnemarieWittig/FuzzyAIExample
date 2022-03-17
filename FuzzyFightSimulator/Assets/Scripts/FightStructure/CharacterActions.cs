using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    public IEnumerator InitiateAttack(Character attacker, Character defender, CharacterDescription defenderWindow, DialogueUI dialogueUI)
    {
        yield return StartCoroutine(dialogueUI.RunDialogue(attacker.attackChoice));

        if (Character.CalculateSuccessOfAction(attacker.hitChance))
        {
            bool crit = attacker.Attack(defender, defenderWindow);
            if (crit)
                yield return StartCoroutine(dialogueUI.RunDialogue(attacker.CriticalAttack));

            if (defender.GetDefenseState() == DefenseState.DEFENDING)
                yield return StartCoroutine(dialogueUI.RunDialogue(defender.attackAgainstDefender));
            else
                yield return StartCoroutine(dialogueUI.RunDialogue(defender.attackAgainstOpen));
        }
        else
        {
            yield return StartCoroutine(dialogueUI.RunDialogue(attacker.attackMiss));
        }
        yield return defender.GetIsDead();
    }

    public IEnumerator InitiateDefense(Character character, DialogueUI dialogueUI, CharacterDescription characterWindow)
    {
        yield return StartCoroutine(dialogueUI.RunDialogue(character.defenseChoice));
        bool healed = character.StartDefense(characterWindow);
        if (healed)
            yield return StartCoroutine(dialogueUI.RunDialogue(character.SelfHealed));
    }

}
