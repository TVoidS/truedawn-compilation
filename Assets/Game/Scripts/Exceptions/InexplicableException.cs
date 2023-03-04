using System;

public class InexplicableException : Exception
{
    public InexplicableException() { }
    public InexplicableException(string message) : base(message)
    {

    }
    public InexplicableException(string message, Exception innerException) : base(message, innerException) 
    {

    }
}
