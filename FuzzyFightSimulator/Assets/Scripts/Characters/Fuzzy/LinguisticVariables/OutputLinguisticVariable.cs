using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleChoices { ATTACK, BLOCK, ESCAPE }

public class OutputLinguisticVariable : MonoBehaviour
{
    [SerializeField] public OutputTrapezeEquation[] outputEquations;

    private void Start()
    {
        List<BattleChoices> counter = new List<BattleChoices>();
        foreach (OutputTrapezeEquation equation in outputEquations)
        {
            BattleChoices choice = equation.getCorrespongingBattleChoice();
            if (counter.Contains(choice))
                return; //ERROR
            else
                counter.Add(choice);
        }
    }

    public List<Rectangle> calculateDefuzzyfication(Dictionary<BattleChoices, double> membershipToLinguisticValues)
    {
        List<Rectangle> rectanglesForAOM = new List<Rectangle>();
        foreach (OutputTrapezeEquation membershipEquation in outputEquations)
        {
            Rectangle generatedRectangle = calculateRectangleFromMembershipEquation(membershipEquation, membershipToLinguisticValues);
            rectanglesForAOM.Add(generatedRectangle);
        }
        return rectanglesForAOM;
    }

    private Rectangle calculateRectangleFromMembershipEquation(OutputTrapezeEquation membershipEquation, Dictionary<BattleChoices, double> membershipToLinguisticValues)
    {
        BattleChoices correspondingChoice = membershipEquation.getCorrespongingBattleChoice();
        double membershipValue = membershipToLinguisticValues[correspondingChoice];
        LinearEquation equationToCut = new LinearEquation(0, membershipValue);
        return membershipEquation.CutTrapezeHorizontallyAndGenerateRectangle(equationToCut);
    }
}
