using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;

using PS3Lib2.Attributes;
using PS3Lib2.Exceptions;
using PS3Lib2.Interfaces;

using TargetManagerApi = PS3TMAPI;

namespace PS3Lib2.Tmapi;

public sealed class TMAPI_Wrapper : IPlaystationApi
{
    private Assembly assembly;

    public uint ProcessId { get; private set; } = 0;

    public int ConnectedTarget { get; set; } = -1;

    private const string _libName = "ps3tmapi_net.dll";
    private const string _LibPathX = @"C:\Program Files\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private const string _LibPathX64 = @"C:\Program Files (x64)\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private const string _LibPathX86 = @"C:\Program Files (x86)\SN Systems\PS3\bin\ps3tmapi_net.dll";
    private readonly string[] _LibLocations;

    private string[] LibLocations
    {
        get => _LibLocations;
    }

    public bool IsConnected => 
        TargetManagerApi.GetConnectStatus(ConnectedTarget, out TargetManagerApi.ConnectStatus status, out _) is TargetManagerApi.SNRESULT.SN_S_OK && 
            status is TargetManagerApi.ConnectStatus.Connected;

    public TMAPI_Wrapper()
    {
        _LibLocations = [_libName, _LibPathX, _LibPathX64, _LibPathX86];

        // Default Connect Target
        ConnectedTarget = 0;

        Internal_Init();
    }

    public TMAPI_Wrapper(int connectTarget)
    {
        _LibLocations = [_libName, _LibPathX, _LibPathX64, _LibPathX86];

        ConnectedTarget = connectTarget;

        Internal_Init();
    }

    private void Internal_Init()
    {
        Internal_InitLibs();

        if (!TargetManagerApi.SUCCEEDED(TargetManagerApi.InitTargetComms()))
            throw new PlaystationApiObjectInstanceException("Failed to InitTargetComms for TMAPI!");
    }

    private void Internal_InitLibs()
    {
        foreach (string libPath in LibLocations)
            if (File.Exists(libPath))
            {
                Assembly.LoadFile(libPath);
                return;
            }

        throw new PlaystationApiObjectInstanceException($"You must have TargetManagerApi_net.dll Installed!");
    }

    public bool Connect(string ip) => TargetManagerApi.SUCCEEDED(TargetManagerApi.Connect(ConnectedTarget, ip));

    public bool Disconnect() => TargetManagerApi.SUCCEEDED(TargetManagerApi.Disconnect(ConnectedTarget));

    [PlaystationApiMethodUnSupportedAttribute()]
    public void RingBuzzer() => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the RingBuzzer() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public void VshNotify(string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the VshNotify() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public void SetIdps(string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetIdps() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public void SetPsid(string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetPsid() call!");

    public void ShutDown() => TargetManagerApi.PowerOff(ConnectedTarget, true);

    [PlaystationApiMethodUnSupportedAttribute()]
    public void GetTemprature(ref uint _, ref uint __) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the GetTemprature() call!");

    public bool AttachGameProcess()
    {
        uint[] pids;
        TargetManagerApi.GetProcessList(ConnectedTarget, out pids);

        if (pids.Length < 0)
            return false;

        return AttachProccess(pids[0]);
    }

    public bool AttachProccess(uint processId)
    {
        if (TargetManagerApi.ProcessAttach(ConnectedTarget, TargetManagerApi.UnitType.PPU, processId) is TargetManagerApi.SNRESULT.SN_S_OK &&
            TargetManagerApi.ProcessContinue(ConnectedTarget, processId) is TargetManagerApi.SNRESULT.SN_S_OK)
        {
            ProcessId = processId;
            return true;
        }

        return false;
    }


    #region Read / Write

    //TODO: Handle endianess

    public void ReadMemory(uint address, uint size, out byte[] bytes) { bytes = new byte[size]; TargetManagerApi.ProcessGetMemory(ConnectedTarget, TargetManagerApi.UnitType.PPU, ProcessId, 0, address, ref bytes); }
    public byte[] ReadMemory(uint address, uint size) { byte[] returnMe; ReadMemory(address, size, out returnMe); return returnMe; }

    // Bytes

    public void ReadMemoryI8(uint address, out sbyte ret)
    {
        uint size = sizeof(sbyte);
        ret = (sbyte)ReadMemory(address, size)[0];
    }

    public sbyte ReadMemoryI8(uint address)
    {
        ReadMemoryI8(address, out sbyte ret);
        return ret;
    }


    public void ReadMemoryU8(uint address, out byte ret)
    {
        uint size = sizeof(byte);
        ret = (byte)ReadMemory(address, size)[0];
    }

    public byte ReadMemoryU8(uint address)
    {
        ReadMemoryU8(address, out byte ret);
        return ret;
    }

    // Shorts

    public void ReadMemoryI16(uint address, out short ret)
    {
        uint size = sizeof(short);
        ret = BitConverter.ToInt16(ReadMemory(address, size), 0);
    }

    public short ReadMemoryI16(uint address)
    {
        ReadMemoryI16(address, out short ret);
        return ret;
    }

    public void ReadMemoryU16(uint address, out ushort ret)
    {
        uint size = sizeof(ushort);
        ret = BitConverter.ToUInt16(ReadMemory(address, size), 0);
    }

    public ushort ReadMemoryU16(uint address)
    {
        ReadMemoryU16(address, out ushort ret);
        return ret;
    }

    // Ints

    public void ReadMemoryI32(uint address, out int ret)
    {
        uint size = sizeof(int);
        ret = BitConverter.ToInt32(ReadMemory(address, size), 0);
    }

    public int ReadMemoryI32(uint address)
    {
        ReadMemoryI32(address, out int ret);
        return ret;
    }

    public void ReadMemoryU32(uint address, out uint ret)
    {
        uint size = sizeof(int);
        ret = BitConverter.ToUInt32(ReadMemory(address, size), 0);
    }

    public uint ReadMemoryU32(uint address)
    {
        ReadMemoryU32(address, out uint ret);
        return ret;
    }

    // Floats

    public void ReadMemoryF32(uint address, out float ret)
    {

        uint size = sizeof(float);
        ret = BitConverter.ToSingle(ReadMemory(address, size), 0);
    }


    public float ReadMemoryF32(uint address)
    {
        ReadMemoryF32(address, out float ret);
        return ret;
    }

    // Longs

    public void ReadMemoryI64(uint address, out long ret)
    {

        uint size = sizeof(long);
        ret = BitConverter.ToInt64(ReadMemory(address, size), 0);
    }


    public long ReadMemoryI64(uint address)
    {
        ReadMemoryI64(address, out long ret);
        return ret;
    }

    public void ReadMemoryU64(uint address, out ulong ret)
    {

        uint size = sizeof(ulong);
        ret = BitConverter.ToUInt64(ReadMemory(address, size), 0);
    }


    public ulong ReadMemoryU64(uint address)
    {
        ReadMemoryU64(address, out ulong ret);
        return ret;
    }

    // Doubles


    public void ReadMemoryF64(uint address, out double ret)
    {

        uint size = sizeof(double);
        ret = BitConverter.ToDouble(ReadMemory(address, size), 0);
    }


    public double ReadMemoryF64(uint address)
    {
        ReadMemoryF64(address, out double ret);
        return ret;
    }

    // String

    public string ReadMemoryString(uint address)
    {
        throw new NotImplementedException();
    }


    //[PlaystationApiMethodUnSupportedAttribute()]
    //public bool TryPatternScan
    //   (in byte?[] patternInput,
    //    in uint patternSearchStartAddress,
    //    in uint patternSearchEndAddress,
    //    out uint? startingAddress,
    //    out byte[]? dataRead,
    //    out uint? length)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region Write Memory

    public void WriteMemory(uint address, byte[] bytes) => TargetManagerApi.ProcessSetMemory(ConnectedTarget, PS3TMAPI.UnitType.PPU, ProcessId, 0, address, bytes);
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

    #endregion

    [DllImport("kernel32.dll")]
    static extern bool FreeLibrary(IntPtr a);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    public void Dispose()
    {
        if (IsConnected)
            Disconnect();

        IntPtr tmapiHandle = GetModuleHandle(_libName);

        if (tmapiHandle == IntPtr.Zero)
            return;

        FreeLibrary(tmapiHandle);
    }
}
