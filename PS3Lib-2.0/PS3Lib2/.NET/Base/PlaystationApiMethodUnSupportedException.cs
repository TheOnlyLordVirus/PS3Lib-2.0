using System;

namespace PS3Lib2.Exceptions;

internal sealed class PlaystationApiMethodUnSupportedException : NotImplementedException
{
    public PlaystationApiMethodUnSupportedException(string message) : base(message) { }
}