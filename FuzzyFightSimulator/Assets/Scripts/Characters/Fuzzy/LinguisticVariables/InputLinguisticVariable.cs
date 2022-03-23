using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/InputLinguisticVariable")]
public class InputLinguisticVariable : LinguisticVariable
{ //degree of membership

    [SerializeField] public InputTrapezeEquation[] InputTrapezeEquations;

    public Dictionary<BattleChoices, double> GetHighestMembershipForRuleEvaluation(double input, Dictionary<BattleChoices, double> currentMembershipDegrees)
    {
        foreach (InputTrapezeEquation equation in InputTrapezeEquations)
        {
            BattleChoices eqChoice = equation.getWishedOutcome;
            currentMembershipDegrees[eqChoice] = getHigherMembershipDegree(equation, currentMembershipDegrees[eqChoice], input);
        }
        return currentMembershipDegrees;
    }

    private double getHigherMembershipDegree(InputTrapezeEquation equation, double currentValue, double input)
    {
        double otherValue = equation.CalculateY(input);
        if (otherValue > currentValue)
            return otherValue;
        else
            return currentValue;
    }

    /*
    Testcase Lebenspunkte 
            Dictionary<BattleChoices, double> dict = new Dictionary<BattleChoices, double>();
            dict.Add(BattleChoices.ATTACK, 0d);
            dict.Add(BattleChoices.BLOCK, 0d);
            dict.Add(BattleChoices.ESCAPE, 0.0);
            Dictionary<BattleChoices, double> membertest = GetHighestMembershipDegreeToRules(75, dict);
            Debug.Log("Erwartet ist:");
            Debug.Log("Key: ATTACK hat Zugehörigkeit: " + 9.0 / 13.0);
            Debug.Log("Key: BLOCK hat Zugehörigkeit: " + 4.0 / 13.0);
            Debug.Log("Key: ESCAPE hat Zugehörigkeit: " + 0.0);
            Debug.Log("Berechnet wurde:");
            foreach (var key in membertest.Keys)
            {
                Debug.Log("Key: " + key + " hat Zugehörigkeit: " + membertest[key]);
            }
    Testcase Schaden
            Dictionary<BattleChoices, double> dict = new Dictionary<BattleChoices, double>();
            dict.Add(BattleChoices.ATTACK, 0d);
            dict.Add(BattleChoices.BLOCK, 0d);
            dict.Add(BattleChoices.ESCAPE, 0.0);
            Dictionary<BattleChoices, double> membertest = GetHighestMembershipDegreeToRules(30, dict);
            Debug.Log("Erwartet ist:");
            Debug.Log("Key: ATTACK hat Zugehörigkeit: " + 3.0/4.0);
            Debug.Log("Key: BLOCK hat Zugehörigkeit: " + 0.0);
            Debug.Log("Key: ESCAPE hat Zugehörigkeit: " + 0.0);
            Debug.Log("Berechnet wurde:");
            foreach (var key in membertest.Keys)
            {
                Debug.Log("Key: " + key + " hat Zugehörigkeit: " + membertest[key]);
            }
    Testcase Trainingseinheiten
            Dictionary<BattleChoices, double> dict = new Dictionary<BattleChoices, double>();
            dict.Add(BattleChoices.ATTACK, 0d);
            dict.Add(BattleChoices.BLOCK, 0d);
            dict.Add(BattleChoices.ESCAPE, 0.0);
            Dictionary<BattleChoices, double> membertest = GetHighestMembershipDegreeToRules(35, dict);
            Debug.Log("Erwartet ist:");
            Debug.Log("Key: ATTACK hat Zugehörigkeit: " + 0);
            Debug.Log("Key: BLOCK hat Zugehörigkeit: " + 7.0 / 13.0);
            Debug.Log("Key: ESCAPE hat Zugehörigkeit: " + 6.0 / 13.0);
            Debug.Log("Berechnet wurde:");
            foreach (var key in membertest.Keys)
            {
                Debug.Log("Key: " + key + " hat Zugehörigkeit: " + membertest[key]);
            }
    Testcase Schaden (Achtung, Rundungsfehler ganz am Ende)
            Dictionary<BattleChoices, double> dict = new Dictionary<BattleChoices, double>();
            dict.Add(BattleChoices.ATTACK, 0d);
            dict.Add(BattleChoices.BLOCK, 0d);
            dict.Add(BattleChoices.ESCAPE, 0.0);
            Dictionary<BattleChoices, double> membertest = GetHighestMembershipDegreeToRules(70, dict);
            Debug.Log("Erwartet ist:");
            Debug.Log("Key: ATTACK hat Zugehörigkeit: " + 2.0 / 3.0);
            Debug.Log("Key: BLOCK hat Zugehörigkeit: " + 0);
            Debug.Log("Key: ESCAPE hat Zugehörigkeit: " + 0);
            Debug.Log("Berechnet wurde:");
            foreach (var key in membertest.Keys)
            {
                Debug.Log("Key: " + key + " hat Zugehörigkeit: " + membertest[key]);
            }
    */

}
