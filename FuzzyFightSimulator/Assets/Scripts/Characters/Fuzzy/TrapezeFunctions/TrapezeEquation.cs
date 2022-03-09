using UnityEngine;

public class TrapezeEquation : ScriptableObject, TrapezeEquationInterface
{
    [SerializeField] public double[] intersectionPoints;
    [SerializeField] public LinearEquation[] equations;
    [SerializeField] public string label;

    public double CalculateY(double input)
    {
        if (!(intersectionPoints[0] <= input || input <= intersectionPoints[intersectionPoints.Length - 1]))
            return -1;

        for (int i = 0; i < intersectionPoints.Length - 1; i++)
        {
            if (i % 2 != 0 && (intersectionPoints[i] < input && input < intersectionPoints[i + 1]))
                return equations[i].GetY(input);

            else if (i % 2 == 0 && (intersectionPoints[i] <= input && input <= intersectionPoints[i + 1]))
                return equations[i].GetY(input);
        }
        return -1;
    }

    public double CalculacteIntersection(LinearEquation one, LinearEquation two)
    {
        double denominator = (double)one.m - two.m;
        double numerator = (double)two.b - one.b;
        return (double)numerator / denominator;
    }

    /*
        public LinearEquation GetEquationForInput(double x)
        {
            if (!(intersectionPoints[0] <= x || x <= intersectionPoints[intersectionPoints.Length - 1]))
                return null;

            else if (intersectionPoints[0] <= x && x <= intersectionPoints[1])
                return equations[0];

            else if (intersectionPoints[1] < x && x < intersectionPoints[2])
                return equations[1];

            else if (intersectionPoints[2] <= x && x <= intersectionPoints[3])
                return equations[2];

            if (intersectionPoints.Length > 3 && equations.Length > 3)
            {
                if (intersectionPoints[3] <= x && x <= intersectionPoints[4])
                    return equations[3];
            }
            return null;
        }
    */

    /*
    Testcase 1
        equations = new LinearEquation[3];
        equations[0] = new LinearEquation(0, 1);
        equations[1] = new LinearEquation((-1d / 18d), (11d / 6d));
        equations[2] = new LinearEquation(0, 0);
        intersectionPoints = new[] { 0.0, 15.0, 33.0, 100.0 };
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
        equations = new LinearEquation[5];
        equations[0] = new LinearEquation(0, 0);
        equations[1] = new LinearEquation((1d / 18d), (-5d / 6d));
        equations[2] = new LinearEquation(0, 1);
        equations[3] = new LinearEquation((-1d / 15d), (16d / 3d));
        equations[4] = new LinearEquation(0, 0);
        intersectionPoints = new[] { 0.0, 15.0, 33.0, 65.0, 80.0, 100.0 };
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
