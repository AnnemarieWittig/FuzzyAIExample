using System.Collections.Generic;
using NUnit.Framework;

/*
    Die Testklasse überprüft die Implementierung der linguistischen Variable Trainingseinheiten. 
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
    Der Test TrainingseinheitenTestOne zeigt dabei die Berechnung zu der Eingabe, welche ebenfalls beispielhaft in der 
    Bachelorarbeit genutzt wurde.
*/
public class TrainingseinheitenVariableTest
{
    private double _delta = TestObjectRetriever.Delta;

    #region test calls - Methods needed to run tests with given input

    private void RunTest(double input, double expectedAttack, double expectedBlock, double expectedEscape)
    {
        InputLinguisticVariable Trainingseinheiten = TestObjectRetriever.GetTrainingseinheiten();
        Dictionary<BattleChoices, double> dict = TestObjectRetriever.GenerateEmptyDictionary();
        Dictionary<BattleChoices, double> membertest = Trainingseinheiten.GetHighestMembershipForRuleEvaluation(input, dict);

        Assert.AreEqual(expectedAttack, membertest[BattleChoices.ATTACK], _delta, "The values for the ATTACK choice are different than expected.");
        Assert.AreEqual(expectedBlock, membertest[BattleChoices.BLOCK], _delta, "The values for the BLOCK choice are different than expected.");
        Assert.AreEqual(expectedEscape, membertest[BattleChoices.ESCAPE], _delta, "The values for the ESCAPE choice are different than expected.");
    }

    #endregion

    #region test input + annotated test methods

    [Test]
    public void TrainingseinheitenTestOne()
    {
        double input = 35d;
        double expectedAttack = 0d;
        double expectedBlock = 7d / 13d;
        double expectedEscape = 6d / 13d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrainingseinheitenTestTwo()
    {
        double input = 0d;
        double expectedAttack = 0d;
        double expectedBlock = 0d;
        double expectedEscape = 1d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrainingseinheitenTestThree()
    {
        double input = 20d;
        double expectedAttack = 0d;
        double expectedBlock = 4d / 13d;
        double expectedEscape = 9d / 13d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrainingseinheitenTestFour()
    {
        double input = 65d;
        double expectedAttack = 0d;
        double expectedBlock = 1d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrainingseinheitenTestFive()
    {
        double input = 75d;
        double expectedAttack = 2d / 7d;
        double expectedBlock = 5d / 7d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrainingseinheitenTestSix()
    {
        double input = 90d;
        double expectedAttack = 5d / 7d;
        double expectedBlock = 2d / 7d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrainingseinheitenTestSeven()
    {
        double input = 100d;
        double expectedAttack = 1d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    #endregion
}
