using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CalculationOverview : MonoBehaviour
{
    [SerializeField] public TMP_Text Content;
    [SerializeField] public GameObject Overview;

    public void AddIntroduction(Character character)
    {
        string intro = "------------\n" +
        $"Entscheidung für {character.CharacterName} \n" +
        $"HP: {character.CurrentHP}\n" +
        $"DMG: {character.Damage}\n" +
        $"TC: {character.HitChance}\n" +
        $"TE: {character.TrainingHours}";
        AddContent(intro);
    }

    public void AddRectangles(List<Rectangle> list)
    {
        string rectangles = $"Es wurden {list.Count} Rechtecke erzeugt:\n";
        foreach (Rectangle rect in list)
        {
            rectangles += $"Rechteck: Breite von {rect.Start} -> {rect.End} mit Maximum von {rect.Maximum};\n";
        }
        AddContent(rectangles);
    }

    public void AddAOMResult(double result)
    {
        AddContent("Das Ergebnis der AOM-Methode lautet: " + result);
    }

    public void AddDecision(BattleChoices choice)
    {
        switch (choice)
        {
            case BattleChoices.ATTACK:
                AddContent("Der Charakter entscheidet sich für einen Angriff.");
                break;
            case BattleChoices.BLOCK:
                AddContent("Der Charakter entscheidet sich für einen Block.");
                break;
            case BattleChoices.ESCAPE:
                AddContent("Der Charakter entscheidet sich für eine Flucht.");
                break;
        }
    }

    private void AddContent(string content)
    {
        Content.text += "\n";
        Content.text += content;
    }

}
