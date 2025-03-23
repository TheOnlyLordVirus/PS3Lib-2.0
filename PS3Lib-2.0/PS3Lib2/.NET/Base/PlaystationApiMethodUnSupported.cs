using System;

namespace PS3Lib2;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class PlaystationApiMethodUnSupported : Attribute
{
    public PlaystationApiMethodUnSupported() { }
}
