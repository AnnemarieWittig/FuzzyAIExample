using System;

public class TooManyIntersectionsDetected : Exception
{
    public TooManyIntersectionsDetected()
    {
    }

    public TooManyIntersectionsDetected(string message)
        : base(message)
    {
    }

    public TooManyIntersectionsDetected(string message, Exception inner)
        : base(message, inner)
    {
    }
}
