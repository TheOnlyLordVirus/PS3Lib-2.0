using PS3Lib2.Interfaces;

namespace PS3Lib2;

abstract class Api_Wrapper : IPlaystationApi
{
    public abstract bool IsConnected { get; set; }
    public abstract bool Connect();
    public abstract bool Connect(string ip);
    public abstract bool Disconnect();
    public abstract void RingBuzzer();
    public abstract void VshNotify(string _);
    public abstract void SetIdps(string _);
    public abstract void SetPsid(string psid);
    public abstract void ShutDown();
    public abstract void GetTemprature(ref int cell, ref int rsx);
    public abstract bool AttachGameProcess();
    public abstract bool AttachProccess(uint proccessId);
    public abstract void ReadMemory(uint address, uint size, out byte[] bytes);
    public abstract byte[] ReadMemory(uint address, uint size);
    public abstract void ReadMemoryI8(uint address, out sbyte ret);
    public abstract sbyte ReadMemoryI8(uint address);
    public abstract void ReadMemoryU8(uint address, out byte ret);
    public abstract byte ReadMemoryU8(uint address);
    public abstract void ReadMemoryI16(uint address, out short ret);
    public abstract short ReadMemoryI16(uint address);
    public abstract void ReadMemoryU16(uint address, out ushort ret);
    public abstract ushort ReadMemoryU16(uint address);
    public abstract void ReadMemoryI32(uint address, out int ret);
    public abstract int ReadMemoryI32(uint address);
    public abstract void ReadMemoryU32(uint address, out uint ret);
    public abstract uint ReadMemoryU32(uint address);
    public abstract void ReadMemoryF32(uint address, out float ret);
    public abstract float ReadMemoryF32(uint address);
    public abstract void ReadMemoryI64(uint address, out long ret);
    public abstract long ReadMemoryI64(uint address);
    public abstract void ReadMemoryU64(uint address, out ulong ret);
    public abstract ulong ReadMemoryU64(uint address);
    public abstract void ReadMemoryF64(uint address, out double ret);
    public abstract double ReadMemoryF64(uint address);
    public abstract string ReadMemoryString(uint address);
    public abstract bool TryPatternScan
       (in byte?[] patternInput,
        in uint patternSearchStartAddress,
        in uint patternSearchEndAddress,
        out uint? startingAddress,
        out byte[]? dataRead,
        out uint? length);
    public abstract void WriteMemory(uint address, byte[] bytes);
    public abstract void WriteMemoryI8(uint address, sbyte i);
    public abstract void WriteMemoryU8(uint address, byte i);
    public abstract void WriteMemoryI16(uint address, short i);
    public abstract void WriteMemoryU16(uint address, ushort i);
    public abstract void WriteMemoryI32(uint address, int i);
    public abstract void WriteMemoryU32(uint address, uint i);
    public abstract void WriteMemoryF32(uint address, float f);
    public abstract void WriteMemoryI64(uint address, long i);
    public abstract void WriteMemoryU64(uint address, ulong i);
    public abstract void WriteMemoryF64(uint address, double d);
    public abstract void WriteMemoryString(uint address, string s);

    public abstract void Dispose();
}
