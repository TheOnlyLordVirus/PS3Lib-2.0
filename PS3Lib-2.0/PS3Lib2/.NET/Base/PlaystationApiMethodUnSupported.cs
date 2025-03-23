using System;

namespace PS3Lib2;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
internal sealed class PlaystationApiMethodUnSupported : Attribute
{
    public PlaystationApiMethodUnSupported() { }
}
