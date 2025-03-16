using System;

namespace PS3Lib2.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class PlaystationApiMethodUnSupportedAttribute : Attribute
{
    public PlaystationApiMethodUnSupportedAttribute() { }
}
