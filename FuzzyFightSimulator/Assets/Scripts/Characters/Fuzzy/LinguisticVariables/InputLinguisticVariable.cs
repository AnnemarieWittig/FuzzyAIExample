using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLinguisticVariable : MonoBehaviour
{ //degree of membership

    [SerializeField] public InputTrapezeEquation[] inputTrapezeEquations;

    public Dictionary<BattleChoices, double> GetHighestMembershipDegreeToRule(double input, Dictionary<BattleChoices, double> currentMembershipDegrees)
    {
        foreach (InputTrapezeEquation equation in inputTrapezeEquations)
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

}
