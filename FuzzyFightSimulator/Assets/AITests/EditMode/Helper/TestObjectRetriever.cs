using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;


public class TestObjectRetriever
{
    public static readonly double Delta = 0.000000000001;


    #region In- and Output linguistic variables

    public static InputLinguisticVariable GetTrainingseinheiten()
    {
        string _pathToLinguisticVariable = "Assets/ScriptableObjects/FuzzyEquations/Trainingseinheiten";
        string _label = "Trainingseinheiten";
        return GetInputLinguisticVariable(_label, _pathToLinguisticVariable);
    }

    public static InputLinguisticVariable GetSchaden()
    {
        string _pathToLinguisticVariable = "Assets/ScriptableObjects/FuzzyEquations/Schaden";
        string _label = "Schaden";
        return GetInputLinguisticVariable(_label, _pathToLinguisticVariable);
    }

    public static InputLinguisticVariable GetTrefferchance()
    {
        string _pathToLinguisticVariable = "Assets/ScriptableObjects/FuzzyEquations/Trefferchance";
        string _label = "Trefferchance";
        return GetInputLinguisticVariable(_label, _pathToLinguisticVariable);
    }

    public static InputLinguisticVariable GetLebenspunkte()
    {
        string _pathToLinguisticVariable = "Assets/ScriptableObjects/FuzzyEquations/Lebenspunkte";
        string _label = "Lebenspunkte";
        return GetInputLinguisticVariable(_label, _pathToLinguisticVariable);
    }

    public static OutputLinguisticVariable GetKampfentscheidung()
    {
        string _pathToLinguisticVariable = "Assets/ScriptableObjects/FuzzyEquations/Kampfentscheidung";
        string _label = "Kampfentscheidung";
        return GetOutputLinguisticVariable(_label, _pathToLinguisticVariable);
    }

    public static InputLinguisticVariable GetInputLinguisticVariable(string scriptableObjectName, string objectPath)
    {
        string[] guids = AssetDatabase.FindAssets($"t:{nameof(InputLinguisticVariable)} {scriptableObjectName }", new[] { objectPath });
        if (guids.Length == 0)
            Assert.Fail($"No {nameof(InputLinguisticVariable)} found named {scriptableObjectName}");
        if (guids.Length > 1)
            Debug.LogWarning($"More than one {nameof(InputLinguisticVariable)} found named {scriptableObjectName}, taking first one");

        return (InputLinguisticVariable)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(InputLinguisticVariable));
    }

    public static OutputLinguisticVariable GetOutputLinguisticVariable(string scriptableObjectName, string objectPath)
    {
        string[] guids = AssetDatabase.FindAssets($"t:{nameof(OutputLinguisticVariable)} {scriptableObjectName }", new[] { objectPath });
        if (guids.Length == 0)
            Assert.Fail($"No {nameof(OutputLinguisticVariable)} found named {scriptableObjectName}");
        if (guids.Length > 1)
            Debug.LogWarning($"More than one {nameof(OutputLinguisticVariable)} found named {scriptableObjectName}, taking first one");

        return (OutputLinguisticVariable)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(OutputLinguisticVariable));
    }

    #endregion
    #region dictionary

    public static Dictionary<BattleChoices, double> GenerateEmptyDictionary()
    {
        return GenerateFilledDictionary(0d, 0d, 0d);
    }

    public static Dictionary<BattleChoices, double> GenerateFilledDictionary(double attack, double block, double escape)
    {
        Dictionary<BattleChoices, double> dict = new Dictionary<BattleChoices, double>();
        dict.Add(BattleChoices.ATTACK, attack);
        dict.Add(BattleChoices.BLOCK, block);
        dict.Add(BattleChoices.ESCAPE, escape);
        return dict;
    }

    #endregion

    #region Instantiate Monobehaviors
    public static T CreateMonobehavior<T>()
    {
        return (T)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(T));
    }

    public static CharacterAI GenerateCharacterAIMockup()
    {
        CharacterAI characterAi = TestObjectRetriever.CreateMonobehavior<CharacterAI>();
        characterAi.Lebenspunkte = TestObjectRetriever.GetLebenspunkte();
        characterAi.Schaden = TestObjectRetriever.GetSchaden();
        characterAi.Trefferchance = TestObjectRetriever.GetTrefferchance();
        characterAi.Trainingseinheiten = TestObjectRetriever.GetTrainingseinheiten();
        characterAi.Kampfentscheidung = TestObjectRetriever.GetKampfentscheidung();
        return characterAi;
    }

    public static Character GenerateCharacterMockup(float tc, float hp, float te, float dmg)
    {
        Character character = TestObjectRetriever.CreateMonobehavior<Character>();
        character.HitChance = tc;
        character.MaxHP = 100;
        character.CurrentHP = hp;
        character.TrainingHours = te;
        character.Damage = dmg;
        return character;
    }

    #endregion
}
