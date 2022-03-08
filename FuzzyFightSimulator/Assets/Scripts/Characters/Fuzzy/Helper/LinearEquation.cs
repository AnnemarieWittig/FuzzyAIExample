using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equations/LinearEquations")]
public class LinearEquation : ScriptableObject
{
    [SerializeField] public double m;
    [SerializeField] public double b;

    public LinearEquation(double m_init, double b_init)
    {
        this.m = m_init;
        this.b = b_init;
    }

    public double GetY(double x)
    {
        return (double)m * x + b;
    }
}
