using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System;

/*
    Die Testklasse überprüft die Implementierung der linguistischen Variable XXX. 
    Dabei wird zuerst überprüft, dass bei einer gegebenen Eingabe ein korrektes Dictionary erzeugt wird.
    In dieses werden die Zugehörigkeit der Variable zu den linguistischen Werten entsprechend des 
    Regelwerks eingetragen. Das heißt, im Beispiel von der Variable Trefferchance, welche den Werten
    "schlecht", "okay" und "gut" zugeordnet werden kann, lauten die Regeln vereinfacht notiert:
        schlecht -> Block
        okay -> Angriff
        gut -> Anfriff
    In einem Test zu dieser Variable müsste im Dictionary der zu BattleChoices.ESCAPE zugeordnete Wert also 
    immer 0d sein, weil in diesem Fall keine Regeln auf diese BattleChoice angewendet werden soll. Für 
    BattleChoices.BLOCK soll äquivalent nur der Zugehörigkeitswert zu dem linguistischen Wert "schlecht" notiert 
    sein und zu .ATTACK die höhere Zugehörigkeit zu "okay" oder "schlecht". Dieses Prinzip lässt sich auf die 
    anderen Variablen übertragen.
    Der Test XXX TestOne zeigt dabei die Berechnung zu der Eingabe, welche ebenfalls beispielhaft in der 
    Bachelorarbeit genutzt wurde.
*/
public class KampfentscheidungVariableTest
{
    private double _delta = TestObjectRetriever.Delta;

    #region test calls - Methods needed to run tests with given input
    private void RunTest(List<Rectangle> defuzzyRectangles, Rectangle expectedAttackRectangle, Rectangle expectedBlockRectangle, Rectangle expectedEscapeRectangle)
    {
        foreach (Rectangle rectangle in defuzzyRectangles)
        {
            switch (rectangle.CorrespondingBattleChoice)
            {
                case BattleChoices.ATTACK:
                    Assert.IsTrue(compareRectangles(rectangle, expectedAttackRectangle), ErrorMessage(BattleChoices.ATTACK, expectedAttackRectangle, rectangle));
                    break;
                case BattleChoices.BLOCK:
                    Assert.IsTrue(compareRectangles(rectangle, expectedBlockRectangle), ErrorMessage(BattleChoices.BLOCK, expectedBlockRectangle, rectangle));
                    break;
                case BattleChoices.ESCAPE:
                    Assert.IsTrue(compareRectangles(rectangle, expectedEscapeRectangle), ErrorMessage(BattleChoices.ESCAPE, expectedEscapeRectangle, rectangle));
                    break;
            }
        }
    }

    private bool compareRectangles(Rectangle actual, Rectangle expected)
    {
        if (!(Math.Abs(actual.Start - expected.Start) <= _delta))
            return false;
        if (!(Math.Abs(actual.End - expected.End) <= _delta))
            return false;
        if (!(Math.Abs(actual.Maximum - expected.Maximum) <= _delta))
            return false;
        if (actual.CorrespondingBattleChoice != expected.CorrespondingBattleChoice)
            return false;

        return true;
    }

    private string ErrorMessage(BattleChoices choice, Rectangle expected, Rectangle actual)
    {
        return $"The rectangle generated from the membershipfunction for the linguistic value {choice} do not have the correct values:\n"
        + $"Expected was: start {expected.Start}, end {expected.End}, maximum {expected.Maximum} and BattleChoices {expected.CorrespondingBattleChoice};\n "
        + $"But is: start {actual.Start}, end {actual.End}, maximum {actual.Maximum} and BattleChoices {actual.CorrespondingBattleChoice}.";
    }
    #endregion

    #region test input + annotated test methods
    // corresponding to AllInputVariablesTest.AllVariablesTest1
    [Test]
    public void RectangleCalculationTest1()
    {
        OutputLinguisticVariable kampfentscheidung = TestObjectRetriever.GetKampfentscheidung();
        Dictionary<BattleChoices, double> membervalues = TestObjectRetriever.GenerateFilledDictionary(4d / 5d, 7d / 13d, 6d / 13d);
        List<Rectangle> defuzzyRectangles = kampfentscheidung.CalculateDefuzzyfication(membervalues);
        Rectangle attackRectangle = new Rectangle(52d, 100d, 4d / 5d, BattleChoices.ATTACK);
        Rectangle blockRectangle = new Rectangle(235d / 13d, 610d / 13d, 7d / 13d, BattleChoices.BLOCK);
        Rectangle escapeRectangle = new Rectangle(0d, 235d / 13d, 6d / 13d, BattleChoices.ESCAPE);
        Assert.AreEqual(3, defuzzyRectangles.Count);
        RunTest(defuzzyRectangles, attackRectangle, blockRectangle, escapeRectangle);
    }

    // corresponding to AllInputVariablesTest.AllVariablesTest2
    [Test]
    public void RectangleCalculationTest2()
    {
        OutputLinguisticVariable kampfentscheidung = TestObjectRetriever.GetKampfentscheidung();
        Dictionary<BattleChoices, double> membervalues = TestObjectRetriever.GenerateFilledDictionary(0d, 1d, 1d);
        List<Rectangle> defuzzyRectangles = kampfentscheidung.CalculateDefuzzyfication(membervalues);
        Rectangle attackRectangle = null; // A rectangle with the maximum of 0d should not be created;
        Rectangle blockRectangle = new Rectangle(25d, 40d, 1d, BattleChoices.BLOCK);
        Rectangle escapeRectangle = new Rectangle(0d, 10d, 1d, BattleChoices.ESCAPE);
        Assert.AreEqual(2, defuzzyRectangles.Count); // No rectangle for Attack
        RunTest(defuzzyRectangles, attackRectangle, blockRectangle, escapeRectangle);
    }

    // corresponding to AllInputVariablesTest.AllVariablesTest3
    [Test]
    public void RectangleCalculationTest3()
    {
        OutputLinguisticVariable kampfentscheidung = TestObjectRetriever.GetKampfentscheidung();
        Dictionary<BattleChoices, double> membervalues = TestObjectRetriever.GenerateFilledDictionary(1d, 5d / 7d, 1d / 2d);
        List<Rectangle> defuzzyRectangles = kampfentscheidung.CalculateDefuzzyfication(membervalues);
        Rectangle attackRectangle = new Rectangle(55d, 100d, 1d, BattleChoices.ATTACK);
        Rectangle blockRectangle = new Rectangle(145d / 7d, 310d / 7d, 5d / 7d, BattleChoices.BLOCK);
        Rectangle escapeRectangle = new Rectangle(0d, 35d / 2d, 1d / 2d, BattleChoices.ESCAPE);
        Assert.AreEqual(3, defuzzyRectangles.Count);
        RunTest(defuzzyRectangles, attackRectangle, blockRectangle, escapeRectangle);
    }

    // corresponding to AllInputVariablesTest.AllVariablesTest4
    [Test]
    public void RectangleCalculationTest4()
    {
        OutputLinguisticVariable kampfentscheidung = TestObjectRetriever.GetKampfentscheidung();
        Dictionary<BattleChoices, double> membervalues = TestObjectRetriever.GenerateFilledDictionary(1d, 4d / 13d, 1d);
        List<Rectangle> defuzzyRectangles = kampfentscheidung.CalculateDefuzzyfication(membervalues);
        Rectangle attackRectangle = new Rectangle(55d, 100d, 1d, BattleChoices.ATTACK);
        Rectangle blockRectangle = new Rectangle(190d / 13d, 655d / 13d, 4d / 13d, BattleChoices.BLOCK);
        Rectangle escapeRectangle = new Rectangle(0d, 10d, 1d, BattleChoices.ESCAPE);
        Assert.AreEqual(3, defuzzyRectangles.Count);
        RunTest(defuzzyRectangles, attackRectangle, blockRectangle, escapeRectangle);
    }

    // corresponding to AllInputVariablesTest Tests 5 and 6
    [Test]
    public void RectangleCalculationTest5()
    {
        OutputLinguisticVariable kampfentscheidung = TestObjectRetriever.GetKampfentscheidung();
        Dictionary<BattleChoices, double> membervalues = TestObjectRetriever.GenerateFilledDictionary(1d, 1d, 0d);
        List<Rectangle> defuzzyRectangles = kampfentscheidung.CalculateDefuzzyfication(membervalues);
        Rectangle attackRectangle = new Rectangle(55d, 100d, 1d, BattleChoices.ATTACK);
        Rectangle blockRectangle = new Rectangle(25d, 40d, 1d, BattleChoices.BLOCK);
        Rectangle escapeRectangle = null; // A rectangle with the maximum of 0d should not be created;
        Assert.AreEqual(2, defuzzyRectangles.Count);
        RunTest(defuzzyRectangles, attackRectangle, blockRectangle, escapeRectangle);
    }

    // corresponding to no other test
    [Test]
    public void RectangleCalculationTest6()
    {
        OutputLinguisticVariable kampfentscheidung = TestObjectRetriever.GetKampfentscheidung();
        Dictionary<BattleChoices, double> membervalues = TestObjectRetriever.GenerateFilledDictionary(1d / 4d, 1d / 2d, 3d / 4d);
        List<Rectangle> defuzzyRectangles = kampfentscheidung.CalculateDefuzzyfication(membervalues);
        Rectangle attackRectangle = new Rectangle(175d / 4d, 100d, 1d / 4d, BattleChoices.ATTACK);
        Rectangle blockRectangle = new Rectangle(35d / 2d, 95d / 2d, 1d / 2d, BattleChoices.BLOCK);
        Rectangle escapeRectangle = new Rectangle(0d, 55d / 4d, 3d / 4d, BattleChoices.ESCAPE);
        Assert.AreEqual(3, defuzzyRectangles.Count);
        RunTest(defuzzyRectangles, attackRectangle, blockRectangle, escapeRectangle);
    }

    // corresponding to AllInputVariablesTest.AllVariablesTest7
    [Test]
    public void RectangleCalculationTest7()
    {
        OutputLinguisticVariable kampfentscheidung = TestObjectRetriever.GetKampfentscheidung();
        Dictionary<BattleChoices, double> membervalues = TestObjectRetriever.GenerateFilledDictionary(1d, 0d, 0d);
        List<Rectangle> defuzzyRectangles = kampfentscheidung.CalculateDefuzzyfication(membervalues);
        Rectangle attackRectangle = new Rectangle(55d, 100d, 1d, BattleChoices.ATTACK);
        Rectangle blockRectangle = null; // A rectangle with the maximum of 0d should not be created;
        Rectangle escapeRectangle = null; // A rectangle with the maximum of 0d should not be created;
        Assert.AreEqual(1, defuzzyRectangles.Count);
        RunTest(defuzzyRectangles, attackRectangle, blockRectangle, escapeRectangle);
    }
    #endregion

}
