using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    [SerializeField] public Character AiCharacter;
    [SerializeField] public InputLinguisticVariable Trefferchance;
    [SerializeField] public InputLinguisticVariable Lebenspunkte;
    [SerializeField] public InputLinguisticVariable Schaden;
    [SerializeField] public InputLinguisticVariable Trainingseinheiten;
    [SerializeField] public OutputLinguisticVariable Kampfentscheidung;
    [SerializeField] public CalculationOverview Overview;

    public BattleChoices MakeBattleDecision()
    {
        Overview.AddIntroduction(AiCharacter);
        Dictionary<BattleChoices, double> resultOfRuleEvaluation = RuleEvaluation();
        double defuzziedValue = GetDefuzziedValue(resultOfRuleEvaluation);
        BattleChoices translatedAction = TranslateDefuzziedValueToAction(defuzziedValue);
        Overview.AddDecision(translatedAction);
        return translatedAction;
    }

    private Dictionary<BattleChoices, double> RuleEvaluation()
    {
        Dictionary<BattleChoices, double> valuesOfRuleEvaluation = GenerateBasicDictionary();
        valuesOfRuleEvaluation = Trefferchance.GetHighestMembershipForRuleEvaluation(AiCharacter.HitChance, valuesOfRuleEvaluation);
        valuesOfRuleEvaluation = Lebenspunkte.GetHighestMembershipForRuleEvaluation(AiCharacter.CurrentHP, valuesOfRuleEvaluation);
        valuesOfRuleEvaluation = Schaden.GetHighestMembershipForRuleEvaluation(AiCharacter.Damage, valuesOfRuleEvaluation);
        valuesOfRuleEvaluation = Trainingseinheiten.GetHighestMembershipForRuleEvaluation(AiCharacter.TrainingHours, valuesOfRuleEvaluation);
        return valuesOfRuleEvaluation;
    }

    private double GetDefuzziedValue(Dictionary<BattleChoices, double> valuesAfterEvaluation)
    {
        List<Rectangle> generatedRectangles = Kampfentscheidung.calculateDefuzzyfication(valuesAfterEvaluation);
        Overview.AddRectangles(generatedRectangles);
        double defuzziedValue = Aom(generatedRectangles);
        Overview.AddAOMResult(defuzziedValue);
        return defuzziedValue;
    }

    private BattleChoices TranslateDefuzziedValueToAction(double defuzziedValue)
    {
        if (0 <= defuzziedValue && defuzziedValue <= 20)
            return BattleChoices.ESCAPE;
        else if (20 < defuzziedValue && defuzziedValue < 50)
            return BattleChoices.BLOCK;
        else if (50 <= defuzziedValue && defuzziedValue <= 100)
            return BattleChoices.ATTACK;
        else
            throw new CannotTranslateValueToActionException("The defuzzied value is not within the given bounds and thus cannot be translated"); //Error
    }

    private double Aom(List<Rectangle> rectangles)
    {
        double numerator = 0d;
        double denominator = 0d;
        foreach (Rectangle rectangle in rectangles)
        {
            numerator += rectangle.getD() * rectangle.Maximum;
            denominator += rectangle.Maximum;
        }
        return (double)numerator / denominator;
    }

    private Dictionary<BattleChoices, double> GenerateBasicDictionary()
    {
        Dictionary<BattleChoices, double> dict = new Dictionary<BattleChoices, double>();
        dict.Add(BattleChoices.ESCAPE, 0.0);
        dict.Add(BattleChoices.BLOCK, 0.0);
        dict.Add(BattleChoices.ATTACK, 0.0);
        return dict;
    }

    public double AOMTestWrapper(List<Rectangle> rectangles)
    {
        return Aom(rectangles);
    }

    /*
    Testcase
        AiCharacter.HitChance = 70;
        AiCharacter.CurrentHP = 75;
        AiCharacter.TrainingHours = 35;
        AiCharacter.Damage = 30;
        Debug.Log("Expected: ");
        Debug.Log("Escape: " + 6d / 13d);
        Debug.Log("Block: " + 7d / 13d);
        Debug.Log("Attack: " + 3d / 4d);
        Debug.Log("ESCAPE Rechteck: Höhe " + 6d / 13d + ", Start " + 0d + ", Ende " + 300d / 13d);
        Debug.Log("BLOCK: Höhe " + 7d / 13d + ", Start " + 300d / 13d + ", Ende " + 740d / 13d);
        Debug.Log("ATTACK: Höhe " + 0.75 + ", Start " + 61.25 + ", Ende " + 100);
        Debug.Log("AOM result: " + 49.90437447);

        Debug.Log("Generated: ");

        Debug.Log(MakeBattleDecision());
    */
}
