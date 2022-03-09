using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle
{
    private double start;
    private double end;
    private double maximum;

    public Rectangle(double start_init, double end_init, double maximum_init)
    {
        start = start_init;
        end = end_init;
        maximum = maximum_init;
    }

    public double getD()
    {
        return (double)((start + end) / 2f);
    }

    public double getStart => start;
    public double getEnd => end;
    public double getMaximum => maximum;

}
