using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CharacterAIDecisionTest
{

    // corresponding to the results of AllInputVariablesTest.AllVariablesTest1
    [Test]
    public void AITest1()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        Character characterMock = TestObjectRetriever.GenerateCharacterMockup(70f, 50f, 35f, 30f);
        aiMock.AiCharacter = characterMock;

        BattleChoices actualChoice = aiMock.MakeBattleDecision();
        BattleChoices expectedChoice = BattleChoices.BLOCK;
        Assert.AreEqual(expectedChoice, actualChoice, "The AI's decision is not as expected.");
    }

    // corresponding to the results of AllInputVariablesTest.AllVariablesTest2
    [Test]
    public void AITest2()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        Character characterMock = TestObjectRetriever.GenerateCharacterMockup(0f, 0f, 0f, 0f);
        aiMock.AiCharacter = characterMock;

        BattleChoices actualChoice = aiMock.MakeBattleDecision();
        BattleChoices expectedChoice = BattleChoices.ESCAPE;
        Assert.AreEqual(expectedChoice, actualChoice, "The AI's decision is not as expected.");
    }

    // corresponding to the results of AllInputVariablesTest.AllVariablesTest3
    [Test]
    public void AITest3()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        Character characterMock = TestObjectRetriever.GenerateCharacterMockup(30f, 25f, 25f, 75f);
        aiMock.AiCharacter = characterMock;

        BattleChoices actualChoice = aiMock.MakeBattleDecision();
        BattleChoices expectedChoice = BattleChoices.BLOCK;
        Assert.AreEqual(expectedChoice, actualChoice, "The AI's decision is not as expected.");
    }

    // corresponding to the results of AllInputVariablesTest.AllVariablesTest4
    [Test]
    public void AITest4()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        Character characterMock = TestObjectRetriever.GenerateCharacterMockup(80f, 15f, 20f, 70f);
        aiMock.AiCharacter = characterMock;

        BattleChoices actualChoice = aiMock.MakeBattleDecision();
        BattleChoices expectedChoice = BattleChoices.BLOCK;
        Assert.AreEqual(expectedChoice, actualChoice, "The AI's decision is not as expected.");
    }

    // corresponding to the results of AllInputVariablesTest.AllVariablesTest5
    [Test]
    public void AITest5()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        Character characterMock = TestObjectRetriever.GenerateCharacterMockup(50f, 40f, 65f, 45f);
        aiMock.AiCharacter = characterMock;

        BattleChoices actualChoice = aiMock.MakeBattleDecision();
        BattleChoices expectedChoice = BattleChoices.ATTACK;
        Assert.AreEqual(expectedChoice, actualChoice, "The AI's decision is not as expected.");
    }

    // corresponding to the results of AllInputVariablesTest.AllVariablesTest6
    [Test]
    public void AITest6()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        Character characterMock = TestObjectRetriever.GenerateCharacterMockup(10f, 60f, 90f, 12f);
        aiMock.AiCharacter = characterMock;

        BattleChoices actualChoice = aiMock.MakeBattleDecision();
        BattleChoices expectedChoice = BattleChoices.ATTACK;
        Assert.AreEqual(expectedChoice, actualChoice, "The AI's decision is not as expected.");
    }

    // corresponding to the results of AllInputVariablesTest.AllVariablesTest7
    [Test]
    public void AITest7()
    {
        CharacterAI aiMock = TestObjectRetriever.GenerateCharacterAIMockup();
        Character characterMock = TestObjectRetriever.GenerateCharacterMockup(100f, 100f, 100f, 100f);
        aiMock.AiCharacter = characterMock;

        BattleChoices actualChoice = aiMock.MakeBattleDecision();
        BattleChoices expectedChoice = BattleChoices.ATTACK;
        Assert.AreEqual(expectedChoice, actualChoice, "The AI's decision is not as expected.");
    }
}
