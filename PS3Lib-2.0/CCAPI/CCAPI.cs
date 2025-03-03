/**
*
* CCAPI C# Wrapper made by Enstone
* Compatible with CCAPI 2.60, CCAPI 2.70, CCAPI 2.80
* Requires CCAPI.dll
* V1.00
*
**/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using PS3LibNew.Interfaces;

public class CCAPI : IPlaystationApi
{
    public bool Connected
    {
        get
        {
            int state = 0;
            int res = CCAPIGetConnectionStatus(ref state);
            return (res == OK) && (state != 0);
        }
    }

    public bool Connect()
    {
        throw new NotImplementedException();

        // TODO: Get the IP from some sort of interface / window.
        string ip = null;

        return CCAPIConnectConsole(ip).Equals(1) ? true : false;
    }

    public bool Connect(string ip)
    {
        return CCAPIConnectConsole(ip).Equals(1) ? true : false;
    }

    public bool Disconnect()
    {
        return CCAPIDisconnectConsole().Equals(1) ? true : false;
    }

    public void RingBuzzer()
    {
        throw new NotImplementedException();
    }

    public void VshNotify(string message)
    {
        throw new NotImplementedException();
    }

    public string GetConsoleId()
    {
        return "";
    }

    public void SetConsoleId()
    {
        throw new NotImplementedException();
    }

    public void ShutDown()
    {
        throw new NotImplementedException();
    }

    public void GetTemprature()
    {
        throw new NotImplementedException();
    }

    public void AttachProccess(uint proccessId)
    {
        throw new NotImplementedException();
    }

    #region ReadMemory

    public void ReadMemory(uint addr, uint size, out byte[] bytes)
    {
        byte[] data = new byte[size];
        bytes = BitConverter.GetBytes(CCAPIGetMemory(ProcessId, (ulong)addr, size, data));
    }

    /*
    public byte[] ReadMemory(uint addr, uint size)
    {
        byte[] data = new byte[size];
        return BitConverter.GetBytes(CCAPIGetMemory(ProcessId, (ulong)addr, size, data));
    }

    // sbyte
    public void ReadMemoryI8(uint addr, out sbyte ret)
    {
        uint size = sizeof(sbyte);
        byte[] data = new byte[size];
        ret = (sbyte)CCAPIGetMemory(ProcessId, (ulong)addr, size, data);
    }

    public sbyte ReadMemoryI8(uint addr)
    {
        uint size = sizeof(sbyte);
        byte[] data = new byte[size];
        data = BitConverter.GetBytes(CCAPIGetMemory(ProcessId, (ulong)addr, size, data));
        return (sbyte)data[0];
    }



    // byte
    public void ReadMemoryU8(uint addr, out byte ret)
    {
        byte[] data = new byte[sizeof(byte)];
        ret = (byte)ReadMemory(addr, sizeof(byte), data);
    }

    public byte ReadMemoryU8(uint addr)
    {
        byte[] data = new byte[sizeof(byte)];
        ReadMemory(addr, sizeof(byte), data);
        return data[0];
    }



    // short
    public void ReadMemoryI16(uint addr, out short ret)
    {
        byte[] data = new byte[sizeof(byte)];
        ret = (short)ReadMemory(addr, sizeof(byte), data);
    }

    public short ReadMemoryI16(uint addr)
    {
        byte[] data = new byte[sizeof(byte)];
        ReadMemory(addr, sizeof(byte), data);
        return data[0];
    }



    // ushort
    public void ReadMemoryU16(uint addr, out ushort ret)
    {
        byte[] data = new byte[sizeof(byte)];
        ret = (ushort)ReadMemory(addr, sizeof(byte), data);
    }

    public ushort ReadMemoryU16(uint addr)
    {
        byte[] data = new byte[sizeof(byte)];
        ReadMemory(addr, sizeof(byte), data);
        return data[0];
    }



    // Int
    public void ReadMemoryI32(uint addr, out int ret)
    {
        byte[] data = new byte[sizeof(int)];
        ret = ReadMemory(addr, sizeof(int), data);
        Array.Reverse(data);
    }

    public int ReadMemoryI32(uint addr)
    {
        byte[] data = new byte[sizeof(int)];
        ReadMemory(addr, sizeof(int), data);
        Array.Reverse(data);
        return BitConverter.ToInt32(data, 0);
    }



    // Uint
    public void ReadMemoryU32(uint addr, out uint ret)
    {
        byte[] data = new byte[sizeof(uint)];
        ret = (uint)ReadMemory(addr, sizeof(uint), data);
        Array.Reverse(data);
    }

    public uint ReadMemoryU32(uint addr)
    {
        byte[] data = new byte[sizeof(uint)];
        ReadMemory(addr, sizeof(uint), data);
        Array.Reverse(data);
        return BitConverter.ToUInt32(data, 0);
    }



    // Float
    public void ReadMemoryF32(uint addr, out float ret)
    {
        byte[] data = new byte[sizeof(float)];
        ret = ReadMemory(addr, sizeof(float), data);
        Array.Reverse(data);
    }

    public float ReadMemoryF32(uint addr)
    {
        byte[] data = new byte[sizeof(float)];
        ReadMemory(addr, sizeof(float), data);
        Array.Reverse(data);
        return BitConverter.ToSingle(data, 0);
    }



    // Long
    public void ReadMemoryI64(uint addr, out long ret)
    {
        byte[] data = new byte[sizeof(long)];
        ret = ReadMemory(addr, sizeof(long), data);
        Array.Reverse(data);
    }

    public long ReadMemoryI64(uint addr)
    {
        byte[] data = new byte[sizeof(long)];
        ReadMemory(addr, sizeof(long), data);
        Array.Reverse(data);
        return BitConverter.ToInt64(data, 0);
    }



    // ulong
    public void ReadMemoryU64(uint addr, out ulong ret)
    {
        byte[] data = new byte[sizeof(ulong)];
        ret = (ulong)ReadMemory(addr, sizeof(ulong), data);
        Array.Reverse(data);
    }

    public ulong ReadMemoryU64(uint addr)
    {
        byte[] data = new byte[sizeof(ulong)];
        ReadMemory(addr, sizeof(ulong), data);
        Array.Reverse(data);
        return BitConverter.ToUInt64(data, 0);
    }



    // Double
    public void ReadMemoryF64(uint addr, out double ret)
    {
        byte[] data = new byte[sizeof(double)];
        ret = ReadMemory(addr, sizeof(double), data);
        Array.Reverse(data);
    }

    public double ReadMemoryF64(uint addr)
    {
        byte[] data = new byte[sizeof(double)];
        ReadMemory(addr, sizeof(double), data);
        Array.Reverse(data);
        return BitConverter.ToDouble(data, 0);
    }


    // String
    public string ReadMemoryString(uint addr)
    {
        string s = "";

        while (true)
        {
            byte[] chunk = new byte[0x100];
            int r = ReadMemory(addr, (uint)chunk.Length, chunk);
            if (r != OK)
            {
                break;
            }
            else
            {
                for (int i = 0; i < chunk.Length; i++)
                {
                    if (chunk[i] == 0)
                    {
                        s += Encoding.ASCII.GetString(chunk, 0, i);
                        goto end;
                    }
                }

                addr += (uint)chunk.Length;
                s += Encoding.ASCII.GetString(chunk);
            }
        }

    end:

        return s;
    }
    */

    #endregion

    #region WriteMemory

    public int WriteMemory(uint addr, uint size, byte[] data)
    {
        return CCAPISetMemory(ProcessId, (ulong)addr, size, data);
    }

    public void WriteMemory(uint offset, byte[] bytes)
    {
        WriteMemory(offset, (uint)(bytes.Length * sizeof(byte)), bytes);
    }

    /*
    // sByte
    public void WriteMemoryI8(uint addr, sbyte i)
    {
        byte[] data = new byte[sizeof(sbyte)];
        data[0] = (byte)i;
        WriteMemory(addr, sizeof(sbyte), data);
    }

    // Byte
    public void WriteMemoryU8(uint addr, byte i)
    {
        byte[] data = new byte[sizeof(byte)];
        data[0] = i;
        WriteMemory(addr, sizeof(byte), data);
    }

    // Short
    public void WriteMemoryI16(uint addr, short i)
    {
        byte[] data = new byte[sizeof(sbyte)];
        data[0] = (byte)i;
        WriteMemory(addr, sizeof(sbyte), data);
    }

    // Ushort
    public void WriteMemoryU16(uint addr, ushort i)
    {
        byte[] data = BitConverter.GetBytes(i);
        Array.Reverse(data);
        WriteMemory(addr, sizeof(byte), data);
    }

    // Int
    public void WriteMemoryI32(uint addr, int i)
    {
        byte[] data = BitConverter.GetBytes(i);
        Array.Reverse(data);
        WriteMemory(addr, sizeof(int), data);
    }

    // Uint
    public void WriteMemoryU32(uint addr, uint i)
    {
        byte[] data = BitConverter.GetBytes(i);
        Array.Reverse(data);
        WriteMemory(addr, sizeof(uint), data);
    }

    // Float
    public void WriteMemoryF32(uint addr, float f)
    {
        byte[] data = BitConverter.GetBytes(f);
        Array.Reverse(data);
        WriteMemory(addr, sizeof(float), data);
    }

    // Long
    public void WriteMemoryI64(uint addr, long i)
    {
        byte[] data = BitConverter.GetBytes(i);
        Array.Reverse(data);
        WriteMemory(addr, sizeof(long), data);
    }

    // ULong
    public void WriteMemoryU64(uint addr, ulong i)
    {
        byte[] data = BitConverter.GetBytes(i);
        Array.Reverse(data);
        WriteMemory(addr, sizeof(long), data);
    }

    // Double
    public void WriteMemoryF64(uint addr, double d)
    {
        byte[] data = BitConverter.GetBytes(d);
        Array.Reverse(data);
        WriteMemory(addr, sizeof(double), data);
    }

    // String
    public void WriteMemoryString(uint addr, string s)
    {
        byte[] b = System.Text.Encoding.ASCII.GetBytes(s + "\0");
        WriteMemory(addr, (uint)b.Length, b);
    }
    */

    #endregion



    public struct ProcessInfo
    {
        public uint pid;
        public string name;
    };
	public struct ConsoleInfo
	{
		public string name,ip;
	};
    public enum ConsoleIdType
    {
        Idps = 0,
        Psid = 1,
    };
    public enum ShutdownMode
    {
        Shutdown = 1,
        SoftReboot = 2,
        HardReboot = 3,
    };
    public enum BuzzerType
    {
        Continuous = 0,
        Single = 1,
        Double = 2,
        Triple = 3,
    };
    public enum ColorLed
    {
        Green = 1,
        Red = 2,
    };
    public enum StatusLed
    {
        Off = 0,
        On = 1,
        Blink = 2,
    };
    public enum NotifyIcon
    {
        Info = 0,
        Caution = 1,
        Friend = 2,
        Slider = 3,
        WrongWay = 4,
        Dialog = 5,
        DalogShadow = 6,
        Text = 7,
        Pointer = 8,
        Grab = 9,
        Hand = 10,
        Pen = 11,
        Finger = 12,
        Arrow = 13,
        ArrowRight = 14,
        Progress = 15,
        Trophy1 = 16,
        Trophy2 = 17,
        Trophy3 = 18,
        Trophy4 = 19
    };
    public enum ConsoleType
    {
        UNK = 0,
        CEX = 1,
        DEX = 2,
        TOOL = 3,
    };

    public CCAPI(string libpath = "CCAPI.dll")
    {
        ProcessId = 0xFFFFFFFF;
        LibHandle = IntPtr.Zero;
        LibLoaded = false;

        LibLoaded = Init(libpath);
    }
    private bool Init(string libpath)
    {

        LibHandle = LoadLibrary(libpath);

        if (LibHandle == IntPtr.Zero)
        {
            if (GetLastError() == 193)
            {
                
            }
            return false;
        }

        IntPtr pCCAPIConnectConsole = GetProcAddress(LibHandle,"CCAPIConnectConsole");
        IntPtr pCCAPIDisconnectConsole = GetProcAddress(LibHandle,"CCAPIDisconnectConsole");
        IntPtr pCCAPIGetConnectionStatus = GetProcAddress(LibHandle,"CCAPIGetConnectionStatus");
        IntPtr pCCAPISetBootConsoleIds = GetProcAddress(LibHandle,"CCAPISetBootConsoleIds");
        IntPtr pCCAPISetConsoleIds = GetProcAddress(LibHandle,"CCAPISetConsoleIds");
        IntPtr pCCAPISetMemory = GetProcAddress(LibHandle,"CCAPISetMemory");
        IntPtr pCCAPIGetMemory = GetProcAddress(LibHandle,"CCAPIGetMemory");
        IntPtr pCCAPIGetProcessList = GetProcAddress(LibHandle,"CCAPIGetProcessList");
        IntPtr pCCAPIGetProcessName = GetProcAddress(LibHandle,"CCAPIGetProcessName");
        IntPtr pCCAPIGetTemperature = GetProcAddress(LibHandle,"CCAPIGetTemperature");
        IntPtr pCCAPIShutdown = GetProcAddress(LibHandle,"CCAPIShutdown");
        IntPtr pCCAPIRingBuzzer = GetProcAddress(LibHandle,"CCAPIRingBuzzer");
        IntPtr pCCAPISetConsoleLed = GetProcAddress(LibHandle,"CCAPISetConsoleLed");
        IntPtr pCCAPIGetFirmwareInfo = GetProcAddress(LibHandle,"CCAPIGetFirmwareInfo");
        IntPtr pCCAPIVshNotify = GetProcAddress(LibHandle,"CCAPIVshNotify");
        IntPtr pCCAPIGetNumberOfConsoles = GetProcAddress(LibHandle,"CCAPIGetNumberOfConsoles");
        IntPtr pCCAPIGetConsoleInfo = GetProcAddress(LibHandle,"CCAPIGetConsoleInfo");
        IntPtr pCCAPIGetDllVersion = GetProcAddress(LibHandle,"CCAPIGetDllVersion");

       bool loaded = (pCCAPIConnectConsole != IntPtr.Zero)
            && (pCCAPIDisconnectConsole != IntPtr.Zero)
            && (pCCAPIGetConnectionStatus != IntPtr.Zero)
            && (pCCAPISetBootConsoleIds != IntPtr.Zero)
            && (pCCAPISetConsoleIds != IntPtr.Zero)
            && (pCCAPISetMemory != IntPtr.Zero)
            && (pCCAPIGetMemory != IntPtr.Zero)
            && (pCCAPIGetProcessList != IntPtr.Zero)
            && (pCCAPIGetProcessName != IntPtr.Zero)
            && (pCCAPIGetTemperature != IntPtr.Zero)
            && (pCCAPIShutdown != IntPtr.Zero)
            && (pCCAPIRingBuzzer != IntPtr.Zero)
            && (pCCAPISetConsoleLed != IntPtr.Zero)
            && (pCCAPIGetFirmwareInfo != IntPtr.Zero)
            && (pCCAPIVshNotify != IntPtr.Zero)
            && (pCCAPIGetNumberOfConsoles != IntPtr.Zero)
            && (pCCAPIGetConsoleInfo != IntPtr.Zero)
            && (pCCAPIGetDllVersion != IntPtr.Zero);

        if (!loaded)
            return false;

        CCAPIConnectConsole         = (CCAPIConnectConsole_t)           Marshal.GetDelegateForFunctionPointer(pCCAPIConnectConsole,        typeof(CCAPIConnectConsole_t));
        CCAPIDisconnectConsole      = (CCAPIDisconnectConsole_t)        Marshal.GetDelegateForFunctionPointer(pCCAPIDisconnectConsole,     typeof(CCAPIDisconnectConsole_t));
        CCAPIGetConnectionStatus    = (CCAPIGetConnectionStatus_t)      Marshal.GetDelegateForFunctionPointer(pCCAPIGetConnectionStatus,   typeof(CCAPIGetConnectionStatus_t));
        CCAPISetBootConsoleIds      = (CCAPISetBootConsoleIds_t)        Marshal.GetDelegateForFunctionPointer(pCCAPISetBootConsoleIds,     typeof(CCAPISetBootConsoleIds_t));
        CCAPISetConsoleIds          = (CCAPISetConsoleIds_t)            Marshal.GetDelegateForFunctionPointer(pCCAPISetConsoleIds,         typeof(CCAPISetConsoleIds_t));
        CCAPISetMemory              = (CCAPISetMemory_t)                Marshal.GetDelegateForFunctionPointer(pCCAPISetMemory,             typeof(CCAPISetMemory_t));
        CCAPIGetMemory              = (CCAPIGetMemory_t)                Marshal.GetDelegateForFunctionPointer(pCCAPIGetMemory,             typeof(CCAPIGetMemory_t));
        CCAPIGetProcessList         = (CCAPIGetProcessList_t)           Marshal.GetDelegateForFunctionPointer(pCCAPIGetProcessList,        typeof(CCAPIGetProcessList_t));
        CCAPIGetProcessName         = (CCAPIGetProcessName_t)           Marshal.GetDelegateForFunctionPointer(pCCAPIGetProcessName,        typeof(CCAPIGetProcessName_t));
        CCAPIGetTemperature         = (CCAPIGetTemperature_t)           Marshal.GetDelegateForFunctionPointer(pCCAPIGetTemperature,        typeof(CCAPIGetTemperature_t));
        CCAPIShutdown               = (CCAPIShutdown_t)                 Marshal.GetDelegateForFunctionPointer(pCCAPIShutdown,              typeof(CCAPIShutdown_t));
        CCAPIRingBuzzer             = (CCAPIRingBuzzer_t)               Marshal.GetDelegateForFunctionPointer(pCCAPIRingBuzzer,            typeof(CCAPIRingBuzzer_t));
        CCAPISetConsoleLed          = (CCAPISetConsoleLed_t)            Marshal.GetDelegateForFunctionPointer(pCCAPISetConsoleLed,         typeof(CCAPISetConsoleLed_t));
        CCAPIGetFirmwareInfo        = (CCAPIGetFirmwareInfo_t)          Marshal.GetDelegateForFunctionPointer(pCCAPIGetFirmwareInfo,       typeof(CCAPIGetFirmwareInfo_t));
        CCAPIVshNotify              = (CCAPIVshNotify_t)                Marshal.GetDelegateForFunctionPointer(pCCAPIVshNotify,             typeof(CCAPIVshNotify_t));
        CCAPIGetNumberOfConsoles    = (CCAPIGetNumberOfConsoles_t)      Marshal.GetDelegateForFunctionPointer(pCCAPIGetNumberOfConsoles,   typeof(CCAPIGetNumberOfConsoles_t));
        CCAPIGetConsoleInfo         = (CCAPIGetConsoleInfo_t)           Marshal.GetDelegateForFunctionPointer(pCCAPIGetConsoleInfo,        typeof(CCAPIGetConsoleInfo_t));
        CCAPIGetDllVersion          = (CCAPIGetDllVersion_t)            Marshal.GetDelegateForFunctionPointer(pCCAPIGetDllVersion,         typeof(CCAPIGetDllVersion_t));

        return true;
    }

    ~CCAPI()
    {
        LibLoaded = false;
    }

    public bool GetLibraryState()
    {
        return LibLoaded;
    }

    public int GetDllVersion()
    {
        return CCAPIGetDllVersion();
    }


    public List<ConsoleInfo> GetConsoleList()
    {
        List<ConsoleInfo> list = new List<ConsoleInfo>();

        IntPtr name = Marshal.AllocHGlobal((512 * sizeof(char)));
        IntPtr ip   = Marshal.AllocHGlobal((512 * sizeof(char)));

        for (int i=0;i<CCAPIGetNumberOfConsoles();i++)
        {
            ConsoleInfo c = new ConsoleInfo();
            CCAPIGetConsoleInfo(i,name,ip);
            c.name = Marshal.PtrToStringAnsi(name);
            c.ip = Marshal.PtrToStringAnsi(ip);
            list.Add(c);
        }

        Marshal.FreeHGlobal(name);
        Marshal.FreeHGlobal(ip);

        return list;
    }



    public List<ProcessInfo> GetProcessList()
    {
        List<ProcessInfo> list = new List<ProcessInfo>();

        IntPtr ProcessIds = Marshal.AllocHGlobal((sizeof(uint) * 32));
        uint NProcessIds = 32;
        int ret = CCAPIGetProcessList(ref NProcessIds,ProcessIds);
        if (ret != OK)
        {
            Marshal.FreeHGlobal(ProcessIds);
            return list;
        }
        else
        {
            IntPtr pName = Marshal.AllocHGlobal((512 * sizeof(char)));

            for (uint i = 0; i < NProcessIds ;i++)
            {
                uint pid = readFromBuffer<uint>(ProcessIds, i * sizeof(uint));

                ret = CCAPIGetProcessName(pid,pName);
                if (ret != OK)
                {
                    Marshal.FreeHGlobal(ProcessIds);
                    Marshal.FreeHGlobal(pName);
                    return list;
                }
                else
                {
                    ProcessInfo info = new ProcessInfo();
                    info.pid = pid;
                    info.name = Marshal.PtrToStringAnsi(pName);
                    list.Add(info);
                }
            }
            Marshal.FreeHGlobal(pName);
            Marshal.FreeHGlobal(ProcessIds);
            return list;
        }
    }



    public uint GetAttachedProcess()
    {
        return ProcessId;
    }


    public void AttachProcess(uint ProcessId)
    {
        this.ProcessId = ProcessId;
    }


    public bool AttachGameProcess()
    {
        List<ProcessInfo> list = GetProcessList();
        for (int i=0;i<list.Count;i++)
        {
            if (!list[i].name.Contains("dev_flash"))
            {
                AttachProcess(list[i].pid);
                return true;
            }
        }
        return false;
    }

    public int GetTemperature(ref int cell, ref int rsx)
    {
        return CCAPIGetTemperature(ref cell, ref rsx);
    }
    public int Shutdown(ShutdownMode m)
    {
        return CCAPIShutdown((int)m);
    }
    public int RingBuzzer(BuzzerType t)
    {
        return CCAPIRingBuzzer((int)t);
    }
    public int SetConsoleLed(ColorLed color, StatusLed st)
    {
        return CCAPISetConsoleLed((int)color, (int)st);
    }
    public int SetConsoleIds(ConsoleIdType t, string id)
    {
        if (id.Length != 32)
            return ERROR;
        return SetConsoleIds(t, StringToArray(id));
    }
    public int SetConsoleIds(ConsoleIdType t, byte[] id)
    {
        return CCAPISetConsoleIds((int)t, id);
    }
    public int SetBootConsoleIds(ConsoleIdType t, string id)
    {
        if (id.Length != 32)
            return ERROR;
        return SetBootConsoleIds(t, StringToArray(id));
    }
    public int SetBootConsoleIds(ConsoleIdType t, byte[] id)
    {
        return CCAPISetBootConsoleIds((int)t,1,id);
    }
    public int ResetBootConsoleIds(ConsoleIdType t)
    {
        return CCAPISetBootConsoleIds((int)t, 0, null);
    }
    public int VshNotify(NotifyIcon icon, string msg)
    {
        return CCAPIVshNotify((int)icon, msg);
    }


    public int GetFirmwareInfo(ref int firmware, ref int ccapiVersion, ref ConsoleType consoleType)
    {
        int cType = 0;
        int ret = CCAPIGetFirmwareInfo(ref firmware, ref ccapiVersion, ref cType);
        consoleType = (ConsoleType)cType;
        return ret;
    }

    public static string FirmwareToString(int firmware)
    {
        int l = (firmware >> 12) & 0xFF;
        int h = firmware >> 24;

        return String.Format("{0:X}.{1:X}",h,l);
    }


    public static string ConsoleTypeToString(ConsoleType cType)
    {
        string s = "UNK";

        switch(cType)
        {
            case ConsoleType.CEX:
                s = "CEX";
            break;

            case ConsoleType.DEX:
                s = "DEX";
            break;

            case ConsoleType.TOOL:
               s = "TOOL";
            break;

            default:
            break;
        }

        return s;
    }


    public static byte[] StringToArray(string s)
    {
        if (s.Length == 0)
        {
            return null;
        }
        if ((s.Length % 2) != 0)
        {
            s += "0";
        }
        byte[] b = new byte[s.Length/2];
        int j = 0;
        for (int i = 0; i < s.Length; i += 2)
        {
            var sb = s.Substring(i,2);
            b[j++] = Convert.ToByte(sb,16);
        }

        return b;
    }


    public const int OK = 0;
    public const int ERROR = -1;

    protected uint ProcessId;
    private IntPtr LibHandle;
    private bool LibLoaded;

    private T readFromBuffer<T>(IntPtr ptr, uint off)
    {
        return (T) Marshal.PtrToStructure(new IntPtr(ptr.ToInt64() + off), typeof(T));
    }

    [DllImport("kernel32.dll")]
    static extern uint GetLastError();
    [DllImport("kernel32.dll")]
    static extern IntPtr LoadLibrary(string a);
    [DllImport("kernel32.dll")]
    static extern IntPtr GetProcAddress(IntPtr a, string b);
    [DllImport("kernel32.dll")]
    static extern bool FreeLibrary(IntPtr a);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIConnectConsole_t(string ip);
    private CCAPIConnectConsole_t CCAPIConnectConsole;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIDisconnectConsole_t();
    private CCAPIDisconnectConsole_t CCAPIDisconnectConsole;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIGetConnectionStatus_t(ref int status);
    private CCAPIGetConnectionStatus_t CCAPIGetConnectionStatus;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPISetBootConsoleIds_t(int idType, int on, byte[] id);
    private CCAPISetBootConsoleIds_t CCAPISetBootConsoleIds;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPISetConsoleIds_t(int idType, byte[] id);
    private CCAPISetConsoleIds_t CCAPISetConsoleIds;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPISetMemory_t(uint pid, ulong addr, uint size, byte[] data);
    private CCAPISetMemory_t CCAPISetMemory;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIGetMemory_t(uint pid, ulong addr, uint size, byte[] data);
    private CCAPIGetMemory_t CCAPIGetMemory;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIGetProcessList_t(ref uint npid, IntPtr pids);
    private CCAPIGetProcessList_t CCAPIGetProcessList;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIGetProcessName_t(uint pid, IntPtr name);
    private CCAPIGetProcessName_t CCAPIGetProcessName;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIGetTemperature_t(ref int cell, ref int rsx);
    private CCAPIGetTemperature_t CCAPIGetTemperature;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIShutdown_t(int mode);
    private CCAPIShutdown_t CCAPIShutdown;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIRingBuzzer_t(int type);
    private CCAPIRingBuzzer_t CCAPIRingBuzzer;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPISetConsoleLed_t(int color, int status);
    private CCAPISetConsoleLed_t CCAPISetConsoleLed;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIGetFirmwareInfo_t(ref int firmware, ref int ccapiVersion, ref int consoleType);
    private CCAPIGetFirmwareInfo_t CCAPIGetFirmwareInfo;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIVshNotify_t(int mode, string msg);
    private CCAPIVshNotify_t CCAPIVshNotify;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIGetNumberOfConsoles_t();
    private CCAPIGetNumberOfConsoles_t CCAPIGetNumberOfConsoles;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIGetConsoleInfo_t(int index, IntPtr name, IntPtr ip);
    private CCAPIGetConsoleInfo_t CCAPIGetConsoleInfo;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int CCAPIGetDllVersion_t();
    private CCAPIGetDllVersion_t CCAPIGetDllVersion;
}

