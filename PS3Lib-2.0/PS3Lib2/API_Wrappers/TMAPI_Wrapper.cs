using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using PS3Lib2.Attributes;
using PS3Lib2.Exceptions;

using static PS3TMAPI;

namespace PS3Lib2.Tmapi;

public sealed class TMAPI_Wrapper : Api_Wrapper
{
    public Assembly LoadedAssembly { get; private set; }

    private const int _port = 1000;

    private const string _libName = "ps3tmapi_net.dll";
    private const string _LibPathX = @"C:\Program Files\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private const string _LibPathX64 = @"C:\Program Files (x64)\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private const string _LibPathX86 = @"C:\Program Files (x86)\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private readonly string[] _LibLocations;

    public override bool IsConnected =>
        SUCCEEDED(GetConnectStatus(ConnectedTarget, out ConnectStatus status, out _)) &&
            status is ConnectStatus.Connected;

    public uint CurrentProcessId { get; private set; } = 0;
    public int ConnectedTarget { get; set; } = -1;

    private readonly static List<ConsoleInfo> _consoleList = [];
    private readonly static SearchTargetsCallback _searchTargetsCallback =
        (string name, string _, PS3TMAPI.TCPIPConnectProperties connectInfo, object _) =>
        {
            _consoleList.Add(new ConsoleInfo()
            {
                ConsoleIp = connectInfo.IPAddress,
                Port = connectInfo.Port,
                ConsoleName = name
            });
        };

    public override IEnumerable<ConsoleInfo> ConsolesInfo
    {
        get
        {
            _consoleList.Clear();

            if (!IsConnected)
            {
                return [];
            }

            if (SUCCEEDED(GetNumTargets(out uint numTargets)) &&
                numTargets is 0)
                return [];

            // This might not work.
            object unknownUserObject = new ();

            SearchForTargets
            (
                "10.0.0.0", 
                "10.255.255.255",
                _searchTargetsCallback,
                unknownUserObject,
                _port
            );

            SearchForTargets
            (
                "172.16.0.0",
                "172.31.255.255",
                _searchTargetsCallback,
                unknownUserObject,
                _port
            );

            SearchForTargets
            (
                "192.168.0.0",
                "192.168.255.255",
                _searchTargetsCallback,
                unknownUserObject,
                _port
            );

            _consoleList
                .ForEach(x => 
                { 
                    if (!SUCCEEDED(GetTargetFromName(x.ConsoleName, out int target)))
                        throw new PlaystationApiObjectInstanceException($"Failed to get {x.ConsoleName}'s target Id!");

                    x.LibId = (uint)target;
                });

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

            if (!SUCCEEDED(GetProcessList(ConnectedTarget, out uint[] pids)))
                throw new PlaystationApiObjectInstanceException("Unable to GetProcessList.");

            foreach (var id in pids)
            {
                if (!SUCCEEDED(
                    GetProcessInfoEx2
                        (
                            ConnectedTarget,
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
            ConnectedTarget = defaultTarget;
        else
            ConnectedTarget = 0;

        Internal_Init();
    }

    public TMAPI_Wrapper(int connectTarget) : base()
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

    public override bool Connect(in string ip) => SUCCEEDED(PS3TMAPI.Connect(ConnectedTarget, ip));

    public override bool Disconnect() => SUCCEEDED(PS3TMAPI.Disconnect(ConnectedTarget));

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void RingBuzzer() => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the RingBuzzer() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void VshNotify(in string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the VshNotify() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void SetIdps(in string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetIdps() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void SetPsid(in string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetPsid() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void GetIdps(out string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetPsid() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void GetPsid(out string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetPsid() call!");

    public override void ShutDown() => PowerOff(ConnectedTarget, true);

    [PlaystationApiMethodUnSupportedAttribute()]
    public override void GetTemprature(ref uint _, ref uint __) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the GetTemprature() call!");

    public override bool AttachGameProcess()
    {
        uint[] pids;
        GetProcessList(ConnectedTarget, out pids);

        if (pids.Length < 0)
            return false;

        return AttachProccess(pids[0]);
    }

    public override bool AttachProccess(in uint processId)
    {
        if (!SUCCEEDED(ProcessAttach(ConnectedTarget, UnitType.PPU, processId)) ||
            !SUCCEEDED(ProcessContinue(ConnectedTarget, processId)))
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
                ConnectedTarget,
                UnitType.PPU,
                CurrentProcessId,
                0,
                address,
                ref bytes
            );

        if (!SUCCEEDED(result))
            throw new Exception(result.ToString());
    }

    public override byte[] ReadMemory(in uint address, in uint size) 
    { 
        byte[] returnMe; 
        ReadMemory(address, size, out returnMe); 
        return returnMe; 
    }

    public override string ReadMemoryString(in uint address)
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

    public override void WriteMemory(in uint address, in byte[] bytes) => ProcessSetMemory(ConnectedTarget, PS3TMAPI.UnitType.PPU, CurrentProcessId, 0, address, bytes);

    public override void WriteMemoryString(in uint address, in string s) => throw new NotImplementedException();

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
}
