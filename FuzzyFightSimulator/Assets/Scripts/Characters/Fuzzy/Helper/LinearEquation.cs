using UnityEngine;

[CreateAssetMenu(menuName = "Equations/LinearEquations")]
public class LinearEquation : ScriptableObject
{
    [SerializeField] public double M;
    [SerializeField] public double B;

    public void initialize(double m_init, double b_init)
    {
        this.M = m_init;
        this.B = b_init;
    }

    public double GetY(double x)
    {
        return (double)M * x + B;
    }

    public static LinearEquation GenerateLinearEquation(double m_init, double b_init)
    {
        LinearEquation equation = ScriptableObject.CreateInstance<LinearEquation>();
        equation.initialize(m_init, b_init);
        return equation;
    }
}
