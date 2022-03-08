using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    public IEnumerator InitiateAttack(Character attacker, Character defender, CharacterDescription defenderWindow, DialogueUI dialogueUI)
    {
        yield return StartCoroutine(dialogueUI.RunDialogue(attacker.attackChoice));
        attacker.Attack(defender, defenderWindow);

        if (defender.GetDefenseState() == DefenseState.DEFENDING)
            yield return StartCoroutine(dialogueUI.RunDialogue(defender.attackAgainstDefender));
        else
            yield return StartCoroutine(dialogueUI.RunDialogue(defender.attackAgainstOpen));

        yield return defender.GetIsDead();
    }

    public IEnumerator InitiateDefense(Character character, DialogueUI dialogueUI)
    {
        yield return StartCoroutine(dialogueUI.RunDialogue(character.defenseChoice));
        character.StartDefense();
    }
}
