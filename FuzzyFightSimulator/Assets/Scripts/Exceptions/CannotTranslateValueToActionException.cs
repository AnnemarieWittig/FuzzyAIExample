using System;

public class CannotTranslateValueToActionException : Exception
{
    public CannotTranslateValueToActionException()
    {
    }

    public CannotTranslateValueToActionException(string message)
        : base(message)
    {
    }

    public CannotTranslateValueToActionException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
