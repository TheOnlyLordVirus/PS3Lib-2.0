using System;

namespace PS3Lib2.Exceptions;

public sealed class PlaystationNotConnectedException : Exception
{
    public PlaystationNotConnectedException(string message) : base(message) { }
}
