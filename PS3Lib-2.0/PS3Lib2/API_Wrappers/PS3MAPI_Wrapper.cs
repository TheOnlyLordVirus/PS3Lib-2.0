using System;

using PS3ManagerAPI;

using PS3Lib2.Interfaces;

namespace PS3Lib2.PS3Mapi;

public sealed class PS3MAPI_Wrapper : IPlaystationApi
{
    public PS3MAPI CurrentPS3ManagerApi { get; set; }

    public int Port { get; set; } = 7887;

    public bool IsConnected { get; set; }

    public uint ProcessId { get; set; }

    public PS3MAPI_Wrapper()
    {
        CurrentPS3ManagerApi = new PS3MAPI();
    }

    public PS3MAPI_Wrapper(int port)
    {
        this.Port = port;
    }

    public PS3MAPI_Wrapper(PS3MAPI api)
    {
        CurrentPS3ManagerApi = api;

    }

    public PS3MAPI_Wrapper(PS3MAPI api, int port)
    {
        this.Port = port;
        CurrentPS3ManagerApi = api;
    }

    public bool Connect(string ip) => CurrentPS3ManagerApi.ConnectTarget(ip, Port);

    public bool Disconnect()
    {
        CurrentPS3ManagerApi.DisconnectTarget();
        return true;
    }

    public void RingBuzzer() => CurrentPS3ManagerApi.PS3.RingBuzzer(PS3MAPI.PS3_CMD.BuzzerMode.Single);

    public void VshNotify(string msg) => CurrentPS3ManagerApi.PS3.Notify(msg);

    public void SetIdps(string idps) => CurrentPS3ManagerApi.PS3.SetIDPS(idps);

    public void SetPsid(string psid) => CurrentPS3ManagerApi.PS3.SetPSID(psid);

    public void ShutDown() => CurrentPS3ManagerApi.PS3.Power(PS3MAPI.PS3_CMD.PowerFlags.ShutDown);

    public void GetTemprature(ref uint cell, ref uint rsx) => CurrentPS3ManagerApi.PS3.GetTemperature(out cell, out rsx);

    public bool AttachGameProcess()
    {
        uint[] processIds = CurrentPS3ManagerApi.Process.GetPidProcesses();

        if (processIds.Length is 0)
            return false;

        return AttachProccess(processIds[0]);
    }

    public bool AttachProccess(uint proccessId)
    {
        ProcessId = proccessId;
        return CurrentPS3ManagerApi.AttachProcess(proccessId);
    }

    public void ReadMemory(uint address, uint size, out byte[] bytes)
    {
        bytes = new byte[size];
        CurrentPS3ManagerApi.Process.Memory.Get(ProcessId, address, bytes);
    }
        
    public byte[] ReadMemory(uint address, uint size)
    {
        byte[] bytes = new byte[size];
        ReadMemory(address, size, out bytes);
        return bytes;
    }

    public void ReadMemoryI8(uint address, out sbyte ret)
    {
        ret = (sbyte)ReadMemory(address, 1)[0];
    }

    public sbyte ReadMemoryI8(uint address)
    {
        return (sbyte)ReadMemory(address, 1)[0];
    }

    public void ReadMemoryU8(uint address, out byte ret)
    {
        ret = ReadMemory(address, 1)[0];
    }

    public byte ReadMemoryU8(uint address)
    {
        return ReadMemory(address, 1)[0];
    }

    public void ReadMemoryI16(uint address, out short ret) => throw new NotImplementedException();
    public short ReadMemoryI16(uint address) => throw new NotImplementedException();

    public void ReadMemoryU16(uint address, out ushort ret) => throw new NotImplementedException();
    public ushort ReadMemoryU16(uint address) => throw new NotImplementedException();

    public void ReadMemoryI32(uint address, out int ret) => throw new NotImplementedException();
    public int ReadMemoryI32(uint address) => throw new NotImplementedException();

    public void ReadMemoryU32(uint address, out uint ret) => throw new NotImplementedException();
    public uint ReadMemoryU32(uint address) => throw new NotImplementedException();

    public void ReadMemoryF32(uint address, out float ret) => throw new NotImplementedException();
    public float ReadMemoryF32(uint address) => throw new NotImplementedException();

    public void ReadMemoryI64(uint address, out long ret) => throw new NotImplementedException();
    public long ReadMemoryI64(uint address) => throw new NotImplementedException();

    public void ReadMemoryU64(uint address, out ulong ret) => throw new NotImplementedException();
    public ulong ReadMemoryU64(uint address) => throw new NotImplementedException();

    public void ReadMemoryF64(uint address, out double ret) => throw new NotImplementedException();
    public double ReadMemoryF64(uint address) => throw new NotImplementedException();

    public string ReadMemoryString(uint address) => throw new NotImplementedException();

    //public bool TryPatternScan
    //   (in byte?[] patternInput,
    //    in uint patternSearchStartAddress,
    //    in uint patternSearchEndAddress,
    //    out uint? startingAddress,
    //    out byte[]? dataRead,
    //    out uint? length) => throw new NotImplementedException();

    public void WriteMemory(uint address, byte[] bytes) => CurrentPS3ManagerApi.Process.Memory.Set(ProcessId, address, bytes);

    public void WriteMemoryI8(uint address, sbyte i) => WriteMemory(address, [(byte)i]);
    public void WriteMemoryU8(uint address, byte i) => WriteMemory(address, [i]);

    public void WriteMemoryI16(uint address, short i) => throw new NotImplementedException();
    public void WriteMemoryU16(uint address, ushort i) => throw new NotImplementedException();

    public void WriteMemoryI32(uint address, int i) => throw new NotImplementedException();
    public void WriteMemoryU32(uint address, uint i) => throw new NotImplementedException();

    public void WriteMemoryF32(uint address, float f) => throw new NotImplementedException();

    public void WriteMemoryI64(uint address, long i) => throw new NotImplementedException();
    public void WriteMemoryU64(uint address, ulong i) => throw new NotImplementedException();

    public void WriteMemoryF64(uint address, double d) => throw new NotImplementedException();

    public void WriteMemoryString(uint address, string s) => throw new NotImplementedException();

    public void Dispose()
    {
        CurrentPS3ManagerApi.DisconnectTarget();

        CurrentPS3ManagerApi = null;
    }
}
