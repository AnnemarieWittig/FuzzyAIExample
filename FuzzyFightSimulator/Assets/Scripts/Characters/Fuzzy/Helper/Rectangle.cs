public class Rectangle
{
    private double Start;
    private double End;
    private double Maximum;

    public Rectangle(double start_init, double end_init, double maximum_init)
    {
        Start = start_init;
        End = end_init;
        Maximum = maximum_init;
    }

    public double getD()
    {
        return (double)((Start + End) / 2f);
    }

    public double getStart => Start;
    public double getEnd => End;
    public double getMaximum => Maximum;

}
