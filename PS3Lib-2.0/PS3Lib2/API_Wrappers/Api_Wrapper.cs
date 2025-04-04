using System;
using System.Collections.Generic;

namespace PS3Lib2.Base;

public abstract class Api_Wrapper : IPlaystationApi
{
    private readonly IEnumerable<string> _unsupportedMethods;
    public virtual IEnumerable<string> UnsupportedMethods => _unsupportedMethods;

    private readonly IEnumerable<string> _supportedMethods;
    public virtual IEnumerable<string> SupportedMethods => _supportedMethods;

    public Api_Wrapper()
    {
        List<string> unsupportedMethods = new ();
        List<string> supportedMethods = new ();

        var methods = this.GetType().GetMethods();

        foreach (var method in methods)
        {
            if (!Attribute.IsDefined(method, typeof(PlaystationApiMethodUnSupported)))
            {
                supportedMethods.Add(method.Name);

                continue;
            }

            unsupportedMethods.Add(method.Name);
        }

        _supportedMethods = supportedMethods;
        _unsupportedMethods = unsupportedMethods;
    }

    public abstract bool IsConnected { get; }

    public abstract IEnumerable<ConsoleInfo> ConsolesInfo { get; }
    public abstract IEnumerable<ProcessInfo> ProcessesInfo { get; }

    public abstract bool Connect(in string _);
    public abstract bool Disconnect();
    public abstract void RingBuzzer(in BuzzerMode _);
    public abstract void VshNotify(in string _);
    public abstract void SetIdps(in string _);
    public abstract void SetPsid(in string _);
    public abstract void GetIdps(out string _);
    public abstract void GetPsid(out string _);
    public abstract void ShutDown();
    public abstract void GetTemprature(ref uint _, ref uint __);
    public abstract bool AttachGameProcess();
    public abstract bool AttachProccess(in uint _);

    public abstract void ReadMemory(in uint _, in uint __, out byte[] ___);

    public virtual byte[] ReadMemory(in uint address, in uint size)
    {
        byte[] returnMe;
        ReadMemory(address, size, out returnMe);
        return returnMe;
    }

    public virtual void ReadMemoryI8(in uint address, out sbyte ret)
    {
        uint size = sizeof(sbyte);
        ret = (sbyte)ReadMemory(address, size)[0];
    }

    public virtual sbyte ReadMemoryI8(in uint address)
    {
        ReadMemoryI8(address, out sbyte ret);
        return ret;
    }


    public virtual void ReadMemoryU8(in uint address, out byte ret)
    {
        uint size = sizeof(byte);
        ret = (byte)ReadMemory(address, size)[0];
    }

    public virtual byte ReadMemoryU8(in uint address)
    {
        ReadMemoryU8(address, out byte ret);
        return ret;
    }

    public virtual void ReadMemoryI16(in uint address, out short ret)
    {
        uint size = sizeof(short);
        ret = BitConverter.ToInt16(ReadMemory(address, size), 0);
    }

    public virtual short ReadMemoryI16(in uint address)
    {
        ReadMemoryI16(address, out short ret);
        return ret;
    }

    public virtual void ReadMemoryU16(in uint address, out ushort ret)
    {
        uint size = sizeof(ushort);
        ret = BitConverter.ToUInt16(ReadMemory(address, size), 0);
    }

    public virtual ushort ReadMemoryU16(in uint address)
    {
        ReadMemoryU16(address, out ushort ret);
        return ret;
    }

    public virtual void ReadMemoryI32(in uint address, out int ret)
    {
        uint size = sizeof(int);
        ret = BitConverter.ToInt32(ReadMemory(address, size), 0);
    }

    public virtual int ReadMemoryI32(in uint address)
    {
        ReadMemoryI32(address, out int ret);
        return ret;
    }

    public virtual void ReadMemoryU32(in uint address, out uint ret)
    {
        uint size = sizeof(int);
        ret = BitConverter.ToUInt32(ReadMemory(address, size), 0);
    }

    public virtual uint ReadMemoryU32(in uint address)
    {
        ReadMemoryU32(address, out uint ret);
        return ret;
    }

    public virtual void ReadMemoryF32(in uint address, out float ret)
    {
        uint size = sizeof(float);
        ret = BitConverter.ToSingle(ReadMemory(address, size), 0);
    }

    public virtual float ReadMemoryF32(in uint address)
    {
        ReadMemoryF32(address, out float ret);
        return ret;
    }

    public virtual void ReadMemoryI64(in uint address, out long ret)
    {
        uint size = sizeof(long);
        ret = BitConverter.ToInt64(ReadMemory(address, size), 0);
    }


    public virtual long ReadMemoryI64(in uint address)
    {
        ReadMemoryI64(address, out long ret);
        return ret;
    }

    public virtual void ReadMemoryU64(in uint address, out ulong ret)
    {
        uint size = sizeof(ulong);
        ret = BitConverter.ToUInt64(ReadMemory(address, size), 0);
    }


    public virtual ulong ReadMemoryU64(in uint address)
    {
        ReadMemoryU64(address, out ulong ret);
        return ret;
    }

    public virtual void ReadMemoryF64(in uint address, out double ret)
    {
        uint size = sizeof(double);
        ret = BitConverter.ToDouble(ReadMemory(address, size), 0);
    }


    public virtual double ReadMemoryF64(in uint address)
    {
        ReadMemoryF64(address, out double ret);
        return ret;
    }

    public abstract string ReadMemoryString(in uint _);

    public abstract bool TryPatternScan
       (in byte?[] _,
        in uint __,
        in uint ___,
        out uint? ____,
        out byte[]? _____,
        out uint? ______);

    public abstract void WriteMemory(in uint _, in byte[] __);

    public virtual void WriteMemoryI8(in uint address, in sbyte i)
    {
        byte[] bytes = BitConverter.GetBytes(i);
        WriteMemory(address, bytes);
    }
    public virtual void WriteMemoryU8(in uint address, in byte i) => WriteMemory(address, [i]);

    public virtual void WriteMemoryI16(in uint address, in short i)
    {
        byte[] bytes = BitConverter.GetBytes(i);
        WriteMemory(address, bytes);
    }

    public virtual void WriteMemoryU16(in uint address, in ushort i)
    {
        byte[] bytes = BitConverter.GetBytes(i);
        WriteMemory(address, bytes);
    }

    public virtual void WriteMemoryI32(in uint address, in int i)
    {
        byte[] bytes = BitConverter.GetBytes(i);
        WriteMemory(address, bytes);
    }

    public virtual void WriteMemoryU32(in uint address, in uint i)
    {
        byte[] bytes = BitConverter.GetBytes(i);
        WriteMemory(address, bytes);
    }

    public virtual void WriteMemoryF32(in uint address, in float f)
    {
        byte[] bytes = BitConverter.GetBytes(f);
        WriteMemory(address, bytes);
    }

    public virtual void WriteMemoryI64(in uint address, in long i)
    {
        byte[] bytes = BitConverter.GetBytes(i);
        WriteMemory(address, bytes);
    }

    public virtual void WriteMemoryU64(in uint address, in ulong i)
    {
        byte[] bytes = BitConverter.GetBytes(i);
        WriteMemory(address, bytes);
    }

    public virtual void WriteMemoryF64(in uint address, in double d)
    {
        byte[] bytes = BitConverter.GetBytes(d);
        WriteMemory(address, bytes);
    }

    public abstract void WriteMemoryString(in uint _, in string __);

    public virtual void Dispose()
    {
        if (IsConnected)
            Disconnect();
    }
}
