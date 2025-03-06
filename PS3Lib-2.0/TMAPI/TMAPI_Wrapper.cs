using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using PS3Lib.NET;

using PS3Lib2.Attributes;
using PS3Lib2.Exceptions;
using PS3Lib2.Interfaces;

namespace PS3Lib2.Tmapi;

[PlaystationApiSupportAttribute<TMAPI_Wrapper>()]
internal sealed class TMAPI_Wrapper : IPlaystationApi
{
    public int ConnectedTarget
    {
        get;
        set;
    }

    private const string _libName = "ps3tmapi_net.dll";
    private readonly string _LibPathX = string.Format(@"C:\Program Files\SN Systems\PS3\bin\ps3tmapi_net.dll", _libName);
    private readonly string _LibPathX64 = string.Format(@"C:\Program Files (x64)\SN Systems\PS3\bin\ps3tmapi_net.dll", _libName);
    private readonly string _LibPathX86 = string.Format(@"C:\Program Files (x86)\SN Systems\PS3\bin\ps3tmapi_net.dll", _libName);
    private readonly string[] _LibLocations;

    private string[] LibLocations
    {
        get => _LibLocations;

        init => _LibLocations = [_libName, _LibPathX, _LibPathX64, _LibPathX86];
    }

    public bool IsConnected => 
        PS3TMAPI.GetConnectStatus(ConnectedTarget, out PS3TMAPI.ConnectStatus status, out _) is PS3TMAPI.SNRESULT.SN_S_OK && 
            status is PS3TMAPI.ConnectStatus.Connected;

    public TMAPI_Wrapper()
    {
        // Default Connect Target
        ConnectedTarget = 0;

        foreach (string libPath in LibLocations)
            if (File.Exists(libPath))
            {
                Assembly.LoadFile(libPath);
                return;
            }

        throw new FileNotFoundException($"You must have ps3tmapi_net.dll Installed!");
    }

    [PlaystationApiMethodUnSupportedAttribute()]
    public bool Connect()
    {
        throw new NotImplementedException();
    }

    public bool Connect(string ip) => PS3TMAPI.Connect(ConnectedTarget, ip) is PS3TMAPI.SNRESULT.SN_S_OK;

    public bool Disconnect() => PS3TMAPI.Disconnect(ConnectedTarget) is PS3TMAPI.SNRESULT.SN_S_OK;

    [PlaystationApiMethodUnSupportedAttribute()]
    public void RingBuzzer() => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the RingBuzzer() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public void VshNotify(string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the VshNotify() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public void SetIdps(string _) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetIdps() call!");

    [PlaystationApiMethodUnSupportedAttribute()]
    public void SetPsid(string psid) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the SetPsid() call!");

    public void ShutDown() => PS3TMAPI.PowerOff(ConnectedTarget, true);

    [PlaystationApiMethodUnSupportedAttribute()]
    public void GetTemprature(ref int cell, ref int rsx) => throw new PlaystationApiMethodUnSupportedException("TargetManagerApi does not support the GetTemprature() call!");

    public bool AttachGameProcess()
    {
        uint[] pids;
        PS3TMAPI.GetProcessList(0, out pids);

        if (pids.Length < 0)
            return false;

        return AttachProccess(pids[0]);
    }

    public bool AttachProccess(uint proccessId) =>
        (PS3TMAPI.ProcessAttach(ConnectedTarget, PS3TMAPI.UnitType.PPU, proccessId) is PS3TMAPI.SNRESULT.SN_S_OK &&
            PS3TMAPI.ProcessContinue(ConnectedTarget, proccessId) is PS3TMAPI.SNRESULT.SN_S_OK);

    #region Read / Write

    public void ReadMemory(uint address, uint size, out byte[] bytes) { bytes = new byte[size]; PS3TMAPI.ProcessGetMemory(ConnectedTarget, address, size, bytes); }
    public byte[] ReadMemory(uint address, uint size) { byte[] returnMe; ReadMemory(address, size, out returnMe); return returnMe; }

    // Bytes

    public void ReadMemoryI8(uint address, out sbyte ret) => throw new NotImplementedException();
    public sbyte ReadMemoryI8(uint address) => throw new NotImplementedException();

    public void ReadMemoryU8(uint address, out byte ret) => throw new NotImplementedException();
    public byte ReadMemoryU8(uint address) => throw new NotImplementedException();

    // Shorts

    public void ReadMemoryI16(uint address, out short ret) => throw new NotImplementedException();

    public short ReadMemoryI16(uint address) => throw new NotImplementedException();

    public void ReadMemoryU16(uint address, out ushort ret) => throw new NotImplementedException();
    public ushort ReadMemoryU16(uint address) => throw new NotImplementedException();

    // Ints

    public void ReadMemoryI32(uint address, out int ret) => throw new NotImplementedException();
    public int ReadMemoryI32(uint address) => throw new NotImplementedException();

    public void ReadMemoryU32(uint address, out uint ret) => throw new NotImplementedException();
    public uint ReadMemoryU32(uint address) => throw new NotImplementedException();

    // Floats

    public void ReadMemoryF32(uint address, out float ret) => throw new NotImplementedException();
    public float ReadMemoryF32(uint address) => throw new NotImplementedException();

    // Longs

    public void ReadMemoryI64(uint address, out long ret) => throw new NotImplementedException();
    public long ReadMemoryI64(uint address) => throw new NotImplementedException();

    public void ReadMemoryU64(uint address, out ulong ret) => throw new NotImplementedException();
    public ulong ReadMemoryU64(uint address) => throw new NotImplementedException();

    // Doubles

    public void ReadMemoryF64(uint address, out double ret) => throw new NotImplementedException();
    public double ReadMemoryF64(uint address) => throw new NotImplementedException();

    // String

    public string ReadMemoryString(uint address) => throw new NotImplementedException();


    [PlaystationApiMethodUnSupportedAttribute()]
    public bool TryPatternScan
       (in byte?[] patternInput,
        in uint patternSearchStartAddress,
        in uint patternSearchEndAddress,
        out uint? startingAddress,
        out byte[]? dataRead,
        out uint? length)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Write Memory

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

    #endregion

    [DllImport("kernel32.dll")]
    static extern bool FreeLibrary(IntPtr a);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    public void Dispose()
    {
        IntPtr tmapiHandle = GetModuleHandle(_libName);

        if (tmapiHandle == IntPtr.Zero)
            return;

        FreeLibrary(tmapiHandle);
    }
}
