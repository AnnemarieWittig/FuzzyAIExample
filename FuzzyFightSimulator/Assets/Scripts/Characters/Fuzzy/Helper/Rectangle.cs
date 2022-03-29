public class Rectangle
{
    private double _start;
    private double _end;
    private double _maximum;
    private BattleChoices _correspondingBattleChoice;

    public Rectangle(double start_init, double end_init, double maximum_init, BattleChoices battleChoice_init)
    {
        _start = start_init;
        _end = end_init;
        _maximum = maximum_init;
        _correspondingBattleChoice = battleChoice_init;
    }
    public Rectangle(double start_init, double end_init, double maximum_init)
    {
        _start = start_init;
        _end = end_init;
        _maximum = maximum_init;
    }

    public double getD()
    {
        return (double)((_start + _end) / 2f);
    }

    public double Start => _start;
    public double End => _end;
    public double Maximum => _maximum;
    public BattleChoices CorrespondingBattleChoice => _correspondingBattleChoice;
    public BattleChoices SetCorrespondingBattleChoice(BattleChoices choice) => _correspondingBattleChoice = choice;

}
