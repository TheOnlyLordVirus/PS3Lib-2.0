using PS3Lib2.Gui;
using System;

namespace PS3Lib2.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class PlaystationApiMethodUnSupportedAttribute : Attribute
{

    public PlaystationApiMethodUnSupportedAttribute() { }

    public PlaystationApiMethodUnSupportedAttribute(IPlaystationApiGui gui)
    {
        try
        {

        }

        catch 
        {
        
        }
    }
}
