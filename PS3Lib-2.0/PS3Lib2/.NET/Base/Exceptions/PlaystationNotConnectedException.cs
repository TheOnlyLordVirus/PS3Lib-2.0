using System;

namespace PS3Lib2.Exceptions;

public sealed class PlaystationNotConnectedException(string message) : Exception(message)
{
}
