﻿using System.Collections.Generic;

using PS3Lib2.Base;
using PS3Lib2.Exceptions;

namespace PS3Lib2.Extentions;

public static class Extentions
{
    public static IEnumerable<string> GetSupportedMethods(this IPlaystationApi playstationApi)
    {
        IEnumerable<string> supportedMethods;

        try
        {
            var wrapper = playstationApi as Api_Wrapper;

            supportedMethods = wrapper.SupportedMethods;
        }

        catch (PlaystationApiObjectInstanceException Ex)
        {
            throw new PlaystationApiObjectInstanceException(Ex.Message);
        }

        return supportedMethods;
    }

    public static IEnumerable<string> GetUnSupportedMethods(this IPlaystationApi playstationApi)
    {
        IEnumerable<string> unsupportedMethods;

        try
        {
            var wrapper = playstationApi as Api_Wrapper;

            unsupportedMethods = wrapper.UnsupportedMethods;
        }

        catch (PlaystationApiObjectInstanceException Ex)
        {
            throw new PlaystationApiObjectInstanceException(Ex.Message);
        }

        return unsupportedMethods;
    }
}