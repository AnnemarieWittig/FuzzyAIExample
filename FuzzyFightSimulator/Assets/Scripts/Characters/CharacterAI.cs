using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    [SerializeField] public Character aiCharacter;
    [SerializeField] public InputLinguisticVariable trefferchance;
    [SerializeField] public InputLinguisticVariable lebenspunkte;
    [SerializeField] public InputLinguisticVariable schaden;
    [SerializeField] public InputLinguisticVariable trainingseinheiten;
    [SerializeField] public OutputLinguisticVariable kampfentscheidung;

    public BattleChoices makeBattleDecision()
    {
        Dictionary<BattleChoices, double> resultOfRuleEvaluation = ruleEvaluation();
        double defuzziedValue = getDefuzziedValue(resultOfRuleEvaluation);
        BattleChoices translatedAction = translateDefuzziedValueToAction(defuzziedValue);
        return translatedAction;
    }

    private Dictionary<BattleChoices, double> ruleEvaluation()
    {
        Dictionary<BattleChoices, double> valuesOfRuleEvaluation = generateBasicDictionary();
        valuesOfRuleEvaluation = trefferchance.GetHighestMembershipForRuleEvaluation(aiCharacter.hitChance, valuesOfRuleEvaluation);
        valuesOfRuleEvaluation = lebenspunkte.GetHighestMembershipForRuleEvaluation(aiCharacter.currentHP, valuesOfRuleEvaluation);
        valuesOfRuleEvaluation = schaden.GetHighestMembershipForRuleEvaluation(aiCharacter.damage, valuesOfRuleEvaluation);
        valuesOfRuleEvaluation = trainingseinheiten.GetHighestMembershipForRuleEvaluation(aiCharacter.trainingHours, valuesOfRuleEvaluation);
        return valuesOfRuleEvaluation;
    }

    private double getDefuzziedValue(Dictionary<BattleChoices, double> valuesAfterEvaluation)
    {
        List<Rectangle> generatedRectangles = kampfentscheidung.calculateDefuzzyfication(valuesAfterEvaluation);
        double defuzziedValue = aom(generatedRectangles);
        return defuzziedValue;
    }

    private BattleChoices translateDefuzziedValueToAction(double defuzziedValue)
    {
        if (0 <= defuzziedValue && defuzziedValue <= 33)
            return BattleChoices.ESCAPE;
        else if (33 < defuzziedValue && defuzziedValue < 66)
            return BattleChoices.BLOCK;
        else if (66 <= defuzziedValue && defuzziedValue <= 100)
            return BattleChoices.ATTACK;
        else
            return BattleChoices.ESCAPE; //Error
    }
    private double aom(List<Rectangle> rectangles)
    {
        double numerator = 0d;
        double denominator = 0d;
        foreach (Rectangle rectangle in rectangles)
        {
            numerator += rectangle.getD() * rectangle.getMaximum;
            denominator += rectangle.getMaximum;
        }
        return (double)numerator / denominator;
    }

    private Dictionary<BattleChoices, double> generateBasicDictionary()
    {
        Dictionary<BattleChoices, double> dict = new Dictionary<BattleChoices, double>();
        dict.Add(BattleChoices.ESCAPE, 0.0);
        dict.Add(BattleChoices.BLOCK, 0.0);
        dict.Add(BattleChoices.ATTACK, 0.0);
        return dict;
    }

    /*
    Testcase
        aiCharacter.hitChance = 70;
        aiCharacter.currentHP = 75;
        aiCharacter.trainingHours = 35;
        aiCharacter.damage = 30;
        Debug.Log("Expected: ");
        Debug.Log("Escape: " + 6d / 13d);
        Debug.Log("Block: " + 7d / 13d);
        Debug.Log("Attack: " + 3d / 4d);
        Debug.Log("ESCAPE Rechteck: Höhe " + 6d / 13d + ", Start " + 0d + ", Ende " + 300d / 13d);
        Debug.Log("BLOCK: Höhe " + 7d / 13d + ", Start " + 300d / 13d + ", Ende " + 740d / 13d);
        Debug.Log("ATTACK: Höhe " + 0.75 + ", Start " + 61.25 + ", Ende " + 100);
        Debug.Log("AOM result: " + 49.90437447);

        Debug.Log("Generated: ");

        Debug.Log(makeBattleDecision());
    */
}
