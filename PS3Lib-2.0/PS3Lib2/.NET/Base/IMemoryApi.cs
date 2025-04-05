namespace PS3Lib2;

#nullable enable

public interface IMemoryApi
{
    public bool AttachProccess(in uint proccessId);

    // TODO: Refactor for TryReads / TryWrites

    // General Read / Write

    public void ReadMemory(in uint address, in uint size, out byte[] bytes);
    public byte[] ReadMemory(in uint address, in uint size);

    public void WriteMemory(in uint address, in byte[] bytes);

    // Bytes

    public void ReadMemoryI8(in uint address, out sbyte ret);
    public sbyte ReadMemoryI8(in uint address);

    public void ReadMemoryU8(in uint address, out byte ret);
    public byte ReadMemoryU8(in uint address);

    public void WriteMemoryI8(in uint address, in sbyte i);
    public void WriteMemoryU8(in uint address, in byte i);

    // Shorts

    public void ReadMemoryI16(in uint address, out short ret);
    public short ReadMemoryI16(in uint address);

    public void ReadMemoryU16(in uint address, out ushort ret);
    public ushort ReadMemoryU16(in uint address);

    public void WriteMemoryI16(in uint address, in short i);
    public void WriteMemoryU16(in uint address, in ushort i);

    // Ints

    public void ReadMemoryI32(in uint address, out int ret);
    public int ReadMemoryI32(in uint address);

    public void ReadMemoryU32(in uint address, out uint ret);
    public uint ReadMemoryU32(in uint address);

    public void WriteMemoryI32(in uint address, in int i);
    public void WriteMemoryU32(in uint address, in uint i);

    // Floats

    public void ReadMemoryF32(in uint address, out float ret);
    public float ReadMemoryF32(in uint address);

    public void WriteMemoryF32(in uint address, in float f);

    // Longs

    public void ReadMemoryI64(in uint address, out long ret);
    public long ReadMemoryI64(in uint address);

    public void ReadMemoryU64(in uint address, out ulong ret);
    public ulong ReadMemoryU64(in uint address);

    public void WriteMemoryI64(in uint address, in long i);
    public void WriteMemoryU64(in uint address, in ulong i);

    // Doubles

    public void ReadMemoryF64(in uint address, out double ret);
    public double ReadMemoryF64(in uint address);

    public void WriteMemoryF64(in uint address, in double d);

    // Strings

    public string ReadMemoryString(in uint address);

    public void WriteMemoryString(in uint address, in string s);

    public bool TryPatternScan
       (in byte?[] patternInput,
        in uint patternSearchStartAddress,
        in uint patternSearchEndAddress,
        out uint? startingAddress,
        out byte[]? dataRead,
        out uint? length);
}
