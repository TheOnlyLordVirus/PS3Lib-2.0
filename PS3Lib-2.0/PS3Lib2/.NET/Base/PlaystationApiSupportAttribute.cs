﻿using System;
using System.Collections.Generic;

using PS3Lib2.Interfaces;

namespace PS3Lib2.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class PlaystationApiSupportAttribute : Attribute
{
    public IPlaystationApi API { get; private set; }

    private readonly string[] _supportedMethods;
    public string[] SupportedMethods
    {
        get => _supportedMethods;
    }

    private readonly bool _hasFullApiSupport;
    public bool HasFullApiSupport
    {
        get => _hasFullApiSupport;

        init
        {
            List<string> supportedMethods = new();

            var methods = API.GetType().GetMethods();

            bool tempSupportVar = true;

            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(PlaystationApiMethodUnSupportedAttribute)))
                {
                    supportedMethods.Add(method.Name);

                    continue;
                }

                tempSupportVar = false;
            }

            _supportedMethods = supportedMethods.ToArray();

            _hasFullApiSupport = tempSupportVar;
        }
    }

    public bool HasPartialApiSupport => !_hasFullApiSupport;


    public PlaystationApiSupportAttribute()
    {

    }
}


