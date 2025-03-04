namespace PS3LibNew.Interfaces;

internal interface IMemoryApi
{
    public void AttachProccess(uint proccessId);

    // General Read / Write

    public void ReadMemory(uint offset, uint size, out byte[] bytes);
    public byte[] ReadMemory(uint offset, uint size);

    // Bytes

    public void ReadMemoryI8(uint addr, out sbyte ret);
    public sbyte ReadMemoryI8(uint addr);

    public void ReadMemoryU8(uint addr, out byte ret);
    public byte ReadMemoryU8(uint addr);

    // Shorts

    public void ReadMemoryI16(uint addr, out short ret);
    public short ReadMemoryI16(uint addr);

    public void ReadMemoryU16(uint addr, out ushort ret);
    public ushort ReadMemoryU16(uint addr);

    // Ints

    public void ReadMemoryI32(uint addr, out int ret);
    public int ReadMemoryI32(uint addr);

    public void ReadMemoryU32(uint addr, out uint ret);
    public uint ReadMemoryU32(uint addr);

    // Floats

    public void ReadMemoryF32(uint addr, out float ret);
    public float ReadMemoryF32(uint addr);

    // Longs

    public void ReadMemoryI64(uint addr, out long ret);
    public long ReadMemoryI64(uint addr);

    public void ReadMemoryU64(uint addr, out ulong ret);
    public ulong ReadMemoryU64(uint addr);

    // Doubles

    public void ReadMemoryF64(uint addr, out double ret);
    public double ReadMemoryF64(uint addr);

    // String

    public string ReadMemoryString(uint addr);

    public void WriteMemory(uint offset, byte[] bytes);

    public void WriteMemoryI8(uint addr, sbyte i);
    public void WriteMemoryU8(uint addr, byte i);

    public void WriteMemoryI16(uint addr, short i);
    public void WriteMemoryU16(uint addr, ushort i);

    public void WriteMemoryI32(uint addr, int i);
    public void WriteMemoryU32(uint addr, uint i);

    public void WriteMemoryF32(uint addr, float f);

    public void WriteMemoryI64(uint addr, long i);
    public void WriteMemoryU64(uint addr, ulong i);

    public void WriteMemoryF64(uint addr, double d);

    public void WriteMemoryString(uint addr, string s);
}
