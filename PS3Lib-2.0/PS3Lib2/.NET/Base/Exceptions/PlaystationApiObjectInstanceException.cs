using System;

namespace PS3Lib2.Exceptions;

public sealed class PlaystationApiObjectInstanceException(string Message) : Exception(Message)
{
}
