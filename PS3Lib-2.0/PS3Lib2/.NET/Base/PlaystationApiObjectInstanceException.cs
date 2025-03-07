using System;

namespace PS3Lib2.Exceptions;

internal sealed class PlaystationApiObjectInstanceException : Exception
{
    public PlaystationApiObjectInstanceException(string Message) : base(Message)
    {

    }
}
