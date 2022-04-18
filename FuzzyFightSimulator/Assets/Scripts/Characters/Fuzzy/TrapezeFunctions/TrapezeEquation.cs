using UnityEngine;

public class TrapezeEquation : ScriptableObject, TrapezeEquationInterface
{
    [SerializeField] public double[] IntersectionPoints;
    [SerializeField] public LinearEquation[] Equations;
    [SerializeField] public string Label;

    public double CalculateY(double input)
    {
        if (!(IntersectionPoints[0] <= input || input <= IntersectionPoints[IntersectionPoints.Length - 1]))
            return -1;

        for (int i = 0; i < IntersectionPoints.Length - 1; i++)
        {
            if (i % 2 != 0 && (IntersectionPoints[i] < input && input < IntersectionPoints[i + 1]))
                return Equations[i].GetY(input);

            else if (i % 2 == 0 && (IntersectionPoints[i] <= input && input <= IntersectionPoints[i + 1]))
                return Equations[i].GetY(input);
        }
        return -1;
    }

    public double CalculateIntersection(LinearEquation one, LinearEquation two)
    {
        double denominator = (double)one.M - two.M;
        double numerator = (double)two.B - one.B;
        return (double)numerator / denominator;
    }

    /*
    Testcase 1
        Equations = new LinearEquation[3];
        Equations[0] = new LinearEquation(0, 1);
        Equations[1] = new LinearEquation((-1d / 18d), (11d / 6d));
        Equations[2] = new LinearEquation(0, 0);
        IntersectionPoints = new[] { 0.0, 15.0, 33.0, 100.0 };
        Debug.Log("Für x=" + 0 + " ergibt sich " + CalculateY(0));
        Debug.Log("Für x=" + 15 + " ergibt sich " + CalculateY(15));
        Debug.Log("Für x=" + 16 + " ergibt sich " + CalculateY(16));
        Debug.Log("Für x=" + 20 + " ergibt sich " + CalculateY(20));
        Debug.Log("Für x=" + 26 + " ergibt sich " + CalculateY(26));
        Debug.Log("Für x=" + 33 + " ergibt sich " + CalculateY(33));
        Debug.Log("Für x=" + 35 + " ergibt sich " + CalculateY(35));
        Debug.Log("Für x=" + 70 + " ergibt sich " + CalculateY(70));
        Debug.Log("Für x=" + 100 + " ergibt sich " + CalculateY(100));
        Rectangle test = CutTrapezeHorizontallyAndGenerateRectangle(new LinearEquation(0.0d, 0.6d));
        Debug.Log("Start: " + test.getStart + "; End: " + test.getEnd + "; Height: " + test.getMaximum);
    */

    /*
    Testcase 2
        Equations = new LinearEquation[5];
        Equations[0] = new LinearEquation(0, 0);
        Equations[1] = new LinearEquation((1d / 18d), (-5d / 6d));
        Equations[2] = new LinearEquation(0, 1);
        Equations[3] = new LinearEquation((-1d / 15d), (16d / 3d));
        Equations[4] = new LinearEquation(0, 0);
        IntersectionPoints = new[] { 0.0, 15.0, 33.0, 65.0, 80.0, 100.0 };
        Debug.Log((-1.0 / 18.0));
        Debug.Log("Für x=" + 0 + " ergibt sich " + CalculateY(0));
        Debug.Log("Für x=" + 15 + " ergibt sich " + CalculateY(15));
        Debug.Log("Für x=" + 20 + " ergibt sich " + CalculateY(20));
        Debug.Log("Für x=" + 25 + " ergibt sich " + CalculateY(25));
        Debug.Log("Für x=" + 30 + " ergibt sich " + CalculateY(30));
        Debug.Log("Für x=" + 33 + " ergibt sich " + CalculateY(33));
        Debug.Log("Für x=" + 35 + " ergibt sich " + CalculateY(35));
        Debug.Log("Für x=" + 50 + " ergibt sich " + CalculateY(50));
        Debug.Log("Für x=" + 65 + " ergibt sich " + CalculateY(65));
        Debug.Log("Für x=" + 70 + " ergibt sich " + CalculateY(70));
        Debug.Log("Für x=" + 80 + " ergibt sich " + CalculateY(80));
        Debug.Log("Für x=" + 90 + " ergibt sich " + CalculateY(90));
        Debug.Log("Für x=" + 100 + " ergibt sich " + CalculateY(100));
        Rectangle test = CutTrapezeHorizontallyAndGenerateRectangle(new LinearEquation(0.0d, 0.6d));
        Debug.Log("Start: " + test.getStart + "; End: " + test.getEnd + "; Height: " + test.getMaximum);
    */
}
