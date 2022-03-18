using System;

public class LinguisticValueCannotHaveMultipleEquations : Exception
{
    public LinguisticValueCannotHaveMultipleEquations()
    {
    }

    public LinguisticValueCannotHaveMultipleEquations(string message)
        : base(message)
    {
    }

    public LinguisticValueCannotHaveMultipleEquations(string message, Exception inner)
        : base(message, inner)
    {
    }
}
