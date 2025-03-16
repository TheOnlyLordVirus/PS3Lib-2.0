using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using PS3Lib2.Attributes;
using PS3Lib2.Exceptions;

using TargetManagerApi = PS3TMAPI;

namespace PS3Lib2.Tmapi;

public sealed class TMAPI_Wrapper : Api_Wrapper
{
    public Assembly loadedAssembly { get; private set; }

    public uint CurrentProcessId { get; private set; } = 0;

    public int ConnectedTarget { get; set; } = -1;

    private const string _libName = "ps3tmapi_net.dll";
    private const string _LibPathX = @"C:\Program Files\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private const string _LibPathX64 = @"C:\Program Files (x64)\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private const string _LibPathX86 = @"C:\Program Files (x86)\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private readonly string[] _LibLocations;

    public override bool IsConnected => 
        TargetManagerApi.GetConnectStatus(ConnectedTarget, out TargetManagerApi.ConnectStatus status, out _) is TargetManagerApi.SNRESULT.SN_S_OK && 
            status is TargetManagerApi.ConnectStatus.Connected;

    public TMAPI_Wrapper()
    {
        _LibLocations = [_libName, _LibPathX, _LibPathX64, _LibPathX86];

        // Default Connect Target
        ConnectedTarget = 0;

        Internal_Init();
    }

    public TMAPI_Wrapper(int connectTarget)
    {
        _LibLocations = [_libName, _LibPathX, _LibPathX64, _LibPathX86];

        ConnectedTarget = connectTarget;

        Internal_Init();
    }

    ~TMAPI_Wrapper()
    {
        Dispose();
    }

    private void Internal_Init()
    {
        Internal_InitLibs();

        if (!TargetManagerApi.SUCCEEDED(TargetManagerApi.InitTargetComms()))
            throw new PlaystationApiObjectInstanceException("Failed to InitTargetComms for TMAPI!");
    }

    private void Internal_InitLibs()
    {
        foreach (string libPath in _LibLocations)
            if (File.Exists(libPath))
            {
                loadedAssembly = Assembly.LoadFile(libPath);
                return;
            }

        throw new PlaystationApiObjectInstanceException($"You must have TargetManagerApi_net.dll Installed!");
    }

    public override bool Connect(string ip) => TargetManagerApi.SUCCEEDED(TargetManagerApi.Connect(ConnectedTarget, ip));

    public override bool Disconnect() => TargetManagerApi.SUCCEEDED(TargetManagerApi.Disconnect(ConnectedTarget));

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void RingBuzzer() => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the RingBuzzer() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void VshNotify(string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the VshNotify() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void SetIdps(string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetIdps() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void SetPsid(string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetPsid() call!");

    public override void ShutDown() => TargetManagerApi.PowerOff(ConnectedTarget, true);

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void GetTemprature(ref uint _, ref uint __) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the GetTemprature() call!");

    public override bool AttachGameProcess()
    {
        uint[] pids;
        TargetManagerApi.GetProcessList(ConnectedTarget, out pids);

        if (pids.Length < 0)
            return false;

        return AttachProccess(pids[0]);
    }

    public override bool AttachProccess(uint processId)
    {
        if (!TargetManagerApi
                .SUCCEEDED(
                    TargetManagerApi
                        .ProcessAttach(ConnectedTarget, TargetManagerApi.UnitType.PPU, processId)) ||
            !TargetManagerApi
                .SUCCEEDED(
                    TargetManagerApi
                        .ProcessContinue(ConnectedTarget, processId)))
            return false;

        CurrentProcessId = processId;

        return true;
    }


    #region Read Memory

    //TODO: Handle endianess

    public override void ReadMemory(uint address, uint size, out byte[] bytes) 
    { 
        bytes = new byte[size];

        var result = TargetManagerApi.ProcessGetMemory
            (
                ConnectedTarget,
                TargetManagerApi.UnitType.PPU,
                CurrentProcessId,
                0,
                address,
                ref bytes
            );

        if (!TargetManagerApi.SUCCEEDED(result))
            throw new Exception(result.ToString());
    }

    public override byte[] ReadMemory(uint address, uint size) 
    { 
        byte[] returnMe; 
        ReadMemory(address, size, out returnMe); 
        return returnMe; 
    }

    public override string ReadMemoryString(uint address)
    {
        throw new NotImplementedException();
    }


    //[PlaystationApiMethodUnSupportedAttribute()]
    //public bool TryPatternScan
    //   (in byte?[] patternInput,
    //    in uint patternSearchStartAddress,
    //    in uint patternSearchEndAddress,
    //    out uint? startingAddress,
    //    out byte[]? dataRead,
    //    out uint? length)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region Write Memory

    public override void WriteMemory(uint address, byte[] bytes) => TargetManagerApi.ProcessSetMemory(ConnectedTarget, PS3TMAPI.UnitType.PPU, CurrentProcessId, 0, address, bytes);

    public override void WriteMemoryString(uint address, string s) => throw new NotImplementedException();

    #endregion

    [DllImport("kernel32.dll")]
    static extern bool FreeLibrary(IntPtr a);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    public override void Dispose()
    {
        if (IsConnected)
            Disconnect();

        IntPtr tmapiHandle = GetModuleHandle(_libName);

        if (tmapiHandle == IntPtr.Zero)
            return;

        FreeLibrary(tmapiHandle);
    }
}
