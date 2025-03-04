using Microsoft.Win32;
using PS3LibNew.Enums;
using PS3LibNew.Interfaces;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CCAPI;


internal sealed class CCAPI_API_Wrapper : IPlaystationApi, IDisposable
{
    private readonly string _dllPath;

    private readonly CCAPI _consoleControllApi;

    public CCAPI ConsoleControlApi { get => _consoleControllApi; }
    public bool Connected => ConsoleControlApi.IsConnected();


    public CCAPI_API_Wrapper()
    {
        RegistryKey Key = Registry
                .CurrentUser
                .OpenSubKey(@"Software\FrenchModdingTeam\CCAPI\InstallFolder");

        if (Key is null)
        {
            MessageBox.Show("You need to install CCAPI 2.60+ to use this library.", "CCAPI not installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Dispose();
            return;
        }
            
        string Path = Key.GetValue("path") as String;

        if (string.IsNullOrEmpty(Path))
        {
            MessageBox.Show("Invalid CCAPI folder, please re-install it.", "CCAPI not installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Dispose();
            return;
        }

        _dllPath = Path + @"\CCAPI.dll";

        if (!File.Exists(_dllPath))
        {
            MessageBox.Show("You need to install CCAPI 2.60+ to use this library.", "CCAPI.dll not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Dispose();
            return;
        }

        _consoleControllApi = new CCAPI(_dllPath);
    }

    public bool Connect() 
    {
        throw new NotImplementedException();
    }

    public bool Connect(string ip) 
    { 
        return ConsoleControlApi.Connect(ip) != 0;
    }

    public bool Disconnect() { return ConsoleControlApi.Disconnect() != 0; }

    public void RingBuzzer() { ConsoleControlApi.RingBuzzer(CCAPI.BuzzerType.Triple); }

    public void VshNotify(string message) { throw new NotImplementedException(); }

    public string GetConsoleId() { throw new NotImplementedException(); }

    public void SetConsoleId() { throw new NotImplementedException(); }

    public void ShutDown() { throw new NotImplementedException(); }

    public void GetTemprature() { throw new NotImplementedException(); }

    public void AttachProccess(uint proccessId) { throw new NotImplementedException(); }

    // General Read / Write

    public void ReadMemory(uint offset, uint size, out byte[] bytes) { throw new NotImplementedException(); }
    public byte[] ReadMemory(uint offset, uint size) { throw new NotImplementedException(); }

    // Bytes

    public void ReadMemoryI8(uint addr, out sbyte ret) { throw new NotImplementedException(); }
    public sbyte ReadMemoryI8(uint addr) { throw new NotImplementedException(); }

    public void ReadMemoryU8(uint addr, out byte ret) { throw new NotImplementedException(); }
    public byte ReadMemoryU8(uint addr) { throw new NotImplementedException(); }

    // Shorts

    public void ReadMemoryI16(uint addr, out short ret) { throw new NotImplementedException(); }
    public short ReadMemoryI16(uint addr) { throw new NotImplementedException(); }

    public void ReadMemoryU16(uint addr, out ushort ret) { throw new NotImplementedException(); }
    public ushort ReadMemoryU16(uint addr) { throw new NotImplementedException(); }

    // Ints

    public void ReadMemoryI32(uint addr, out int ret) { throw new NotImplementedException(); }
    public int ReadMemoryI32(uint addr) { throw new NotImplementedException(); }

    public void ReadMemoryU32(uint addr, out uint ret) { throw new NotImplementedException(); }
    public uint ReadMemoryU32(uint addr) { throw new NotImplementedException(); }

    // Floats

    public void ReadMemoryF32(uint addr, out float ret) { throw new NotImplementedException(); }
    public float ReadMemoryF32(uint addr) { throw new NotImplementedException(); }

    // Longs

    public void ReadMemoryI64(uint addr, out long ret) { throw new NotImplementedException(); }
    public long ReadMemoryI64(uint addr) { throw new NotImplementedException(); }

    public void ReadMemoryU64(uint addr, out ulong ret) { throw new NotImplementedException(); }
    public ulong ReadMemoryU64(uint addr) { throw new NotImplementedException(); }

    // Doubles

    public void ReadMemoryF64(uint addr, out double ret) { throw new NotImplementedException(); }
    public double ReadMemoryF64(uint addr) { throw new NotImplementedException(); }

    // String

    public string ReadMemoryString(uint addr) { throw new NotImplementedException(); }

    #region Write Memory

    public void WriteMemory(uint offset, byte[] bytes) { throw new NotImplementedException(); }

    public void WriteMemoryI8(uint addr, sbyte i) { throw new NotImplementedException(); }
    public void WriteMemoryU8(uint addr, byte i) { throw new NotImplementedException(); }

    public void WriteMemoryI16(uint addr, short i) { throw new NotImplementedException(); }
    public void WriteMemoryU16(uint addr, ushort i) { throw new NotImplementedException(); }

    public void WriteMemoryI32(uint addr, int i) { throw new NotImplementedException(); }
    public void WriteMemoryU32(uint addr, uint i) { throw new NotImplementedException(); }

    public void WriteMemoryF32(uint addr, float f) { throw new NotImplementedException(); }

    public void WriteMemoryI64(uint addr, long i) { throw new NotImplementedException(); }
    public void WriteMemoryU64(uint addr, ulong i) { throw new NotImplementedException(); }

    public void WriteMemoryF64(uint addr, double d) { throw new NotImplementedException(); }

    public void WriteMemoryString(uint addr, string s) { throw new NotImplementedException(); }

    #endregion

    public void Dispose()
    {

    }
}



