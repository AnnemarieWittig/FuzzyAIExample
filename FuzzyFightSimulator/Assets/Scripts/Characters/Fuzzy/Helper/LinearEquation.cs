using UnityEngine;

[CreateAssetMenu(menuName = "Equations/LinearEquations")]
public class LinearEquation : ScriptableObject
{
    [SerializeField] public double m;
    [SerializeField] public double b;

    public void initialize(double m_init, double b_init)
    {
        this.m = m_init;
        this.b = b_init;
    }

    public double GetY(double x)
    {
        return (double)m * x + b;
    }

    public static LinearEquation GenerateLinearEquation(double m_init, double b_init)
    {
        LinearEquation equation = ScriptableObject.CreateInstance<LinearEquation>();
        equation.initialize(m_init, b_init);
        return equation;
    }
}
