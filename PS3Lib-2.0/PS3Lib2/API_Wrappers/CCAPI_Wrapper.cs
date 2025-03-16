using System;
using System.IO;
using System.Runtime.InteropServices;

using Microsoft.Win32;

using PS3Lib2.Interfaces;
using PS3Lib2.Attributes;
using PS3Lib2.Exceptions;

using ConsoleControlApi = CCAPI;

namespace PS3Lib2.Capi;

[PlaystationApiSupportAttribute<CCAPI_Wrapper>()]
public sealed class CCAPI_Wrapper : IPlaystationApi
{
    private string _dllPath;

    private ConsoleControlApi _consoleControllApi;

    public ConsoleControlApi ConsoleControlApi 
    {   
        get => _consoleControllApi; 
    }

    public bool IsConnected => ConsoleControlApi.IsConnected();

    public CCAPI_Wrapper()
    {
        RegistryKey Key = Registry
            .CurrentUser
            .OpenSubKey(@"Software\FrenchModdingTeam\CCAPI\InstallFolder");

        if (Key is null)
            throw new PlaystationApiObjectInstanceException("You need to install CCAPI 2.60+ to use this library. CCAPI is not installed!");

        string Path = Key.GetValue("path") as String;

        if (string.IsNullOrEmpty(Path))
            throw new PlaystationApiObjectInstanceException("Invalid CCAPI folder, please re-install it. CCAPI is not installed!");

        _dllPath = Path + @"\CCAPI.dll";

        if (!File.Exists(_dllPath))
            throw new PlaystationApiObjectInstanceException("You need to install CCAPI 2.60+ to use this library. CCAPI.dll was not found!");

        _consoleControllApi = new ConsoleControlApi(_dllPath);
    }

    public CCAPI_Wrapper(ConsoleControlApi api)
    {
        _consoleControllApi = api;
    }

    ~CCAPI_Wrapper()
    {
        Dispose();
    }

    public bool Connect(string ip) 
    { 
        return ConsoleControlApi.Connect(ip) != 0;
    }

    public bool Disconnect() { return ConsoleControlApi.Disconnect() != 0; }

    // TODO: Create standard Enums for the wrappers / interfaces (Buzzer, Shutdown, restart, led color, ect, ect.) 
    public void RingBuzzer() { ConsoleControlApi.RingBuzzer(ConsoleControlApi.BuzzerType.Single); }

    public void VshNotify(string message/*Notify icon type enum here too.*/) { ConsoleControlApi.VshNotify(ConsoleControlApi.NotifyIcon.Info, message); }

    public void SetIdps(string consoleId) { ConsoleControlApi.SetBootConsoleIds(ConsoleControlApi.ConsoleIdType.Idps, consoleId); }

    public void SetPsid(string psid) { ConsoleControlApi.SetBootConsoleIds(ConsoleControlApi.ConsoleIdType.Idps, psid); }

    public void ShutDown() { ConsoleControlApi.Shutdown(ConsoleControlApi.ShutdownMode.Shutdown); }

    public void GetTemprature(ref uint cell, ref uint rsx) 
    {
        int cpu = 0, gpu = 0;

        ConsoleControlApi.GetTemperature(ref cpu, ref gpu);
        cell = (uint)cpu;
        rsx = (uint)gpu;
    }

    public bool AttachGameProcess() => ConsoleControlApi.AttachGameProcess();

    public bool AttachProccess(uint proccessId) 
    { 
        ConsoleControlApi.AttachProcess(proccessId);

        return true;
    }

    #region Read / Write

    public void ReadMemory(uint address, uint size, out byte[] bytes) { bytes = new byte[size]; ConsoleControlApi.ReadMemory(address, size, bytes); }
    public byte[] ReadMemory(uint address, uint size) { byte[] returnMe; ReadMemory(address, size, out returnMe); return returnMe; }

    public void ReadMemoryI8(uint address, out sbyte ret) { ret = ConsoleControlApi.ReadMemoryI8(address); }
    public sbyte ReadMemoryI8(uint address) { return ConsoleControlApi.ReadMemoryI8(address); }

    public void ReadMemoryU8(uint address, out byte ret) { ret = ConsoleControlApi.ReadMemoryU8(address); }
    public byte ReadMemoryU8(uint address) { return ConsoleControlApi.ReadMemoryU8(address); }

    public void ReadMemoryI16(uint address, out short ret) 
    {
        byte[] data = new byte[sizeof(short)];
        int bytesWritten = ConsoleControlApi.ReadMemory(address, sizeof(short), data);
        Array.Reverse(data);
        ret = BitConverter.ToInt16(data, 0);
    }

    public short ReadMemoryI16(uint address) 
    {
        byte[] data = new byte[sizeof(short)];
        int bytesWritten = ConsoleControlApi.ReadMemory(address, sizeof(short), data);
        Array.Reverse(data);
        return BitConverter.ToInt16(data, 0);

    }

    public void ReadMemoryU16(uint address, out ushort ret)
    {
        byte[] data = new byte[sizeof(ushort)];
        int bytesWritten = ConsoleControlApi.ReadMemory(address, sizeof(ushort), data);
        Array.Reverse(data);
        ret = BitConverter.ToUInt16(data, 0);
    }

    public ushort ReadMemoryU16(uint address)
    {
        byte[] data = new byte[sizeof(ushort)];
        int bytesWritten = ConsoleControlApi.ReadMemory(address, sizeof(ushort), data);
        Array.Reverse(data);
        return BitConverter.ToUInt16(data, 0);

    }

    public void ReadMemoryI32(uint address, out int ret) { ret = ConsoleControlApi.ReadMemoryI32(address); }
    public int ReadMemoryI32(uint address) { return ConsoleControlApi.ReadMemoryI32(address); }

    public void ReadMemoryU32(uint address, out uint ret) { ret = ConsoleControlApi.ReadMemoryU32(address); }
    public uint ReadMemoryU32(uint address) { return ConsoleControlApi.ReadMemoryU32(address); }

    public void ReadMemoryF32(uint address, out float ret) { ret = ConsoleControlApi.ReadMemoryF32(address); }
    public float ReadMemoryF32(uint address) { return ConsoleControlApi.ReadMemoryF32(address); }

    public void ReadMemoryI64(uint address, out long ret) { ret = ConsoleControlApi.ReadMemoryI64(address); }
    public long ReadMemoryI64(uint address) { return ConsoleControlApi.ReadMemoryI64(address); }

    public void ReadMemoryU64(uint address, out ulong ret) { ret = ConsoleControlApi.ReadMemoryU64(address); }
    public ulong ReadMemoryU64(uint address) { return ConsoleControlApi.ReadMemoryU64(address); }

    public void ReadMemoryF64(uint address, out double ret) { ret = ConsoleControlApi.ReadMemoryF64(address); }
    public double ReadMemoryF64(uint address) { return ConsoleControlApi.ReadMemoryF64(address); }

    public string ReadMemoryString(uint address) { return ConsoleControlApi.ReadMemoryString(address); }

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

    public void WriteMemory(uint address, byte[] bytes) { ConsoleControlApi.WriteMemory(address, (uint)bytes.Length, bytes); }

    public void WriteMemoryI8(uint address, sbyte i) { ConsoleControlApi.WriteMemoryI8(address, i); }
    public void WriteMemoryU8(uint address, byte i) { ConsoleControlApi.WriteMemoryU8(address, i); }

    public void WriteMemoryI16(uint address, short i) { ConsoleControlApi.WriteMemory(address, sizeof(short), BitConverter.GetBytes(i)); }
    public void WriteMemoryU16(uint address, ushort i) { ConsoleControlApi.WriteMemory(address, sizeof(ushort), BitConverter.GetBytes(i)); }

    public void WriteMemoryI32(uint address, int i) { ConsoleControlApi.WriteMemoryI32(address, i); }
    public void WriteMemoryU32(uint address, uint i) { ConsoleControlApi.WriteMemoryU32(address, i); }

    public void WriteMemoryF32(uint address, float f) { ConsoleControlApi.WriteMemoryF32(address, f); }

    public void WriteMemoryI64(uint address, long i) { ConsoleControlApi.WriteMemoryI64(address, i); }
    public void WriteMemoryU64(uint address, ulong i) { ConsoleControlApi.WriteMemoryU64(address, i); }

    public void WriteMemoryF64(uint address, double d) { ConsoleControlApi.WriteMemoryF64(address, d); }

    public void WriteMemoryString(uint address, string s) { ConsoleControlApi.WriteMemoryString(address, s); }

    #endregion

    [DllImport("kernel32.dll")]
    static extern bool FreeLibrary(IntPtr a);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    public void Dispose()
    {
        if (IsConnected)
            Disconnect();

        IntPtr ccapiHandle = GetModuleHandle("CCAPI.dll");

        if (ccapiHandle == IntPtr.Zero)
            return;

        FreeLibrary(ccapiHandle);
    }
}



