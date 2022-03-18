using System;

public class NotCharacterTurnException : Exception
{
    public NotCharacterTurnException()
    {
    }

    public NotCharacterTurnException(string message)
        : base(message)
    {
    }

    public NotCharacterTurnException(string message, Exception inner)
        : base(message, inner)
    {
    }
}