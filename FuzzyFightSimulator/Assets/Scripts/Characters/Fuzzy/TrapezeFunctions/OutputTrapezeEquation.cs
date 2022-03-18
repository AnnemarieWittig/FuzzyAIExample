using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equations/OutputTrapezeEquations")]
public class OutputTrapezeEquation : TrapezeEquation
{
    [SerializeField] private BattleChoices correspondingEnum;

    public Rectangle CutTrapezeHorizontallyAndGenerateRectangle(LinearEquation horizontalLine)
    {
        double height = horizontalLine.b;
        List<LinearEquation> equationsToCut = GetVerticalLines();
        if (equationsToCut.Count == 1)
            return GenerateRectangleWithOneIntersection(horizontalLine, equationsToCut[0], intersectionPoints[0], intersectionPoints[intersectionPoints.Length - 1]);
        else if (equationsToCut.Count == 2)
            return GenerateRectangleWithTwoIntersections(horizontalLine, equationsToCut[0], equationsToCut[1]);
        else
            throw new TooManyIntersectionsDetected("With the current implementation, a rectangle can only be created between two intersections. However, we detected "
            + equationsToCut.Count + " equations that would intersect with the given one.");
    }

    private Rectangle GenerateRectangleWithOneIntersection(LinearEquation horizontalLine, LinearEquation FirstLineToCut, double min, double max)
    {
        double height = horizontalLine.b;
        double intersectionX = CalculacteIntersection(FirstLineToCut, horizontalLine);
        double rectangleStart = intersectionX;
        double rectangleEnd = intersectionX;
        if (CalculateY(intersectionX + 0.1d) >= height)
            rectangleEnd = max;
        else if (CalculateY(intersectionX - 0.1d) >= height)
            rectangleStart = min;
        return new Rectangle(rectangleStart, rectangleEnd, height);
    }

    private Rectangle GenerateRectangleWithTwoIntersections(LinearEquation horizontalLine, LinearEquation FirstLineToCut, LinearEquation SecondLineToCut)
    {
        double height = horizontalLine.b;
        double firstIntersection = CalculacteIntersection(FirstLineToCut, horizontalLine);
        double secondIntersection = CalculacteIntersection(SecondLineToCut, horizontalLine);
        if (firstIntersection <= secondIntersection)
            return new Rectangle(firstIntersection, secondIntersection, height);
        else
            return new Rectangle(secondIntersection, firstIntersection, height);
    }

    private List<LinearEquation> GetVerticalLines()
    {
        List<LinearEquation> verticalEquations = new List<LinearEquation>();
        foreach (var line in equations)
        {
            if ((double)line.m != 0.0d)
                verticalEquations.Add(line);
        }
        return verticalEquations;
    }

    public BattleChoices getCorrespongingBattleChoice() => correspondingEnum;
}
