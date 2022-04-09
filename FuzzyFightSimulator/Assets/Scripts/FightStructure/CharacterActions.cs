using System.Collections;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    public IEnumerator InitiateAttack(Character attacker, Character defender, CharacterDescription defenderWindow, DialogueUI dialogueUI)
    {
        yield return StartCoroutine(dialogueUI.RunDialogue(attacker.AttackChoice));

        if (attacker.CalculateSuccessOfAction(attacker.HitChance))
        {
            bool crit = attacker.Attack(defender, defenderWindow);

            if (defender.GetDefenseState() == DefenseState.DEFENDING)
                yield return StartCoroutine(dialogueUI.RunDialogue(defender.AttackAgainstDefender));
            else
                yield return StartCoroutine(dialogueUI.RunDialogue(defender.AttackAgainstOpen));

            if (crit)
                yield return StartCoroutine(dialogueUI.RunDialogue(attacker.CriticalAttack));
        }
        else
        {
            yield return StartCoroutine(dialogueUI.RunDialogue(attacker.AttackMiss));
        }
        yield return defender.GetIsDead();
    }

    public IEnumerator InitiateDefense(Character character, DialogueUI dialogueUI, CharacterDescription characterWindow)
    {
        yield return StartCoroutine(dialogueUI.RunDialogue(character.DefenseChoice));
        bool healed = character.StartDefense(characterWindow);
        if (healed)
            yield return StartCoroutine(dialogueUI.RunDialogue(character.SelfHealed));
    }

}
