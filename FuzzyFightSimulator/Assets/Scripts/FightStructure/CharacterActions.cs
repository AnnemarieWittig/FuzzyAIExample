using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    public IEnumerator InitiateAttack(Character attacker, Character defender, CharacterDescription defenderWindow, DialogueUI dialogueUI)
    {
        yield return StartCoroutine(dialogueUI.RunDialogue(attacker.attackChoice));

        if (succesfullHit(attacker))
        {
            attacker.Attack(defender, defenderWindow);

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

    public IEnumerator InitiateDefense(Character character, DialogueUI dialogueUI)
    {
        yield return StartCoroutine(dialogueUI.RunDialogue(character.defenseChoice));
        character.StartDefense();
    }

    private bool succesfullHit(Character attacker)
    {
        float hit = Random.Range(0f, 100 * 100f);
        if (attacker.hitChance > 0 && 0 <= hit && hit <= attacker.hitChance * 100f)
            return true;
        else
            return false;
    }
}
