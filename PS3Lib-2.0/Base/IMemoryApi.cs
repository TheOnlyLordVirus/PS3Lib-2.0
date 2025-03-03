using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS3LibNew.Interfaces
{
    internal interface IMemoryApi
    {
        void AttachProccess(uint proccessId);

        // General Read / Write

        void ReadMemory(uint offset, uint size, out byte[] bytes);
        //byte[] ReadMemory(uint offset, uint size);

        //// Bytes

        //void ReadMemoryI8(uint addr, out sbyte ret);
        //sbyte ReadMemoryI8(uint addr);

        //void ReadMemoryU8(uint addr, out byte ret);
        //byte ReadMemoryU8(uint addr);

        //// Shorts

        //void ReadMemoryI16(uint addr, out short ret);
        //short ReadMemoryI16(uint addr);

        //void ReadMemoryU16(uint addr, out ushort ret);
        //ushort ReadMemoryU16(uint addr);

        //// Ints

        //void ReadMemoryI32(uint addr, out int ret);
        //int ReadMemoryI32(uint addr);

        //void ReadMemoryU32(uint addr, out uint ret);
        //uint ReadMemoryU32(uint addr);

        //// Floats

        //void ReadMemoryF32(uint addr, out float ret);
        //float ReadMemoryF32(uint addr);

        //// Longs

        //void ReadMemoryI64(uint addr, out long ret);
        //long ReadMemoryI64(uint addr);

        //void ReadMemoryU64(uint addr, out ulong ret);
        //ulong ReadMemoryU64(uint addr);

        //// Doubles

        //void ReadMemoryF64(uint addr, out double ret);
        //double ReadMemoryF64(uint addr);

        //// String

        //string ReadMemoryString(uint addr);

        void WriteMemory(uint offset, byte[] bytes);

        //void WriteMemoryI8(uint addr, sbyte i);
        //void WriteMemoryU8(uint addr, byte i);

        //void WriteMemoryI16(uint addr, short i);
        //void WriteMemoryU16(uint addr, ushort i);

        //void WriteMemoryI32(uint addr, int i);
        //void WriteMemoryU32(uint addr, uint i);

        //void WriteMemoryF32(uint addr, float f);

        //void WriteMemoryI64(uint addr, long i);
        //void WriteMemoryU64(uint addr, ulong i);

        //void WriteMemoryF64(uint addr, double d);

        //void WriteMemoryString(uint addr, string s);
    }
}
