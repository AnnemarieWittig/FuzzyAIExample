using System.Collections.Generic;
using NUnit.Framework;

/*
    Die Testklasse überprüft die Implementierung der linguistischen Variable Lebenspunkte. 
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
    Der Test LebenspunkteTestOne zeigt dabei die Berechnung zu der Eingabe, welche ebenfalls beispielhaft in der 
    Bachelorarbeit genutzt wurde.
*/
public class LebenspunkteVariableTest
{
    private double _delta = 0.00000000001;

    private void RunTest(double input, double expectedAttack, double expectedBlock, double expectedEscape)
    {
        InputLinguisticVariable Lebenspunkte = TestObjectRetriever.GetLebenspunkte();
        Dictionary<BattleChoices, double> dict = TestObjectRetriever.GenerateEmptyDictionary();
        Dictionary<BattleChoices, double> membertest = Lebenspunkte.GetHighestMembershipForRuleEvaluation(input, dict);

        Assert.AreEqual(expectedAttack, membertest[BattleChoices.ATTACK], _delta, "The values for the ATTACK choice are different than expected.");
        Assert.AreEqual(expectedBlock, membertest[BattleChoices.BLOCK], _delta, "The values for the BLOCK choice are different than expected.");
        Assert.AreEqual(expectedEscape, membertest[BattleChoices.ESCAPE], _delta, "The values for the ESCAPE choice are different than expected.");
    }

    [Test]
    public void LebenspunkteTestOne()
    {
        double input = 50d;
        double expectedAttack = 1d / 2d;
        double expectedBlock = 1d / 2d;
        double expectedEscape = 0d;
        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void LebenspunkteTestTwo()
    {
        double input = 0d;
        double expectedAttack = 0d;
        double expectedBlock = 0d;
        double expectedEscape = 1d;
        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void LebenspunkteTestThree()
    {
        double input = 15d;
        double expectedAttack = 0d;
        double expectedBlock = 0d;
        double expectedEscape = 1d;
        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void LebenspunkteTestFour()
    {
        double input = 25d;
        double expectedAttack = 0d;
        double expectedBlock = 1d / 2d;
        double expectedEscape = 1d / 2d;
        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void LebenspunkteTestFive()
    {
        double input = 40d;
        double expectedAttack = 0d;
        double expectedBlock = 1d;
        double expectedEscape = 0d;
        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void LebenspunkteTestSix()
    {
        double input = 60d;
        double expectedAttack = 1d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;
        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }

    [Test]
    public void LebenspunkteTestSeven()
    {
        double input = 100d;
        double expectedAttack = 1d;
        double expectedBlock = 0d;
        double expectedEscape = 0d;
        RunTest(input, expectedAttack, expectedBlock, expectedEscape);
    }
}
