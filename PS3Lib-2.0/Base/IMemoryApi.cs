namespace PS3Lib2.Interfaces;

internal interface IMemoryApi
{
    public bool AttachGameProcess();

    public void AttachProccess(uint proccessId);

    // General Read / Write

    public void ReadMemory(uint address, uint size, out byte[] bytes);
    public byte[] ReadMemory(uint address, uint size);

    // Bytes

    public void ReadMemoryI8(uint address, out sbyte ret);
    public sbyte ReadMemoryI8(uint address);

    public void ReadMemoryU8(uint address, out byte ret);
    public byte ReadMemoryU8(uint address);

    // Shorts

    public void ReadMemoryI16(uint address, out short ret);
    public short ReadMemoryI16(uint address);

    public void ReadMemoryU16(uint address, out ushort ret);
    public ushort ReadMemoryU16(uint address);

    // Ints

    public void ReadMemoryI32(uint address, out int ret);
    public int ReadMemoryI32(uint address);

    public void ReadMemoryU32(uint address, out uint ret);
    public uint ReadMemoryU32(uint address);

    // Floats

    public void ReadMemoryF32(uint address, out float ret);
    public float ReadMemoryF32(uint address);

    // Longs

    public void ReadMemoryI64(uint address, out long ret);
    public long ReadMemoryI64(uint address);

    public void ReadMemoryU64(uint address, out ulong ret);
    public ulong ReadMemoryU64(uint address);

    // Doubles

    public void ReadMemoryF64(uint address, out double ret);
    public double ReadMemoryF64(uint address);

    // String

    public string ReadMemoryString(uint address);

    public void WriteMemory(uint address, byte[] bytes);

    public void WriteMemoryI8(uint address, sbyte i);
    public void WriteMemoryU8(uint address, byte i);

    public void WriteMemoryI16(uint address, short i);
    public void WriteMemoryU16(uint address, ushort i);

    public void WriteMemoryI32(uint address, int i);
    public void WriteMemoryU32(uint address, uint i);

    public void WriteMemoryF32(uint address, float f);

    public void WriteMemoryI64(uint address, long i);
    public void WriteMemoryU64(uint address, ulong i);

    public void WriteMemoryF64(uint address, double d);

    public void WriteMemoryString(uint address, string s);
}
