using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

using PS3Lib2.Base;
using PS3Lib2.Exceptions;

using static PS3TMAPI;

namespace PS3Lib2.Tmapi;

public sealed class TMAPI_Wrapper : Api_Wrapper
{
    private const string _toString = "TMAPI";

    public Assembly LoadedAssembly { get; private set; }

    private const int _port = 1000;
    private const string _ipPortRegex = @"([\d]{1,3}\.[\d]{1,3}\.[\d]{1,3}\.[\d]{1,3}):([\d]{1,5})";

    private const string _libName = "ps3tmapi_net.dll";
    private const string _LibPathX = @"C:\Program Files\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private const string _LibPathX64 = @"C:\Program Files (x64)\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private const string _LibPathX86 = @"C:\Program Files (x86)\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private readonly string[] _LibLocations;

    public override bool IsConnected =>
        SUCCEEDED(GetConnectStatus(CurrentTarget, out ConnectStatus status, out _)) &&
            status is ConnectStatus.Connected;

    private uint CurrentProcessId { get; set; } = 0;
    public int CurrentTarget { get; set; } = -1;

    private readonly static List<ConsoleInfo> _consoleList = [];

    public override IEnumerable<ConsoleInfo> ConsolesInfo
    {
        get
        {
            _consoleList.Clear();

            if (SUCCEEDED(GetNumTargets(out uint numTargets)) &&
                numTargets is 0)
                return [];

            for (int curTarget =  0; curTarget < numTargets; ++curTarget)
            {

                TargetInfo TargetInfo = new ();
                TargetInfo.Flags = TargetInfoFlag.TargetID;
                TargetInfo.Target = curTarget;

                if (!SUCCEEDED(GetTargetInfo(ref TargetInfo)))
                    continue;

                var match = Regex.Match(TargetInfo.Info, _ipPortRegex);

                if (!match.Success)
                    continue;

                string[] token = match.Value.Split(':');

                if (token.Length == 0)
                    continue;

                if (!uint.TryParse(token[1], out uint port))
                    port = _port;

                _consoleList.Add(new ()
                {
                    Id = (uint)TargetInfo.Target,
                    Port = port,
                    ConsoleIp = token[0],
                    ConsoleName = TargetInfo.Name,
                    ConsoleDescription = TargetInfo.Info
                });
            }

            return _consoleList;
        }
    }

    public override IEnumerable<ProcessInfo> ProcessesInfo
    {
        get 
        {
            List<ProcessInfo> procList = [];

            if (!IsConnected)
                return [];

            if (!SUCCEEDED(GetProcessList(CurrentTarget, out uint[] pids)))
                throw new PlaystationApiObjectInstanceException("Unable to GetProcessList.");

            foreach (var id in pids)
            {
                if (!SUCCEEDED(
                    GetProcessInfoEx2
                        (
                            CurrentTarget,
                            id, 
                            out PS3TMAPI.ProcessInfo processInfo,
                            out ExtraProcessInfo extraProcessInfo,
                            out ProcessLoadInfo processLoadInfo
                        )))
                        throw new PlaystationApiObjectInstanceException("Unable to GetProcessList.");

                procList.Add(new ProcessInfo()
                {
                    ProcessName = processInfo.Hdr.ELFPath,
                    ProcessId = id,
                });
            }

            return procList;
        } 
    }

    public TMAPI_Wrapper() : base()
    {
        _LibLocations = [_libName, _LibPathX, _LibPathX64, _LibPathX86];

        // Default Connect Target
        if (SUCCEEDED(GetDefaultTarget(out int defaultTarget)))
            CurrentTarget = defaultTarget;
        else
            CurrentTarget = 0;

        Internal_Init();
    }

    public TMAPI_Wrapper(in int connectTarget) : base()
    {
        _LibLocations = [_libName, _LibPathX, _LibPathX64, _LibPathX86];

        CurrentTarget = connectTarget;

        Internal_Init();
    }

    ~TMAPI_Wrapper()
    {
        Dispose();
    }

    private void Internal_Init()
    {
        Internal_InitLibs();

        if (!SUCCEEDED(InitTargetComms()))
            throw new PlaystationApiObjectInstanceException("Failed to InitTargetComms for TMAPI!");
    }

    private void Internal_InitLibs()
    {
        foreach (string libPath in _LibLocations)
            if (File.Exists(libPath))
            {
                LoadedAssembly = Assembly.LoadFile(libPath);
                return;
            }

        throw new PlaystationApiObjectInstanceException($"You must have TargetManagerApi_net.dll Installed!");
    }

    public override bool Connect(in string ip) => SUCCEEDED(PS3TMAPI.Connect(CurrentTarget, ip));

    public override bool Disconnect() => SUCCEEDED(PS3TMAPI.Disconnect(CurrentTarget));

    [PlaystationApiMethodUnSupported()]
    public override void RingBuzzer(in BuzzerMode _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the RingBuzzer() call!");

    [PlaystationApiMethodUnSupported()]
    public override void VshNotify(in string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the VshNotify() call!");

    [PlaystationApiMethodUnSupported()]
    public override void SetIdps(in string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetIdps() call!");

    [PlaystationApiMethodUnSupported()]
    public override void SetPsid(in string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetPsid() call!");

    [PlaystationApiMethodUnSupported()]
    public override void GetIdps(out string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the GetIdps() call!");

    [PlaystationApiMethodUnSupported()]
    public override void GetPsid(out string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the GetPsid() call!");

    public override void ShutDown() => PowerOff(CurrentTarget, true);

    [PlaystationApiMethodUnSupported()]
    public override void GetTemprature(ref uint _, ref uint __) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the GetTemprature() call!");

    public override bool AttachGameProcess()
    {
        uint[] pids;
        GetProcessList(CurrentTarget, out pids);

        if (pids.Length < 0)
            return false;

        return AttachProccess(pids[0]);
    }

    public override bool AttachProccess(in uint processId)
    {
        if (!SUCCEEDED(ProcessAttach(CurrentTarget, UnitType.PPU, processId)) ||
            !SUCCEEDED(ProcessContinue(CurrentTarget, processId)))
            return false;

        CurrentProcessId = processId;

        return true;
    }


    #region Read Memory

    //TODO: Handle endianess

    public override void ReadMemory(in uint address, in uint size, out byte[] bytes) 
    { 
        bytes = new byte[size];

        var result = ProcessGetMemory
            (
                CurrentTarget,
                UnitType.PPU,
                CurrentProcessId,
                0,
                address,
                ref bytes
            );

        if (!SUCCEEDED(result))
            throw new PlaystationApiObjectInstanceException(result.ToString());
    }

    [PlaystationApiMethodUnSupported()]
    public override string ReadMemoryString(in uint address)
        => throw new PlaystationApiMethodUnSupportedException("The MemoryString methods have not been added yet.");

    [PlaystationApiMethodUnSupported()]
    public override bool TryPatternScan
       (in byte?[] patternInput,
        in uint patternSearchStartAddress,
        in uint patternSearchEndAddress,
        out uint? startingAddress,
        out byte[]? dataRead,
        out uint? length)
            => throw new PlaystationApiMethodUnSupportedException("The TryPatternScan methods have not been added yet.");

    #endregion

    #region Write Memory

    public override void WriteMemory(in uint address, in byte[] bytes) => ProcessSetMemory(CurrentTarget, PS3TMAPI.UnitType.PPU, CurrentProcessId, 0, address, bytes);

    [PlaystationApiMethodUnSupported()]
    public override void WriteMemoryString(in uint address, in string s)
         => throw new PlaystationApiMethodUnSupportedException("The MemoryString methods have not been added yet.");

    #endregion

    [DllImport("kernel32.dll")]
    static extern bool FreeLibrary(IntPtr a);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    public override void Dispose()
    {
        if (IsConnected)
            Disconnect();

        CloseTargetComms();

        IntPtr tmapiHandle = GetModuleHandle(_libName);

        if (tmapiHandle == IntPtr.Zero)
            return;

        FreeLibrary(tmapiHandle);
    }

    public override string ToString() => _toString;
}
