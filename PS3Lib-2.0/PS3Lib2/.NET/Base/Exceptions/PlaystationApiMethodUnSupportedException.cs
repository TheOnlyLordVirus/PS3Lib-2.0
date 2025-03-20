using System;

namespace PS3Lib2.Exceptions;

public sealed class PlaystationApiMethodUnSupportedException(string message) : NotImplementedException(message)
{
}