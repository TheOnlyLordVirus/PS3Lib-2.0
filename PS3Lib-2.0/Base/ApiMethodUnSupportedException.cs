using System;

namespace PS3Lib2.Exceptions;

internal sealed class ApiMethodUnSupportedException : NotImplementedException
{
    public ApiMethodUnSupportedException(string message) : base(message) { }
}