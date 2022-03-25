using System.Collections.Generic;
using NUnit.Framework;

/*
    Die Testklasse überprüft die Implementierung der linguistischen Eingabe-Variablen im Bezug auf die
    Erzeugung eines Dictionaries für ein gegebenes Set von Eingaben.
    Der Test ist dabei eine Weiterführung der Einzeltests der Klassen SchadensVariableTest, LebenspunkteVariableTest,
    TrainingseinheitenVariableTest und TrefferchanceVariableTest. In diesem Fall werden die gleichen Funktionen 
    getestet, jedoch alle nacheinander, sodass am Ende das Dictionary für alle drei BattleChoices (alle drei
    linguistischen Werte zu Kampfentscheidung) die jeweils höchsten zugehörigen Zugehörigkeitsgrade (nach Regelwerk)
    abbilden. 
    Der Test LebenspunkteTestOne zeigt dabei die Berechnung zu der Eingabe, welche ebenfalls beispielhaft in der 
    Bachelorarbeit genutzt wurde.
*/

public class AllInputVariablesTest
{
    private double _delta = 0.00000000001;

    private void RunTest(double inputTc, double inputDmg, double inputHp, double inputTe,
        double expectedAttack, double expectedBlock, double expectedEscape)
    {
        InputLinguisticVariable Lebenspunkte = TestObjectRetriever.GetLebenspunkte();
        InputLinguisticVariable Trefferchance = TestObjectRetriever.GetTrefferchance();
        InputLinguisticVariable Trainingseinheiten = TestObjectRetriever.GetTrainingseinheiten();
        InputLinguisticVariable Schaden = TestObjectRetriever.GetSchaden();

        Dictionary<BattleChoices, double> membertest = TestObjectRetriever.GenerateEmptyDictionary();
        membertest = Lebenspunkte.GetHighestMembershipForRuleEvaluation(inputHp, membertest);
        membertest = Trefferchance.GetHighestMembershipForRuleEvaluation(inputTc, membertest);
        membertest = Trainingseinheiten.GetHighestMembershipForRuleEvaluation(inputTe, membertest);
        membertest = Schaden.GetHighestMembershipForRuleEvaluation(inputDmg, membertest);

        Assert.AreEqual(expectedAttack, membertest[BattleChoices.ATTACK], _delta, "The values for the ATTACK choice are different than expected.");
        Assert.AreEqual(expectedBlock, membertest[BattleChoices.BLOCK], _delta, "The values for the BLOCK choice are different than expected.");
        Assert.AreEqual(expectedEscape, membertest[BattleChoices.ESCAPE], _delta, "The values for the ESCAPE choice are different than expected.");
    }

    [Test]
    public void AllVariablesTestOne()
    {
        double inputTc = 70d;
        double inputDmg = 30d;
        double inputHp = 50d;
        double inputTe = 35d;
        double expectedAttack = 4d / 5d;
        double expectedBlock = 7d / 13d;
        double expectedEscape = 6d / 13d;

        RunTest(inputTc, inputDmg, inputHp, inputTe, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void AllVariablesTestTwo()
    {
        double inputTc = 0d;
        double inputDmg = 0d;
        double inputHp = 0d;
        double inputTe = 0d;
        double expectedAttack = 0d;
        double expectedBlock = 1d;
        double expectedEscape = 1d;

        RunTest(inputTc, inputDmg, inputHp, inputTe, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void AllVariablesTestThree()
    {
        double inputTc = 30d;
        double inputDmg = 25d;
        double inputHp = 25d;
        double inputTe = 75d;
        double expectedAttack = 1d;
        double expectedBlock = 5d / 7d;
        double expectedEscape = 1d / 2d;

        RunTest(inputTc, inputDmg, inputHp, inputTe, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void AllVariablesTestFour()
    {
        double inputTc = 80d;
        double inputDmg = 70d;
        double inputHp = 15d;
        double inputTe = 20d;
        double expectedAttack = 1d;
        double expectedBlock = 4d / 13d;
        double expectedEscape = 1d;

        RunTest(inputTc, inputDmg, inputHp, inputTe, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void AllVariablesTestFive()
    {
        double inputTc = 50d;
        double inputDmg = 45d;
        double inputHp = 40d;
        double inputTe = 65d;
        double expectedAttack = 1d;
        double expectedBlock = 1d;
        double expectedEscape = 0d;

        RunTest(inputTc, inputDmg, inputHp, inputTe, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void AllVariablesTestSix()
    {
        double inputTc = 10d;
        double inputDmg = 12d;
        double inputHp = 60d;
        double inputTe = 90d;
        double expectedAttack = 1d;
        double expectedBlock = 1d;
        double expectedEscape = 0d;

        RunTest(inputTc, inputDmg, inputHp, inputTe, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void AllVariablesTestSeven()
    {
        double inputTc = 100d;
        double inputDmg = 100d;
        double inputHp = 100d;
        double inputTe = 100d;
        double expectedAttack = 1d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;

        RunTest(inputTc, inputDmg, inputHp, inputTe, expectedAttack, expectedBlock, expectedEscape);
    }
}
