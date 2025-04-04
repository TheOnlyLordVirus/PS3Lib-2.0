using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Microsoft.Win32;

using PS3Lib2.Base;
using PS3Lib2.Exceptions;

namespace PS3Lib2.Capi;

public sealed class CCAPI_Wrapper : Api_Wrapper
{
    private const string _toString = "CCAPI";

    private const int _ccapiSuccessCode = 0;

    private readonly CCAPI _consoleControllApi;
    public CCAPI Api => _consoleControllApi;

    private readonly string _dllPath;
    public bool IsLibraryLoaded => GetModuleHandle("CCAPI.dll") != IntPtr.Zero;
    public override bool IsConnected => IsLibraryLoaded && Api.IsConnected();

    private int consoleInfoId = 0; 
    public override IEnumerable<ConsoleInfo> ConsolesInfo
    {
        get
        {
            consoleInfoId = 0;

            CCAPI.ConsoleType type = new();

            return Api
                .GetConsoleList()
                    .Select(c => new ConsoleInfo()
                    {
                        Id = (uint)consoleInfoId++,
                        Port = null,
                        ConsoleIp = c.ip,
                        ConsoleName = c.name,
                        ConsoleDescription = Internal_GetConsoleType(ref consoleInfoId, ref type)
                    });
        }
    }

    private string Internal_GetConsoleType(ref int id, ref CCAPI.ConsoleType typeRef)
    {
        Api.GetFirmwareInfo(ref id, ref id, ref typeRef);

        return typeRef.ToString();
    }

    public override IEnumerable<ProcessInfo> ProcessesInfo =>
        Api
            .GetProcessList()
                .Select(p => new ProcessInfo()
                {
                    ProcessId = p.pid,
                    ProcessName = p.name
                });

    public CCAPI_Wrapper() : base()
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

        _consoleControllApi = new CCAPI(_dllPath);
    }

    public CCAPI_Wrapper(CCAPI api) : base()
    {
        _consoleControllApi = api;
    }

    ~CCAPI_Wrapper()
    {
        Dispose();
    }

    public override bool Connect(in string ip) => Api.Connect(ip) is _ccapiSuccessCode;

    public override bool Disconnect() => Api.Disconnect() is _ccapiSuccessCode;

    private CCAPI.BuzzerType Internal_GetBuzzerMode(BuzzerMode buzzerMode)
    {
        switch (buzzerMode)
        {
            case BuzzerMode.Single:
                return CCAPI.BuzzerType.Single;

            case BuzzerMode.Double:
                return CCAPI.BuzzerType.Double;

            case BuzzerMode.Triple:
                return CCAPI.BuzzerType.Triple;

            default:
                return CCAPI.BuzzerType.Single;
        }
    }

    public override void RingBuzzer(in BuzzerMode buzzerMode) { Api.RingBuzzer(Internal_GetBuzzerMode(buzzerMode)); }

    public override void VshNotify(in string message) { Api.VshNotify(CCAPI.NotifyIcon.Info, message); }

    public override void SetIdps(in string consoleId) { Api.SetBootConsoleIds(CCAPI.ConsoleIdType.Idps, consoleId); }

    public override void SetPsid(in string psid) { Api.SetBootConsoleIds(CCAPI.ConsoleIdType.Idps, psid); }

    [PlaystationApiMethodUnSupported()]
    public override void GetIdps(out string _) => throw new PlaystationApiMethodUnSupportedException("CCAPI does not support the GetIdps() call!");

    [PlaystationApiMethodUnSupported()]
    public override void GetPsid(out string _) => throw new PlaystationApiMethodUnSupportedException("CCAPI does not support the GetPsid() call!");

    public override void ShutDown() { Api.Shutdown(CCAPI.ShutdownMode.Shutdown); }

    public override void GetTemprature(ref uint cell, ref uint rsx) 
    {
        int cpu = 0, gpu = 0;

        Api.GetTemperature(ref cpu, ref gpu);
        cell = (uint)cpu;
        rsx = (uint)gpu;
    }

    public override bool AttachGameProcess() => Api.AttachGameProcess();

    public override bool AttachProccess(in uint proccessId) 
    {
        Api.AttachProcess(proccessId);

        return true;
    }

    #region Read Memory

    public override void ReadMemory(in uint address, in uint size, out byte[] bytes) { bytes = new byte[size]; Api.ReadMemory(address, size, bytes); }
    public override byte[] ReadMemory(in uint address, in uint size) { byte[] returnMe; ReadMemory(address, size, out returnMe); return returnMe; }

    public override void ReadMemoryI8(in uint address, out sbyte ret) { ret = Api.ReadMemoryI8(address); }
    public override sbyte ReadMemoryI8(in uint address) { return Api.ReadMemoryI8(address); }

    public override void ReadMemoryU8(in uint address, out byte ret) { ret = Api.ReadMemoryU8(address); }
    public override byte ReadMemoryU8(in uint address) { return Api.ReadMemoryU8(address); }

    public override void ReadMemoryI16(in uint address, out short ret) 
    {
        byte[] data = new byte[sizeof(short)];
        int bytesWritten = Api.ReadMemory(address, sizeof(short), data);
        Array.Reverse(data);
        ret = BitConverter.ToInt16(data, 0);
    }

    public override short ReadMemoryI16(in uint address) 
    {
        byte[] data = new byte[sizeof(short)];
        int bytesWritten = Api.ReadMemory(address, sizeof(short), data);
        Array.Reverse(data);
        return BitConverter.ToInt16(data, 0);

    }

    public override void ReadMemoryU16(in uint address, out ushort ret)
    {
        byte[] data = new byte[sizeof(ushort)];
        int bytesWritten = Api.ReadMemory(address, sizeof(ushort), data);
        Array.Reverse(data);
        ret = BitConverter.ToUInt16(data, 0);
    }

    public override ushort ReadMemoryU16(in uint address)
    {
        byte[] data = new byte[sizeof(ushort)];
        int bytesWritten = Api.ReadMemory(address, sizeof(ushort), data);
        Array.Reverse(data);
        return BitConverter.ToUInt16(data, 0);

    }

    public override void ReadMemoryI32(in uint address, out int ret) { ret = Api.ReadMemoryI32(address); }
    public override int ReadMemoryI32(in uint address) { return Api.ReadMemoryI32(address); }
           
    public override void ReadMemoryU32(in uint address, out uint ret) { ret = Api.ReadMemoryU32(address); }
    public override uint ReadMemoryU32(in uint address) { return Api.ReadMemoryU32(address); }
           
    public override void ReadMemoryF32(in uint address, out float ret) { ret = Api.ReadMemoryF32(address); }
    public override float ReadMemoryF32(in uint address) { return Api.ReadMemoryF32(address); }
           
    public override void ReadMemoryI64(in uint address, out long ret) { ret = Api.ReadMemoryI64(address); }
    public override long ReadMemoryI64(in uint address) { return Api.ReadMemoryI64(address); }
           
    public override void ReadMemoryU64(in uint address, out ulong ret) { ret = Api.ReadMemoryU64(address); }
    public override ulong ReadMemoryU64(in uint address) { return Api.ReadMemoryU64(address); }
           
    public override void ReadMemoryF64(in uint address, out double ret) { ret = Api.ReadMemoryF64(address); }
    public override double ReadMemoryF64(in uint address) { return Api.ReadMemoryF64(address); }
           
    public override string ReadMemoryString(in uint address) { return Api.ReadMemoryString(address); }

    [PlaystationApiMethodUnSupported()]
    public override bool TryPatternScan
       (in byte?[] patternInput,
        in uint patternSearchStartAddress,
        in uint patternSearchEndAddress,
        out uint? startingAddress,
        out byte[]? dataRead,
        out uint? length)
            => throw new PlaystationApiMethodUnSupportedException("The TryPatternScan methods have not been added yet.");

    #endregion

    #region Write Memory

    public override void WriteMemory(in uint address, in byte[] bytes) { Api.WriteMemory(address, (uint)bytes.Length, bytes); }
           
    public override void WriteMemoryI8(in uint address, in sbyte i) { Api.WriteMemoryI8(address, i); }
    public override void WriteMemoryU8(in uint address, in byte i) { Api.WriteMemoryU8(address, i); }
           
    public override void WriteMemoryI16(in uint address, in short i) { Api.WriteMemory(address, sizeof(short), BitConverter.GetBytes(i)); }
    public override void WriteMemoryU16(in uint address, in ushort i) { Api.WriteMemory(address, sizeof(ushort), BitConverter.GetBytes(i)); }
           
    public override void WriteMemoryI32(in uint address, in int i) { Api.WriteMemoryI32(address, i); }
    public override void WriteMemoryU32(in uint address, in uint i) { Api.WriteMemoryU32(address, i); }
             
    public override void WriteMemoryI64(in uint address, in long i) { Api.WriteMemoryI64(address, i); }
    public override void WriteMemoryU64(in uint address, in ulong i) { Api.WriteMemoryU64(address, i); }

    public override void WriteMemoryF32(in uint address, in float f) { Api.WriteMemoryF32(address, f); }
    public override void WriteMemoryF64(in uint address, in double d) { Api.WriteMemoryF64(address, d); }
           
    public override void WriteMemoryString(in uint address, in string s) { Api.WriteMemoryString(address, s); }

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

    public override string ToString() => _toString;
}



