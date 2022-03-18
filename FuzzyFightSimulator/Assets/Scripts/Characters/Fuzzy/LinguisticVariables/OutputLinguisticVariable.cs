using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/OutputLinguisticVariable")]
public class OutputLinguisticVariable : LinguisticVariable
{
    [SerializeField] public OutputTrapezeEquation[] outputEquations;

    private void Start()
    {
        checkForDuplicateEquations();
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
        LinearEquation equationToCut = LinearEquation.GenerateLinearEquation(0, membershipValue);
        return membershipEquation.CutTrapezeHorizontallyAndGenerateRectangle(equationToCut);
    }

    private void checkForDuplicateEquations()
    {
        List<BattleChoices> counter = new List<BattleChoices>();
        foreach (OutputTrapezeEquation equation in outputEquations)
        {
            BattleChoices choice = equation.getCorrespongingBattleChoice();
            if (counter.Contains(choice))
                throw new LinguisticValueCannotHaveMultipleEquations("There are multiple equations for " + choice);
            else
                counter.Add(choice);
        }
    }

    /*
    Testcase
        Dictionary<BattleChoices, double> dict = new Dictionary<BattleChoices, double>();
        dict.Add(BattleChoices.ESCAPE, 6d / 13d);
        dict.Add(BattleChoices.BLOCK, 7d / 13d);
        dict.Add(BattleChoices.ATTACK, 3d / 4d);
        Debug.Log("Erwartet ist:");
        Debug.Log("ESCAPE Rechteck: Höhe " + 6d / 13d + ", Start " + 0d + ", Ende " + 300d / 13d);
        Debug.Log("BLOCK: Höhe " + 7d / 13d + ", Start " + 300d / 13d + ", Ende " + 740d / 13d);
        Debug.Log("ATTACK: Höhe " + 0.75 + ", Start " + 61.25 + ", Ende " + 100);
        Debug.Log("Berechnet wurde:");
        List<Rectangle> rectangles = calculateDefuzzyfication(dict);
        foreach (var rectangle in rectangles)
        {
            Debug.Log("Rechteck: Höhe " + rectangle.getMaximum + ", Start " + rectangle.getStart + ", Ende " + rectangle.getEnd);
        }
    */
}
