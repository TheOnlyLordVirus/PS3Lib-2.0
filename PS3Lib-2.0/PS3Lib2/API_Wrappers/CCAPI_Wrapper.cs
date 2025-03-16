using System;
using System.IO;
using System.Runtime.InteropServices;

using Microsoft.Win32;

using PS3Lib2.Exceptions;

using ConsoleControlApi = CCAPI;

namespace PS3Lib2.Capi;

public sealed class CCAPI_Wrapper : Api_Wrapper
{
    private readonly ConsoleControlApi _consoleControllApi;

    public ConsoleControlApi ConsoleControlApi => _consoleControllApi;

    private const int _ccapiSuccessCode = 0;

    private readonly string _dllPath;

    public override bool IsConnected => ConsoleControlApi.IsConnected();

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

    public override bool Connect(string ip) => ConsoleControlApi.Connect(ip) is _ccapiSuccessCode;

    public override bool Disconnect() => ConsoleControlApi.Disconnect() is _ccapiSuccessCode;

    // TODO: Create standard Enums for the wrappers / interfaces (Buzzer, Shutdown, restart, led color, ect, ect.) 
    public override void RingBuzzer() { ConsoleControlApi.RingBuzzer(ConsoleControlApi.BuzzerType.Single); }

    public override void VshNotify(string message/*Notify icon type enum here too.*/) { ConsoleControlApi.VshNotify(ConsoleControlApi.NotifyIcon.Info, message); }

    public override void SetIdps(string consoleId) { ConsoleControlApi.SetBootConsoleIds(ConsoleControlApi.ConsoleIdType.Idps, consoleId); }

    public override void SetPsid(string psid) { ConsoleControlApi.SetBootConsoleIds(ConsoleControlApi.ConsoleIdType.Idps, psid); }

    public override void ShutDown() { ConsoleControlApi.Shutdown(ConsoleControlApi.ShutdownMode.Shutdown); }

    public override void GetTemprature(ref uint cell, ref uint rsx) 
    {
        int cpu = 0, gpu = 0;

        ConsoleControlApi.GetTemperature(ref cpu, ref gpu);
        cell = (uint)cpu;
        rsx = (uint)gpu;
    }

    public override bool AttachGameProcess() => ConsoleControlApi.AttachGameProcess();

    public override bool AttachProccess(uint proccessId) 
    { 
        ConsoleControlApi.AttachProcess(proccessId);

        return true;
    }

    #region Read Memory

    public override void ReadMemory(uint address, uint size, out byte[] bytes) { bytes = new byte[size]; ConsoleControlApi.ReadMemory(address, size, bytes); }
    public override byte[] ReadMemory(uint address, uint size) { byte[] returnMe; ReadMemory(address, size, out returnMe); return returnMe; }

    public override void ReadMemoryI8(uint address, out sbyte ret) { ret = ConsoleControlApi.ReadMemoryI8(address); }
    public override sbyte ReadMemoryI8(uint address) { return ConsoleControlApi.ReadMemoryI8(address); }

    public override void ReadMemoryU8(uint address, out byte ret) { ret = ConsoleControlApi.ReadMemoryU8(address); }
    public override byte ReadMemoryU8(uint address) { return ConsoleControlApi.ReadMemoryU8(address); }

    public override void ReadMemoryI16(uint address, out short ret) 
    {
        byte[] data = new byte[sizeof(short)];
        int bytesWritten = ConsoleControlApi.ReadMemory(address, sizeof(short), data);
        Array.Reverse(data);
        ret = BitConverter.ToInt16(data, 0);
    }

    public override short ReadMemoryI16(uint address) 
    {
        byte[] data = new byte[sizeof(short)];
        int bytesWritten = ConsoleControlApi.ReadMemory(address, sizeof(short), data);
        Array.Reverse(data);
        return BitConverter.ToInt16(data, 0);

    }

    public override void ReadMemoryU16(uint address, out ushort ret)
    {
        byte[] data = new byte[sizeof(ushort)];
        int bytesWritten = ConsoleControlApi.ReadMemory(address, sizeof(ushort), data);
        Array.Reverse(data);
        ret = BitConverter.ToUInt16(data, 0);
    }

    public override ushort ReadMemoryU16(uint address)
    {
        byte[] data = new byte[sizeof(ushort)];
        int bytesWritten = ConsoleControlApi.ReadMemory(address, sizeof(ushort), data);
        Array.Reverse(data);
        return BitConverter.ToUInt16(data, 0);

    }

    public override void ReadMemoryI32(uint address, out int ret) { ret = ConsoleControlApi.ReadMemoryI32(address); }
    public override int ReadMemoryI32(uint address) { return ConsoleControlApi.ReadMemoryI32(address); }
           
    public override void ReadMemoryU32(uint address, out uint ret) { ret = ConsoleControlApi.ReadMemoryU32(address); }
    public override uint ReadMemoryU32(uint address) { return ConsoleControlApi.ReadMemoryU32(address); }
           
    public override void ReadMemoryF32(uint address, out float ret) { ret = ConsoleControlApi.ReadMemoryF32(address); }
    public override float ReadMemoryF32(uint address) { return ConsoleControlApi.ReadMemoryF32(address); }
           
    public override void ReadMemoryI64(uint address, out long ret) { ret = ConsoleControlApi.ReadMemoryI64(address); }
    public override long ReadMemoryI64(uint address) { return ConsoleControlApi.ReadMemoryI64(address); }
           
    public override void ReadMemoryU64(uint address, out ulong ret) { ret = ConsoleControlApi.ReadMemoryU64(address); }
    public override ulong ReadMemoryU64(uint address) { return ConsoleControlApi.ReadMemoryU64(address); }
           
    public override void ReadMemoryF64(uint address, out double ret) { ret = ConsoleControlApi.ReadMemoryF64(address); }
    public override double ReadMemoryF64(uint address) { return ConsoleControlApi.ReadMemoryF64(address); }
           
    public override string ReadMemoryString(uint address) { return ConsoleControlApi.ReadMemoryString(address); }

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

    public override void WriteMemory(uint address, byte[] bytes) { ConsoleControlApi.WriteMemory(address, (uint)bytes.Length, bytes); }
           
    public override void WriteMemoryI8(uint address, sbyte i) { ConsoleControlApi.WriteMemoryI8(address, i); }
    public override void WriteMemoryU8(uint address, byte i) { ConsoleControlApi.WriteMemoryU8(address, i); }
           
    public override void WriteMemoryI16(uint address, short i) { ConsoleControlApi.WriteMemory(address, sizeof(short), BitConverter.GetBytes(i)); }
    public override void WriteMemoryU16(uint address, ushort i) { ConsoleControlApi.WriteMemory(address, sizeof(ushort), BitConverter.GetBytes(i)); }
           
    public override void WriteMemoryI32(uint address, int i) { ConsoleControlApi.WriteMemoryI32(address, i); }
    public override void WriteMemoryU32(uint address, uint i) { ConsoleControlApi.WriteMemoryU32(address, i); }
             
    public override void WriteMemoryI64(uint address, long i) { ConsoleControlApi.WriteMemoryI64(address, i); }
    public override void WriteMemoryU64(uint address, ulong i) { ConsoleControlApi.WriteMemoryU64(address, i); }

    public override void WriteMemoryF32(uint address, float f) { ConsoleControlApi.WriteMemoryF32(address, f); }
    public override void WriteMemoryF64(uint address, double d) { ConsoleControlApi.WriteMemoryF64(address, d); }
           
    public override void WriteMemoryString(uint address, string s) { ConsoleControlApi.WriteMemoryString(address, s); }

    #endregion

    [DllImport("kernel32.dll")]
    static extern bool FreeLibrary(IntPtr a);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    public override void Dispose()
    {
        if (IsConnected)
            Disconnect();

        IntPtr ccapiHandle = GetModuleHandle("CCAPI.dll");

        if (ccapiHandle == IntPtr.Zero)
            return;

        FreeLibrary(ccapiHandle);
    }
}



