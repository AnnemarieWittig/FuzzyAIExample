using System.Collections.Generic;
using NUnit.Framework;

/*
The test setup of this test class is suboptimal as the method AOM_Testing that is checked here is usually
supposed to be a private method. However, for the sake of proving the workings of the code, a public
wrapper for this method has been built in. This shall point out, that the result of the Average of Maxima
calculation works as it has to and is mainly to support the Bachelor Thesis this code was invented as part of.
*/
public class CharacterAiAOMTest
{
    private double _delta = TestObjectRetriever.Delta;

    // corresponding to the results of KampfentscheidungVariableTest.RectangleCalculationTest1
    [Test]
    public void AomTest1()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        List<Rectangle> rectangles = new List<Rectangle>();
        Rectangle attackRectangle = new Rectangle(52d, 100d, 4d / 5d, BattleChoices.ATTACK);
        Rectangle blockRectangle = new Rectangle(235d / 13d, 610d / 13d, 7d / 13d, BattleChoices.BLOCK);
        Rectangle escapeRectangle = new Rectangle(0d, 235d / 13d, 6d / 13d, BattleChoices.ESCAPE);
        rectangles.AddRange(new[] { attackRectangle, blockRectangle, escapeRectangle });

        double actualAomResult = aiMock.AOMTestWrapper(rectangles);
        double expectedAomResult = 139377d / 3042d;
        Assert.AreEqual(expectedAomResult, actualAomResult, _delta, "The result of the AOM method is not as expected.");
    }

    // corresponding to the results of KampfentscheidungVariableTest.RectangleCalculationTest2
    [Test]
    public void AomTest2()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        List<Rectangle> rectangles = new List<Rectangle>();
        Rectangle blockRectangle = new Rectangle(25d, 40d, 1d, BattleChoices.BLOCK);
        Rectangle escapeRectangle = new Rectangle(0d, 10d, 1d, BattleChoices.ESCAPE);
        rectangles.AddRange(new[] { blockRectangle, escapeRectangle });

        double actualAomResult = aiMock.AOMTestWrapper(rectangles);
        double expectedAomResult = 75d / 4d;
        Assert.AreEqual(expectedAomResult, actualAomResult, _delta, "The result of the AOM method is not as expected.");
    }

    // corresponding to the results of KampfentscheidungVariableTest.RectangleCalculationTest3
    [Test]
    public void AomTest3()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        List<Rectangle> rectangles = new List<Rectangle>();
        Rectangle attackRectangle = new Rectangle(55d, 100d, 1d, BattleChoices.ATTACK);
        Rectangle blockRectangle = new Rectangle(145d / 7d, 310d / 7d, 5d / 7d, BattleChoices.BLOCK);
        Rectangle escapeRectangle = new Rectangle(0d, 35d / 2d, 1d / 2d, BattleChoices.ESCAPE);
        rectangles.AddRange(new[] { attackRectangle, blockRectangle, escapeRectangle });

        double actualAomResult = aiMock.AOMTestWrapper(rectangles);
        double expectedAomResult = 5885d / 124d;
        Assert.AreEqual(expectedAomResult, actualAomResult, _delta, "The result of the AOM method is not as expected.");
    }

    // corresponding to the results of KampfentscheidungVariableTest.RectangleCalculationTest4
    [Test]
    public void AomTest4()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        List<Rectangle> rectangles = new List<Rectangle>();
        Rectangle attackRectangle = new Rectangle(55d, 100d, 1d, BattleChoices.ATTACK);
        Rectangle blockRectangle = new Rectangle(190d / 13d, 655d / 13d, 4d / 13d, BattleChoices.BLOCK);
        Rectangle escapeRectangle = new Rectangle(0d, 10d, 1d, BattleChoices.ESCAPE);
        rectangles.AddRange(new[] { attackRectangle, blockRectangle, escapeRectangle });

        double actualAomResult = aiMock.AOMTestWrapper(rectangles);
        double expectedAomResult = 481d / 12d;
        Assert.AreEqual(expectedAomResult, actualAomResult, _delta, "The result of the AOM method is not as expected.");
    }

    // corresponding to the results of KampfentscheidungVariableTest.RectangleCalculationTest5
    [Test]
    public void AomTest5()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        List<Rectangle> rectangles = new List<Rectangle>();
        Rectangle attackRectangle = new Rectangle(55d, 100d, 1d, BattleChoices.ATTACK);
        Rectangle blockRectangle = new Rectangle(25d, 40d, 1d, BattleChoices.BLOCK);
        rectangles.AddRange(new[] { attackRectangle, blockRectangle });

        double actualAomResult = aiMock.AOMTestWrapper(rectangles);
        double expectedAomResult = 55d;
        Assert.AreEqual(expectedAomResult, actualAomResult, _delta, "The result of the AOM method is not as expected.");
    }

    // corresponding to the results of KampfentscheidungVariableTest.RectangleCalculationTest6
    [Test]
    public void AomTest6()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        List<Rectangle> rectangles = new List<Rectangle>();
        Rectangle attackRectangle = new Rectangle(175d / 4d, 100d, 1d / 4d, BattleChoices.ATTACK);
        Rectangle blockRectangle = new Rectangle(35d / 2d, 95d / 2d, 1d / 2d, BattleChoices.BLOCK);
        Rectangle escapeRectangle = new Rectangle(0d, 55d / 4d, 3d / 4d, BattleChoices.ESCAPE);
        rectangles.AddRange(new[] { attackRectangle, blockRectangle, escapeRectangle });

        double actualAomResult = aiMock.AOMTestWrapper(rectangles);
        double expectedAomResult = 105d / 4d;
        Assert.AreEqual(expectedAomResult, actualAomResult, _delta, "The result of the AOM method is not as expected.");
    }

    // corresponding to the results of KampfentscheidungVariableTest.RectangleCalculationTest7
    [Test]
    public void AomTest7()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        List<Rectangle> rectangles = new List<Rectangle>();
        Rectangle attackRectangle = new Rectangle(55d, 100d, 1d, BattleChoices.ATTACK);
        rectangles.AddRange(new[] { attackRectangle });

        double actualAomResult = aiMock.AOMTestWrapper(rectangles);
        double expectedAomResult = 155d / 2d;
        Assert.AreEqual(expectedAomResult, actualAomResult, _delta, "The result of the AOM method is not as expected.");
    }
}
