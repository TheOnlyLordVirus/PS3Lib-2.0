using System;
using System.Collections.Generic;
using System.Linq;
using PS3ManagerAPI;

namespace PS3Lib2.PS3Mapi;

public sealed class PS3MAPI_Wrapper : Api_Wrapper
{
    private readonly PS3MAPI _currentPS3ManagerApi;

    public PS3MAPI CurrentPS3ManagerApi => _currentPS3ManagerApi;

    public int Port { get; set; } = 7887;

    public uint CurrentProcessId { get; private set; } = 0;

    public override bool IsConnected => CurrentPS3ManagerApi.IsConnected;

    public override IEnumerable<ConsoleInfo> ConsolesInfo => throw new NotImplementedException();

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

    public PS3MAPI_Wrapper()
    {
        _currentPS3ManagerApi = new PS3MAPI();
    }

    public PS3MAPI_Wrapper(int port)
    {
        _currentPS3ManagerApi = new PS3MAPI();
        this.Port = port;
    }

    public PS3MAPI_Wrapper(PS3MAPI api)
    {
        _currentPS3ManagerApi = api;
    }

    public PS3MAPI_Wrapper(PS3MAPI api, int port)
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

    public override void RingBuzzer() => CurrentPS3ManagerApi.PS3.RingBuzzer(PS3MAPI.PS3_CMD.BuzzerMode.Single);

    public override void VshNotify(in string msg) => CurrentPS3ManagerApi.PS3.Notify(msg);

    public override void SetIdps(in string idps) => CurrentPS3ManagerApi.PS3.SetIDPS(idps);

    public override void SetPsid(in string psid) => CurrentPS3ManagerApi.PS3.SetPSID(psid);

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

    public override string ReadMemoryString(in uint address) => throw new NotImplementedException();

    //public bool TryPatternScan
    //   (in byte?[] patternInput,
    //    in uint patternSearchStartAddress,
    //    in uint patternSearchEndAddress,
    //    out uint? startingAddress,
    //    out byte[]? dataRead,
    //    out uint? length) => throw new NotImplementedException();

    #endregion

    #region Write Memory

    public override void WriteMemory(in uint address, in byte[] bytes) => CurrentPS3ManagerApi.Process.Memory.Set(CurrentProcessId, address, bytes);

    public override void WriteMemoryString(in uint address, in string s) => throw new NotImplementedException();

    public override void GetIdps(out string _)
    {
        throw new NotImplementedException();
    }

    public override void GetPsid(out string _)
    {
        throw new NotImplementedException();
    }

    #endregion
}
