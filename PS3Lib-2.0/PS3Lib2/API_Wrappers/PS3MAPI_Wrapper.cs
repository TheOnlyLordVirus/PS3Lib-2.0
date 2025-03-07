using System;

using PS3ManagerAPI;

using PS3Lib2.Interfaces;

using PS3ManagerAPI = PS3ManagerAPI.PS3MAPI;

namespace PS3Lib2.PS3Mapi;

internal sealed class PS3MAPI_Wrapper : IPlaystationApi
{
    public bool IsConnected { get; set; }
    public bool Connect() => throw new NotImplementedException();
    public bool Connect(string ip) => throw new NotImplementedException();
    public bool Disconnect() => throw new NotImplementedException();
    public void RingBuzzer() => throw new NotImplementedException();
    public void VshNotify(string _) => throw new NotImplementedException();
    public void SetIdps(string _) => throw new NotImplementedException();
    public void SetPsid(string psid) => throw new NotImplementedException();
    public void ShutDown() => throw new NotImplementedException();
    public void GetTemprature(ref int cell, ref int rsx) => throw new NotImplementedException();
    public bool AttachGameProcess() => throw new NotImplementedException();
    public bool AttachProccess(uint proccessId) => throw new NotImplementedException();
    public void ReadMemory(uint address, uint size, out byte[] bytes) => throw new NotImplementedException();
    public byte[] ReadMemory(uint address, uint size) => throw new NotImplementedException();
    public void ReadMemoryI8(uint address, out sbyte ret) => throw new NotImplementedException();
    public sbyte ReadMemoryI8(uint address) => throw new NotImplementedException();
    public void ReadMemoryU8(uint address, out byte ret) => throw new NotImplementedException();
    public byte ReadMemoryU8(uint address) => throw new NotImplementedException();
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
    public bool TryPatternScan
       (in byte?[] patternInput,
        in uint patternSearchStartAddress,
        in uint patternSearchEndAddress,
        out uint? startingAddress,
        out byte[]? dataRead,
        out uint? length) => throw new NotImplementedException();
    public void WriteMemory(uint address, byte[] bytes) => throw new NotImplementedException();
    public void WriteMemoryI8(uint address, sbyte i) => throw new NotImplementedException();
    public void WriteMemoryU8(uint address, byte i) => throw new NotImplementedException();
    public void WriteMemoryI16(uint address, short i) => throw new NotImplementedException();
    public void WriteMemoryU16(uint address, ushort i) => throw new NotImplementedException();
    public void WriteMemoryI32(uint address, int i) => throw new NotImplementedException();
    public void WriteMemoryU32(uint address, uint i) => throw new NotImplementedException();
    public void WriteMemoryF32(uint address, float f) => throw new NotImplementedException();
    public void WriteMemoryI64(uint address, long i) => throw new NotImplementedException();
    public void WriteMemoryU64(uint address, ulong i) => throw new NotImplementedException();
    public void WriteMemoryF64(uint address, double d) => throw new NotImplementedException();
    public void WriteMemoryString(uint address, string s) => throw new NotImplementedException();

    public void Dispose() => throw new NotImplementedException();
}
