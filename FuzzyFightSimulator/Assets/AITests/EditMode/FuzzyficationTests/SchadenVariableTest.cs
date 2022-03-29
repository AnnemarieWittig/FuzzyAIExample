using System.Collections.Generic;
using NUnit.Framework;

/*
    Die Testklasse überprüft die Implementierung der linguistischen Variable Schaden. 
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
    Der Test SchadenTestOne zeigt dabei die Berechnung zu der Eingabe, welche ebenfalls beispielhaft in der 
    Bachelorarbeit genutzt wurde.
*/
public class SchadenVariableTest
{
    private double _delta = TestObjectRetriever.Delta;

    #region test calls - Methods needed to run tests with given input

    public void RunTest(double input, double expectedAttack, double expectedBlock, double expectedEscape)
    {
        InputLinguisticVariable Schaden = TestObjectRetriever.GetSchaden();
        Dictionary<BattleChoices, double> dict = TestObjectRetriever.GenerateEmptyDictionary();
        Dictionary<BattleChoices, double> membertest = Schaden.GetHighestMembershipForRuleEvaluation(input, dict);

        Assert.AreEqual(expectedAttack, membertest[BattleChoices.ATTACK], _delta, "The values for the ATTACK choice are different than expected.");
        Assert.AreEqual(expectedBlock, membertest[BattleChoices.BLOCK], _delta, "The values for the BLOCK choice are different than expected.");
        Assert.AreEqual(expectedEscape, membertest[BattleChoices.ESCAPE], _delta, "The values for the ESCAPE choice are different than expected.");
    }

    #endregion

    #region test input + annotated test methods

    [Test]
    public void SchadenTestOne()
    {
        double input = 30d;
        double expectedAttack = 4d / 5d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void SchadenTestTwo()
    {
        double input = 0d;
        double expectedAttack = 0d;
        double expectedBlock = 1d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void SchadenTestThree()
    {
        double input = 12d;
        double expectedAttack = 2d / 5d;
        double expectedBlock = 3d / 5d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void SchadenTestFour()
    {
        double input = 25d;
        double expectedAttack = 1d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void SchadenTestFive()
    {
        double input = 45d;
        double expectedAttack = 4d / 5d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void SchadenTestSix()
    {
        double input = 70d;
        double expectedAttack = 1d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void SchadenTestSeven()
    {
        double input = 100d;
        double expectedAttack = 1d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    #endregion
}
