using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TrapezeEquationInterface
{
    public double CalculateY(double x);

    public double CalculacteIntersection(LinearEquation one, LinearEquation two);

    //public Rectangle CutTrapezeHorizontallyAndGenerateRectangle(LinearEquation horizontalLine);

}
