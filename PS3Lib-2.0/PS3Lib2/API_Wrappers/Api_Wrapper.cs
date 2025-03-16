using PS3Lib2.Interfaces;

namespace PS3Lib2;

abstract class Api_Wrapper : IPlaystationApi
{
    public abstract bool IsConnected { get; set; }
    public abstract bool Connect();
    public abstract bool Connect(string _);
    public abstract bool Disconnect();
    public abstract void RingBuzzer();
    public abstract void VshNotify(string _);
    public abstract void SetIdps(string _);
    public abstract void SetPsid(string _);
    public abstract void ShutDown();
    public abstract void GetTemprature(ref uint _, ref uint __);
    public abstract bool AttachGameProcess();
    public abstract bool AttachProccess(uint _);
    public abstract void ReadMemory(uint _, uint __, out byte[] ___);
    public abstract byte[] ReadMemory(uint _, uint __);
    public abstract void ReadMemoryI8(uint _, out sbyte __);
    public abstract sbyte ReadMemoryI8(uint _);
    public abstract void ReadMemoryU8(uint _, out byte __);
    public abstract byte ReadMemoryU8(uint _);
    public abstract void ReadMemoryI16(uint _, out short __);
    public abstract short ReadMemoryI16(uint _);
    public abstract void ReadMemoryU16(uint _, out ushort __);
    public abstract ushort ReadMemoryU16(uint _);
    public abstract void ReadMemoryI32(uint _, out int __);
    public abstract int ReadMemoryI32(uint _);
    public abstract void ReadMemoryU32(uint _, out uint __);
    public abstract uint ReadMemoryU32(uint _);
    public abstract void ReadMemoryF32(uint _, out float __);
    public abstract float ReadMemoryF32(uint _);
    public abstract void ReadMemoryI64(uint _, out long __);
    public abstract long ReadMemoryI64(uint _);
    public abstract void ReadMemoryU64(uint _, out ulong __);
    public abstract ulong ReadMemoryU64(uint _);
    public abstract void ReadMemoryF64(uint _, out double __);
    public abstract double ReadMemoryF64(uint _);
    public abstract string ReadMemoryString(uint _);
    public abstract bool TryPatternScan
       (in byte?[] _,
        in uint __,
        in uint ___,
        out uint? ____,
        out byte[]? _____,
        out uint? ______);
    public abstract void WriteMemory(uint _, byte[] __);
    public abstract void WriteMemoryI8(uint _, sbyte __);
    public abstract void WriteMemoryU8(uint _, byte __);
    public abstract void WriteMemoryI16(uint _, short __);
    public abstract void WriteMemoryU16(uint _, ushort __);
    public abstract void WriteMemoryI32(uint _, int __);
    public abstract void WriteMemoryU32(uint _, uint __);
    public abstract void WriteMemoryF32(uint _, float f);
    public abstract void WriteMemoryI64(uint _, long __);
    public abstract void WriteMemoryU64(uint _, ulong __);
    public abstract void WriteMemoryF64(uint _, double __);
    public abstract void WriteMemoryString(uint _, string __);

    public abstract void Dispose();
}
