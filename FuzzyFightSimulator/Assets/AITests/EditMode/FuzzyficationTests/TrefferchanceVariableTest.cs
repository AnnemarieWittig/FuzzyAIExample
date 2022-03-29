using System.Collections.Generic;
using NUnit.Framework;

/*
    Die Testklasse überprüft die Implementierung der linguistischen Variable Trefferchance. 
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
    Der Test TrefferchanceTestOne zeigt dabei die Berechnung zu der Eingabe, welche ebenfalls beispielhaft in der 
    Bachelorarbeit genutzt wurde.
*/
public class TrefferchanceVariableTest
{
    private double _delta = TestObjectRetriever.Delta;

    #region test calls - Methods needed to run tests with given input

    private void RunTest(double input, double expectedAttack, double expectedBlock, double expectedEscape)
    {
        InputLinguisticVariable Trefferchance = TestObjectRetriever.GetTrefferchance();
        Dictionary<BattleChoices, double> dict = TestObjectRetriever.GenerateEmptyDictionary();
        Dictionary<BattleChoices, double> membertest = Trefferchance.GetHighestMembershipForRuleEvaluation(input, dict);

        Assert.AreEqual(expectedAttack, membertest[BattleChoices.ATTACK], _delta, "The values for the ATTACK choice are different than expected.");
        Assert.AreEqual(expectedBlock, membertest[BattleChoices.BLOCK], _delta, "The values for the BLOCK choice are different than expected.");
        Assert.AreEqual(expectedEscape, membertest[BattleChoices.ESCAPE], _delta, "The values for the ESCAPE choice are different than expected.");
    }

    #endregion

    #region test input + annotated test methods

    [Test]
    public void TrefferchanceTestOne()
    {
        double input = 70d;
        double expectedAttack = 2d / 3d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrefferchanceTestTwo()
    {
        double input = 0d;
        double expectedAttack = 0d;
        double expectedBlock = 1d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrefferchanceTestThree()
    {
        double input = 10d;
        double expectedAttack = 0d;
        double expectedBlock = 1d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrefferchanceTestFour()
    {
        double input = 30d;
        double expectedAttack = 4d / 5d;
        double expectedBlock = 1d / 5d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrefferchanceTestFive()
    {
        double input = 50d;
        double expectedAttack = 1d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrefferchanceTestSix()
    {
        double input = 80d;
        double expectedAttack = 1d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void TrefferchanceTestSeven()
    {
        double input = 100d;
        double expectedAttack = 1d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;

        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    #endregion

}
