using PS3Lib2.Gui;
using System;

namespace PS3Lib2.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class ApiMethodUnSupportedAttribute : Attribute
{

    public ApiMethodUnSupportedAttribute() { }

    public ApiMethodUnSupportedAttribute(IPlaystationApiGui gui)
    {
        try
        {

        }

        catch 
        {
        
        }
    }
}
