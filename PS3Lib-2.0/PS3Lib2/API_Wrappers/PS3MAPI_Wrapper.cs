using System.Linq;
using System.Collections.Generic;

using PS3ManagerAPI;

using PS3Lib2.Base;
using PS3Lib2.Exceptions;

namespace PS3Lib2.PS3Mapi;

public sealed class PS3MAPI_Wrapper : Api_Wrapper
{
    private const string _toString = "PS3MAPI";

    private readonly PS3MAPI _currentPS3ManagerApi;
    public PS3MAPI CurrentPS3ManagerApi => _currentPS3ManagerApi;

    public override bool IsConnected => CurrentPS3ManagerApi.IsConnected;
    public int Port { get; set; } = 7887;
    
    public override IEnumerable<ConsoleInfo> ConsolesInfo
    {
        [PlaystationApiMethodUnSupported()]
        get
        {
             throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the get_ConsolesInfo call!");
        }
    }

    private uint CurrentProcessId { get; set; } = 0;

    public override IEnumerable<ProcessInfo> ProcessesInfo =>
        CurrentPS3ManagerApi
            .Process.GetPidProcesses()
                .Select
                (
                    p => new ProcessInfo()
                    {
                        ProcessId = p,
                        ProcessName = CurrentPS3ManagerApi.Process.GetName(p)
                    }
                );

    public PS3MAPI_Wrapper() : base()
    {
        _currentPS3ManagerApi = new PS3MAPI();
    }

    public PS3MAPI_Wrapper(int port) : base()
    {
        _currentPS3ManagerApi = new PS3MAPI();
        this.Port = port;
    }

    public PS3MAPI_Wrapper(PS3MAPI api) : base()
    {
        _currentPS3ManagerApi = api;
    }

    public PS3MAPI_Wrapper(PS3MAPI api, int port) : base()
    {
        this.Port = port;
        _currentPS3ManagerApi = api;
    }

    ~PS3MAPI_Wrapper()
    {
        Dispose();
    }

    public override bool Connect(in string ip) => CurrentPS3ManagerApi.ConnectTarget(ip, Port);

    public override bool Disconnect()
    {
        CurrentPS3ManagerApi.DisconnectTarget();
        return true;
    }

    private PS3MAPI.PS3_CMD.BuzzerMode Internal_GetBuzzerMode(BuzzerMode buzzerMode)
    {
        switch (buzzerMode)
        {
            case BuzzerMode.Single:
                return PS3MAPI.PS3_CMD.BuzzerMode.Single;

            case BuzzerMode.Double:
                return PS3MAPI.PS3_CMD.BuzzerMode.Double;

            case BuzzerMode.Triple:
                return PS3MAPI.PS3_CMD.BuzzerMode.Triple;

            default:
                return PS3MAPI.PS3_CMD.BuzzerMode.Single;
        }
    }

    public override void RingBuzzer(BuzzerMode buzzerMode) => CurrentPS3ManagerApi.PS3.RingBuzzer(Internal_GetBuzzerMode(buzzerMode));

    public override void VshNotify(in string msg) => CurrentPS3ManagerApi.PS3.Notify(msg);

    public override void SetIdps(in string idps) => CurrentPS3ManagerApi.PS3.SetIDPS(idps);

    public override void SetPsid(in string psid) => CurrentPS3ManagerApi.PS3.SetPSID(psid);

    public override void GetIdps(out string idps) { idps = CurrentPS3ManagerApi.PS3.GetIDPS(); }

    public override void GetPsid(out string psid) { psid = CurrentPS3ManagerApi.PS3.GetPSID(); }

    public override void ShutDown() => CurrentPS3ManagerApi.PS3.Power(PS3MAPI.PS3_CMD.PowerFlags.ShutDown);

    public override void GetTemprature(ref uint cell, ref uint rsx) => CurrentPS3ManagerApi.PS3.GetTemperature(out cell, out rsx);

    public override bool AttachGameProcess()
    {
        uint[] processIds = CurrentPS3ManagerApi.Process.GetPidProcesses();

        if (processIds.Length is 0)
            return false;

        return AttachProccess(processIds[0]);
    }

    public override bool AttachProccess(in uint proccessId)
    {
        CurrentProcessId = proccessId;
        return CurrentPS3ManagerApi.AttachProcess(proccessId);
    }

    #region Read Memory

    public override void ReadMemory(in uint address, in uint size, out byte[] bytes)
    {
        bytes = new byte[size];
        CurrentPS3ManagerApi.Process.Memory.Get(CurrentProcessId, address, bytes);
    }

    public override byte[] ReadMemory(in uint address, in uint size)
    {
        byte[] bytes = new byte[size];
        ReadMemory(address, size, out bytes);
        return bytes;
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

    public override void WriteMemory(in uint address, in byte[] bytes) => CurrentPS3ManagerApi.Process.Memory.Set(CurrentProcessId, address, bytes);

    [PlaystationApiMethodUnSupported()]
    public override void WriteMemoryString(in uint address, in string s)
        => throw new PlaystationApiMethodUnSupportedException("The MemoryString methods have not been added yet.");

    #endregion

    public override string ToString() => _toString;
}
