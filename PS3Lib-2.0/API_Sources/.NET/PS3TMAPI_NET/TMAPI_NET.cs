using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x02000002 RID: 2
public class PS3TMAPI
{
    // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
    public static bool FAILED(PS3TMAPI.SNRESULT res)
    {
        return !PS3TMAPI.SUCCEEDED(res);
    }

    // Token: 0x06000002 RID: 2 RVA: 0x0000205B File Offset: 0x0000025B
    public static bool SUCCEEDED(PS3TMAPI.SNRESULT res)
    {
        return res >= PS3TMAPI.SNRESULT.SN_S_OK;
    }

    // Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
    private static bool Is32Bit()
    {
        return IntPtr.Size == 4;
    }

    // Token: 0x06000004 RID: 4 RVA: 0x0000206E File Offset: 0x0000026E
    private static byte VersionMajor(ulong version)
    {
        return (byte)(version >> 16);
    }

    // Token: 0x06000005 RID: 5 RVA: 0x00002075 File Offset: 0x00000275
    private static byte VersionMinor(ulong version)
    {
        return (byte)(version >> 8);
    }

    // Token: 0x06000006 RID: 6 RVA: 0x0000207B File Offset: 0x0000027B
    private static byte VersionFix(ulong version)
    {
        return (byte)version;
    }

    // Token: 0x06000007 RID: 7 RVA: 0x0000207F File Offset: 0x0000027F
    private static void VersionComponents(ulong version, out byte major, out byte minor, out byte fix)
    {
        major = PS3TMAPI.VersionMajor(version);
        minor = PS3TMAPI.VersionMinor(version);
        fix = PS3TMAPI.VersionFix(version);
    }

    // Token: 0x06000008 RID: 8 RVA: 0x00002099 File Offset: 0x00000299
    public static byte SDKVersionMajor(ulong sdkVersion)
    {
        return PS3TMAPI.VersionMajor(sdkVersion);
    }

    // Token: 0x06000009 RID: 9 RVA: 0x000020A1 File Offset: 0x000002A1
    public static byte SDKVersionMinor(ulong sdkVersion)
    {
        return PS3TMAPI.VersionMinor(sdkVersion);
    }

    // Token: 0x0600000A RID: 10 RVA: 0x000020A9 File Offset: 0x000002A9
    public static byte SDKVersionFix(ulong sdkVersion)
    {
        return PS3TMAPI.VersionFix(sdkVersion);
    }

    // Token: 0x0600000B RID: 11 RVA: 0x000020B1 File Offset: 0x000002B1
    public static void SDKVersionComponents(ulong sdkVersion, out byte major, out byte minor, out byte fix)
    {
        major = PS3TMAPI.SDKVersionMajor(sdkVersion);
        minor = PS3TMAPI.SDKVersionMinor(sdkVersion);
        fix = PS3TMAPI.SDKVersionFix(sdkVersion);
    }

    // Token: 0x0600000C RID: 12 RVA: 0x000020CB File Offset: 0x000002CB
    public static byte CPVersionMajor(ulong cpVersion)
    {
        return PS3TMAPI.VersionMajor(cpVersion);
    }

    // Token: 0x0600000D RID: 13 RVA: 0x000020D3 File Offset: 0x000002D3
    public static byte CPVersionMinor(ulong cpVersion)
    {
        return PS3TMAPI.VersionMinor(cpVersion);
    }

    // Token: 0x0600000E RID: 14 RVA: 0x000020DB File Offset: 0x000002DB
    public static byte CPVersionFix(ulong cpVersion)
    {
        return PS3TMAPI.VersionFix(cpVersion);
    }

    // Token: 0x0600000F RID: 15 RVA: 0x000020E3 File Offset: 0x000002E3
    public static void CPVersionComponents(ulong cpVersion, out byte major, out byte minor, out byte fix)
    {
        major = PS3TMAPI.CPVersionMajor(cpVersion);
        minor = PS3TMAPI.CPVersionMinor(cpVersion);
        fix = PS3TMAPI.CPVersionFix(cpVersion);
    }

    // Token: 0x06000010 RID: 16
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetTMVersion")]
    private static extern PS3TMAPI.SNRESULT GetTMVersionX86(out IntPtr version);

    // Token: 0x06000011 RID: 17
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetTMVersion")]
    private static extern PS3TMAPI.SNRESULT GetTMVersionX64(out IntPtr version);

    // Token: 0x06000012 RID: 18 RVA: 0x00002100 File Offset: 0x00000300
    public static PS3TMAPI.SNRESULT GetTMVersion(out string version)
    {
        IntPtr utf8Ptr;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetTMVersionX86(out utf8Ptr) : PS3TMAPI.GetTMVersionX64(out utf8Ptr);
        version = PS3TMAPI.Utf8ToString(utf8Ptr, uint.MaxValue);
        return result;
    }

    // Token: 0x06000013 RID: 19
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetAPIVersion")]
    private static extern PS3TMAPI.SNRESULT GetAPIVersionX86(out IntPtr version);

    // Token: 0x06000014 RID: 20
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetAPIVersion")]
    private static extern PS3TMAPI.SNRESULT GetAPIVersionX64(out IntPtr version);

    // Token: 0x06000015 RID: 21 RVA: 0x00002130 File Offset: 0x00000330
    public static PS3TMAPI.SNRESULT GetAPIVersion(out string version)
    {
        IntPtr utf8Ptr;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetAPIVersionX86(out utf8Ptr) : PS3TMAPI.GetAPIVersionX64(out utf8Ptr);
        version = PS3TMAPI.Utf8ToString(utf8Ptr, uint.MaxValue);
        return result;
    }

    // Token: 0x06000016 RID: 22
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3TranslateError")]
    private static extern PS3TMAPI.SNRESULT TranslateErrorX86(PS3TMAPI.SNRESULT res, out IntPtr message);

    // Token: 0x06000017 RID: 23
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3TranslateError")]
    private static extern PS3TMAPI.SNRESULT TranslateErrorX64(PS3TMAPI.SNRESULT res, out IntPtr message);

    // Token: 0x06000018 RID: 24 RVA: 0x00002160 File Offset: 0x00000360
    public static PS3TMAPI.SNRESULT TranslateError(PS3TMAPI.SNRESULT errorCode, out string message)
    {
        IntPtr utf8Ptr;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.TranslateErrorX86(errorCode, out utf8Ptr) : PS3TMAPI.TranslateErrorX64(errorCode, out utf8Ptr);
        message = PS3TMAPI.Utf8ToString(utf8Ptr, uint.MaxValue);
        return result;
    }

    // Token: 0x06000019 RID: 25
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetErrorQualifier")]
    private static extern PS3TMAPI.SNRESULT GetErrorQualifierX86(out uint qualifier, out IntPtr message);

    // Token: 0x0600001A RID: 26
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetErrorQualifier")]
    private static extern PS3TMAPI.SNRESULT GetErrorQualifierX64(out uint qualifier, out IntPtr message);

    // Token: 0x0600001B RID: 27 RVA: 0x00002194 File Offset: 0x00000394
    public static PS3TMAPI.SNRESULT GetErrorQualifier(out uint qualifier, out string message)
    {
        IntPtr utf8Ptr;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetErrorQualifierX86(out qualifier, out utf8Ptr) : PS3TMAPI.GetErrorQualifierX64(out qualifier, out utf8Ptr);
        message = PS3TMAPI.Utf8ToString(utf8Ptr, uint.MaxValue);
        return result;
    }

    // Token: 0x0600001C RID: 28
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetConnectStatus")]
    private static extern PS3TMAPI.SNRESULT GetConnectStatusX86(int target, out uint status, out IntPtr usage);

    // Token: 0x0600001D RID: 29
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetConnectStatus")]
    private static extern PS3TMAPI.SNRESULT GetConnectStatusX64(int target, out uint status, out IntPtr usage);

    // Token: 0x0600001E RID: 30 RVA: 0x000021C8 File Offset: 0x000003C8
    public static PS3TMAPI.SNRESULT GetConnectStatus(int target, out PS3TMAPI.ConnectStatus status, out string usage)
    {
        uint num;
        IntPtr utf8Ptr;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetConnectStatusX86(target, out num, out utf8Ptr) : PS3TMAPI.GetConnectStatusX64(target, out num, out utf8Ptr);
        status = (PS3TMAPI.ConnectStatus)num;
        usage = PS3TMAPI.Utf8ToString(utf8Ptr, uint.MaxValue);
        return result;
    }

    // Token: 0x0600001F RID: 31
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3InitTargetComms")]
    private static extern PS3TMAPI.SNRESULT InitTargetCommsX86();

    // Token: 0x06000020 RID: 32
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3InitTargetComms")]
    private static extern PS3TMAPI.SNRESULT InitTargetCommsX64();

    // Token: 0x06000021 RID: 33 RVA: 0x00002200 File Offset: 0x00000400
    public static PS3TMAPI.SNRESULT InitTargetComms()
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.InitTargetCommsX64();
        }
        return PS3TMAPI.InitTargetCommsX86();
    }

    // Token: 0x06000022 RID: 34
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CloseTargetComms")]
    private static extern PS3TMAPI.SNRESULT CloseTargetCommsX86();

    // Token: 0x06000023 RID: 35
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CloseTargetComms")]
    private static extern PS3TMAPI.SNRESULT CloseTargetCommsX64();

    // Token: 0x06000024 RID: 36 RVA: 0x00002214 File Offset: 0x00000414
    public static PS3TMAPI.SNRESULT CloseTargetComms()
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.CloseTargetCommsX64();
        }
        return PS3TMAPI.CloseTargetCommsX86();
    }

    // Token: 0x06000025 RID: 37
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnumerateTargets")]
    private static extern PS3TMAPI.SNRESULT EnumerateTargetsX86(PS3TMAPI.EnumerateTargetsCallback callback);

    // Token: 0x06000026 RID: 38
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnumerateTargets")]
    private static extern PS3TMAPI.SNRESULT EnumerateTargetsX64(PS3TMAPI.EnumerateTargetsCallback callback);

    // Token: 0x06000027 RID: 39 RVA: 0x00002228 File Offset: 0x00000428
    public static PS3TMAPI.SNRESULT EnumerateTargets(PS3TMAPI.EnumerateTargetsCallback callback)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.EnumerateTargetsX64(callback);
        }
        return PS3TMAPI.EnumerateTargetsX86(callback);
    }

    // Token: 0x06000028 RID: 40 RVA: 0x0000223E File Offset: 0x0000043E
    private static int EnumTargetsExPriv(int target, IntPtr unused)
    {
        if (PS3TMAPI.ms_enumTargetsExCallback == null)
        {
            return -1;
        }
        return PS3TMAPI.ms_enumTargetsExCallback(target, PS3TMAPI.ms_enumTargetsExUserData);
    }

    // Token: 0x06000029 RID: 41
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnumerateTargetsEx")]
    private static extern PS3TMAPI.SNRESULT EnumerateTargetsExX86(PS3TMAPI.EnumerateTargetsExCallbackPriv callback, IntPtr unused);

    // Token: 0x0600002A RID: 42
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnumerateTargetsEx")]
    private static extern PS3TMAPI.SNRESULT EnumerateTargetsExX64(PS3TMAPI.EnumerateTargetsExCallbackPriv callback, IntPtr unused);

    // Token: 0x0600002B RID: 43 RVA: 0x00002259 File Offset: 0x00000459
    public static PS3TMAPI.SNRESULT EnumerateTargetsEx(PS3TMAPI.EnumerateTargetsExCallback callback, ref object userData)
    {
        if (callback == null)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        PS3TMAPI.ms_enumTargetsExCallback = callback;
        PS3TMAPI.ms_enumTargetsExUserData = userData;
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.EnumerateTargetsExX64(PS3TMAPI.ms_enumTargetsExCallbackPriv, IntPtr.Zero);
        }
        return PS3TMAPI.EnumerateTargetsExX86(PS3TMAPI.ms_enumTargetsExCallbackPriv, IntPtr.Zero);
    }

    // Token: 0x0600002C RID: 44
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetNumTargets")]
    private static extern PS3TMAPI.SNRESULT GetNumTargetsX86(out uint numTargets);

    // Token: 0x0600002D RID: 45
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetNumTargets")]
    private static extern PS3TMAPI.SNRESULT GetNumTargetsX64(out uint numTargets);

    // Token: 0x0600002E RID: 46 RVA: 0x00002294 File Offset: 0x00000494
    public static PS3TMAPI.SNRESULT GetNumTargets(out uint numTargets)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetNumTargetsX64(out numTargets);
        }
        return PS3TMAPI.GetNumTargetsX86(out numTargets);
    }

    // Token: 0x0600002F RID: 47
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetTargetFromName")]
    private static extern PS3TMAPI.SNRESULT GetTargetFromNameX86(IntPtr name, out int target);

    // Token: 0x06000030 RID: 48
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetTargetFromName")]
    private static extern PS3TMAPI.SNRESULT GetTargetFromNameX64(IntPtr name, out int target);

    // Token: 0x06000031 RID: 49 RVA: 0x000022AC File Offset: 0x000004AC
    public static PS3TMAPI.SNRESULT GetTargetFromName(string name, out int target)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(name));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetTargetFromNameX64(scopedGlobalHeapPtr.Get(), out target);
        }
        return PS3TMAPI.GetTargetFromNameX86(scopedGlobalHeapPtr.Get(), out target);
    }

    // Token: 0x06000032 RID: 50
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Reset")]
    private static extern PS3TMAPI.SNRESULT ResetX86(int target, ulong resetParameter);

    // Token: 0x06000033 RID: 51
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Reset")]
    private static extern PS3TMAPI.SNRESULT ResetX64(int target, ulong resetParameter);

    // Token: 0x06000034 RID: 52 RVA: 0x000022E5 File Offset: 0x000004E5
    public static PS3TMAPI.SNRESULT Reset(int target, PS3TMAPI.ResetParameter resetParameter)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ResetX64(target, (ulong)resetParameter);
        }
        return PS3TMAPI.ResetX86(target, (ulong)resetParameter);
    }

    // Token: 0x06000035 RID: 53
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ResetEx")]
    private static extern PS3TMAPI.SNRESULT ResetExX86(int target, ulong boot, ulong bootMask, ulong reset, ulong resetMask, ulong system, ulong systemMask);

    // Token: 0x06000036 RID: 54
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ResetEx")]
    private static extern PS3TMAPI.SNRESULT ResetExX64(int target, ulong boot, ulong bootMask, ulong reset, ulong resetMask, ulong system, ulong systemMask);

    // Token: 0x06000037 RID: 55 RVA: 0x000022FD File Offset: 0x000004FD
    public static PS3TMAPI.SNRESULT ResetEx(int target, PS3TMAPI.BootParameter bootParameter, PS3TMAPI.BootParameterMask bootMask, PS3TMAPI.ResetParameter resetParameter, PS3TMAPI.ResetParameterMask resetMask, PS3TMAPI.SystemParameter systemParameter, PS3TMAPI.SystemParameterMask systemMask)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ResetExX64(target, (ulong)bootParameter, (ulong)bootMask, (ulong)resetParameter, (ulong)resetMask, (ulong)systemParameter, (ulong)systemMask);
        }
        return PS3TMAPI.ResetExX86(target, (ulong)bootParameter, (ulong)bootMask, (ulong)resetParameter, (ulong)resetMask, (ulong)systemParameter, (ulong)systemMask);
    }

    // Token: 0x06000038 RID: 56
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetResetParameters")]
    private static extern PS3TMAPI.SNRESULT GetResetParametersX86(int target, out ulong boot, out ulong bootMask, out ulong reset, out ulong resetMask, out ulong system, out ulong systemMask);

    // Token: 0x06000039 RID: 57
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetResetParameters")]
    private static extern PS3TMAPI.SNRESULT GetResetParametersX64(int target, out ulong boot, out ulong bootMask, out ulong reset, out ulong resetMask, out ulong system, out ulong systemMask);

    // Token: 0x0600003A RID: 58 RVA: 0x00002328 File Offset: 0x00000528
    public static PS3TMAPI.SNRESULT GetResetParameters(int target, out PS3TMAPI.BootParameter bootParameter, out PS3TMAPI.BootParameterMask bootMask, out PS3TMAPI.ResetParameter resetParameter, out PS3TMAPI.ResetParameterMask resetMask, out PS3TMAPI.SystemParameter systemParameter, out PS3TMAPI.SystemParameterMask systemMask)
    {
        ulong num;
        ulong num2;
        ulong num3;
        ulong num4;
        ulong num5;
        ulong num6;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetResetParametersX86(target, out num, out num2, out num3, out num4, out num5, out num6) : PS3TMAPI.GetResetParametersX64(target, out num, out num2, out num3, out num4, out num5, out num6);
        bootParameter = (PS3TMAPI.BootParameter)num;
        bootMask = (PS3TMAPI.BootParameterMask)num2;
        resetParameter = (PS3TMAPI.ResetParameter)num3;
        resetMask = (PS3TMAPI.ResetParameterMask)num4;
        systemParameter = (PS3TMAPI.SystemParameter)num5;
        systemMask = (PS3TMAPI.SystemParameterMask)num6;
        return result;
    }

    // Token: 0x0600003B RID: 59
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetBootParameter")]
    private static extern PS3TMAPI.SNRESULT SetBootParameterX86(int target, ulong boot, ulong bootMask);

    // Token: 0x0600003C RID: 60
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetBootParameter")]
    private static extern PS3TMAPI.SNRESULT SetBootParameterX64(int target, ulong boot, ulong bootMask);

    // Token: 0x0600003D RID: 61 RVA: 0x0000237D File Offset: 0x0000057D
    public static PS3TMAPI.SNRESULT SetBootParameter(int target, PS3TMAPI.BootParameter bootParameter, PS3TMAPI.BootParameterMask bootMask)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetBootParameterX64(target, (ulong)bootParameter, (ulong)bootMask);
        }
        return PS3TMAPI.SetBootParameterX86(target, (ulong)bootParameter, (ulong)bootMask);
    }

    // Token: 0x0600003E RID: 62
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetCurrentBootParameter")]
    private static extern PS3TMAPI.SNRESULT GetCurrentBootParameterX86(int target, out ulong boot);

    // Token: 0x0600003F RID: 63
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetCurrentBootParameter")]
    private static extern PS3TMAPI.SNRESULT GetCurrentBootParameterX64(int target, out ulong boot);

    // Token: 0x06000040 RID: 64 RVA: 0x00002398 File Offset: 0x00000598
    public static PS3TMAPI.SNRESULT GetCurrentBootParameter(int target, out PS3TMAPI.BootParameter bootParameter)
    {
        ulong num;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetCurrentBootParameterX86(target, out num) : PS3TMAPI.GetCurrentBootParameterX64(target, out num);
        bootParameter = (PS3TMAPI.BootParameter)num;
        return result;
    }

    // Token: 0x06000041 RID: 65
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetSystemParameter")]
    private static extern PS3TMAPI.SNRESULT SetSystemParameterX86(int target, ulong system, ulong systemMask);

    // Token: 0x06000042 RID: 66
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetSystemParameter")]
    private static extern PS3TMAPI.SNRESULT SetSystemParameterX64(int target, ulong system, ulong systemMask);

    // Token: 0x06000043 RID: 67 RVA: 0x000023C3 File Offset: 0x000005C3
    public static PS3TMAPI.SNRESULT SetSystemParameter(int target, PS3TMAPI.SystemParameter systemParameter, PS3TMAPI.SystemParameterMask systemMask)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetSystemParameterX64(target, (ulong)systemParameter, (ulong)systemMask);
        }
        return PS3TMAPI.SetSystemParameterX86(target, (ulong)systemParameter, (ulong)systemMask);
    }

    // Token: 0x06000044 RID: 68
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetTargetInfo")]
    private static extern PS3TMAPI.SNRESULT GetTargetInfoX86(ref PS3TMAPI.TargetInfoPriv targetInfoPriv);

    // Token: 0x06000045 RID: 69
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetTargetInfo")]
    private static extern PS3TMAPI.SNRESULT GetTargetInfoX64(ref PS3TMAPI.TargetInfoPriv targetInfoPriv);

    // Token: 0x06000046 RID: 70 RVA: 0x000023E0 File Offset: 0x000005E0
    public static PS3TMAPI.SNRESULT GetTargetInfo(ref PS3TMAPI.TargetInfo targetInfo)
    {
        PS3TMAPI.TargetInfoPriv targetInfoPriv = default(PS3TMAPI.TargetInfoPriv);
        targetInfoPriv.Flags = targetInfo.Flags;
        targetInfoPriv.Target = targetInfo.Target;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetTargetInfoX86(ref targetInfoPriv) : PS3TMAPI.GetTargetInfoX64(ref targetInfoPriv);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        targetInfo.Flags = targetInfoPriv.Flags;
        targetInfo.Target = targetInfoPriv.Target;
        targetInfo.Name = PS3TMAPI.Utf8ToString(targetInfoPriv.Name, uint.MaxValue);
        targetInfo.Type = PS3TMAPI.Utf8ToString(targetInfoPriv.Type, uint.MaxValue);
        targetInfo.Info = PS3TMAPI.Utf8ToString(targetInfoPriv.Info, uint.MaxValue);
        targetInfo.HomeDir = PS3TMAPI.Utf8ToString(targetInfoPriv.HomeDir, uint.MaxValue);
        targetInfo.FSDir = PS3TMAPI.Utf8ToString(targetInfoPriv.FSDir, uint.MaxValue);
        targetInfo.Boot = targetInfoPriv.Boot;
        return snresult;
    }

    // Token: 0x06000047 RID: 71
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetTargetInfo")]
    private static extern PS3TMAPI.SNRESULT SetTargetInfoX86(ref PS3TMAPI.TargetInfoPriv info);

    // Token: 0x06000048 RID: 72
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetTargetInfo")]
    private static extern PS3TMAPI.SNRESULT SetTargetInfoX64(ref PS3TMAPI.TargetInfoPriv info);

    // Token: 0x06000049 RID: 73 RVA: 0x000024B8 File Offset: 0x000006B8
    public static PS3TMAPI.SNRESULT SetTargetInfo(PS3TMAPI.TargetInfo targetInfo)
    {
        PS3TMAPI.TargetInfoPriv targetInfoPriv = default(PS3TMAPI.TargetInfoPriv);
        targetInfoPriv.Flags = targetInfo.Flags;
        targetInfoPriv.Target = targetInfo.Target;
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(IntPtr.Zero);
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(IntPtr.Zero);
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr3 = new PS3TMAPI.ScopedGlobalHeapPtr(IntPtr.Zero);
        if ((targetInfo.Flags & PS3TMAPI.TargetInfoFlag.Name) > (PS3TMAPI.TargetInfoFlag)0U)
        {
            scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(targetInfo.Name));
            targetInfoPriv.Name = scopedGlobalHeapPtr.Get();
        }
        if ((targetInfo.Flags & PS3TMAPI.TargetInfoFlag.HomeDir) > (PS3TMAPI.TargetInfoFlag)0U)
        {
            scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(targetInfo.HomeDir));
            targetInfoPriv.HomeDir = scopedGlobalHeapPtr2.Get();
        }
        if ((targetInfo.Flags & PS3TMAPI.TargetInfoFlag.FileServingDir) > (PS3TMAPI.TargetInfoFlag)0U)
        {
            scopedGlobalHeapPtr3 = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(targetInfo.FSDir));
            targetInfoPriv.FSDir = scopedGlobalHeapPtr3.Get();
        }
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetTargetInfoX64(ref targetInfoPriv);
        }
        return PS3TMAPI.SetTargetInfoX86(ref targetInfoPriv);
    }

    // Token: 0x0600004A RID: 74
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ListTargetTypes")]
    private static extern PS3TMAPI.SNRESULT ListTargetTypesX86(ref uint size, IntPtr targetTypes);

    // Token: 0x0600004B RID: 75
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ListTargetTypes")]
    private static extern PS3TMAPI.SNRESULT ListTargetTypesX64(ref uint size, IntPtr targetTypes);

    // Token: 0x0600004C RID: 76 RVA: 0x000025A4 File Offset: 0x000007A4
    public static PS3TMAPI.SNRESULT ListTargetTypes(out PS3TMAPI.TargetType[] targetTypes)
    {
        targetTypes = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.ListTargetTypesX86(ref num, IntPtr.Zero) : PS3TMAPI.ListTargetTypesX64(ref num, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)((long)Marshal.SizeOf(typeof(PS3TMAPI.TargetType)) * (long)((ulong)num))));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.ListTargetTypesX86(ref num, scopedGlobalHeapPtr.Get()) : PS3TMAPI.ListTargetTypesX64(ref num, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        targetTypes = new PS3TMAPI.TargetType[num];
        IntPtr utf8Ptr = scopedGlobalHeapPtr.Get();
        for (uint num2 = 0U; num2 < num; num2 += 1U)
        {
            targetTypes[(int)((UIntPtr)num2)].Type = PS3TMAPI.Utf8ToString(utf8Ptr, 64U);
            utf8Ptr = new IntPtr(utf8Ptr.ToInt64() + 64L);
            targetTypes[(int)((UIntPtr)num2)].Description = PS3TMAPI.Utf8ToString(utf8Ptr, 256U);
            utf8Ptr = new IntPtr(utf8Ptr.ToInt64() + 256L);
        }
        return snresult;
    }

    // Token: 0x0600004D RID: 77
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3AddTarget")]
    private static extern PS3TMAPI.SNRESULT AddTargetX86(IntPtr name, IntPtr type, int connParamsSize, IntPtr connectParams, out int target);

    // Token: 0x0600004E RID: 78
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3AddTarget")]
    private static extern PS3TMAPI.SNRESULT AddTargetX64(IntPtr name, IntPtr type, int connParamsSize, IntPtr connectParams, out int target);

    // Token: 0x0600004F RID: 79 RVA: 0x000026A8 File Offset: 0x000008A8
    public static PS3TMAPI.SNRESULT AddTarget(string name, string targetType, PS3TMAPI.TCPIPConnectProperties connectProperties, out int target)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(IntPtr.Zero);
        int num = 0;
        if (connectProperties != null)
        {
            num = Marshal.SizeOf(connectProperties);
            scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal(num));
            Marshal.StructureToPtr(connectProperties, scopedGlobalHeapPtr.Get(), false);
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(name));
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr3 = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(targetType));
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.AddTargetX86(scopedGlobalHeapPtr2.Get(), scopedGlobalHeapPtr3.Get(), num, scopedGlobalHeapPtr.Get(), out target) : PS3TMAPI.AddTargetX64(scopedGlobalHeapPtr2.Get(), scopedGlobalHeapPtr3.Get(), num, scopedGlobalHeapPtr.Get(), out target);
    }

    // Token: 0x06000050 RID: 80
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDefaultTarget")]
    private static extern PS3TMAPI.SNRESULT SetDefaultTargetX86(int target);

    // Token: 0x06000051 RID: 81
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDefaultTarget")]
    private static extern PS3TMAPI.SNRESULT SetDefaultTargetX64(int target);

    // Token: 0x06000052 RID: 82 RVA: 0x0000273C File Offset: 0x0000093C
    public static PS3TMAPI.SNRESULT SetDefaultTarget(int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetDefaultTargetX64(target);
        }
        return PS3TMAPI.SetDefaultTargetX86(target);
    }

    // Token: 0x06000053 RID: 83
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDefaultTarget")]
    private static extern PS3TMAPI.SNRESULT GetDefaultTargetX86(out int target);

    // Token: 0x06000054 RID: 84
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDefaultTarget")]
    private static extern PS3TMAPI.SNRESULT GetDefaultTargetX64(out int target);

    // Token: 0x06000055 RID: 85 RVA: 0x00002752 File Offset: 0x00000952
    public static PS3TMAPI.SNRESULT GetDefaultTarget(out int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetDefaultTargetX64(out target);
        }
        return PS3TMAPI.GetDefaultTargetX86(out target);
    }

    // Token: 0x06000056 RID: 86
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterServerEventHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterServerEventHandlerX86(PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x06000057 RID: 87
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterServerEventHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterServerEventHandlerX64(PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x06000058 RID: 88 RVA: 0x00002768 File Offset: 0x00000968
    public static PS3TMAPI.SNRESULT RegisterServerEventHandler(PS3TMAPI.ServerEventCallback callback, ref object userData)
    {
        if (callback == null)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.RegisterServerEventHandlerX86(PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3TMAPI.RegisterServerEventHandlerX64(PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.ms_serverEventCallback = callback;
            PS3TMAPI.ms_serverEventUserData = userData;
        }
        return snresult;
    }

    // Token: 0x06000059 RID: 89
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnRegisterServerEventHandler")]
    private static extern PS3TMAPI.SNRESULT UnregisterServerEventHandlerX86();

    // Token: 0x0600005A RID: 90
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnRegisterServerEventHandler")]
    private static extern PS3TMAPI.SNRESULT UnregisterServerEventHandlerX64();

    // Token: 0x0600005B RID: 91 RVA: 0x000027BC File Offset: 0x000009BC
    public static PS3TMAPI.SNRESULT UnregisterServerEventHandler()
    {
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.UnregisterServerEventHandlerX86() : PS3TMAPI.UnregisterServerEventHandlerX64();
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.ms_serverEventCallback = null;
            PS3TMAPI.ms_serverEventUserData = null;
        }
        return snresult;
    }

    // Token: 0x0600005C RID: 92 RVA: 0x000027F4 File Offset: 0x000009F4
    private static void MarshalServerEvent(int target, uint param, PS3TMAPI.SNRESULT result, uint length, IntPtr data)
    {
        if (PS3TMAPI.ms_serverEventCallback != null)
        {
            PS3TMAPI.ServerEventHeader serverEventHeader = default(PS3TMAPI.ServerEventHeader);
            PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.ServerEventHeader>(data, ref serverEventHeader);
            PS3TMAPI.ms_serverEventCallback(target, result, serverEventHeader.eventType, PS3TMAPI.ms_serverEventUserData);
        }
    }

    // Token: 0x0600005D RID: 93
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetConnectionInfo")]
    private static extern PS3TMAPI.SNRESULT GetConnectionInfoX86(int target, IntPtr connectProperties);

    // Token: 0x0600005E RID: 94
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetConnectionInfo")]
    private static extern PS3TMAPI.SNRESULT GetConnectionInfoX64(int target, IntPtr connectProperties);

    // Token: 0x0600005F RID: 95 RVA: 0x00002834 File Offset: 0x00000A34
    public static PS3TMAPI.SNRESULT GetConnectionInfo(int target, out PS3TMAPI.TCPIPConnectProperties connectProperties)
    {
        connectProperties = null;
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PS3TMAPI.TCPIPConnectProperties))));
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetConnectionInfoX86(target, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetConnectionInfoX64(target, scopedGlobalHeapPtr.Get());
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            connectProperties = new PS3TMAPI.TCPIPConnectProperties();
            Marshal.PtrToStructure(scopedGlobalHeapPtr.Get(), connectProperties);
        }
        return snresult;
    }

    // Token: 0x06000060 RID: 96
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetConnectionInfo")]
    private static extern PS3TMAPI.SNRESULT SetConnectionInfoX86(int target, IntPtr connectProperties);

    // Token: 0x06000061 RID: 97
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetConnectionInfo")]
    private static extern PS3TMAPI.SNRESULT SetConnectionInfoX64(int target, IntPtr connectProperties);

    // Token: 0x06000062 RID: 98 RVA: 0x000028A0 File Offset: 0x00000AA0
    public static PS3TMAPI.SNRESULT SetConnectionInfo(int target, PS3TMAPI.TCPIPConnectProperties connectProperties)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal(Marshal.SizeOf(connectProperties)));
        PS3TMAPI.WriteDataToUnmanagedIncPtr<PS3TMAPI.TCPIPConnectProperties>(connectProperties, scopedGlobalHeapPtr.Get());
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetConnectionInfoX64(target, scopedGlobalHeapPtr.Get());
        }
        return PS3TMAPI.SetConnectionInfoX86(target, scopedGlobalHeapPtr.Get());
    }

    // Token: 0x06000063 RID: 99
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3DeleteTarget")]
    private static extern PS3TMAPI.SNRESULT DeleteTargetX86(int target);

    // Token: 0x06000064 RID: 100
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3DeleteTarget")]
    private static extern PS3TMAPI.SNRESULT DeleteTargetX64(int target);

    // Token: 0x06000065 RID: 101 RVA: 0x000028EB File Offset: 0x00000AEB
    public static PS3TMAPI.SNRESULT DeleteTarget(int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.DeleteTargetX64(target);
        }
        return PS3TMAPI.DeleteTargetX86(target);
    }

    // Token: 0x06000066 RID: 102
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Connect")]
    private static extern PS3TMAPI.SNRESULT ConnectX86(int target, string application);

    // Token: 0x06000067 RID: 103
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Connect")]
    private static extern PS3TMAPI.SNRESULT ConnectX64(int target, string application);

    // Token: 0x06000068 RID: 104 RVA: 0x00002901 File Offset: 0x00000B01
    public static PS3TMAPI.SNRESULT Connect(int target, string application)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ConnectX64(target, application);
        }
        return PS3TMAPI.ConnectX86(target, application);
    }

    // Token: 0x06000069 RID: 105
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ConnectEx")]
    private static extern PS3TMAPI.SNRESULT ConnectExX86(int target, string application, bool bForceFlag);

    // Token: 0x0600006A RID: 106
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ConnectEx")]
    private static extern PS3TMAPI.SNRESULT ConnectExX64(int target, string application, bool bForceFlag);

    // Token: 0x0600006B RID: 107 RVA: 0x00002919 File Offset: 0x00000B19
    public static PS3TMAPI.SNRESULT ConnectEx(int target, string application, bool bForceFlag)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ConnectExX64(target, application, bForceFlag);
        }
        return PS3TMAPI.ConnectExX86(target, application, bForceFlag);
    }

    // Token: 0x0600006C RID: 108
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Disconnect")]
    private static extern PS3TMAPI.SNRESULT DisconnectX86(int target);

    // Token: 0x0600006D RID: 109
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Disconnect")]
    private static extern PS3TMAPI.SNRESULT DisconnectX64(int target);

    // Token: 0x0600006E RID: 110 RVA: 0x00002933 File Offset: 0x00000B33
    public static PS3TMAPI.SNRESULT Disconnect(int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.DisconnectX64(target);
        }
        return PS3TMAPI.DisconnectX86(target);
    }

    // Token: 0x0600006F RID: 111
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ForceDisconnect")]
    private static extern PS3TMAPI.SNRESULT ForceDisconnectX86(int target);

    // Token: 0x06000070 RID: 112
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ForceDisconnect")]
    private static extern PS3TMAPI.SNRESULT ForceDisconnectX64(int target);

    // Token: 0x06000071 RID: 113 RVA: 0x00002949 File Offset: 0x00000B49
    public static PS3TMAPI.SNRESULT ForceDisconnect(int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ForceDisconnectX64(target);
        }
        return PS3TMAPI.ForceDisconnectX86(target);
    }

    // Token: 0x06000072 RID: 114
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSystemInfo")]
    private static extern PS3TMAPI.SNRESULT GetSystemInfoX86(int target, uint reserved, out uint mask, out PS3TMAPI.SystemInfo info);

    // Token: 0x06000073 RID: 115
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSystemInfo")]
    private static extern PS3TMAPI.SNRESULT GetSystemInfoX64(int target, uint reserved, out uint mask, out PS3TMAPI.SystemInfo info);

    // Token: 0x06000074 RID: 116 RVA: 0x00002960 File Offset: 0x00000B60
    public static PS3TMAPI.SNRESULT GetSystemInfo(int target, out PS3TMAPI.SystemInfoFlag mask, out PS3TMAPI.SystemInfo systemInfo)
    {
        systemInfo = default(PS3TMAPI.SystemInfo);
        uint num;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetSystemInfoX86(target, 0U, out num, out systemInfo) : PS3TMAPI.GetSystemInfoX64(target, 0U, out num, out systemInfo);
        mask = (PS3TMAPI.SystemInfoFlag)num;
        return result;
    }

    // Token: 0x06000075 RID: 117
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetExtraLoadFlags")]
    private static extern PS3TMAPI.SNRESULT GetExtraLoadFlagsX86(int target, out ulong extraLoadFlags);

    // Token: 0x06000076 RID: 118
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetExtraLoadFlags")]
    private static extern PS3TMAPI.SNRESULT GetExtraLoadFlagsX64(int target, out ulong extraLoadFlags);

    // Token: 0x06000077 RID: 119 RVA: 0x00002998 File Offset: 0x00000B98
    public static PS3TMAPI.SNRESULT GetExtraLoadFlags(int target, out PS3TMAPI.ExtraLoadFlag extraLoadFlags)
    {
        ulong num = 0UL;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetExtraLoadFlagsX86(target, out num) : PS3TMAPI.GetExtraLoadFlagsX64(target, out num);
        extraLoadFlags = (PS3TMAPI.ExtraLoadFlag)num;
        return result;
    }

    // Token: 0x06000078 RID: 120
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetExtraLoadFlags")]
    private static extern PS3TMAPI.SNRESULT SetExtraLoadFlagsX86(int target, ulong extraLoadFlags, ulong mask);

    // Token: 0x06000079 RID: 121
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetExtraLoadFlags")]
    private static extern PS3TMAPI.SNRESULT SetExtraLoadFlagsX64(int target, ulong extraLoadFlags, ulong mask);

    // Token: 0x0600007A RID: 122 RVA: 0x000029C6 File Offset: 0x00000BC6
    public static PS3TMAPI.SNRESULT SetExtraLoadFlags(int target, PS3TMAPI.ExtraLoadFlag extraLoadFlags, PS3TMAPI.ExtraLoadFlagMask mask)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetExtraLoadFlagsX64(target, (ulong)extraLoadFlags, (ulong)mask);
        }
        return PS3TMAPI.SetExtraLoadFlagsX86(target, (ulong)extraLoadFlags, (ulong)mask);
    }

    // Token: 0x0600007B RID: 123
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSDKVersion")]
    private static extern PS3TMAPI.SNRESULT GetSDKVersionX86(int target, out ulong sdkVersion);

    // Token: 0x0600007C RID: 124
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSDKVersion")]
    private static extern PS3TMAPI.SNRESULT GetSDKVersionX64(int target, out ulong sdkVersion);

    // Token: 0x0600007D RID: 125 RVA: 0x000029E0 File Offset: 0x00000BE0
    public static PS3TMAPI.SNRESULT GetSDKVersion(int target, out ulong sdkVersion)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetSDKVersionX64(target, out sdkVersion);
        }
        return PS3TMAPI.GetSDKVersionX86(target, out sdkVersion);
    }

    // Token: 0x0600007E RID: 126
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetCPVersion")]
    private static extern PS3TMAPI.SNRESULT GetCPVersionX86(int target, out ulong cpVersion);

    // Token: 0x0600007F RID: 127
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetCPVersion")]
    private static extern PS3TMAPI.SNRESULT GetCPVersionX64(int target, out ulong cpVersion);

    // Token: 0x06000080 RID: 128 RVA: 0x000029F8 File Offset: 0x00000BF8
    public static PS3TMAPI.SNRESULT GetCPVersion(int target, out ulong cpVersion)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetCPVersionX64(target, out cpVersion);
        }
        return PS3TMAPI.GetCPVersionX86(target, out cpVersion);
    }

    // Token: 0x06000081 RID: 129
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetTimeouts")]
    private static extern PS3TMAPI.SNRESULT SetTimeoutsX86(int target, uint numTimeouts, PS3TMAPI.TimeoutType[] timeoutIds, uint[] timeoutValues);

    // Token: 0x06000082 RID: 130
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetTimeouts")]
    private static extern PS3TMAPI.SNRESULT SetTimeoutsX64(int target, uint numTimeouts, PS3TMAPI.TimeoutType[] timeoutIds, uint[] timeoutValues);

    // Token: 0x06000083 RID: 131 RVA: 0x00002A10 File Offset: 0x00000C10
    public static PS3TMAPI.SNRESULT SetTimeouts(int target, PS3TMAPI.TimeoutType[] timeoutTypes, uint[] timeoutValues)
    {
        if (timeoutTypes == null || timeoutTypes.Length < 1 || timeoutValues == null || timeoutValues.Length != timeoutTypes.Length)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetTimeoutsX64(target, (uint)timeoutTypes.Length, timeoutTypes, timeoutValues);
        }
        return PS3TMAPI.SetTimeoutsX86(target, (uint)timeoutTypes.Length, timeoutTypes, timeoutValues);
    }

    // Token: 0x06000084 RID: 132
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetTimeouts")]
    private static extern PS3TMAPI.SNRESULT GetTimeoutsX86(int target, out uint numTimeouts, PS3TMAPI.TimeoutType[] timeoutIds, uint[] timeoutValues);

    // Token: 0x06000085 RID: 133
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetTimeouts")]
    private static extern PS3TMAPI.SNRESULT GetTimeoutsX64(int target, out uint numTimeouts, PS3TMAPI.TimeoutType[] timeoutIds, uint[] timeoutValues);

    // Token: 0x06000086 RID: 134 RVA: 0x00002A48 File Offset: 0x00000C48
    public static PS3TMAPI.SNRESULT GetTimeouts(int target, out PS3TMAPI.TimeoutType[] timeoutTypes, out uint[] timeoutValues)
    {
        timeoutTypes = null;
        timeoutValues = null;
        uint num;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetTimeoutsX86(target, out num, null, null) : PS3TMAPI.GetTimeoutsX64(target, out num, null, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        timeoutTypes = new PS3TMAPI.TimeoutType[num];
        timeoutValues = new uint[num];
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetTimeoutsX64(target, out num, timeoutTypes, timeoutValues);
        }
        return PS3TMAPI.GetTimeoutsX86(target, out num, timeoutTypes, timeoutValues);
    }

    // Token: 0x06000087 RID: 135
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ListTTYStreams")]
    private static extern PS3TMAPI.SNRESULT ListTtyStreamsX86(int target, ref uint size, IntPtr streamArray);

    // Token: 0x06000088 RID: 136
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ListTTYStreams")]
    private static extern PS3TMAPI.SNRESULT ListTtyStreamsX64(int target, ref uint size, IntPtr streamArray);

    // Token: 0x06000089 RID: 137 RVA: 0x00002AB8 File Offset: 0x00000CB8
    public static PS3TMAPI.SNRESULT ListTTYStreams(int target, out PS3TMAPI.TTYStream[] streamArray)
    {
        streamArray = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.ListTtyStreamsX86(target, ref num, IntPtr.Zero) : PS3TMAPI.ListTtyStreamsX64(target, ref num, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)((long)Marshal.SizeOf(typeof(PS3TMAPI.TTYStream)) * (long)((ulong)num))));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.ListTtyStreamsX86(target, ref num, scopedGlobalHeapPtr.Get()) : PS3TMAPI.ListTtyStreamsX64(target, ref num, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        IntPtr unmanagedBuf = scopedGlobalHeapPtr.Get();
        streamArray = new PS3TMAPI.TTYStream[num];
        for (uint num2 = 0U; num2 < num; num2 += 1U)
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.TTYStream>(unmanagedBuf, ref streamArray[(int)((UIntPtr)num2)]);
        }
        return snresult;
    }

    // Token: 0x0600008A RID: 138
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterTTYEventHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterTtyEventHandlerX86(int target, uint streamIndex, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x0600008B RID: 139
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterTTYEventHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterTtyEventHandlerX64(int target, uint streamIndex, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x0600008C RID: 140 RVA: 0x00002B79 File Offset: 0x00000D79
    public static PS3TMAPI.SNRESULT RegisterTTYEventHandler(int target, uint streamID, PS3TMAPI.TTYCallback callback, ref object userData)
    {
        return PS3TMAPI.RegisterTTYEventHandlerHelper(target, streamID, callback, ref userData);
    }

    // Token: 0x0600008D RID: 141 RVA: 0x00002B84 File Offset: 0x00000D84
    public static PS3TMAPI.SNRESULT RegisterTTYEventHandlerRaw(int target, uint streamID, PS3TMAPI.TTYCallbackRaw callback, ref object userData)
    {
        return PS3TMAPI.RegisterTTYEventHandlerHelper(target, streamID, callback, ref userData);
    }

    // Token: 0x0600008E RID: 142 RVA: 0x00002B90 File Offset: 0x00000D90
    private static PS3TMAPI.SNRESULT RegisterTTYEventHandlerHelper(int target, uint streamID, object callback, ref object userData)
    {
        if (callback == null)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.RegisterTtyEventHandlerX86(target, streamID, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3TMAPI.RegisterTtyEventHandlerX64(target, streamID, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        List<PS3TMAPI.TTYChannel> list = new List<PS3TMAPI.TTYChannel>();
        if (streamID == 4294967295U)
        {
            PS3TMAPI.TTYStream[] array = null;
            snresult = PS3TMAPI.ListTTYStreams(target, out array);
            if (PS3TMAPI.FAILED(snresult) || array == null || array.Length == 0)
            {
                return snresult;
            }
            foreach (PS3TMAPI.TTYStream ttystream in array)
            {
                list.Add(new PS3TMAPI.TTYChannel(target, ttystream.Index));
            }
        }
        else
        {
            list.Add(new PS3TMAPI.TTYChannel(target, streamID));
        }
        if (PS3TMAPI.ms_userTtyCallbacks == null)
        {
            PS3TMAPI.ms_userTtyCallbacks = new Dictionary<PS3TMAPI.TTYChannel, PS3TMAPI.TTYCallbackAndUserData>(1);
        }
        foreach (PS3TMAPI.TTYChannel key in list)
        {
            PS3TMAPI.TTYCallbackAndUserData ttycallbackAndUserData;
            if (!PS3TMAPI.ms_userTtyCallbacks.TryGetValue(key, out ttycallbackAndUserData))
            {
                ttycallbackAndUserData = new PS3TMAPI.TTYCallbackAndUserData();
            }
            if (callback is PS3TMAPI.TTYCallback)
            {
                ttycallbackAndUserData.m_callback = (PS3TMAPI.TTYCallback)callback;
                ttycallbackAndUserData.m_userData = userData;
            }
            else
            {
                ttycallbackAndUserData.m_callbackRaw = (PS3TMAPI.TTYCallbackRaw)callback;
                ttycallbackAndUserData.m_userDataRaw = userData;
            }
            PS3TMAPI.ms_userTtyCallbacks[key] = ttycallbackAndUserData;
        }
        return snresult;
    }

    // Token: 0x0600008F RID: 143
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CancelTTYEvents")]
    private static extern PS3TMAPI.SNRESULT CancelTtyEventsX86(int target, uint index);

    // Token: 0x06000090 RID: 144
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CancelTTYEvents")]
    private static extern PS3TMAPI.SNRESULT CancelTtyEventsX64(int target, uint index);

    // Token: 0x06000091 RID: 145 RVA: 0x00002CF4 File Offset: 0x00000EF4
    public static PS3TMAPI.SNRESULT CancelTTYEvents(int target, uint streamID)
    {
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.CancelTtyEventsX86(target, streamID) : PS3TMAPI.CancelTtyEventsX64(target, streamID);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            if (PS3TMAPI.ms_userTtyCallbacks == null)
            {
                return snresult;
            }
            if (streamID == 4294967295U)
            {
                List<PS3TMAPI.TTYChannel> list = new List<PS3TMAPI.TTYChannel>();
                foreach (KeyValuePair<PS3TMAPI.TTYChannel, PS3TMAPI.TTYCallbackAndUserData> keyValuePair in PS3TMAPI.ms_userTtyCallbacks)
                {
                    if (keyValuePair.Key.Target == target)
                    {
                        list.Add(keyValuePair.Key);
                    }
                }
                using (List<PS3TMAPI.TTYChannel>.Enumerator enumerator2 = list.GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        PS3TMAPI.TTYChannel key = enumerator2.Current;
                        PS3TMAPI.ms_userTtyCallbacks.Remove(key);
                    }
                    return snresult;
                }
            }
            PS3TMAPI.ms_userTtyCallbacks.Remove(new PS3TMAPI.TTYChannel(target, streamID));
        }
        return snresult;
    }

    // Token: 0x06000092 RID: 146
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SendTTY")]
    private static extern PS3TMAPI.SNRESULT SendTTYX86(int target, uint index, string text);

    // Token: 0x06000093 RID: 147
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SendTTY")]
    private static extern PS3TMAPI.SNRESULT SendTTYX64(int target, uint index, string text);

    // Token: 0x06000094 RID: 148 RVA: 0x00002DEC File Offset: 0x00000FEC
    public static PS3TMAPI.SNRESULT SendTTY(int target, uint streamID, string text)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SendTTYX64(target, streamID, text);
        }
        return PS3TMAPI.SendTTYX86(target, streamID, text);
    }

    // Token: 0x06000095 RID: 149
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SendTTY")]
    private static extern PS3TMAPI.SNRESULT SendTTYRawX86(int target, uint index, byte[] text);

    // Token: 0x06000096 RID: 150
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SendTTY")]
    private static extern PS3TMAPI.SNRESULT SendTTYRawX64(int target, uint index, byte[] text);

    // Token: 0x06000097 RID: 151 RVA: 0x00002E08 File Offset: 0x00001008
    public static PS3TMAPI.SNRESULT SendTTYRaw(int target, uint streamID, byte[] text)
    {
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.SendTTYRawX86(target, streamID, text) : PS3TMAPI.SendTTYRawX64(target, streamID, text);
    }

    // Token: 0x06000098 RID: 152 RVA: 0x00002E30 File Offset: 0x00001030
    private static void MarshalTTYEvent(int target, uint param, PS3TMAPI.SNRESULT result, uint length, IntPtr data)
    {
        if (PS3TMAPI.ms_userTtyCallbacks == null)
        {
            return;
        }
        PS3TMAPI.TTYChannel key = new PS3TMAPI.TTYChannel(target, param);
        PS3TMAPI.TTYCallbackAndUserData ttycallbackAndUserData;
        if (PS3TMAPI.ms_userTtyCallbacks.TryGetValue(key, out ttycallbackAndUserData))
        {
            if (ttycallbackAndUserData.m_callback != null)
            {
                string data2 = Marshal.PtrToStringAnsi(data, (int)length);
                ttycallbackAndUserData.m_callback(target, param, result, data2, ttycallbackAndUserData.m_userData);
            }
            if (ttycallbackAndUserData.m_callbackRaw != null)
            {
                byte[] array = new byte[length];
                Marshal.Copy(data, array, 0, (int)length);
                ttycallbackAndUserData.m_callbackRaw(target, param, result, array, ttycallbackAndUserData.m_userDataRaw);
            }
        }
    }

    // Token: 0x06000099 RID: 153
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ClearTTYCache")]
    private static extern PS3TMAPI.SNRESULT ClearTTYCacheX86(int target);

    // Token: 0x0600009A RID: 154
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ClearTTYCache")]
    private static extern PS3TMAPI.SNRESULT ClearTTYCacheX64(int target);

    // Token: 0x0600009B RID: 155 RVA: 0x00002EB4 File Offset: 0x000010B4
    public static PS3TMAPI.SNRESULT ClearTTYCache(int target)
    {
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.ClearTTYCacheX86(target) : PS3TMAPI.ClearTTYCacheX64(target);
    }

    // Token: 0x0600009C RID: 156
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Kick")]
    private static extern PS3TMAPI.SNRESULT KickX86();

    // Token: 0x0600009D RID: 157
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Kick")]
    private static extern PS3TMAPI.SNRESULT KickX64();

    // Token: 0x0600009E RID: 158 RVA: 0x00002ED8 File Offset: 0x000010D8
    public static PS3TMAPI.SNRESULT Kick()
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.KickX64();
        }
        return PS3TMAPI.KickX86();
    }

    // Token: 0x0600009F RID: 159
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetStatus")]
    private static extern PS3TMAPI.SNRESULT GetStatusX86(int target, PS3TMAPI.UnitType unit, out long status, IntPtr reasonCode);

    // Token: 0x060000A0 RID: 160
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetStatus")]
    private static extern PS3TMAPI.SNRESULT GetStatusX64(int target, PS3TMAPI.UnitType unit, out long status, IntPtr reasonCode);

    // Token: 0x060000A1 RID: 161 RVA: 0x00002EEC File Offset: 0x000010EC
    public static PS3TMAPI.SNRESULT GetStatus(int target, PS3TMAPI.UnitType unit, out PS3TMAPI.UnitStatus unitStatus)
    {
        long num;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetStatusX86(target, unit, out num, IntPtr.Zero) : PS3TMAPI.GetStatusX64(target, unit, out num, IntPtr.Zero);
        unitStatus = (PS3TMAPI.UnitStatus)num;
        return result;
    }

    // Token: 0x060000A2 RID: 162
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "SNPS3ProcessLoad")]
    private static extern PS3TMAPI.SNRESULT ProcessLoadX86(int target, uint priority, IntPtr fileName, int argCount, string[] args, int envCount, string[] env, out uint processId, out ulong threadId, uint flags);

    // Token: 0x060000A3 RID: 163
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "SNPS3ProcessLoad")]
    private static extern PS3TMAPI.SNRESULT ProcessLoadX64(int target, uint priority, IntPtr fileName, int argCount, string[] args, int envCount, string[] env, out uint processId, out ulong threadId, uint flags);

    // Token: 0x060000A4 RID: 164 RVA: 0x00002F24 File Offset: 0x00001124
    public static PS3TMAPI.SNRESULT ProcessLoad(int target, uint priority, string fileName, string[] argv, string[] envv, out uint processID, out ulong threadID, PS3TMAPI.LoadFlag loadFlags)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(fileName));
        int argCount = 0;
        if (argv != null)
        {
            argCount = argv.Length;
        }
        int envCount = 0;
        if (envv != null)
        {
            envCount = envv.Length;
        }
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ProcessLoadX64(target, priority, scopedGlobalHeapPtr.Get(), argCount, argv, envCount, envv, out processID, out threadID, (uint)loadFlags);
        }
        return PS3TMAPI.ProcessLoadX86(target, priority, scopedGlobalHeapPtr.Get(), argCount, argv, envCount, envv, out processID, out threadID, (uint)loadFlags);
    }

    // Token: 0x060000A5 RID: 165
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessList")]
    private static extern PS3TMAPI.SNRESULT GetProcessListX86(int target, ref uint count, IntPtr processIdArray);

    // Token: 0x060000A6 RID: 166
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessList")]
    private static extern PS3TMAPI.SNRESULT GetProcessListX64(int target, ref uint count, IntPtr processIdArray);

    // Token: 0x060000A7 RID: 167 RVA: 0x00002F8C File Offset: 0x0000118C
    public static PS3TMAPI.SNRESULT GetProcessList(int target, out uint[] processIDs)
    {
        processIDs = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetProcessListX86(target, ref num, IntPtr.Zero) : PS3TMAPI.GetProcessListX64(target, ref num, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)(4U * num)));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetProcessListX86(target, ref num, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetProcessListX64(target, ref num, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        IntPtr unmanagedBuf = scopedGlobalHeapPtr.Get();
        processIDs = new uint[num];
        for (uint num2 = 0U; num2 < num; num2 += 1U)
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref processIDs[(int)((UIntPtr)num2)]);
        }
        return snresult;
    }

    // Token: 0x060000A8 RID: 168
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UserProcessList")]
    private static extern PS3TMAPI.SNRESULT GetUserProcessListX86(int target, ref uint count, IntPtr processIdArray);

    // Token: 0x060000A9 RID: 169
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UserProcessList")]
    private static extern PS3TMAPI.SNRESULT GetUserProcessListX64(int target, ref uint count, IntPtr processIdArray);

    // Token: 0x060000AA RID: 170 RVA: 0x0000303C File Offset: 0x0000123C
    public static PS3TMAPI.SNRESULT GetUserProcessList(int target, out uint[] processIDs)
    {
        uint num = 0U;
        processIDs = null;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetUserProcessListX86(target, ref num, IntPtr.Zero) : PS3TMAPI.GetUserProcessListX64(target, ref num, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)(4U * num)));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetUserProcessListX86(target, ref num, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetUserProcessListX64(target, ref num, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        IntPtr unmanagedBuf = scopedGlobalHeapPtr.Get();
        processIDs = new uint[num];
        for (uint num2 = 0U; num2 < num; num2 += 1U)
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref processIDs[(int)((UIntPtr)num2)]);
        }
        return snresult;
    }

    // Token: 0x060000AB RID: 171
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessStop")]
    private static extern PS3TMAPI.SNRESULT ProcessStopX86(int target, uint processId);

    // Token: 0x060000AC RID: 172
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessStop")]
    private static extern PS3TMAPI.SNRESULT ProcessStopX64(int target, uint processId);

    // Token: 0x060000AD RID: 173 RVA: 0x000030EC File Offset: 0x000012EC
    public static PS3TMAPI.SNRESULT ProcessStop(int target, uint processID)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ProcessStopX64(target, processID);
        }
        return PS3TMAPI.ProcessStopX86(target, processID);
    }

    // Token: 0x060000AE RID: 174
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessContinue")]
    private static extern PS3TMAPI.SNRESULT ProcessContinueX86(int target, uint processId);

    // Token: 0x060000AF RID: 175
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessContinue")]
    private static extern PS3TMAPI.SNRESULT ProcessContinueX64(int target, uint processId);

    // Token: 0x060000B0 RID: 176 RVA: 0x00003104 File Offset: 0x00001304
    public static PS3TMAPI.SNRESULT ProcessContinue(int target, uint processID)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ProcessContinueX64(target, processID);
        }
        return PS3TMAPI.ProcessContinueX86(target, processID);
    }

    // Token: 0x060000B1 RID: 177
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessKill")]
    private static extern PS3TMAPI.SNRESULT ProcessKillX86(int target, uint processId);

    // Token: 0x060000B2 RID: 178
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessKill")]
    private static extern PS3TMAPI.SNRESULT ProcessKillX64(int target, uint processId);

    // Token: 0x060000B3 RID: 179 RVA: 0x0000311C File Offset: 0x0000131C
    public static PS3TMAPI.SNRESULT ProcessKill(int target, uint processID)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ProcessKillX64(target, processID);
        }
        return PS3TMAPI.ProcessKillX86(target, processID);
    }

    // Token: 0x060000B4 RID: 180
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3TerminateGameProcess")]
    private static extern PS3TMAPI.SNRESULT TerminateGameProcessX86(int target, uint processId, uint timeout);

    // Token: 0x060000B5 RID: 181
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3TerminateGameProcess")]
    private static extern PS3TMAPI.SNRESULT TerminateGameProcessX64(int target, uint processId, uint timeout);

    // Token: 0x060000B6 RID: 182 RVA: 0x00003134 File Offset: 0x00001334
    public static PS3TMAPI.SNRESULT TerminateGameProcess(int target, uint processID, uint timeout)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.TerminateGameProcessX64(target, processID, timeout);
        }
        return PS3TMAPI.TerminateGameProcessX86(target, processID, timeout);
    }

    // Token: 0x060000B7 RID: 183
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadList")]
    private static extern PS3TMAPI.SNRESULT GetThreadListX86(int target, uint processId, ref uint numPPUThreads, ulong[] ppuThreadIds, ref uint numSPUThreadGroups, ulong[] spuThreadGroupIds);

    // Token: 0x060000B8 RID: 184
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadList")]
    private static extern PS3TMAPI.SNRESULT GetThreadListX64(int target, uint processId, ref uint numPPUThreads, ulong[] ppuThreadIds, ref uint numSPUThreadGroups, ulong[] spuThreadGroupIds);

    // Token: 0x060000B9 RID: 185 RVA: 0x00003150 File Offset: 0x00001350
    public static PS3TMAPI.SNRESULT GetThreadList(int target, uint processID, out ulong[] ppuThreadIDs, out ulong[] spuThreadGroupIDs)
    {
        ppuThreadIDs = null;
        spuThreadGroupIDs = null;
        uint num = 0U;
        uint num2 = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetThreadListX86(target, processID, ref num, null, ref num2, null) : PS3TMAPI.GetThreadListX64(target, processID, ref num, null, ref num2, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        ppuThreadIDs = new ulong[num];
        spuThreadGroupIDs = new ulong[num2];
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.GetThreadListX86(target, processID, ref num, ppuThreadIDs, ref num2, spuThreadGroupIDs) : PS3TMAPI.GetThreadListX64(target, processID, ref num, ppuThreadIDs, ref num2, spuThreadGroupIDs);
    }

    // Token: 0x060000BA RID: 186
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadStop")]
    private static extern PS3TMAPI.SNRESULT ThreadStopX86(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId);

    // Token: 0x060000BB RID: 187
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadStop")]
    private static extern PS3TMAPI.SNRESULT ThreadStopX64(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId);

    // Token: 0x060000BC RID: 188 RVA: 0x000031D0 File Offset: 0x000013D0
    public static PS3TMAPI.SNRESULT ThreadStop(int target, PS3TMAPI.UnitType unit, uint processID, ulong threadID)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ThreadStopX64(target, unit, processID, threadID);
        }
        return PS3TMAPI.ThreadStopX86(target, unit, processID, threadID);
    }

    // Token: 0x060000BD RID: 189
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadContinue")]
    private static extern PS3TMAPI.SNRESULT ThreadContinueX86(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId);

    // Token: 0x060000BE RID: 190
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadContinue")]
    private static extern PS3TMAPI.SNRESULT ThreadContinueX64(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId);

    // Token: 0x060000BF RID: 191 RVA: 0x000031EC File Offset: 0x000013EC
    public static PS3TMAPI.SNRESULT ThreadContinue(int target, PS3TMAPI.UnitType unit, uint processID, ulong threadID)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ThreadContinueX64(target, unit, processID, threadID);
        }
        return PS3TMAPI.ThreadContinueX86(target, unit, processID, threadID);
    }

    // Token: 0x060000C0 RID: 192
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadGetRegisters")]
    private static extern PS3TMAPI.SNRESULT ThreadGetRegistersX86(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId, uint numRegisters, uint[] registerNums, ulong[] registerValues);

    // Token: 0x060000C1 RID: 193
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadGetRegisters")]
    private static extern PS3TMAPI.SNRESULT ThreadGetRegistersX64(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId, uint numRegisters, uint[] registerNums, ulong[] registerValues);

    // Token: 0x060000C2 RID: 194 RVA: 0x00003208 File Offset: 0x00001408
    public static PS3TMAPI.SNRESULT ThreadGetRegisters(int target, PS3TMAPI.UnitType unit, uint processID, ulong threadID, uint[] registerNums, out ulong[] registerValues)
    {
        registerValues = null;
        if (registerNums == null)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        registerValues = new ulong[registerNums.Length];
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ThreadGetRegistersX64(target, unit, processID, threadID, (uint)registerNums.Length, registerNums, registerValues);
        }
        return PS3TMAPI.ThreadGetRegistersX86(target, unit, processID, threadID, (uint)registerNums.Length, registerNums, registerValues);
    }

    // Token: 0x060000C3 RID: 195
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadSetRegisters")]
    private static extern PS3TMAPI.SNRESULT ThreadSetRegistersX86(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId, uint numRegisters, uint[] registerNums, ulong[] registerValues);

    // Token: 0x060000C4 RID: 196
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadSetRegisters")]
    private static extern PS3TMAPI.SNRESULT ThreadSetRegistersX64(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId, uint numRegisters, uint[] registerNums, ulong[] registerValues);

    // Token: 0x060000C5 RID: 197 RVA: 0x00003258 File Offset: 0x00001458
    public static PS3TMAPI.SNRESULT ThreadSetRegisters(int target, PS3TMAPI.UnitType unit, uint processID, ulong threadID, uint[] registerNums, ulong[] registerValues)
    {
        if (registerNums == null || registerValues == null || registerNums.Length != registerValues.Length)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ThreadSetRegistersX64(target, unit, processID, threadID, (uint)registerNums.Length, registerNums, registerValues);
        }
        return PS3TMAPI.ThreadSetRegistersX86(target, unit, processID, threadID, (uint)registerNums.Length, registerNums, registerValues);
    }

    // Token: 0x060000C6 RID: 198 RVA: 0x000032A4 File Offset: 0x000014A4
    private static void ProcessInfoMarshalHelper(IntPtr unmanagedBuf, ref PS3TMAPI.ProcessInfo processInfo)
    {
        uint status = 0U;
        unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref status);
        processInfo.Hdr.Status = (PS3TMAPI.ProcessStatus)status;
        unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref processInfo.Hdr.NumPPUThreads);
        unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref processInfo.Hdr.NumSPUThreads);
        unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref processInfo.Hdr.ParentProcessID);
        unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref processInfo.Hdr.MaxMemorySize);
        processInfo.Hdr.ELFPath = PS3TMAPI.Utf8ToString(unmanagedBuf, 512U);
        unmanagedBuf = new IntPtr(unmanagedBuf.ToInt64() + 512L);
        uint num = processInfo.Hdr.NumPPUThreads + processInfo.Hdr.NumSPUThreads;
        processInfo.ThreadIDs = new ulong[num];
        int num2 = 0;
        while ((long)num2 < (long)((ulong)num))
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref processInfo.ThreadIDs[num2]);
            num2++;
        }
    }

    // Token: 0x060000C7 RID: 199
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessInfo")]
    private static extern PS3TMAPI.SNRESULT GetProcessInfoX86(int target, uint processId, ref uint bufferSize, IntPtr processInfo);

    // Token: 0x060000C8 RID: 200
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessInfo")]
    private static extern PS3TMAPI.SNRESULT GetProcessInfoX64(int target, uint processId, ref uint bufferSize, IntPtr processInfo);

    // Token: 0x060000C9 RID: 201 RVA: 0x00003388 File Offset: 0x00001588
    public static PS3TMAPI.SNRESULT GetProcessInfo(int target, uint processID, out PS3TMAPI.ProcessInfo processInfo)
    {
        processInfo = default(PS3TMAPI.ProcessInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetProcessInfoX86(target, processID, ref cb, IntPtr.Zero) : PS3TMAPI.GetProcessInfoX64(target, processID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetProcessInfoX86(target, processID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetProcessInfoX64(target, processID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.ProcessInfoMarshalHelper(scopedGlobalHeapPtr.Get(), ref processInfo);
        }
        return snresult;
    }

    // Token: 0x060000CA RID: 202
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessInfoEx")]
    private static extern PS3TMAPI.SNRESULT GetProcessInfoExX86(int target, uint processId, ref uint bufferSize, IntPtr processInfo, out PS3TMAPI.ExtraProcessInfo extraProcessInfo);

    // Token: 0x060000CB RID: 203
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessInfoEx")]
    private static extern PS3TMAPI.SNRESULT GetProcessInfoExX64(int target, uint processId, ref uint bufferSize, IntPtr processInfo, out PS3TMAPI.ExtraProcessInfo extraProcessInfo);

    // Token: 0x060000CC RID: 204 RVA: 0x00003418 File Offset: 0x00001618
    public static PS3TMAPI.SNRESULT GetProcessInfoEx(int target, uint processID, out PS3TMAPI.ProcessInfo processInfo, out PS3TMAPI.ExtraProcessInfo extraProcessInfo)
    {
        processInfo = default(PS3TMAPI.ProcessInfo);
        extraProcessInfo = default(PS3TMAPI.ExtraProcessInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetProcessInfoExX86(target, processID, ref cb, IntPtr.Zero, out extraProcessInfo) : PS3TMAPI.GetProcessInfoExX64(target, processID, ref cb, IntPtr.Zero, out extraProcessInfo);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetProcessInfoExX86(target, processID, ref cb, scopedGlobalHeapPtr.Get(), out extraProcessInfo) : PS3TMAPI.GetProcessInfoExX64(target, processID, ref cb, scopedGlobalHeapPtr.Get(), out extraProcessInfo));
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.ProcessInfoMarshalHelper(scopedGlobalHeapPtr.Get(), ref processInfo);
        }
        return snresult;
    }

    // Token: 0x060000CD RID: 205
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessInfoEx2")]
    private static extern PS3TMAPI.SNRESULT GetProcessInfoEx2X86(int target, uint processId, ref uint bufferSize, IntPtr processInfo, out PS3TMAPI.ExtraProcessInfo extraProcessInfo, out PS3TMAPI.ProcessLoadInfo processLoadInfo);

    // Token: 0x060000CE RID: 206
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessInfoEx2")]
    private static extern PS3TMAPI.SNRESULT GetProcessInfoEx2X64(int target, uint processId, ref uint bufferSize, IntPtr processInfo, out PS3TMAPI.ExtraProcessInfo extraProcessInfo, out PS3TMAPI.ProcessLoadInfo processLoadInfo);

    // Token: 0x060000CF RID: 207 RVA: 0x000034B4 File Offset: 0x000016B4
    public static PS3TMAPI.SNRESULT GetProcessInfoEx2(int target, uint processID, out PS3TMAPI.ProcessInfo processInfo, out PS3TMAPI.ExtraProcessInfo extraProcessInfo, out PS3TMAPI.ProcessLoadInfo processLoadInfo)
    {
        uint cb = 0U;
        processInfo = default(PS3TMAPI.ProcessInfo);
        extraProcessInfo = default(PS3TMAPI.ExtraProcessInfo);
        processLoadInfo = default(PS3TMAPI.ProcessLoadInfo);
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetProcessInfoEx2X86(target, processID, ref cb, IntPtr.Zero, out extraProcessInfo, out processLoadInfo) : PS3TMAPI.GetProcessInfoEx2X64(target, processID, ref cb, IntPtr.Zero, out extraProcessInfo, out processLoadInfo);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetProcessInfoEx2X86(target, processID, ref cb, scopedGlobalHeapPtr.Get(), out extraProcessInfo, out processLoadInfo) : PS3TMAPI.GetProcessInfoEx2X64(target, processID, ref cb, scopedGlobalHeapPtr.Get(), out extraProcessInfo, out processLoadInfo));
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.ProcessInfoMarshalHelper(scopedGlobalHeapPtr.Get(), ref processInfo);
        }
        return snresult;
    }

    // Token: 0x060000D0 RID: 208
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetModuleList")]
    private static extern PS3TMAPI.SNRESULT GetModuleListX86(int target, uint processId, ref uint numModules, uint[] moduleList);

    // Token: 0x060000D1 RID: 209
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetModuleList")]
    private static extern PS3TMAPI.SNRESULT GetModuleListX64(int target, uint processId, ref uint numModules, uint[] moduleList);

    // Token: 0x060000D2 RID: 210 RVA: 0x00003560 File Offset: 0x00001760
    public static PS3TMAPI.SNRESULT GetModuleList(int target, uint processID, out uint[] modules)
    {
        modules = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetModuleListX86(target, processID, ref num, null) : PS3TMAPI.GetModuleListX64(target, processID, ref num, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        modules = new uint[num];
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.GetModuleListX86(target, processID, ref num, modules) : PS3TMAPI.GetModuleListX64(target, processID, ref num, modules);
    }

    // Token: 0x060000D3 RID: 211 RVA: 0x000035C4 File Offset: 0x000017C4
    private static IntPtr ModuleInfoHdrMarshalHelper(IntPtr unmanagedBuf, ref PS3TMAPI.ModuleInfoHdr moduleInfoHdr)
    {
        PS3TMAPI.ModuleInfoHdrPriv moduleInfoHdrPriv = default(PS3TMAPI.ModuleInfoHdrPriv);
        unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.ModuleInfoHdrPriv>(unmanagedBuf, ref moduleInfoHdrPriv);
        moduleInfoHdr.Name = PS3TMAPI.Utf8FixedSizeByteArrayToString(moduleInfoHdrPriv.Name);
        moduleInfoHdr.Version = moduleInfoHdrPriv.Version;
        moduleInfoHdr.Attribute = moduleInfoHdrPriv.Attribute;
        moduleInfoHdr.StartEntry = moduleInfoHdrPriv.StartEntry;
        moduleInfoHdr.StopEntry = moduleInfoHdrPriv.StopEntry;
        moduleInfoHdr.ELFName = PS3TMAPI.Utf8FixedSizeByteArrayToString(moduleInfoHdrPriv.ELFName);
        moduleInfoHdr.NumSegments = moduleInfoHdrPriv.NumSegments;
        return unmanagedBuf;
    }

    // Token: 0x060000D4 RID: 212
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetModuleInfo")]
    private static extern PS3TMAPI.SNRESULT GetModuleInfoX86(int target, uint processId, uint moduleId, ref ulong bufferSize, IntPtr moduleInfo);

    // Token: 0x060000D5 RID: 213
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetModuleInfo")]
    private static extern PS3TMAPI.SNRESULT GetModuleInfoX64(int target, uint processId, uint moduleId, ref ulong bufferSize, IntPtr moduleInfo);

    // Token: 0x060000D6 RID: 214 RVA: 0x0000364C File Offset: 0x0000184C
    public static PS3TMAPI.SNRESULT GetModuleInfo(int target, uint processID, uint moduleID, out PS3TMAPI.ModuleInfo moduleInfo)
    {
        moduleInfo = default(PS3TMAPI.ModuleInfo);
        ulong num = 0UL;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetModuleInfoX86(target, processID, moduleID, ref num, IntPtr.Zero) : PS3TMAPI.GetModuleInfoX64(target, processID, moduleID, ref num, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        if (num > 2147483647UL)
        {
            return PS3TMAPI.SNRESULT.SN_E_ERROR;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)num));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetModuleInfoX86(target, processID, moduleID, ref num, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetModuleInfoX64(target, processID, moduleID, ref num, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        IntPtr unmanagedBuf = scopedGlobalHeapPtr.Get();
        unmanagedBuf = PS3TMAPI.ModuleInfoHdrMarshalHelper(unmanagedBuf, ref moduleInfo.Hdr);
        moduleInfo.Segments = new PS3TMAPI.PRXSegment[moduleInfo.Hdr.NumSegments];
        int num2 = 0;
        while ((long)num2 < (long)((ulong)moduleInfo.Hdr.NumSegments))
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.PRXSegment>(unmanagedBuf, ref moduleInfo.Segments[num2]);
            num2++;
        }
        return snresult;
    }

    // Token: 0x060000D7 RID: 215
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetModuleInfoEx")]
    private static extern PS3TMAPI.SNRESULT GetModuleInfoExX86(int target, uint processId, uint moduleId, ref ulong bufferSize, IntPtr moduleInfoEx, out IntPtr mselfInfo, out PS3TMAPI.ExtraModuleInfo extraModuleInfo);

    // Token: 0x060000D8 RID: 216
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetModuleInfoEx")]
    private static extern PS3TMAPI.SNRESULT GetModuleInfoExX64(int target, uint processId, uint moduleId, ref ulong bufferSize, IntPtr moduleInfoEx, out IntPtr mselfInfo, out PS3TMAPI.ExtraModuleInfo extraModuleInfo);

    // Token: 0x060000D9 RID: 217 RVA: 0x00003744 File Offset: 0x00001944
    public static PS3TMAPI.SNRESULT GetModuleInfoEx(int target, uint processID, uint moduleID, out PS3TMAPI.ModuleInfoEx moduleInfoEx, out PS3TMAPI.MSELFInfo mselfInfo, out PS3TMAPI.ExtraModuleInfo extraModuleInfo)
    {
        moduleInfoEx = default(PS3TMAPI.ModuleInfoEx);
        mselfInfo = default(PS3TMAPI.MSELFInfo);
        extraModuleInfo = default(PS3TMAPI.ExtraModuleInfo);
        ulong num = 0UL;
        IntPtr zero = IntPtr.Zero;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetModuleInfoExX86(target, processID, moduleID, ref num, IntPtr.Zero, out zero, out extraModuleInfo) : PS3TMAPI.GetModuleInfoExX64(target, processID, moduleID, ref num, IntPtr.Zero, out zero, out extraModuleInfo);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        if (num > 2147483647UL)
        {
            return PS3TMAPI.SNRESULT.SN_E_ERROR;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)num));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetModuleInfoExX86(target, processID, moduleID, ref num, scopedGlobalHeapPtr.Get(), out zero, out extraModuleInfo) : PS3TMAPI.GetModuleInfoExX64(target, processID, moduleID, ref num, scopedGlobalHeapPtr.Get(), out zero, out extraModuleInfo));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        IntPtr unmanagedBuf = scopedGlobalHeapPtr.Get();
        unmanagedBuf = PS3TMAPI.ModuleInfoHdrMarshalHelper(unmanagedBuf, ref moduleInfoEx.Hdr);
        moduleInfoEx.Segments = new PS3TMAPI.PRXSegmentEx[moduleInfoEx.Hdr.NumSegments];
        int num2 = 0;
        while ((long)num2 < (long)((ulong)moduleInfoEx.Hdr.NumSegments))
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.PRXSegmentEx>(unmanagedBuf, ref moduleInfoEx.Segments[num2]);
            num2++;
        }
        PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.MSELFInfo>(zero, ref mselfInfo);
        return snresult;
    }

    // Token: 0x060000DA RID: 218
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadInfo")]
    private static extern PS3TMAPI.SNRESULT GetThreadInfoX86(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x060000DB RID: 219
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadInfo")]
    private static extern PS3TMAPI.SNRESULT GetThreadInfoX64(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x060000DC RID: 220 RVA: 0x00003870 File Offset: 0x00001A70
    public static PS3TMAPI.SNRESULT GetPPUThreadInfo(int target, uint processID, ulong threadID, out PS3TMAPI.PPUThreadInfo threadInfo)
    {
        threadInfo = default(PS3TMAPI.PPUThreadInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetThreadInfoX86(target, PS3TMAPI.UnitType.PPU, processID, threadID, ref cb, IntPtr.Zero) : PS3TMAPI.GetThreadInfoX64(target, PS3TMAPI.UnitType.PPU, processID, threadID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetThreadInfoX86(target, PS3TMAPI.UnitType.PPU, processID, threadID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetThreadInfoX64(target, PS3TMAPI.UnitType.PPU, processID, threadID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.PPUThreadInfoPriv pputhreadInfoPriv = default(PS3TMAPI.PPUThreadInfoPriv);
        IntPtr utf8Ptr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.PPUThreadInfoPriv>(scopedGlobalHeapPtr.Get(), ref pputhreadInfoPriv);
        threadInfo.ThreadID = pputhreadInfoPriv.ThreadID;
        threadInfo.Priority = pputhreadInfoPriv.Priority;
        threadInfo.State = (PS3TMAPI.PPUThreadState)pputhreadInfoPriv.State;
        threadInfo.StackAddress = pputhreadInfoPriv.StackAddress;
        threadInfo.StackSize = pputhreadInfoPriv.StackSize;
        if (pputhreadInfoPriv.ThreadNameLen > 0U)
        {
            threadInfo.ThreadName = PS3TMAPI.Utf8ToString(utf8Ptr, uint.MaxValue);
        }
        return snresult;
    }

    // Token: 0x060000DD RID: 221
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3PPUThreadInfoEx")]
    private static extern PS3TMAPI.SNRESULT GetPPUThreadInfoExX86(int target, uint processId, ulong threadId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x060000DE RID: 222
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3PPUThreadInfoEx")]
    private static extern PS3TMAPI.SNRESULT GetPPUThreadInfoExX64(int target, uint processId, ulong threadId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x060000DF RID: 223 RVA: 0x00003970 File Offset: 0x00001B70
    public static PS3TMAPI.SNRESULT GetPPUThreadInfoEx(int target, uint processID, ulong threadID, out PS3TMAPI.PPUThreadInfoEx threadInfoEx)
    {
        threadInfoEx = default(PS3TMAPI.PPUThreadInfoEx);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetPPUThreadInfoExX86(target, processID, threadID, ref cb, IntPtr.Zero) : PS3TMAPI.GetPPUThreadInfoExX64(target, processID, threadID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetPPUThreadInfoExX86(target, processID, threadID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetPPUThreadInfoExX64(target, processID, threadID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.PPUThreadInfoExPriv pputhreadInfoExPriv = default(PS3TMAPI.PPUThreadInfoExPriv);
        IntPtr utf8Ptr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.PPUThreadInfoExPriv>(scopedGlobalHeapPtr.Get(), ref pputhreadInfoExPriv);
        threadInfoEx.ThreadID = pputhreadInfoExPriv.ThreadId;
        threadInfoEx.Priority = pputhreadInfoExPriv.Priority;
        threadInfoEx.BasePriority = pputhreadInfoExPriv.BasePriority;
        threadInfoEx.State = (PS3TMAPI.PPUThreadState)pputhreadInfoExPriv.State;
        threadInfoEx.StackAddress = pputhreadInfoExPriv.StackAddress;
        threadInfoEx.StackSize = pputhreadInfoExPriv.StackSize;
        if (pputhreadInfoExPriv.ThreadNameLen > 0U)
        {
            threadInfoEx.ThreadName = PS3TMAPI.Utf8ToString(utf8Ptr, uint.MaxValue);
        }
        return snresult;
    }

    // Token: 0x060000E0 RID: 224 RVA: 0x00003A78 File Offset: 0x00001C78
    public static PS3TMAPI.SNRESULT GetSPUThreadInfo(int target, uint processID, ulong threadID, out PS3TMAPI.SPUThreadInfo threadInfo)
    {
        threadInfo = default(PS3TMAPI.SPUThreadInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetThreadInfoX86(target, PS3TMAPI.UnitType.SPU, processID, threadID, ref cb, IntPtr.Zero) : PS3TMAPI.GetThreadInfoX64(target, PS3TMAPI.UnitType.SPU, processID, threadID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetThreadInfoX86(target, PS3TMAPI.UnitType.SPU, processID, threadID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetThreadInfoX64(target, PS3TMAPI.UnitType.SPU, processID, threadID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.SpuThreadInfoPriv spuThreadInfoPriv = default(PS3TMAPI.SpuThreadInfoPriv);
        IntPtr utf8Ptr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.SpuThreadInfoPriv>(scopedGlobalHeapPtr.Get(), ref spuThreadInfoPriv);
        threadInfo.ThreadGroupID = spuThreadInfoPriv.ThreadGroupId;
        threadInfo.ThreadID = spuThreadInfoPriv.ThreadId;
        if (spuThreadInfoPriv.FilenameLen > 0U)
        {
            threadInfo.Filename = PS3TMAPI.Utf8ToString(utf8Ptr, uint.MaxValue);
        }
        if (spuThreadInfoPriv.ThreadNameLen > 0U)
        {
            IntPtr utf8Ptr2 = new IntPtr(utf8Ptr.ToInt64() + (long)((ulong)spuThreadInfoPriv.FilenameLen));
            threadInfo.ThreadName = PS3TMAPI.Utf8ToString(utf8Ptr2, uint.MaxValue);
        }
        return snresult;
    }

    // Token: 0x060000E1 RID: 225
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDefaultPPUThreadStackSize")]
    private static extern PS3TMAPI.SNRESULT SetDefaultPPUThreadStackSizeX86(int target, PS3TMAPI.ELFStackSize size);

    // Token: 0x060000E2 RID: 226
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDefaultPPUThreadStackSize")]
    private static extern PS3TMAPI.SNRESULT SetDefaultPPUThreadStackSizeX64(int target, PS3TMAPI.ELFStackSize size);

    // Token: 0x060000E3 RID: 227 RVA: 0x00003B7D File Offset: 0x00001D7D
    public static PS3TMAPI.SNRESULT SetDefaultPPUThreadStackSize(int target, PS3TMAPI.ELFStackSize stackSize)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetDefaultPPUThreadStackSizeX64(target, stackSize);
        }
        return PS3TMAPI.SetDefaultPPUThreadStackSizeX86(target, stackSize);
    }

    // Token: 0x060000E4 RID: 228
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDefaultPPUThreadStackSize")]
    private static extern PS3TMAPI.SNRESULT GetDefaultPPUThreadStackSizeX86(int target, out PS3TMAPI.ELFStackSize size);

    // Token: 0x060000E5 RID: 229
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDefaultPPUThreadStackSize")]
    private static extern PS3TMAPI.SNRESULT GetDefaultPPUThreadStackSizeX64(int target, out PS3TMAPI.ELFStackSize size);

    // Token: 0x060000E6 RID: 230 RVA: 0x00003B95 File Offset: 0x00001D95
    public static PS3TMAPI.SNRESULT GetDefaultPPUThreadStackSize(int target, out PS3TMAPI.ELFStackSize stackSize)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetDefaultPPUThreadStackSizeX64(target, out stackSize);
        }
        return PS3TMAPI.GetDefaultPPUThreadStackSizeX86(target, out stackSize);
    }

    // Token: 0x060000E7 RID: 231
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetSPULoopPoint")]
    private static extern PS3TMAPI.SNRESULT SetSPULoopPointX86(int target, uint processId, ulong threadId, uint address, int bCurrentPc);

    // Token: 0x060000E8 RID: 232
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetSPULoopPoint")]
    private static extern PS3TMAPI.SNRESULT SetSPULoopPointX64(int target, uint processId, ulong threadId, uint address, int bCurrentPc);

    // Token: 0x060000E9 RID: 233 RVA: 0x00003BB0 File Offset: 0x00001DB0
    public static PS3TMAPI.SNRESULT SetSPULoopPoint(int target, uint processID, ulong threadID, uint address, bool bCurrentPC)
    {
        int bCurrentPc = bCurrentPC ? 1 : 0;
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetSPULoopPointX64(target, processID, threadID, address, bCurrentPc);
        }
        return PS3TMAPI.SetSPULoopPointX86(target, processID, threadID, address, bCurrentPc);
    }

    // Token: 0x060000EA RID: 234
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ClearSPULoopPoint")]
    private static extern PS3TMAPI.SNRESULT ClearSPULoopPointX86(int target, uint processId, ulong threadId, uint address, bool bCurrentPc);

    // Token: 0x060000EB RID: 235
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ClearSPULoopPoint")]
    private static extern PS3TMAPI.SNRESULT ClearSPULoopPointX64(int target, uint processId, ulong threadId, uint address, bool bCurrentPc);

    // Token: 0x060000EC RID: 236 RVA: 0x00003BE2 File Offset: 0x00001DE2
    public static PS3TMAPI.SNRESULT ClearSPULoopPoint(int target, uint processID, ulong threadID, uint address, bool bCurrentPC)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ClearSPULoopPointX64(target, processID, threadID, address, bCurrentPC);
        }
        return PS3TMAPI.ClearSPULoopPointX86(target, processID, threadID, address, bCurrentPC);
    }

    // Token: 0x060000ED RID: 237
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetBreakPoint")]
    private static extern PS3TMAPI.SNRESULT SetBreakPointX86(int target, uint unit, uint processId, ulong threadId, ulong address);

    // Token: 0x060000EE RID: 238
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetBreakPoint")]
    private static extern PS3TMAPI.SNRESULT SetBreakPointX64(int target, uint unit, uint processId, ulong threadId, ulong address);

    // Token: 0x060000EF RID: 239 RVA: 0x00003C02 File Offset: 0x00001E02
    public static PS3TMAPI.SNRESULT SetBreakPoint(int target, PS3TMAPI.UnitType unit, uint processID, ulong threadID, ulong address)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetBreakPointX64(target, (uint)unit, processID, threadID, address);
        }
        return PS3TMAPI.SetBreakPointX86(target, (uint)unit, processID, threadID, address);
    }

    // Token: 0x060000F0 RID: 240
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ClearBreakPoint")]
    private static extern PS3TMAPI.SNRESULT ClearBreakPointX86(int target, uint unit, uint processId, ulong threadId, ulong address);

    // Token: 0x060000F1 RID: 241
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ClearBreakPoint")]
    private static extern PS3TMAPI.SNRESULT ClearBreakPointX64(int target, uint unit, uint processId, ulong threadId, ulong address);

    // Token: 0x060000F2 RID: 242 RVA: 0x00003C22 File Offset: 0x00001E22
    public static PS3TMAPI.SNRESULT ClearBreakPoint(int target, PS3TMAPI.UnitType unit, uint processID, ulong threadID, ulong address)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ClearBreakPointX64(target, (uint)unit, processID, threadID, address);
        }
        return PS3TMAPI.ClearBreakPointX86(target, (uint)unit, processID, threadID, address);
    }

    // Token: 0x060000F3 RID: 243
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetBreakPoints")]
    private static extern PS3TMAPI.SNRESULT GetBreakPointsX86(int target, uint unit, uint processId, ulong threadId, out uint numBreakpoints, ulong[] addresses);

    // Token: 0x060000F4 RID: 244
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetBreakPoints")]
    private static extern PS3TMAPI.SNRESULT GetBreakPointsX64(int target, uint unit, uint processId, ulong threadId, out uint numBreakpoints, ulong[] addresses);

    // Token: 0x060000F5 RID: 245 RVA: 0x00003C44 File Offset: 0x00001E44
    public static PS3TMAPI.SNRESULT GetBreakPoints(int target, PS3TMAPI.UnitType unit, uint processID, ulong threadID, out ulong[] bpAddresses)
    {
        bpAddresses = null;
        uint num;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetBreakPointsX86(target, (uint)unit, processID, threadID, out num, null) : PS3TMAPI.GetBreakPointsX64(target, (uint)unit, processID, threadID, out num, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        bpAddresses = new ulong[num];
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetBreakPointsX64(target, (uint)unit, processID, threadID, out num, bpAddresses);
        }
        return PS3TMAPI.GetBreakPointsX86(target, (uint)unit, processID, threadID, out num, bpAddresses);
    }

    // Token: 0x060000F6 RID: 246
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDebugThreadControlInfo")]
    private static extern PS3TMAPI.SNRESULT GetDebugThreadControlInfoX86(int target, uint processId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x060000F7 RID: 247
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDebugThreadControlInfo")]
    private static extern PS3TMAPI.SNRESULT GetDebugThreadControlInfoX64(int target, uint processId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x060000F8 RID: 248 RVA: 0x00003CB0 File Offset: 0x00001EB0
    public static PS3TMAPI.SNRESULT GetDebugThreadControlInfo(int target, uint processID, out PS3TMAPI.DebugThreadControlInfo threadCtrlInfo)
    {
        threadCtrlInfo = default(PS3TMAPI.DebugThreadControlInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetDebugThreadControlInfoX86(target, processID, ref cb, IntPtr.Zero) : PS3TMAPI.GetDebugThreadControlInfoX64(target, processID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetDebugThreadControlInfoX86(target, processID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetDebugThreadControlInfoX64(target, processID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.DebugThreadControlInfoPriv debugThreadControlInfoPriv = default(PS3TMAPI.DebugThreadControlInfoPriv);
        IntPtr intPtr = scopedGlobalHeapPtr.Get();
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.DebugThreadControlInfoPriv>(intPtr, ref debugThreadControlInfoPriv);
        threadCtrlInfo.ControlFlags = debugThreadControlInfoPriv.ControlFlags;
        uint numEntries = debugThreadControlInfoPriv.NumEntries;
        threadCtrlInfo.ControlKeywords = new PS3TMAPI.ControlKeywordEntry[numEntries];
        for (uint num = 0U; num < numEntries; num += 1U)
        {
            intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref threadCtrlInfo.ControlKeywords[(int)((UIntPtr)num)].MatchConditionFlags);
            threadCtrlInfo.ControlKeywords[(int)((UIntPtr)num)].Keyword = PS3TMAPI.Utf8ToString(intPtr, 128U);
            intPtr = new IntPtr(intPtr.ToInt64() + 128L);
        }
        return snresult;
    }

    // Token: 0x060000F9 RID: 249
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDebugThreadControlInfo")]
    private static extern PS3TMAPI.SNRESULT SetDebugThreadControlInfoX86(int target, uint processId, IntPtr threadCtrlInfo, out uint maxEntries);

    // Token: 0x060000FA RID: 250
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDebugThreadControlInfo")]
    private static extern PS3TMAPI.SNRESULT SetDebugThreadControlInfoX64(int target, uint processId, IntPtr threadCtrlInfo, out uint maxEntries);

    // Token: 0x060000FB RID: 251 RVA: 0x00003DD8 File Offset: 0x00001FD8
    public static PS3TMAPI.SNRESULT SetDebugThreadControlInfo(int target, uint processID, PS3TMAPI.DebugThreadControlInfo threadCtrlInfo, out uint maxEntries)
    {
        PS3TMAPI.DebugThreadControlInfoPriv debugThreadControlInfoPriv = default(PS3TMAPI.DebugThreadControlInfoPriv);
        debugThreadControlInfoPriv.ControlFlags = threadCtrlInfo.ControlFlags;
        if (threadCtrlInfo.ControlKeywords != null)
        {
            debugThreadControlInfoPriv.NumEntries = (uint)threadCtrlInfo.ControlKeywords.Length;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal(Marshal.SizeOf(debugThreadControlInfoPriv) + (int)(debugThreadControlInfoPriv.NumEntries * (uint)Marshal.SizeOf(typeof(PS3TMAPI.ControlKeywordEntry)))));
        IntPtr unmanagedBuf = PS3TMAPI.WriteDataToUnmanagedIncPtr<PS3TMAPI.DebugThreadControlInfoPriv>(debugThreadControlInfoPriv, scopedGlobalHeapPtr.Get());
        int num = 0;
        while ((long)num < (long)((ulong)debugThreadControlInfoPriv.NumEntries))
        {
            unmanagedBuf = PS3TMAPI.WriteDataToUnmanagedIncPtr<PS3TMAPI.ControlKeywordEntry>(threadCtrlInfo.ControlKeywords[num], unmanagedBuf);
            num++;
        }
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetDebugThreadControlInfoX64(target, processID, scopedGlobalHeapPtr.Get(), out maxEntries);
        }
        return PS3TMAPI.SetDebugThreadControlInfoX86(target, processID, scopedGlobalHeapPtr.Get(), out maxEntries);
    }

    // Token: 0x060000FC RID: 252
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadExceptionClean")]
    private static extern PS3TMAPI.SNRESULT ThreadExceptionCleanX86(int target, uint processId, ulong threadId);

    // Token: 0x060000FD RID: 253
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ThreadExceptionClean")]
    private static extern PS3TMAPI.SNRESULT ThreadExceptionCleanX64(int target, uint processId, ulong threadId);

    // Token: 0x060000FE RID: 254 RVA: 0x00003EA0 File Offset: 0x000020A0
    public static PS3TMAPI.SNRESULT ThreadExceptionClean(int target, uint processID, ulong threadID)
    {
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.ThreadExceptionCleanX86(target, processID, threadID) : PS3TMAPI.ThreadExceptionCleanX64(target, processID, threadID);
    }

    // Token: 0x060000FF RID: 255
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetRawSPULogicalIDs")]
    private static extern PS3TMAPI.SNRESULT GetRawSPULogicalIdsX86(int target, uint processId, ulong[] logicalIds);

    // Token: 0x06000100 RID: 256
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetRawSPULogicalIDs")]
    private static extern PS3TMAPI.SNRESULT GetRawSPULogicalIdsX64(int target, uint processId, ulong[] logicalIds);

    // Token: 0x06000101 RID: 257 RVA: 0x00003EC8 File Offset: 0x000020C8
    public static PS3TMAPI.SNRESULT GetRawSPULogicalIDs(int target, uint processID, out ulong[] logicalIDs)
    {
        logicalIDs = new ulong[8];
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetRawSPULogicalIdsX64(target, processID, logicalIDs);
        }
        return PS3TMAPI.GetRawSPULogicalIdsX86(target, processID, logicalIDs);
    }

    // Token: 0x06000102 RID: 258
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SPUThreadGroupStop")]
    private static extern PS3TMAPI.SNRESULT SPUThreadGroupStopX86(int target, uint processId, ulong threadGroupId);

    // Token: 0x06000103 RID: 259
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SPUThreadGroupStop")]
    private static extern PS3TMAPI.SNRESULT SPUThreadGroupStopX64(int target, uint processId, ulong threadGroupId);

    // Token: 0x06000104 RID: 260 RVA: 0x00003EEC File Offset: 0x000020EC
    public static PS3TMAPI.SNRESULT SPUThreadGroupStop(int target, uint processID, ulong threadGroupID)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SPUThreadGroupStopX64(target, processID, threadGroupID);
        }
        return PS3TMAPI.SPUThreadGroupStopX86(target, processID, threadGroupID);
    }

    // Token: 0x06000105 RID: 261
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SPUThreadGroupContinue")]
    private static extern PS3TMAPI.SNRESULT SPUThreadGroupContinueX86(int target, uint processId, ulong threadGroupId);

    // Token: 0x06000106 RID: 262
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SPUThreadGroupContinue")]
    private static extern PS3TMAPI.SNRESULT SPUThreadGroupContinueX64(int target, uint processId, ulong threadGroupId);

    // Token: 0x06000107 RID: 263 RVA: 0x00003F06 File Offset: 0x00002106
    public static PS3TMAPI.SNRESULT SPUThreadGroupContinue(int target, uint processID, ulong threadGroupID)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SPUThreadGroupContinueX64(target, processID, threadGroupID);
        }
        return PS3TMAPI.SPUThreadGroupContinueX86(target, processID, threadGroupID);
    }

    // Token: 0x06000108 RID: 264
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetProcessTree")]
    private static extern PS3TMAPI.SNRESULT GetProcessTreeX86(int target, ref uint numProcesses, IntPtr buffer);

    // Token: 0x06000109 RID: 265
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetProcessTree")]
    private static extern PS3TMAPI.SNRESULT GetProcessTreeX64(int target, ref uint numProcesses, IntPtr buffer);

    // Token: 0x0600010A RID: 266 RVA: 0x00003F20 File Offset: 0x00002120
    public static PS3TMAPI.SNRESULT GetProcessTree(int target, out PS3TMAPI.ProcessTreeBranch[] processTree)
    {
        processTree = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetProcessTreeX86(target, ref num, IntPtr.Zero) : PS3TMAPI.GetProcessTreeX64(target, ref num, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)(num * (uint)Marshal.SizeOf(typeof(PS3TMAPI.ProcessTreeBranchPriv)))));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetProcessTreeX86(target, ref num, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetProcessTreeX64(target, ref num, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        processTree = new PS3TMAPI.ProcessTreeBranch[num];
        int num2 = 0;
        while ((long)num2 < (long)((ulong)num))
        {
            PS3TMAPI.ProcessTreeBranchPriv processTreeBranchPriv = (PS3TMAPI.ProcessTreeBranchPriv)Marshal.PtrToStructure(scopedGlobalHeapPtr.Get(), typeof(PS3TMAPI.ProcessTreeBranchPriv));
            processTree[num2].ProcessID = processTreeBranchPriv.ProcessId;
            processTree[num2].ProcessState = processTreeBranchPriv.ProcessState;
            processTree[num2].ProcessFlags = processTreeBranchPriv.ProcessFlags;
            processTree[num2].RawSPU = processTreeBranchPriv.RawSPU;
            processTree[num2].PPUThreadStatuses = new PS3TMAPI.PPUThreadStatus[processTreeBranchPriv.NumPpuThreads];
            processTree[num2].SPUThreadGroupStatuses = new PS3TMAPI.SPUThreadGroupStatus[processTreeBranchPriv.NumSpuThreadGroups];
            int num3 = 0;
            while ((long)num3 < (long)((ulong)processTreeBranchPriv.NumPpuThreads))
            {
                processTreeBranchPriv.PpuThreadStatuses = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.PPUThreadStatus>(processTreeBranchPriv.PpuThreadStatuses, ref processTree[num2].PPUThreadStatuses[num3]);
                num3++;
            }
            int num4 = 0;
            while ((long)num4 < (long)((ulong)processTreeBranchPriv.NumSpuThreadGroups))
            {
                processTreeBranchPriv.SpuThreadGroupStatuses = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.SPUThreadGroupStatus>(processTreeBranchPriv.SpuThreadGroupStatuses, ref processTree[num2].SPUThreadGroupStatuses[num4]);
                num4++;
            }
            num2++;
        }
        return snresult;
    }

    // Token: 0x0600010B RID: 267
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSPUThreadGroupInfo")]
    private static extern PS3TMAPI.SNRESULT GetSPUThreadGroupInfoX86(int target, uint processId, ulong threadGroupId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x0600010C RID: 268
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSPUThreadGroupInfo")]
    private static extern PS3TMAPI.SNRESULT GetSPUThreadGroupInfoX64(int target, uint processId, ulong threadGroupId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x0600010D RID: 269 RVA: 0x000040E8 File Offset: 0x000022E8
    public static PS3TMAPI.SNRESULT GetSPUThreadGroupInfo(int target, uint processID, ulong threadGroupID, out PS3TMAPI.SPUThreadGroupInfo threadGroupInfo)
    {
        threadGroupInfo = default(PS3TMAPI.SPUThreadGroupInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetSPUThreadGroupInfoX86(target, processID, threadGroupID, ref cb, IntPtr.Zero) : PS3TMAPI.GetSPUThreadGroupInfoX64(target, processID, threadGroupID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetSPUThreadGroupInfoX86(target, processID, threadGroupID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetSPUThreadGroupInfoX64(target, processID, threadGroupID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.SpuThreadGroupInfoPriv spuThreadGroupInfoPriv = default(PS3TMAPI.SpuThreadGroupInfoPriv);
        IntPtr intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.SpuThreadGroupInfoPriv>(scopedGlobalHeapPtr.Get(), ref spuThreadGroupInfoPriv);
        threadGroupInfo.ThreadGroupID = spuThreadGroupInfoPriv.ThreadGroupId;
        threadGroupInfo.State = (PS3TMAPI.SPUThreadGroupState)spuThreadGroupInfoPriv.State;
        threadGroupInfo.Priority = spuThreadGroupInfoPriv.Priority;
        threadGroupInfo.ThreadIDs = new uint[spuThreadGroupInfoPriv.NumThreads];
        int num = 0;
        while ((long)num < (long)((ulong)spuThreadGroupInfoPriv.NumThreads))
        {
            intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref threadGroupInfo.ThreadIDs[num]);
            num++;
        }
        if (spuThreadGroupInfoPriv.ThreadGroupNameLen > 0U)
        {
            threadGroupInfo.GroupName = PS3TMAPI.Utf8ToString(intPtr, uint.MaxValue);
        }
        return snresult;
    }

    // Token: 0x0600010E RID: 270
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessGetMemory")]
    private static extern PS3TMAPI.SNRESULT ProcessGetMemoryX86(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);

    // Token: 0x0600010F RID: 271
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessGetMemory")]
    private static extern PS3TMAPI.SNRESULT ProcessGetMemoryX64(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);

    // Token: 0x06000110 RID: 272 RVA: 0x00004208 File Offset: 0x00002408
    public static PS3TMAPI.SNRESULT ProcessGetMemory(int target, PS3TMAPI.UnitType unit, uint processID, ulong threadID, ulong address, ref byte[] buffer)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ProcessGetMemoryX64(target, unit, processID, threadID, address, buffer.Length, buffer);
        }
        return PS3TMAPI.ProcessGetMemoryX86(target, unit, processID, threadID, address, buffer.Length, buffer);
    }

    // Token: 0x06000111 RID: 273
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessSetMemory")]
    private static extern PS3TMAPI.SNRESULT ProcessSetMemoryX86(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);

    // Token: 0x06000112 RID: 274
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessSetMemory")]
    private static extern PS3TMAPI.SNRESULT ProcessSetMemoryX64(int target, PS3TMAPI.UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);

    // Token: 0x06000113 RID: 275 RVA: 0x00004238 File Offset: 0x00002438
    public static PS3TMAPI.SNRESULT ProcessSetMemory(int target, PS3TMAPI.UnitType unit, uint processID, ulong threadID, ulong address, byte[] buffer)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ProcessSetMemoryX64(target, unit, processID, threadID, address, buffer.Length, buffer);
        }
        return PS3TMAPI.ProcessSetMemoryX86(target, unit, processID, threadID, address, buffer.Length, buffer);
    }

    // Token: 0x06000114 RID: 276
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMemoryCompressed")]
    private static extern PS3TMAPI.SNRESULT GetMemoryCompressedX86(int target, uint processId, uint compressionLevel, uint address, uint size, byte[] buffer);

    // Token: 0x06000115 RID: 277
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMemoryCompressed")]
    private static extern PS3TMAPI.SNRESULT GetMemoryCompressedX64(int target, uint processId, uint compressionLevel, uint address, uint size, byte[] buffer);

    // Token: 0x06000116 RID: 278 RVA: 0x00004264 File Offset: 0x00002464
    public static PS3TMAPI.SNRESULT GetMemoryCompressed(int target, uint processID, PS3TMAPI.MemoryCompressionLevel compressionLevel, uint address, ref byte[] buffer)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetMemoryCompressedX64(target, processID, (uint)compressionLevel, address, (uint)buffer.Length, buffer);
        }
        return PS3TMAPI.GetMemoryCompressedX86(target, processID, (uint)compressionLevel, address, (uint)buffer.Length, buffer);
    }

    // Token: 0x06000117 RID: 279
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMemory64Compressed")]
    private static extern PS3TMAPI.SNRESULT GetMemory64CompressedX86(int target, uint processId, uint compressionLevel, ulong address, uint size, byte[] buffer);

    // Token: 0x06000118 RID: 280
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMemory64Compressed")]
    private static extern PS3TMAPI.SNRESULT GetMemory64CompressedX64(int target, uint processId, uint compressionLevel, ulong address, uint size, byte[] buffer);

    // Token: 0x06000119 RID: 281 RVA: 0x00004290 File Offset: 0x00002490
    public static PS3TMAPI.SNRESULT GetMemory64Compressed(int target, uint processID, PS3TMAPI.MemoryCompressionLevel compressionLevel, ulong address, ref byte[] buffer)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetMemory64CompressedX64(target, processID, (uint)compressionLevel, address, (uint)buffer.Length, buffer);
        }
        return PS3TMAPI.GetMemory64CompressedX86(target, processID, (uint)compressionLevel, address, (uint)buffer.Length, buffer);
    }

    // Token: 0x0600011A RID: 282
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetVirtualMemoryInfo")]
    private static extern PS3TMAPI.SNRESULT GetVirtualMemoryInfoX86(int target, uint processId, bool bStatsOnly, out uint areaCount, out uint bufferSize, IntPtr buffer);

    // Token: 0x0600011B RID: 283
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetVirtualMemoryInfo")]
    private static extern PS3TMAPI.SNRESULT GetVirtualMemoryInfoX64(int target, uint processId, bool bStatsOnly, out uint areaCount, out uint bufferSize, IntPtr buffer);

    // Token: 0x0600011C RID: 284 RVA: 0x000042BC File Offset: 0x000024BC
    public static PS3TMAPI.SNRESULT GetVirtualMemoryInfo(int target, uint processID, bool bStatsOnly, out PS3TMAPI.VirtualMemoryArea[] vmAreas)
    {
        vmAreas = null;
        uint num;
        uint cb;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetVirtualMemoryInfoX86(target, processID, bStatsOnly, out num, out cb, IntPtr.Zero) : PS3TMAPI.GetVirtualMemoryInfoX64(target, processID, bStatsOnly, out num, out cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetVirtualMemoryInfoX86(target, processID, bStatsOnly, out num, out cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetVirtualMemoryInfoX64(target, processID, bStatsOnly, out num, out cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        vmAreas = new PS3TMAPI.VirtualMemoryArea[num];
        IntPtr unmanagedBuf = scopedGlobalHeapPtr.Get();
        int num2 = 0;
        while ((long)num2 < (long)((ulong)num))
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num2].Address);
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num2].Flags);
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num2].VSize);
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num2].Options);
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num2].PageFaultPPU);
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num2].PageFaultSPU);
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num2].PageIn);
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num2].PageOut);
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num2].PMemTotal);
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num2].PMemUsed);
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num2].Time);
            ulong num3 = 0UL;
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref num3);
            vmAreas[num2].Pages = new ulong[num3];
            IntPtr zero = IntPtr.Zero;
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<IntPtr>(unmanagedBuf, ref zero);
            num2++;
        }
        int num4 = 0;
        while ((long)num4 < (long)((ulong)num))
        {
            int num5 = vmAreas[num4].Pages.Length;
            for (int i = 0; i < num5; i++)
            {
                unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref vmAreas[num4].Pages[i]);
            }
            num4++;
        }
        return snresult;
    }

    // Token: 0x0600011D RID: 285
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSyncPrimitiveCountsEx")]
    private static extern PS3TMAPI.SNRESULT GetSyncPrimitiveCountsExX86(int target, uint processId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x0600011E RID: 286
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSyncPrimitiveCountsEx")]
    private static extern PS3TMAPI.SNRESULT GetSyncPrimitiveCountsExX64(int target, uint processId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x0600011F RID: 287 RVA: 0x000044F8 File Offset: 0x000026F8
    public static PS3TMAPI.SNRESULT GetSyncPrimitiveCounts(int target, uint processID, out PS3TMAPI.SyncPrimitiveCounts primitiveCounts)
    {
        primitiveCounts = default(PS3TMAPI.SyncPrimitiveCounts);
        uint cb = 32U;
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetSyncPrimitiveCountsExX86(target, processID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetSyncPrimitiveCountsExX64(target, processID, ref cb, scopedGlobalHeapPtr.Get());
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        primitiveCounts = (PS3TMAPI.SyncPrimitiveCounts)Marshal.PtrToStructure(scopedGlobalHeapPtr.Get(), typeof(PS3TMAPI.SyncPrimitiveCounts));
        return snresult;
    }

    // Token: 0x06000120 RID: 288
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMutexList")]
    private static extern PS3TMAPI.SNRESULT GetMutexListX86(int target, uint processId, ref uint numMutexes, uint[] mutexList);

    // Token: 0x06000121 RID: 289
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMutexList")]
    private static extern PS3TMAPI.SNRESULT GetMutexListX64(int target, uint processId, ref uint numMutexes, uint[] mutexList);

    // Token: 0x06000122 RID: 290 RVA: 0x00004570 File Offset: 0x00002770
    public static PS3TMAPI.SNRESULT GetMutexList(int target, uint processID, out uint[] mutexIDs)
    {
        mutexIDs = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetMutexListX86(target, processID, ref num, null) : PS3TMAPI.GetMutexListX64(target, processID, ref num, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        mutexIDs = new uint[num];
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.GetMutexListX86(target, processID, ref num, mutexIDs) : PS3TMAPI.GetMutexListX64(target, processID, ref num, mutexIDs);
    }

    // Token: 0x06000123 RID: 291
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMutexInfo")]
    private static extern PS3TMAPI.SNRESULT GetMutexInfoX86(int target, uint processId, uint mutexId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x06000124 RID: 292
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMutexInfo")]
    private static extern PS3TMAPI.SNRESULT GetMutexInfoX64(int target, uint processId, uint mutexId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x06000125 RID: 293 RVA: 0x000045D4 File Offset: 0x000027D4
    public static PS3TMAPI.SNRESULT GetMutexInfo(int target, uint processID, uint mutexID, out PS3TMAPI.MutexInfo mutexInfo)
    {
        mutexInfo = default(PS3TMAPI.MutexInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetMutexInfoX86(target, processID, mutexID, ref cb, IntPtr.Zero) : PS3TMAPI.GetMutexInfoX64(target, processID, mutexID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetMutexInfoX86(target, processID, mutexID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetMutexInfoX64(target, processID, mutexID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        IntPtr intPtr = scopedGlobalHeapPtr.Get();
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref mutexInfo.ID);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref mutexInfo.Attribute.Protocol);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref mutexInfo.Attribute.Recursive);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref mutexInfo.Attribute.PShared);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref mutexInfo.Attribute.Adaptive);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref mutexInfo.Attribute.Key);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref mutexInfo.Attribute.Flags);
        mutexInfo.Attribute.Name = PS3TMAPI.Utf8ToString(intPtr, 8U);
        intPtr = new IntPtr(intPtr.ToInt64() + 8L);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref mutexInfo.OwnerThreadID);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref mutexInfo.LockCounter);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref mutexInfo.ConditionRefCounter);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref mutexInfo.ConditionVarID);
        uint num = 0U;
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref num);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref mutexInfo.NumWaitAllThreads);
        mutexInfo.WaitingThreads = new ulong[num];
        int num2 = 0;
        while ((long)num2 < (long)((ulong)num))
        {
            intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref mutexInfo.WaitingThreads[num2]);
            num2++;
        }
        return snresult;
    }

    // Token: 0x06000126 RID: 294
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetLightWeightMutexList")]
    private static extern PS3TMAPI.SNRESULT GetLightWeightMutexListX86(int target, uint processId, ref uint numLWMutexes, uint[] lwMutexList);

    // Token: 0x06000127 RID: 295
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetLightWeightMutexList")]
    private static extern PS3TMAPI.SNRESULT GetLightWeightMutexListX64(int target, uint processId, ref uint numLWMutexes, uint[] lwMutexList);

    // Token: 0x06000128 RID: 296 RVA: 0x00004784 File Offset: 0x00002984
    public static PS3TMAPI.SNRESULT GetLightWeightMutexList(int target, uint processID, out uint[] lwMutexIDs)
    {
        lwMutexIDs = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetLightWeightMutexListX86(target, processID, ref num, null) : PS3TMAPI.GetLightWeightMutexListX64(target, processID, ref num, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        lwMutexIDs = new uint[num];
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.GetLightWeightMutexListX86(target, processID, ref num, lwMutexIDs) : PS3TMAPI.GetLightWeightMutexListX64(target, processID, ref num, lwMutexIDs);
    }

    // Token: 0x06000129 RID: 297
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetLightWeightMutexInfo")]
    private static extern PS3TMAPI.SNRESULT GetLightWeightMutexInfoX86(int target, uint processId, uint lwMutexId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x0600012A RID: 298
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetLightWeightMutexInfo")]
    private static extern PS3TMAPI.SNRESULT GetLightWeightMutexInfoX64(int target, uint processId, uint lwMutexId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x0600012B RID: 299 RVA: 0x000047E8 File Offset: 0x000029E8
    public static PS3TMAPI.SNRESULT GetLightWeightMutexInfo(int target, uint processID, uint lwMutexID, out PS3TMAPI.LWMutexInfo lwMutexInfo)
    {
        lwMutexInfo = default(PS3TMAPI.LWMutexInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetLightWeightMutexInfoX86(target, processID, lwMutexID, ref cb, IntPtr.Zero) : PS3TMAPI.GetLightWeightMutexInfoX64(target, processID, lwMutexID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetLightWeightMutexInfoX86(target, processID, lwMutexID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetLightWeightMutexInfoX64(target, processID, lwMutexID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.LwMutexInfoPriv lwMutexInfoPriv = default(PS3TMAPI.LwMutexInfoPriv);
        IntPtr unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.LwMutexInfoPriv>(scopedGlobalHeapPtr.Get(), ref lwMutexInfoPriv);
        lwMutexInfo.ID = lwMutexInfoPriv.Id;
        lwMutexInfo.Attribute = lwMutexInfoPriv.Attribute;
        lwMutexInfo.OwnerThreadID = lwMutexInfoPriv.OwnerThreadId;
        lwMutexInfo.LockCounter = lwMutexInfoPriv.LockCounter;
        lwMutexInfo.NumWaitAllThreads = lwMutexInfoPriv.NumWaitAllThreads;
        lwMutexInfo.WaitingThreads = new ulong[lwMutexInfoPriv.NumWaitingThreads];
        int num = 0;
        while ((long)num < (long)((ulong)lwMutexInfoPriv.NumWaitingThreads))
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref lwMutexInfo.WaitingThreads[num]);
            num++;
        }
        return snresult;
    }

    // Token: 0x0600012C RID: 300
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetConditionalVariableList")]
    private static extern PS3TMAPI.SNRESULT GetConditionalVariableListX86(int target, uint processId, ref uint numConditionVars, uint[] conditionVarList);

    // Token: 0x0600012D RID: 301
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetConditionalVariableList")]
    private static extern PS3TMAPI.SNRESULT GetConditionalVariableListX64(int target, uint processId, ref uint numConditionVars, uint[] conditionVarList);

    // Token: 0x0600012E RID: 302 RVA: 0x0000490C File Offset: 0x00002B0C
    public static PS3TMAPI.SNRESULT GetConditionalVariableList(int target, uint processID, out uint[] conditionVarIDs)
    {
        conditionVarIDs = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetConditionalVariableListX86(target, processID, ref num, null) : PS3TMAPI.GetConditionalVariableListX64(target, processID, ref num, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        conditionVarIDs = new uint[num];
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.GetConditionalVariableListX86(target, processID, ref num, conditionVarIDs) : PS3TMAPI.GetConditionalVariableListX64(target, processID, ref num, conditionVarIDs);
    }

    // Token: 0x0600012F RID: 303
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetConditionalVariableInfo")]
    private static extern PS3TMAPI.SNRESULT GetConditionalVariableInfoX86(int target, uint processId, uint conditionVarId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x06000130 RID: 304
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetConditionalVariableInfo")]
    private static extern PS3TMAPI.SNRESULT GetConditionalVariableInfoX64(int target, uint processId, uint conditionVarId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x06000131 RID: 305 RVA: 0x00004970 File Offset: 0x00002B70
    public static PS3TMAPI.SNRESULT GetConditionalVariableInfo(int target, uint processID, uint conditionVarID, out PS3TMAPI.ConditionVarInfo conditionVarInfo)
    {
        conditionVarInfo = default(PS3TMAPI.ConditionVarInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetConditionalVariableInfoX86(target, processID, conditionVarID, ref cb, IntPtr.Zero) : PS3TMAPI.GetConditionalVariableInfoX64(target, processID, conditionVarID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetConditionalVariableInfoX86(target, processID, conditionVarID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetConditionalVariableInfoX64(target, processID, conditionVarID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ConditionVarInfoPriv conditionVarInfoPriv = default(PS3TMAPI.ConditionVarInfoPriv);
        IntPtr unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.ConditionVarInfoPriv>(scopedGlobalHeapPtr.Get(), ref conditionVarInfoPriv);
        conditionVarInfo.ID = conditionVarInfoPriv.Id;
        conditionVarInfo.Attribute = conditionVarInfoPriv.Attribute;
        conditionVarInfo.MutexID = conditionVarInfoPriv.MutexId;
        conditionVarInfo.NumWaitAllThreads = conditionVarInfoPriv.NumWaitAllThreads;
        conditionVarInfo.WaitingThreads = new ulong[conditionVarInfoPriv.NumWaitingThreads];
        int num = 0;
        while ((long)num < (long)((ulong)conditionVarInfoPriv.NumWaitingThreads))
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref conditionVarInfo.WaitingThreads[num]);
            num++;
        }
        return snresult;
    }

    // Token: 0x06000132 RID: 306
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetLightWeightConditionalList")]
    private static extern PS3TMAPI.SNRESULT GetLightWeightConditionalListX86(int target, uint processId, ref uint numLWConditionVars, uint[] lwConditionVarList);

    // Token: 0x06000133 RID: 307
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetLightWeightConditionalList")]
    private static extern PS3TMAPI.SNRESULT GetLightWeightConditionalListX64(int target, uint processId, ref uint numLWConditionVars, uint[] lwConditionVarList);

    // Token: 0x06000134 RID: 308 RVA: 0x00004A88 File Offset: 0x00002C88
    public static PS3TMAPI.SNRESULT GetLightWeightConditionalList(int target, uint processID, out uint[] lwConditionVarIDs)
    {
        lwConditionVarIDs = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetLightWeightConditionalListX86(target, processID, ref num, null) : PS3TMAPI.GetLightWeightConditionalListX64(target, processID, ref num, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        lwConditionVarIDs = new uint[num];
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.GetLightWeightConditionalListX86(target, processID, ref num, lwConditionVarIDs) : PS3TMAPI.GetLightWeightConditionalListX64(target, processID, ref num, lwConditionVarIDs);
    }

    // Token: 0x06000135 RID: 309
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetLightWeightConditionalInfo")]
    private static extern PS3TMAPI.SNRESULT GetLightWeightConditionalInfoX86(int target, uint processId, uint lwCondVarId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x06000136 RID: 310
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetLightWeightConditionalInfo")]
    private static extern PS3TMAPI.SNRESULT GetLightWeightConditionalInfoX64(int target, uint processId, uint lwCondVarId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x06000137 RID: 311 RVA: 0x00004AEC File Offset: 0x00002CEC
    public static PS3TMAPI.SNRESULT GetLightWeightConditionalInfo(int target, uint processID, uint lwCondVarID, out PS3TMAPI.LWConditionVarInfo lwConditonVarInfo)
    {
        lwConditonVarInfo = default(PS3TMAPI.LWConditionVarInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetLightWeightConditionalInfoX86(target, processID, lwCondVarID, ref cb, IntPtr.Zero) : PS3TMAPI.GetLightWeightConditionalInfoX64(target, processID, lwCondVarID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetLightWeightConditionalInfoX86(target, processID, lwCondVarID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetLightWeightConditionalInfoX64(target, processID, lwCondVarID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.LwConditionVarInfoPriv lwConditionVarInfoPriv = default(PS3TMAPI.LwConditionVarInfoPriv);
        IntPtr unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.LwConditionVarInfoPriv>(scopedGlobalHeapPtr.Get(), ref lwConditionVarInfoPriv);
        lwConditonVarInfo = default(PS3TMAPI.LWConditionVarInfo);
        lwConditonVarInfo.ID = lwConditionVarInfoPriv.Id;
        lwConditonVarInfo.Attribute = lwConditionVarInfoPriv.Attribute;
        lwConditonVarInfo.LWMutexID = lwConditionVarInfoPriv.LwMutexId;
        lwConditonVarInfo.NumWaitAllThreads = lwConditionVarInfoPriv.NumWaitAllThreads;
        lwConditonVarInfo.WaitingThreads = new ulong[lwConditionVarInfoPriv.NumWaitingThreads];
        int num = 0;
        while ((long)num < (long)((ulong)lwConditionVarInfoPriv.NumWaitingThreads))
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref lwConditonVarInfo.WaitingThreads[num]);
            num++;
        }
        return snresult;
    }

    // Token: 0x06000138 RID: 312
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetReadWriteLockList")]
    private static extern PS3TMAPI.SNRESULT GetReadWriteLockListX86(int target, uint processId, ref uint numRWLocks, uint[] rwLockList);

    // Token: 0x06000139 RID: 313
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetReadWriteLockList")]
    private static extern PS3TMAPI.SNRESULT GetReadWriteLockListX64(int target, uint processId, ref uint numRWLocks, uint[] rwLockList);

    // Token: 0x0600013A RID: 314 RVA: 0x00004C08 File Offset: 0x00002E08
    public static PS3TMAPI.SNRESULT GetReadWriteLockList(int target, uint processID, out uint[] rwLockList)
    {
        rwLockList = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetReadWriteLockListX86(target, processID, ref num, null) : PS3TMAPI.GetReadWriteLockListX64(target, processID, ref num, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        rwLockList = new uint[num];
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.GetReadWriteLockListX86(target, processID, ref num, rwLockList) : PS3TMAPI.GetReadWriteLockListX64(target, processID, ref num, rwLockList);
    }

    // Token: 0x0600013B RID: 315
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetReadWriteLockInfo")]
    private static extern PS3TMAPI.SNRESULT GetReadWriteLockInfoX86(int target, uint processId, uint rwLockId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x0600013C RID: 316
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetReadWriteLockInfo")]
    private static extern PS3TMAPI.SNRESULT GetReadWriteLockInfoX64(int target, uint processId, uint rwLockId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x0600013D RID: 317 RVA: 0x00004C6C File Offset: 0x00002E6C
    public static PS3TMAPI.SNRESULT GetReadWriteLockInfo(int target, uint processID, uint rwLockID, out PS3TMAPI.RWLockInfo rwLockInfo)
    {
        rwLockInfo = default(PS3TMAPI.RWLockInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetReadWriteLockInfoX86(target, processID, rwLockID, ref cb, IntPtr.Zero) : PS3TMAPI.GetReadWriteLockInfoX64(target, processID, rwLockID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetReadWriteLockInfoX86(target, processID, rwLockID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetReadWriteLockInfoX64(target, processID, rwLockID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.RwLockInfoPriv rwLockInfoPriv = default(PS3TMAPI.RwLockInfoPriv);
        IntPtr unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.RwLockInfoPriv>(scopedGlobalHeapPtr.Get(), ref rwLockInfoPriv);
        rwLockInfo.ID = rwLockInfoPriv.Id;
        rwLockInfo.Attribute = rwLockInfoPriv.Attribute;
        rwLockInfo.NumWaitingReadThreads = rwLockInfoPriv.NumWaitingReadThreads;
        rwLockInfo.NumWaitAllReadThreads = rwLockInfoPriv.NumWaitAllReadThreads;
        rwLockInfo.NumWaitingWriteThreads = rwLockInfoPriv.NumWaitingWriteThreads;
        rwLockInfo.NumWaitAllWriteThreads = rwLockInfoPriv.NumWaitAllWriteThreads;
        uint num = rwLockInfo.NumWaitingReadThreads + rwLockInfo.NumWaitingWriteThreads;
        rwLockInfo.WaitingThreads = new ulong[num];
        int num2 = 0;
        while ((long)num2 < (long)((ulong)num))
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref rwLockInfo.WaitingThreads[num2]);
            num2++;
        }
        return snresult;
    }

    // Token: 0x0600013E RID: 318
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSemaphoreList")]
    private static extern PS3TMAPI.SNRESULT GetSemaphoreListX86(int target, uint processId, ref uint numSemaphores, uint[] semaphoreList);

    // Token: 0x0600013F RID: 319
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSemaphoreList")]
    private static extern PS3TMAPI.SNRESULT GetSemaphoreListX64(int target, uint processId, ref uint numSemaphores, uint[] semaphoreList);

    // Token: 0x06000140 RID: 320 RVA: 0x00004DA0 File Offset: 0x00002FA0
    public static PS3TMAPI.SNRESULT GetSemaphoreList(int target, uint processID, out uint[] semaphoreIDs)
    {
        semaphoreIDs = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetSemaphoreListX86(target, processID, ref num, null) : PS3TMAPI.GetSemaphoreListX64(target, processID, ref num, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        semaphoreIDs = new uint[num];
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.GetSemaphoreListX86(target, processID, ref num, semaphoreIDs) : PS3TMAPI.GetSemaphoreListX64(target, processID, ref num, semaphoreIDs);
    }

    // Token: 0x06000141 RID: 321
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSemaphoreInfo")]
    private static extern PS3TMAPI.SNRESULT GetSemaphoreInfoX86(int target, uint processId, uint semaphoreId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x06000142 RID: 322
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetSemaphoreInfo")]
    private static extern PS3TMAPI.SNRESULT GetSemaphoreInfoX64(int target, uint processId, uint semaphoreId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x06000143 RID: 323 RVA: 0x00004E04 File Offset: 0x00003004
    public static PS3TMAPI.SNRESULT GetSemaphoreInfo(int target, uint processID, uint semaphoreID, out PS3TMAPI.SemaphoreInfo semaphoreInfo)
    {
        semaphoreInfo = default(PS3TMAPI.SemaphoreInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetSemaphoreInfoX86(target, processID, semaphoreID, ref cb, IntPtr.Zero) : PS3TMAPI.GetSemaphoreInfoX64(target, processID, semaphoreID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetSemaphoreInfoX86(target, processID, semaphoreID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetSemaphoreInfoX64(target, processID, semaphoreID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.SemaphoreInfoPriv semaphoreInfoPriv = default(PS3TMAPI.SemaphoreInfoPriv);
        IntPtr unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.SemaphoreInfoPriv>(scopedGlobalHeapPtr.Get(), ref semaphoreInfoPriv);
        semaphoreInfo.ID = semaphoreInfoPriv.Id;
        semaphoreInfo.Attribute = semaphoreInfoPriv.Attribute;
        semaphoreInfo.MaxValue = semaphoreInfoPriv.MaxValue;
        semaphoreInfo.CurrentValue = semaphoreInfoPriv.CurrentValue;
        semaphoreInfo.NumWaitAllThreads = semaphoreInfoPriv.NumWaitAllThreads;
        semaphoreInfo.WaitingThreads = new ulong[semaphoreInfoPriv.NumWaitingThreads];
        int num = 0;
        while ((long)num < (long)((ulong)semaphoreInfoPriv.NumWaitingThreads))
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref semaphoreInfo.WaitingThreads[num]);
            num++;
        }
        return snresult;
    }

    // Token: 0x06000144 RID: 324
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetEventQueueList")]
    private static extern PS3TMAPI.SNRESULT GetEventQueueListX86(int target, uint processId, ref uint numEventQueues, uint[] eventQueueList);

    // Token: 0x06000145 RID: 325
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetEventQueueList")]
    private static extern PS3TMAPI.SNRESULT GetEventQueueListX64(int target, uint processId, ref uint numEventQueues, uint[] eventQueueList);

    // Token: 0x06000146 RID: 326 RVA: 0x00004F28 File Offset: 0x00003128
    public static PS3TMAPI.SNRESULT GetEventQueueList(int target, uint processID, out uint[] eventQueueIDs)
    {
        eventQueueIDs = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetEventQueueListX86(target, processID, ref num, null) : PS3TMAPI.GetEventQueueListX64(target, processID, ref num, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        eventQueueIDs = new uint[num];
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.GetEventQueueListX86(target, processID, ref num, eventQueueIDs) : PS3TMAPI.GetEventQueueListX64(target, processID, ref num, eventQueueIDs);
    }

    // Token: 0x06000147 RID: 327
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetEventQueueInfo")]
    private static extern PS3TMAPI.SNRESULT GetEventQueueInfoX86(int target, uint processId, uint eventQueueId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x06000148 RID: 328
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetEventQueueInfo")]
    private static extern PS3TMAPI.SNRESULT GetEventQueueInfoX64(int target, uint processId, uint eventQueueId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x06000149 RID: 329 RVA: 0x00004F8C File Offset: 0x0000318C
    public static PS3TMAPI.SNRESULT GetEventQueueInfo(int target, uint processID, uint eventQueueID, out PS3TMAPI.EventQueueInfo eventQueueInfo)
    {
        eventQueueInfo = default(PS3TMAPI.EventQueueInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetEventQueueInfoX86(target, processID, eventQueueID, ref cb, IntPtr.Zero) : PS3TMAPI.GetEventQueueInfoX64(target, processID, eventQueueID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetEventQueueInfoX86(target, processID, eventQueueID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetEventQueueInfoX64(target, processID, eventQueueID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.EventQueueInfoPriv eventQueueInfoPriv = (PS3TMAPI.EventQueueInfoPriv)Marshal.PtrToStructure(scopedGlobalHeapPtr.Get(), typeof(PS3TMAPI.EventQueueInfoPriv));
        eventQueueInfo.ID = eventQueueInfoPriv.Id;
        eventQueueInfo.Attribute = eventQueueInfoPriv.Attribute;
        eventQueueInfo.Key = eventQueueInfoPriv.Key;
        eventQueueInfo.Size = eventQueueInfoPriv.Size;
        eventQueueInfo.NumWaitAllThreads = eventQueueInfoPriv.NumWaitAllThreads;
        eventQueueInfo.NumReadableAllEvQueue = eventQueueInfoPriv.NumReadableAllEvQueue;
        eventQueueInfo.WaitingThreadIDs = new ulong[eventQueueInfoPriv.NumWaitingThreads];
        IntPtr unmanagedBuf = eventQueueInfoPriv.WaitingThreadIds;
        int num = 0;
        while ((long)num < (long)((ulong)eventQueueInfoPriv.NumWaitingThreads))
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref eventQueueInfo.WaitingThreadIDs[num]);
            num++;
        }
        eventQueueInfo.QueueEntries = new PS3TMAPI.SystemEvent[eventQueueInfoPriv.NumReadableEvQueue];
        IntPtr unmanagedBuf2 = eventQueueInfoPriv.QueueEntries;
        int num2 = 0;
        while ((long)num2 < (long)((ulong)eventQueueInfoPriv.NumReadableEvQueue))
        {
            unmanagedBuf2 = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.SystemEvent>(unmanagedBuf2, ref eventQueueInfo.QueueEntries[num2]);
            num2++;
        }
        return snresult;
    }

    // Token: 0x0600014A RID: 330
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetEventFlagList")]
    private static extern PS3TMAPI.SNRESULT GetEventFlagListX86(int target, uint processId, ref uint numEventFlags, uint[] eventFlagList);

    // Token: 0x0600014B RID: 331
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetEventFlagList")]
    private static extern PS3TMAPI.SNRESULT GetEventFlagListX64(int target, uint processId, ref uint numEventFlags, uint[] eventFlagList);

    // Token: 0x0600014C RID: 332 RVA: 0x00005114 File Offset: 0x00003314
    public static PS3TMAPI.SNRESULT GetEventFlagList(int target, uint processID, out uint[] eventFlagIDs)
    {
        eventFlagIDs = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetEventFlagListX86(target, processID, ref num, null) : PS3TMAPI.GetEventFlagListX64(target, processID, ref num, null);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        eventFlagIDs = new uint[num];
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.GetEventFlagListX86(target, processID, ref num, eventFlagIDs) : PS3TMAPI.GetEventFlagListX64(target, processID, ref num, eventFlagIDs);
    }

    // Token: 0x0600014D RID: 333
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetEventFlagInfo")]
    private static extern PS3TMAPI.SNRESULT GetEventFlagInfoX86(int target, uint processId, uint eventFlagId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x0600014E RID: 334
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetEventFlagInfo")]
    private static extern PS3TMAPI.SNRESULT GetEventFlagInfoX64(int target, uint processId, uint eventFlagId, ref uint bufferSize, IntPtr buffer);

    // Token: 0x0600014F RID: 335 RVA: 0x00005178 File Offset: 0x00003378
    public static PS3TMAPI.SNRESULT GetEventFlagInfo(int target, uint processID, uint eventFlagID, out PS3TMAPI.EventFlagInfo eventFlagInfo)
    {
        eventFlagInfo = default(PS3TMAPI.EventFlagInfo);
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetEventFlagInfoX86(target, processID, eventFlagID, ref cb, IntPtr.Zero) : PS3TMAPI.GetEventFlagInfoX64(target, processID, eventFlagID, ref cb, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetEventFlagInfoX86(target, processID, eventFlagID, ref cb, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetEventFlagInfoX64(target, processID, eventFlagID, ref cb, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        IntPtr unmanagedBuf = scopedGlobalHeapPtr.Get();
        unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref eventFlagInfo.ID);
        unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.EventFlagAttr>(unmanagedBuf, ref eventFlagInfo.Attribute);
        unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref eventFlagInfo.BitPattern);
        uint num = 0U;
        unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref num);
        unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref eventFlagInfo.NumWaitAllThreads);
        eventFlagInfo.WaitingThreads = new PS3TMAPI.EventFlagWaitThread[num];
        int num2 = 0;
        while ((long)num2 < (long)((ulong)num))
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.EventFlagWaitThread>(unmanagedBuf, ref eventFlagInfo.WaitingThreads[num2]);
            num2++;
        }
        return snresult;
    }

    // Token: 0x06000150 RID: 336
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3PickTarget")]
    private static extern PS3TMAPI.SNRESULT PickTargetX86(IntPtr hWndOwner, out int target);

    // Token: 0x06000151 RID: 337
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3PickTarget")]
    private static extern PS3TMAPI.SNRESULT PickTargetX64(IntPtr hWndOwner, out int target);

    // Token: 0x06000152 RID: 338 RVA: 0x0000527D File Offset: 0x0000347D
    public static PS3TMAPI.SNRESULT PickTarget(IntPtr hWndOwner, out int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.PickTargetX64(hWndOwner, out target);
        }
        return PS3TMAPI.PickTargetX86(hWndOwner, out target);
    }

    // Token: 0x06000153 RID: 339
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnableAutoStatusUpdate")]
    private static extern PS3TMAPI.SNRESULT EnableAutoStatusUpdateX86(int target, uint enabled, out uint previousState);

    // Token: 0x06000154 RID: 340
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnableAutoStatusUpdate")]
    private static extern PS3TMAPI.SNRESULT EnableAutoStatusUpdateX64(int target, uint enabled, out uint previousState);

    // Token: 0x06000155 RID: 341 RVA: 0x00005298 File Offset: 0x00003498
    public static PS3TMAPI.SNRESULT EnableAutoStatusUpdate(int target, bool bEnabled, out bool bPreviousState)
    {
        uint enabled = bEnabled ? 1U : 0U;
        uint num;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.EnableAutoStatusUpdateX86(target, enabled, out num) : PS3TMAPI.EnableAutoStatusUpdateX64(target, enabled, out num);
        bPreviousState = (num != 0U);
        return result;
    }

    // Token: 0x06000156 RID: 342
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetPowerStatus")]
    private static extern PS3TMAPI.SNRESULT GetPowerStatusX86(int target, out PS3TMAPI.PowerStatus status);

    // Token: 0x06000157 RID: 343
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetPowerStatus")]
    private static extern PS3TMAPI.SNRESULT GetPowerStatusX64(int target, out PS3TMAPI.PowerStatus status);

    // Token: 0x06000158 RID: 344 RVA: 0x000052D3 File Offset: 0x000034D3
    public static PS3TMAPI.SNRESULT GetPowerStatus(int target, out PS3TMAPI.PowerStatus status)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetPowerStatusX64(target, out status);
        }
        return PS3TMAPI.GetPowerStatusX86(target, out status);
    }

    // Token: 0x06000159 RID: 345
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3PowerOn")]
    private static extern PS3TMAPI.SNRESULT PowerOnX86(int target);

    // Token: 0x0600015A RID: 346
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3PowerOn")]
    private static extern PS3TMAPI.SNRESULT PowerOnX64(int target);

    // Token: 0x0600015B RID: 347 RVA: 0x000052EB File Offset: 0x000034EB
    public static PS3TMAPI.SNRESULT PowerOn(int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.PowerOnX64(target);
        }
        return PS3TMAPI.PowerOnX86(target);
    }

    // Token: 0x0600015C RID: 348
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3PowerOff")]
    private static extern PS3TMAPI.SNRESULT PowerOffX86(int target, uint force);

    // Token: 0x0600015D RID: 349
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3PowerOff")]
    private static extern PS3TMAPI.SNRESULT PowerOffX64(int target, uint force);

    // Token: 0x0600015E RID: 350 RVA: 0x00005304 File Offset: 0x00003504
    public static PS3TMAPI.SNRESULT PowerOff(int target, bool bForce)
    {
        uint force = bForce ? 1U : 0U;
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.PowerOffX64(target, force);
        }
        return PS3TMAPI.PowerOffX86(target, force);
    }

    // Token: 0x0600015F RID: 351
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetUserMemoryStats")]
    private static extern PS3TMAPI.SNRESULT GetUserMemoryStatsX86(int target, uint processId, out PS3TMAPI.UserMemoryStats memoryStats);

    // Token: 0x06000160 RID: 352
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetUserMemoryStats")]
    private static extern PS3TMAPI.SNRESULT GetUserMemoryStatsX64(int target, uint processId, out PS3TMAPI.UserMemoryStats memoryStats);

    // Token: 0x06000161 RID: 353 RVA: 0x0000532F File Offset: 0x0000352F
    public static PS3TMAPI.SNRESULT GetUserMemoryStats(int target, uint processID, out PS3TMAPI.UserMemoryStats memoryStats)
    {
        memoryStats = default(PS3TMAPI.UserMemoryStats);
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetUserMemoryStatsX64(target, processID, out memoryStats);
        }
        return PS3TMAPI.GetUserMemoryStatsX86(target, processID, out memoryStats);
    }

    // Token: 0x06000162 RID: 354
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDefaultLoadPriority")]
    private static extern PS3TMAPI.SNRESULT SetDefaultLoadPriorityX86(int target, uint priority);

    // Token: 0x06000163 RID: 355
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDefaultLoadPriority")]
    private static extern PS3TMAPI.SNRESULT SetDefaultLoadPriorityX64(int target, uint priority);

    // Token: 0x06000164 RID: 356 RVA: 0x00005350 File Offset: 0x00003550
    public static PS3TMAPI.SNRESULT SetDefaultLoadPriority(int target, uint priority)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetDefaultLoadPriorityX64(target, priority);
        }
        return PS3TMAPI.SetDefaultLoadPriorityX86(target, priority);
    }

    // Token: 0x06000165 RID: 357
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDefaultLoadPriority")]
    private static extern PS3TMAPI.SNRESULT GetDefaultLoadPriorityX86(int target, out uint priority);

    // Token: 0x06000166 RID: 358
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDefaultLoadPriority")]
    private static extern PS3TMAPI.SNRESULT GetDefaultLoadPriorityX64(int target, out uint priority);

    // Token: 0x06000167 RID: 359 RVA: 0x00005368 File Offset: 0x00003568
    public static PS3TMAPI.SNRESULT GetDefaultLoadPriority(int target, out uint priority)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetDefaultLoadPriorityX64(target, out priority);
        }
        return PS3TMAPI.GetDefaultLoadPriorityX86(target, out priority);
    }

    // Token: 0x06000168 RID: 360
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetGamePortIPAddrData")]
    private static extern PS3TMAPI.SNRESULT GetGamePortIPAddrDataX86(int target, string deviceName, out PS3TMAPI.GamePortIPAddressData ipAddressData);

    // Token: 0x06000169 RID: 361
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetGamePortIPAddrData")]
    private static extern PS3TMAPI.SNRESULT GetGamePortIPAddrDataX64(int target, string deviceName, out PS3TMAPI.GamePortIPAddressData ipAddressData);

    // Token: 0x0600016A RID: 362 RVA: 0x00005380 File Offset: 0x00003580
    public static PS3TMAPI.SNRESULT GetGamePortIPAddrData(int target, string deviceName, out PS3TMAPI.GamePortIPAddressData ipAddressData)
    {
        ipAddressData = default(PS3TMAPI.GamePortIPAddressData);
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetGamePortIPAddrDataX64(target, deviceName, out ipAddressData);
        }
        return PS3TMAPI.GetGamePortIPAddrDataX86(target, deviceName, out ipAddressData);
    }

    // Token: 0x0600016B RID: 363
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetGamePortDebugIPAddrData")]
    private static extern PS3TMAPI.SNRESULT GetGamePortDebugIPAddrDataX86(int target, string deviceName, out PS3TMAPI.GamePortIPAddressData data);

    // Token: 0x0600016C RID: 364
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetGamePortDebugIPAddrData")]
    private static extern PS3TMAPI.SNRESULT GetGamePortDebugIPAddrDataX64(int target, string deviceName, out PS3TMAPI.GamePortIPAddressData data);

    // Token: 0x0600016D RID: 365 RVA: 0x000053A1 File Offset: 0x000035A1
    public static PS3TMAPI.SNRESULT GetGamePortDebugIPAddrData(int target, string deviceName, out PS3TMAPI.GamePortIPAddressData ipAddressData)
    {
        ipAddressData = default(PS3TMAPI.GamePortIPAddressData);
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetGamePortDebugIPAddrDataX64(target, deviceName, out ipAddressData);
        }
        return PS3TMAPI.GetGamePortDebugIPAddrDataX86(target, deviceName, out ipAddressData);
    }

    // Token: 0x0600016E RID: 366
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDABR")]
    private static extern PS3TMAPI.SNRESULT SetDABRX86(int target, uint processId, ulong address);

    // Token: 0x0600016F RID: 367
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDABR")]
    private static extern PS3TMAPI.SNRESULT SetDABRX64(int target, uint processId, ulong address);

    // Token: 0x06000170 RID: 368 RVA: 0x000053C2 File Offset: 0x000035C2
    public static PS3TMAPI.SNRESULT SetDABR(int target, uint processID, ulong address)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetDABRX64(target, processID, address);
        }
        return PS3TMAPI.SetDABRX86(target, processID, address);
    }

    // Token: 0x06000171 RID: 369
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDABR")]
    private static extern PS3TMAPI.SNRESULT GetDABRX86(int target, uint processId, out ulong address);

    // Token: 0x06000172 RID: 370
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDABR")]
    private static extern PS3TMAPI.SNRESULT GetDABRX64(int target, uint processId, out ulong address);

    // Token: 0x06000173 RID: 371 RVA: 0x000053DC File Offset: 0x000035DC
    public static PS3TMAPI.SNRESULT GetDABR(int target, uint processID, out ulong address)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.GetDABRX64(target, processID, out address);
        }
        return PS3TMAPI.GetDABRX86(target, processID, out address);
    }

    // Token: 0x06000174 RID: 372
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetRSXProfilingFlags")]
    private static extern PS3TMAPI.SNRESULT SetRSXProfilingFlagsX86(int target, ulong rsxFlags);

    // Token: 0x06000175 RID: 373
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetRSXProfilingFlags")]
    private static extern PS3TMAPI.SNRESULT SetRSXProfilingFlagsX64(int target, ulong rsxFlags);

    // Token: 0x06000176 RID: 374 RVA: 0x000053F6 File Offset: 0x000035F6
    public static PS3TMAPI.SNRESULT SetRSXProfilingFlags(int target, PS3TMAPI.RSXProfilingFlag rsxFlags)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetRSXProfilingFlagsX64(target, (ulong)rsxFlags);
        }
        return PS3TMAPI.SetRSXProfilingFlagsX86(target, (ulong)rsxFlags);
    }

    // Token: 0x06000177 RID: 375
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetRSXProfilingFlags")]
    private static extern PS3TMAPI.SNRESULT GetRSXProfilingFlagsX86(int target, out ulong rsxFlags);

    // Token: 0x06000178 RID: 376
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetRSXProfilingFlags")]
    private static extern PS3TMAPI.SNRESULT GetRSXProfilingFlagsX64(int target, out ulong rsxFlags);

    // Token: 0x06000179 RID: 377 RVA: 0x00005410 File Offset: 0x00003610
    public static PS3TMAPI.SNRESULT GetRSXProfilingFlags(int target, out PS3TMAPI.RSXProfilingFlag rsxFlags)
    {
        ulong num;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetRSXProfilingFlagsX86(target, out num) : PS3TMAPI.GetRSXProfilingFlagsX64(target, out num);
        rsxFlags = (PS3TMAPI.RSXProfilingFlag)num;
        return result;
    }

    // Token: 0x0600017A RID: 378
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetCustomParamSFOMappingDirectory")]
    private static extern PS3TMAPI.SNRESULT SetCustomParamSFOMappingDirectoryX86(int target, IntPtr paramSfoDir);

    // Token: 0x0600017B RID: 379
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetCustomParamSFOMappingDirectory")]
    private static extern PS3TMAPI.SNRESULT SetCustomParamSFOMappingDirectoryX64(int target, IntPtr paramSfoDir);

    // Token: 0x0600017C RID: 380 RVA: 0x0000543C File Offset: 0x0000363C
    public static PS3TMAPI.SNRESULT SetCustomParamSFOMappingDirectory(int target, string paramSFODir)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(paramSFODir));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetCustomParamSFOMappingDirectoryX64(target, scopedGlobalHeapPtr.Get());
        }
        return PS3TMAPI.SetCustomParamSFOMappingDirectoryX86(target, scopedGlobalHeapPtr.Get());
    }

    // Token: 0x0600017D RID: 381
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnableXMBSettings")]
    private static extern PS3TMAPI.SNRESULT EnableXMBSettingsX86(int target, int enable);

    // Token: 0x0600017E RID: 382
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnableXMBSettings")]
    private static extern PS3TMAPI.SNRESULT EnableXMBSettingsX64(int target, int enable);

    // Token: 0x0600017F RID: 383 RVA: 0x00005478 File Offset: 0x00003678
    public static PS3TMAPI.SNRESULT EnableXMBSettings(int target, bool bEnable)
    {
        int enable = bEnable ? 1 : 0;
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.EnableXMBSettingsX64(target, enable);
        }
        return PS3TMAPI.EnableXMBSettingsX86(target, enable);
    }

    // Token: 0x06000180 RID: 384
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetXMBSettings")]
    private static extern PS3TMAPI.SNRESULT GetXMBSettingsX86(int target, IntPtr buffer, ref uint bufferSize, bool bUpdateCache);

    // Token: 0x06000181 RID: 385
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetXMBSettings")]
    private static extern PS3TMAPI.SNRESULT GetXMBSettingsX64(int target, IntPtr buffer, ref uint bufferSize, bool bUpdateCache);

    // Token: 0x06000182 RID: 386 RVA: 0x000054A4 File Offset: 0x000036A4
    public static PS3TMAPI.SNRESULT GetXMBSettings(int target, out string xmbSettings, bool bUpdateCache)
    {
        xmbSettings = null;
        uint cb = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetXMBSettingsX86(target, IntPtr.Zero, ref cb, bUpdateCache) : PS3TMAPI.GetXMBSettingsX64(target, IntPtr.Zero, ref cb, bUpdateCache);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetXMBSettingsX86(target, scopedGlobalHeapPtr.Get(), ref cb, bUpdateCache) : PS3TMAPI.GetXMBSettingsX64(target, scopedGlobalHeapPtr.Get(), ref cb, bUpdateCache));
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            xmbSettings = Marshal.PtrToStringAnsi(scopedGlobalHeapPtr.Get());
        }
        return snresult;
    }

    // Token: 0x06000183 RID: 387
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetXMBSettings")]
    private static extern PS3TMAPI.SNRESULT SetXMBSettingsX86(int target, string xmbSettings, bool bUpdateCache);

    // Token: 0x06000184 RID: 388
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetXMBSettings")]
    private static extern PS3TMAPI.SNRESULT SetXMBSettingsX64(int target, string xmbSettings, bool bUpdateCache);

    // Token: 0x06000185 RID: 389 RVA: 0x00005530 File Offset: 0x00003730
    public static PS3TMAPI.SNRESULT SetXMBSettings(int target, string xmbSettings, bool bUpdateCache)
    {
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.SetXMBSettingsX86(target, xmbSettings, bUpdateCache) : PS3TMAPI.SetXMBSettingsX64(target, xmbSettings, bUpdateCache);
    }

    // Token: 0x06000186 RID: 390
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3FootswitchControl")]
    private static extern PS3TMAPI.SNRESULT FootswitchControlX86(int target, uint enabled);

    // Token: 0x06000187 RID: 391
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3FootswitchControl")]
    private static extern PS3TMAPI.SNRESULT FootswitchControlX64(int target, uint enabled);

    // Token: 0x06000188 RID: 392 RVA: 0x00005558 File Offset: 0x00003758
    public static PS3TMAPI.SNRESULT FootswitchControl(int target, bool bEnabled)
    {
        uint enabled = bEnabled ? 1U : 0U;
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.FootswitchControlX64(target, enabled);
        }
        return PS3TMAPI.FootswitchControlX86(target, enabled);
    }

    // Token: 0x06000189 RID: 393
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3TriggerCoreDump")]
    private static extern PS3TMAPI.SNRESULT TriggerCoreDumpX86(int target, uint processId, ulong userData1, ulong userData2, ulong userData3);

    // Token: 0x0600018A RID: 394
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3TriggerCoreDump")]
    private static extern PS3TMAPI.SNRESULT TriggerCoreDumpX64(int target, uint processId, ulong userData1, ulong userData2, ulong userData3);

    // Token: 0x0600018B RID: 395 RVA: 0x00005583 File Offset: 0x00003783
    public static PS3TMAPI.SNRESULT TriggerCoreDump(int target, uint processID, ulong userData1, ulong userData2, ulong userData3)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.TriggerCoreDumpX64(target, processID, userData1, userData2, userData3);
        }
        return PS3TMAPI.TriggerCoreDumpX86(target, processID, userData1, userData2, userData3);
    }

    // Token: 0x0600018C RID: 396
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetCoreDumpFlags")]
    private static extern PS3TMAPI.SNRESULT GetCoreDumpFlagsX86(int target, out ulong flags);

    // Token: 0x0600018D RID: 397
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetCoreDumpFlags")]
    private static extern PS3TMAPI.SNRESULT GetCoreDumpFlagsX64(int target, out ulong flags);

    // Token: 0x0600018E RID: 398 RVA: 0x000055A4 File Offset: 0x000037A4
    public static PS3TMAPI.SNRESULT GetCoreDumpFlags(int target, out PS3TMAPI.CoreDumpFlag flags)
    {
        ulong num;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetCoreDumpFlagsX86(target, out num) : PS3TMAPI.GetCoreDumpFlagsX64(target, out num);
        flags = (PS3TMAPI.CoreDumpFlag)num;
        return result;
    }

    // Token: 0x0600018F RID: 399
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetCoreDumpFlags")]
    private static extern PS3TMAPI.SNRESULT SetCoreDumpFlagsX86(int tarSet, ulong flags);

    // Token: 0x06000190 RID: 400
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetCoreDumpFlags")]
    private static extern PS3TMAPI.SNRESULT SetCoreDumpFlagsX64(int tarSet, ulong flags);

    // Token: 0x06000191 RID: 401 RVA: 0x000055CF File Offset: 0x000037CF
    public static PS3TMAPI.SNRESULT SetCoreDumpFlags(int tarSet, PS3TMAPI.CoreDumpFlag flags)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetCoreDumpFlagsX64(tarSet, (ulong)flags);
        }
        return PS3TMAPI.SetCoreDumpFlagsX86(tarSet, (ulong)flags);
    }

    // Token: 0x06000192 RID: 402
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessAttach")]
    private static extern PS3TMAPI.SNRESULT ProcessAttachX86(int target, uint unitId, uint processId);

    // Token: 0x06000193 RID: 403
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessAttach")]
    private static extern PS3TMAPI.SNRESULT ProcessAttachX64(int target, uint unitId, uint processId);

    // Token: 0x06000194 RID: 404 RVA: 0x000055E7 File Offset: 0x000037E7
    public static PS3TMAPI.SNRESULT ProcessAttach(int target, PS3TMAPI.UnitType unit, uint processID)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ProcessAttachX64(target, (uint)unit, processID);
        }
        return PS3TMAPI.ProcessAttachX86(target, (uint)unit, processID);
    }

    // Token: 0x06000195 RID: 405
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3FlashTarget")]
    private static extern PS3TMAPI.SNRESULT FlashTargetX86(int target, IntPtr updaterToolPath, IntPtr flashImagePath);

    // Token: 0x06000196 RID: 406
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3FlashTarget")]
    private static extern PS3TMAPI.SNRESULT FlashTargetX64(int target, IntPtr updaterToolPath, IntPtr flashImagePath);

    // Token: 0x06000197 RID: 407 RVA: 0x00005604 File Offset: 0x00003804
    public static PS3TMAPI.SNRESULT FlashTarget(int target, string updaterToolPath, string flashImagePath)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(updaterToolPath));
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(flashImagePath));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.FlashTargetX64(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get());
        }
        return PS3TMAPI.FlashTargetX86(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get());
    }

    // Token: 0x06000198 RID: 408
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMacAddress")]
    private static extern PS3TMAPI.SNRESULT GetMacAddressX86(int target, out IntPtr stringPtr);

    // Token: 0x06000199 RID: 409
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMacAddress")]
    private static extern PS3TMAPI.SNRESULT GetMacAddressX64(int target, out IntPtr stringPtr);

    // Token: 0x0600019A RID: 410 RVA: 0x00005658 File Offset: 0x00003858
    public static PS3TMAPI.SNRESULT GetMACAddress(int target, out string macAddress)
    {
        IntPtr ptr;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetMacAddressX86(target, out ptr) : PS3TMAPI.GetMacAddressX64(target, out ptr);
        macAddress = Marshal.PtrToStringAnsi(ptr);
        return result;
    }

    // Token: 0x0600019B RID: 411
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessScatteredSetMemory")]
    private static extern PS3TMAPI.SNRESULT ProcessScatteredSetMemoryX86(int target, uint processId, uint numWrites, uint writeSize, IntPtr writeData, out uint errorCode, out uint failedAddress);

    // Token: 0x0600019C RID: 412
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessScatteredSetMemory")]
    private static extern PS3TMAPI.SNRESULT ProcessScatteredSetMemoryX64(int target, uint processId, uint numWrites, uint writeSize, IntPtr writeData, out uint errorCode, out uint failedAddress);

    // Token: 0x0600019D RID: 413 RVA: 0x00005688 File Offset: 0x00003888
    public static PS3TMAPI.SNRESULT ProcessScatteredSetMemory(int target, uint processID, PS3TMAPI.ScatteredWrite[] writeData, out uint errorCode, out uint failedAddress)
    {
        errorCode = 0U;
        failedAddress = 0U;
        if (writeData == null || writeData.Length == 0)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        int num = writeData.Length;
        if (writeData[0].Data == null)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        int num2 = writeData[0].Data.Length;
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal(num * (Marshal.SizeOf(writeData[0].Address) + num2)));
        IntPtr intPtr = scopedGlobalHeapPtr.Get();
        for (int i = 0; i < num; i++)
        {
            intPtr = PS3TMAPI.WriteDataToUnmanagedIncPtr<uint>(writeData[i].Address, intPtr);
            if (writeData[i].Data == null || writeData[i].Data.Length != num2)
            {
                return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
            }
            Marshal.Copy(writeData[i].Data, 0, intPtr, writeData[i].Data.Length);
            intPtr = new IntPtr(intPtr.ToInt64() + (long)writeData[i].Data.Length);
        }
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ProcessScatteredSetMemoryX64(target, processID, (uint)num, (uint)num2, scopedGlobalHeapPtr.Get(), out errorCode, out failedAddress);
        }
        return PS3TMAPI.ProcessScatteredSetMemoryX86(target, processID, (uint)num, (uint)num2, scopedGlobalHeapPtr.Get(), out errorCode, out failedAddress);
    }

    // Token: 0x0600019E RID: 414
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMATRanges")]
    private static extern PS3TMAPI.SNRESULT GetMATRangesX86(int target, uint processId, ref uint rangeCount, IntPtr matRanges);

    // Token: 0x0600019F RID: 415
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMATRanges")]
    private static extern PS3TMAPI.SNRESULT GetMATRangesX64(int target, uint processId, ref uint rangeCount, IntPtr matRanges);

    // Token: 0x060001A0 RID: 416 RVA: 0x000057B4 File Offset: 0x000039B4
    public static PS3TMAPI.SNRESULT GetMATRanges(int target, uint processID, out PS3TMAPI.MATRange[] matRanges)
    {
        matRanges = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetMATRangesX86(target, processID, ref num, IntPtr.Zero) : PS3TMAPI.GetMATRangesX64(target, processID, ref num, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        if (num == 0U)
        {
            matRanges = new PS3TMAPI.MATRange[0];
            return PS3TMAPI.SNRESULT.SN_S_OK;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)((long)(2 * Marshal.SizeOf(typeof(uint))) * (long)((ulong)num))));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetMATRangesX86(target, processID, ref num, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetMATRangesX64(target, processID, ref num, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        IntPtr unmanagedBuf = scopedGlobalHeapPtr.Get();
        matRanges = new PS3TMAPI.MATRange[num];
        for (uint num2 = 0U; num2 < num; num2 += 1U)
        {
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref matRanges[(int)((UIntPtr)num2)].StartAddress);
            unmanagedBuf = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref matRanges[(int)((UIntPtr)num2)].Size);
        }
        return snresult;
    }

    // Token: 0x060001A1 RID: 417
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMATConditions")]
    private static extern PS3TMAPI.SNRESULT GetMATConditionsX86(int target, uint processId, ref uint rangeCount, IntPtr ranges, ref uint bufSize, IntPtr outputBuf);

    // Token: 0x060001A2 RID: 418
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetMATConditions")]
    private static extern PS3TMAPI.SNRESULT GetMATConditionsX64(int target, uint processId, ref uint rangeCount, IntPtr ranges, ref uint bufSize, IntPtr outputBuf);

    // Token: 0x060001A3 RID: 419 RVA: 0x000058A4 File Offset: 0x00003AA4
    public static PS3TMAPI.SNRESULT GetMATConditions(int target, uint processID, ref PS3TMAPI.MATRange[] matRanges)
    {
        if (matRanges == null || matRanges.Length < 1)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        uint num = (uint)matRanges.Length;
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)(8U * num)));
        IntPtr unmanagedBuf = scopedGlobalHeapPtr.Get();
        foreach (PS3TMAPI.MATRange matrange in matRanges)
        {
            unmanagedBuf = PS3TMAPI.WriteDataToUnmanagedIncPtr<uint>(matrange.StartAddress, unmanagedBuf);
            unmanagedBuf = PS3TMAPI.WriteDataToUnmanagedIncPtr<uint>(matrange.Size, unmanagedBuf);
        }
        uint num2 = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetMATConditionsX86(target, processID, ref num, scopedGlobalHeapPtr.Get(), ref num2, IntPtr.Zero) : PS3TMAPI.GetMATConditionsX64(target, processID, ref num, scopedGlobalHeapPtr.Get(), ref num2, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)num2));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetMATConditionsX86(target, processID, ref num, scopedGlobalHeapPtr.Get(), ref num2, scopedGlobalHeapPtr2.Get()) : PS3TMAPI.GetMATConditionsX64(target, processID, ref num, scopedGlobalHeapPtr.Get(), ref num2, scopedGlobalHeapPtr2.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        IntPtr unmanagedBuf2 = scopedGlobalHeapPtr2.Get();
        int num3 = 0;
        while ((long)num3 < (long)((ulong)num))
        {
            unmanagedBuf2 = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf2, ref matRanges[num3].StartAddress);
            unmanagedBuf2 = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf2, ref matRanges[num3].Size);
            uint num4 = matRanges[num3].Size / 4096U;
            matRanges[num3].PageConditions = new PS3TMAPI.MATCondition[num4];
            int num5 = 0;
            while ((long)num5 < (long)((ulong)num4))
            {
                byte b = 0;
                unmanagedBuf2 = PS3TMAPI.ReadDataFromUnmanagedIncPtr<byte>(unmanagedBuf2, ref b);
                matRanges[num3].PageConditions[num5] = (PS3TMAPI.MATCondition)b;
                num5++;
            }
            num2 -= 8U + num4;
            num3++;
        }
        if (num2 != 0U)
        {
            snresult = PS3TMAPI.SNRESULT.SN_E_ERROR;
        }
        return snresult;
    }

    // Token: 0x060001A4 RID: 420
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetMATConditions")]
    private static extern PS3TMAPI.SNRESULT SetMATConditionsX86(int target, uint processId, uint rangeCount, uint bufSize, IntPtr buffer);

    // Token: 0x060001A5 RID: 421
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetMATConditions")]
    private static extern PS3TMAPI.SNRESULT SetMATConditionsX64(int target, uint processId, uint rangeCount, uint bufSize, IntPtr buffer);

    // Token: 0x060001A6 RID: 422 RVA: 0x00005A7C File Offset: 0x00003C7C
    public static PS3TMAPI.SNRESULT SetMATConditions(int target, uint processID, PS3TMAPI.MATRange[] matRanges)
    {
        if (matRanges == null || matRanges.Length < 1)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        int num = matRanges.Length;
        int num2 = 0;
        foreach (PS3TMAPI.MATRange matrange in matRanges)
        {
            if (matrange.PageConditions == null)
            {
                return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
            }
            num2 += matrange.PageConditions.Length;
        }
        int cb = num2 + 2 * num * 4;
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal(cb));
        IntPtr unmanagedBuf = scopedGlobalHeapPtr.Get();
        foreach (PS3TMAPI.MATRange matrange2 in matRanges)
        {
            unmanagedBuf = PS3TMAPI.WriteDataToUnmanagedIncPtr<uint>(matrange2.StartAddress, unmanagedBuf);
            unmanagedBuf = PS3TMAPI.WriteDataToUnmanagedIncPtr<uint>(matrange2.Size, unmanagedBuf);
            foreach (byte storage in matrange2.PageConditions)
            {
                unmanagedBuf = PS3TMAPI.WriteDataToUnmanagedIncPtr<byte>(storage, unmanagedBuf);
            }
        }
        uint bufSize = 1U;
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetMATConditionsX64(target, processID, (uint)num, bufSize, scopedGlobalHeapPtr.Get());
        }
        return PS3TMAPI.SetMATConditionsX86(target, processID, (uint)num, bufSize, scopedGlobalHeapPtr.Get());
    }

    // Token: 0x060001A7 RID: 423
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SaveSettings")]
    private static extern PS3TMAPI.SNRESULT SaveSettingsX86();

    // Token: 0x060001A8 RID: 424
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SaveSettings")]
    private static extern PS3TMAPI.SNRESULT SaveSettingsX64();

    // Token: 0x060001A9 RID: 425 RVA: 0x00005BA0 File Offset: 0x00003DA0
    public static PS3TMAPI.SNRESULT SaveSettings()
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SaveSettingsX64();
        }
        return PS3TMAPI.SaveSettingsX86();
    }

    // Token: 0x060001AA RID: 426
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Exit")]
    private static extern PS3TMAPI.SNRESULT ExitX86();

    // Token: 0x060001AB RID: 427
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Exit")]
    private static extern PS3TMAPI.SNRESULT ExitX64();

    // Token: 0x060001AC RID: 428 RVA: 0x00005BB4 File Offset: 0x00003DB4
    public static PS3TMAPI.SNRESULT Exit()
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ExitX64();
        }
        return PS3TMAPI.ExitX86();
    }

    // Token: 0x060001AD RID: 429
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ExitEx")]
    private static extern PS3TMAPI.SNRESULT ExitExX86(uint millisecondTimeout);

    // Token: 0x060001AE RID: 430
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ExitEx")]
    private static extern PS3TMAPI.SNRESULT ExitExX64(uint millisecondTimeout);

    // Token: 0x060001AF RID: 431 RVA: 0x00005BC8 File Offset: 0x00003DC8
    public static PS3TMAPI.SNRESULT ExitEx(uint millisecondTimeout)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ExitExX64(millisecondTimeout);
        }
        return PS3TMAPI.ExitExX86(millisecondTimeout);
    }

    // Token: 0x060001B0 RID: 432
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterPadPlaybackNotificationHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterPadPlaybackNotificationHandlerX86(int target, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x060001B1 RID: 433
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterPadPlaybackNotificationHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterPadPlaybackNotificationHandlerX64(int target, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x060001B2 RID: 434 RVA: 0x00005BE0 File Offset: 0x00003DE0
    public static PS3TMAPI.SNRESULT RegisterPadPlaybackHandler(int target, PS3TMAPI.PadPlaybackCallback callback, ref object userData)
    {
        if (callback == null)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.RegisterPadPlaybackNotificationHandlerX86(target, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3TMAPI.RegisterPadPlaybackNotificationHandlerX64(target, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.PadPlaybackCallbackAndUserData padPlaybackCallbackAndUserData = new PS3TMAPI.PadPlaybackCallbackAndUserData();
            padPlaybackCallbackAndUserData.m_callback = callback;
            padPlaybackCallbackAndUserData.m_userData = userData;
            if (PS3TMAPI.ms_userPadPlaybackCallbacks == null)
            {
                PS3TMAPI.ms_userPadPlaybackCallbacks = new Dictionary<int, PS3TMAPI.PadPlaybackCallbackAndUserData>(1);
            }
            PS3TMAPI.ms_userPadPlaybackCallbacks[target] = padPlaybackCallbackAndUserData;
        }
        return snresult;
    }

    // Token: 0x060001B3 RID: 435
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnRegisterPadPlaybackNotificationHandler")]
    private static extern PS3TMAPI.SNRESULT UnregisterPadPlaybackHandlerX86(int target);

    // Token: 0x060001B4 RID: 436
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnRegisterPadPlaybackNotificationHandler")]
    private static extern PS3TMAPI.SNRESULT UnregisterPadPlaybackHandlerX64(int target);

    // Token: 0x060001B5 RID: 437 RVA: 0x00005C5C File Offset: 0x00003E5C
    public static PS3TMAPI.SNRESULT UnregisterPadPlaybackHandler(int target)
    {
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.UnregisterPadPlaybackHandlerX86(target) : PS3TMAPI.UnregisterPadPlaybackHandlerX64(target);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            if (PS3TMAPI.ms_userPadPlaybackCallbacks == null)
            {
                return PS3TMAPI.SNRESULT.SN_E_ERROR;
            }
            PS3TMAPI.ms_userPadPlaybackCallbacks.Remove(target);
        }
        return snresult;
    }

    // Token: 0x060001B6 RID: 438
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StartPadPlayback")]
    private static extern PS3TMAPI.SNRESULT StartPadPlaybackX86(int target);

    // Token: 0x060001B7 RID: 439
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StartPadPlayback")]
    private static extern PS3TMAPI.SNRESULT StartPadPlaybackX64(int target);

    // Token: 0x060001B8 RID: 440 RVA: 0x00005CA1 File Offset: 0x00003EA1
    public static PS3TMAPI.SNRESULT StartPadPlayback(int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.StartPadPlaybackX64(target);
        }
        return PS3TMAPI.StartPadPlaybackX86(target);
    }

    // Token: 0x060001B9 RID: 441
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StopPadPlayback")]
    private static extern PS3TMAPI.SNRESULT StopPadPlaybackX86(int target);

    // Token: 0x060001BA RID: 442
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StopPadPlayback")]
    private static extern PS3TMAPI.SNRESULT StopPadPlaybackX64(int target);

    // Token: 0x060001BB RID: 443 RVA: 0x00005CB7 File Offset: 0x00003EB7
    public static PS3TMAPI.SNRESULT StopPadPlayback(int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.StopPadPlaybackX64(target);
        }
        return PS3TMAPI.StopPadPlaybackX86(target);
    }

    // Token: 0x060001BC RID: 444
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SendPadPlaybackData")]
    private static extern PS3TMAPI.SNRESULT SendPadPlaybackDataX86(int target, ref PS3TMAPI.PadData data);

    // Token: 0x060001BD RID: 445
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SendPadPlaybackData")]
    private static extern PS3TMAPI.SNRESULT SendPadPlaybackDataX64(int target, ref PS3TMAPI.PadData data);

    // Token: 0x060001BE RID: 446 RVA: 0x00005CCD File Offset: 0x00003ECD
    public static PS3TMAPI.SNRESULT SendPadPlaybackData(int target, PS3TMAPI.PadData padData)
    {
        if (padData.buttons == null || padData.buttons.Length != 24)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SendPadPlaybackDataX64(target, ref padData);
        }
        return PS3TMAPI.SendPadPlaybackDataX86(target, ref padData);
    }

    // Token: 0x060001BF RID: 447
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterPadCaptureHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterPadCaptureHandlerX86(int target, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x060001C0 RID: 448
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterPadCaptureHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterPadCaptureHandlerX64(int target, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x060001C1 RID: 449 RVA: 0x00005D00 File Offset: 0x00003F00
    public static PS3TMAPI.SNRESULT RegisterPadCaptureHandler(int target, PS3TMAPI.PadCaptureCallback callback, ref object userData)
    {
        if (callback == null)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.RegisterPadCaptureHandlerX86(target, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3TMAPI.RegisterPadCaptureHandlerX64(target, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.PadCaptureCallbackAndUserData padCaptureCallbackAndUserData = new PS3TMAPI.PadCaptureCallbackAndUserData();
            padCaptureCallbackAndUserData.m_callback = callback;
            padCaptureCallbackAndUserData.m_userData = userData;
            if (PS3TMAPI.ms_userPadCaptureCallbacks == null)
            {
                PS3TMAPI.ms_userPadCaptureCallbacks = new Dictionary<int, PS3TMAPI.PadCaptureCallbackAndUserData>(1);
            }
            PS3TMAPI.ms_userPadCaptureCallbacks[target] = padCaptureCallbackAndUserData;
        }
        return snresult;
    }

    // Token: 0x060001C2 RID: 450
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnRegisterPadCaptureHandler")]
    private static extern PS3TMAPI.SNRESULT UnregisterPadCaptureHandlerX86(int target);

    // Token: 0x060001C3 RID: 451
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnRegisterPadCaptureHandler")]
    private static extern PS3TMAPI.SNRESULT UnregisterPadCaptureHandlerX64(int target);

    // Token: 0x060001C4 RID: 452 RVA: 0x00005D7C File Offset: 0x00003F7C
    public static PS3TMAPI.SNRESULT UnregisterPadCaptureHandler(int target)
    {
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.UnregisterPadCaptureHandlerX86(target) : PS3TMAPI.UnregisterPadCaptureHandlerX64(target);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            if (PS3TMAPI.ms_userPadCaptureCallbacks == null)
            {
                return PS3TMAPI.SNRESULT.SN_E_ERROR;
            }
            PS3TMAPI.ms_userPadCaptureCallbacks.Remove(target);
        }
        return snresult;
    }

    // Token: 0x060001C5 RID: 453
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StartPadCapture")]
    private static extern PS3TMAPI.SNRESULT StartPadCaptureX86(int target);

    // Token: 0x060001C6 RID: 454
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StartPadCapture")]
    private static extern PS3TMAPI.SNRESULT StartPadCaptureX64(int target);

    // Token: 0x060001C7 RID: 455 RVA: 0x00005DC1 File Offset: 0x00003FC1
    public static PS3TMAPI.SNRESULT StartPadCapture(int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.StartPadCaptureX64(target);
        }
        return PS3TMAPI.StartPadCaptureX86(target);
    }

    // Token: 0x060001C8 RID: 456
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StopPadCapture")]
    private static extern PS3TMAPI.SNRESULT StopPadCaptureX86(int target);

    // Token: 0x060001C9 RID: 457
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StopPadCapture")]
    private static extern PS3TMAPI.SNRESULT StopPadCaptureX64(int target);

    // Token: 0x060001CA RID: 458 RVA: 0x00005DD7 File Offset: 0x00003FD7
    public static PS3TMAPI.SNRESULT StopPadCapture(int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.StopPadCaptureX64(target);
        }
        return PS3TMAPI.StopPadCaptureX86(target);
    }

    // Token: 0x060001CB RID: 459 RVA: 0x00005DF0 File Offset: 0x00003FF0
    private static void MarshalPadCaptureEvent(int target, uint param, PS3TMAPI.SNRESULT res, uint length, IntPtr data)
    {
        if (length != 1U)
        {
            return;
        }
        PS3TMAPI.PadData[] array = new PS3TMAPI.PadData[1];
        array[0].buttons = new short[24];
        PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.PadData>(data, ref array[0]);
        if (PS3TMAPI.ms_userPadCaptureCallbacks == null)
        {
            return;
        }
        PS3TMAPI.ms_userPadCaptureCallbacks[target].m_callback(target, res, array, PS3TMAPI.ms_userPadCaptureCallbacks[target].m_userData);
    }

    // Token: 0x060001CC RID: 460 RVA: 0x00005E5C File Offset: 0x0000405C
    private static void MarshalPadPlaybackEvent(int target, uint param, PS3TMAPI.SNRESULT result, uint length, IntPtr data)
    {
        if (length != 1U)
        {
            return;
        }
        uint playbackResult = 0U;
        PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref playbackResult);
        if (PS3TMAPI.ms_userPadPlaybackCallbacks == null)
        {
            return;
        }
        PS3TMAPI.ms_userPadPlaybackCallbacks[target].m_callback(target, result, (PS3TMAPI.PadPlaybackResponse)playbackResult, PS3TMAPI.ms_userPadPlaybackCallbacks[target].m_userData);
    }

    // Token: 0x060001CD RID: 461
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetVRAMCaptureFlags")]
    private static extern PS3TMAPI.SNRESULT GetVRAMCaptureFlagsX86(int target, out ulong vramFlags);

    // Token: 0x060001CE RID: 462
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetVRAMCaptureFlags")]
    private static extern PS3TMAPI.SNRESULT GetVRAMCaptureFlagsX64(int target, out ulong vramFlags);

    // Token: 0x060001CF RID: 463 RVA: 0x00005EAC File Offset: 0x000040AC
    public static PS3TMAPI.SNRESULT GetVRAMCaptureFlags(int target, out PS3TMAPI.VRAMCaptureFlag vramFlags)
    {
        ulong num;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetVRAMCaptureFlagsX86(target, out num) : PS3TMAPI.GetVRAMCaptureFlagsX64(target, out num);
        vramFlags = (PS3TMAPI.VRAMCaptureFlag)num;
        return result;
    }

    // Token: 0x060001D0 RID: 464
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetVRAMCaptureFlags")]
    private static extern PS3TMAPI.SNRESULT SetVRAMCaptureFlagsX86(int target, ulong vramFlags);

    // Token: 0x060001D1 RID: 465
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetVRAMCaptureFlags")]
    private static extern PS3TMAPI.SNRESULT SetVRAMCaptureFlagsX64(int target, ulong vramFlags);

    // Token: 0x060001D2 RID: 466 RVA: 0x00005ED7 File Offset: 0x000040D7
    public static PS3TMAPI.SNRESULT SetVRAMCaptureFlags(int target, PS3TMAPI.VRAMCaptureFlag vramFlags)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetVRAMCaptureFlagsX64(target, (ulong)vramFlags);
        }
        return PS3TMAPI.SetVRAMCaptureFlagsX86(target, (ulong)vramFlags);
    }

    // Token: 0x060001D3 RID: 467
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnableVRAMCapture")]
    private static extern PS3TMAPI.SNRESULT EnableVRAMCaptureX86(int target);

    // Token: 0x060001D4 RID: 468
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnableVRAMCapture")]
    private static extern PS3TMAPI.SNRESULT EnableVRAMCaptureX864(int target);

    // Token: 0x060001D5 RID: 469 RVA: 0x00005EEF File Offset: 0x000040EF
    public static PS3TMAPI.SNRESULT EnableVRAMCapture(int target)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.EnableVRAMCaptureX864(target);
        }
        return PS3TMAPI.EnableVRAMCaptureX86(target);
    }

    // Token: 0x060001D6 RID: 470
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetVRAMInformation")]
    private static extern PS3TMAPI.SNRESULT GetVRAMInformationX86(int target, uint processId, out PS3TMAPI.VramInfoPriv primaryVRAMInfo, out PS3TMAPI.VramInfoPriv secondaryVRAMInfo);

    // Token: 0x060001D7 RID: 471
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetVRAMInformation")]
    private static extern PS3TMAPI.SNRESULT GetVRAMInformationX64(int target, uint processId, out PS3TMAPI.VramInfoPriv primaryVRAMInfo, out PS3TMAPI.VramInfoPriv secondaryVRAMInfo);

    // Token: 0x060001D8 RID: 472 RVA: 0x00005F08 File Offset: 0x00004108
    public static PS3TMAPI.SNRESULT GetVRAMInformation(int target, uint processID, out PS3TMAPI.VRAMInfo primaryVRAMInfo, out PS3TMAPI.VRAMInfo secondaryVRAMInfo)
    {
        primaryVRAMInfo = null;
        secondaryVRAMInfo = null;
        PS3TMAPI.VramInfoPriv vramInfoPriv = default(PS3TMAPI.VramInfoPriv);
        PS3TMAPI.VramInfoPriv vramInfoPriv2 = default(PS3TMAPI.VramInfoPriv);
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetVRAMInformationX86(target, processID, out vramInfoPriv, out vramInfoPriv2) : PS3TMAPI.GetVRAMInformationX64(target, processID, out vramInfoPriv, out vramInfoPriv2);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        primaryVRAMInfo = new PS3TMAPI.VRAMInfo();
        primaryVRAMInfo.BPAddress = vramInfoPriv.BpAddress;
        primaryVRAMInfo.TopAddressPointer = vramInfoPriv.TopAddressPointer;
        primaryVRAMInfo.Width = vramInfoPriv.Width;
        primaryVRAMInfo.Height = vramInfoPriv.Height;
        primaryVRAMInfo.Pitch = vramInfoPriv.Pitch;
        primaryVRAMInfo.Colour = vramInfoPriv.Colour;
        secondaryVRAMInfo = new PS3TMAPI.VRAMInfo();
        secondaryVRAMInfo.BPAddress = vramInfoPriv2.BpAddress;
        secondaryVRAMInfo.TopAddressPointer = vramInfoPriv2.TopAddressPointer;
        secondaryVRAMInfo.Width = vramInfoPriv2.Width;
        secondaryVRAMInfo.Height = vramInfoPriv2.Height;
        secondaryVRAMInfo.Pitch = vramInfoPriv2.Pitch;
        secondaryVRAMInfo.Colour = vramInfoPriv2.Colour;
        return snresult;
    }

    // Token: 0x060001D9 RID: 473
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3VRAMCapture")]
    private static extern PS3TMAPI.SNRESULT VRAMCaptureX86(int target, uint processId, IntPtr vramInfo, IntPtr fileName);

    // Token: 0x060001DA RID: 474
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3VRAMCapture")]
    private static extern PS3TMAPI.SNRESULT VRAMCaptureX64(int target, uint processId, IntPtr vramInfo, IntPtr fileName);

    // Token: 0x060001DB RID: 475 RVA: 0x0000600C File Offset: 0x0000420C
    public static PS3TMAPI.SNRESULT VRAMCapture(int target, uint processID, PS3TMAPI.VRAMInfo vramInfo, string fileName)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(IntPtr.Zero);
        if (vramInfo != null)
        {
            PS3TMAPI.VramInfoPriv vramInfoPriv = new PS3TMAPI.VramInfoPriv
            {
                BpAddress = vramInfo.BPAddress,
                TopAddressPointer = vramInfo.TopAddressPointer,
                Width = vramInfo.Width,
                Height = vramInfo.Height,
                Pitch = vramInfo.Pitch,
                Colour = vramInfo.Colour
            };
            scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal(Marshal.SizeOf(vramInfoPriv)));
            Marshal.StructureToPtr(vramInfoPriv, scopedGlobalHeapPtr.Get(), false);
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(fileName));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.VRAMCaptureX64(target, processID, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get());
        }
        return PS3TMAPI.VRAMCaptureX86(target, processID, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get());
    }

    // Token: 0x060001DC RID: 476 RVA: 0x000060E0 File Offset: 0x000042E0
    private static void CustomProtocolHandler(int target, PS3TMAPI.PS3Protocol ps3Protocol, IntPtr unmanagedBuf, uint length, IntPtr userData)
    {
        PS3TMAPI.PS3ProtocolPriv protocol = new PS3TMAPI.PS3ProtocolPriv(ps3Protocol.Protocol, ps3Protocol.Port);
        PS3TMAPI.CustomProtocolId key = new PS3TMAPI.CustomProtocolId(target, protocol);
        if (PS3TMAPI.ms_userCustomProtoCallbacks == null)
        {
            return;
        }
        PS3TMAPI.CusProtoCallbackAndUserData cusProtoCallbackAndUserData;
        if (PS3TMAPI.ms_userCustomProtoCallbacks.TryGetValue(key, out cusProtoCallbackAndUserData))
        {
            byte[] array = new byte[length];
            Marshal.Copy(unmanagedBuf, array, 0, array.Length);
            cusProtoCallbackAndUserData.m_callback(target, ps3Protocol, array, cusProtoCallbackAndUserData.m_userData);
        }
    }

    // Token: 0x060001DD RID: 477
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterCustomProtocolEx")]
    private static extern PS3TMAPI.SNRESULT RegisterCustomProtocolExX86(int target, uint protocol, uint port, string lparDesc, uint priority, out PS3TMAPI.PS3Protocol ps3Protocol, PS3TMAPI.CustomProtocolCallbackPriv callback, IntPtr userData);

    // Token: 0x060001DE RID: 478
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterCustomProtocolEx")]
    private static extern PS3TMAPI.SNRESULT RegisterCustomProtocolExX64(int target, uint protocol, uint port, string lparDesc, uint priority, out PS3TMAPI.PS3Protocol ps3Protocol, PS3TMAPI.CustomProtocolCallbackPriv callback, IntPtr userData);

    // Token: 0x060001DF RID: 479 RVA: 0x0000614C File Offset: 0x0000434C
    public static PS3TMAPI.SNRESULT RegisterCustomProtocol(int target, uint protocol, uint port, string lparDesc, uint priority, out PS3TMAPI.PS3Protocol ps3Protocol, PS3TMAPI.CustomProtocolCallback callback, ref object userData)
    {
        ps3Protocol = default(PS3TMAPI.PS3Protocol);
        if (callback == null)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.RegisterCustomProtocolExX86(target, protocol, port, lparDesc, priority, out ps3Protocol, PS3TMAPI.ms_customProtoCallbackPriv, IntPtr.Zero) : PS3TMAPI.RegisterCustomProtocolExX64(target, protocol, port, lparDesc, priority, out ps3Protocol, PS3TMAPI.ms_customProtoCallbackPriv, IntPtr.Zero);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.PS3ProtocolPriv protocol2 = new PS3TMAPI.PS3ProtocolPriv(ps3Protocol.Protocol, ps3Protocol.Port);
            PS3TMAPI.CustomProtocolId key = new PS3TMAPI.CustomProtocolId(target, protocol2);
            PS3TMAPI.CusProtoCallbackAndUserData cusProtoCallbackAndUserData = new PS3TMAPI.CusProtoCallbackAndUserData();
            cusProtoCallbackAndUserData.m_callback = callback;
            cusProtoCallbackAndUserData.m_userData = userData;
            if (PS3TMAPI.ms_userCustomProtoCallbacks == null)
            {
                PS3TMAPI.ms_userCustomProtoCallbacks = new Dictionary<PS3TMAPI.CustomProtocolId, PS3TMAPI.CusProtoCallbackAndUserData>(1);
            }
            PS3TMAPI.ms_userCustomProtoCallbacks[key] = cusProtoCallbackAndUserData;
        }
        return snresult;
    }

    // Token: 0x060001E0 RID: 480
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnRegisterCustomProtocol")]
    private static extern PS3TMAPI.SNRESULT UnregisterCustomProtocolX86(int target, ref PS3TMAPI.PS3Protocol protocol);

    // Token: 0x060001E1 RID: 481
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnRegisterCustomProtocol")]
    private static extern PS3TMAPI.SNRESULT UnregisterCustomProtocolX64(int target, ref PS3TMAPI.PS3Protocol protocol);

    // Token: 0x060001E2 RID: 482 RVA: 0x000061FC File Offset: 0x000043FC
    public static PS3TMAPI.SNRESULT UnregisterCustomProtocol(int target, PS3TMAPI.PS3Protocol ps3Protocol)
    {
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.UnregisterCustomProtocolX86(target, ref ps3Protocol) : PS3TMAPI.UnregisterCustomProtocolX64(target, ref ps3Protocol);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.PS3ProtocolPriv protocol = new PS3TMAPI.PS3ProtocolPriv(ps3Protocol.Protocol, ps3Protocol.Port);
            PS3TMAPI.CustomProtocolId key = new PS3TMAPI.CustomProtocolId(target, protocol);
            if (PS3TMAPI.ms_userCustomProtoCallbacks == null)
            {
                return PS3TMAPI.SNRESULT.SN_E_ERROR;
            }
            PS3TMAPI.ms_userCustomProtoCallbacks.Remove(key);
        }
        return snresult;
    }

    // Token: 0x060001E3 RID: 483
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ForceUnRegisterCustomProtocol")]
    private static extern PS3TMAPI.SNRESULT ForceUnregisterCustomProtocolX86(int target, ref PS3TMAPI.PS3Protocol protocol);

    // Token: 0x060001E4 RID: 484
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ForceUnRegisterCustomProtocol")]
    private static extern PS3TMAPI.SNRESULT ForceUnregisterCustomProtocolX64(int target, ref PS3TMAPI.PS3Protocol protocol);

    // Token: 0x060001E5 RID: 485 RVA: 0x00006264 File Offset: 0x00004464
    public static PS3TMAPI.SNRESULT ForceUnregisterCustomProtocol(int target, PS3TMAPI.PS3Protocol ps3Protocol)
    {
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.ForceUnregisterCustomProtocolX86(target, ref ps3Protocol) : PS3TMAPI.ForceUnregisterCustomProtocolX64(target, ref ps3Protocol);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.PS3ProtocolPriv protocol = new PS3TMAPI.PS3ProtocolPriv(ps3Protocol.Protocol, ps3Protocol.Port);
            PS3TMAPI.CustomProtocolId key = new PS3TMAPI.CustomProtocolId(target, protocol);
            if (PS3TMAPI.ms_userCustomProtoCallbacks == null)
            {
                return PS3TMAPI.SNRESULT.SN_E_ERROR;
            }
            PS3TMAPI.ms_userCustomProtoCallbacks.Remove(key);
        }
        return snresult;
    }

    // Token: 0x060001E6 RID: 486
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SendCustomProtocolData")]
    private static extern PS3TMAPI.SNRESULT SendCustomProtocolDataX86(int target, ref PS3TMAPI.PS3Protocol protocol, byte[] data, int length);

    // Token: 0x060001E7 RID: 487
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SendCustomProtocolData")]
    private static extern PS3TMAPI.SNRESULT SendCustomProtocolDataX64(int target, ref PS3TMAPI.PS3Protocol protocol, byte[] data, int length);

    // Token: 0x060001E8 RID: 488 RVA: 0x000062CB File Offset: 0x000044CB
    public static PS3TMAPI.SNRESULT SendCustomProtocolData(int target, PS3TMAPI.PS3Protocol ps3Protocol, byte[] data)
    {
        if (data == null || data.Length < 1)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SendCustomProtocolDataX64(target, ref ps3Protocol, data, data.Length);
        }
        return PS3TMAPI.SendCustomProtocolDataX86(target, ref ps3Protocol, data, data.Length);
    }

    // Token: 0x060001E9 RID: 489
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetFileServingEventFlags")]
    private static extern PS3TMAPI.SNRESULT SetFileServingEventFlagsX86(int target, ulong eventFlags);

    // Token: 0x060001EA RID: 490
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetFileServingEventFlags")]
    private static extern PS3TMAPI.SNRESULT SetFileServingEventFlagsX64(int target, ulong eventFlags);

    // Token: 0x060001EB RID: 491 RVA: 0x000062F9 File Offset: 0x000044F9
    public static PS3TMAPI.SNRESULT SetFileServingEventFlags(int target, PS3TMAPI.FileServingEventFlag eventFlags)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetFileServingEventFlagsX64(target, (ulong)eventFlags);
        }
        return PS3TMAPI.SetFileServingEventFlagsX86(target, (ulong)eventFlags);
    }

    // Token: 0x060001EC RID: 492
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetFileServingEventFlags")]
    private static extern PS3TMAPI.SNRESULT GetFileServingEventFlagsX86(int target, ref ulong eventFlags);

    // Token: 0x060001ED RID: 493
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetFileServingEventFlags")]
    private static extern PS3TMAPI.SNRESULT GetFileServingEventFlagsX64(int target, ref ulong eventFlags);

    // Token: 0x060001EE RID: 494 RVA: 0x00006314 File Offset: 0x00004514
    public static PS3TMAPI.SNRESULT GetFileServingEventFlags(int target, out PS3TMAPI.FileServingEventFlag eventFlags)
    {
        ulong num = 0UL;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetFileServingEventFlagsX86(target, ref num) : PS3TMAPI.GetFileServingEventFlagsX64(target, ref num);
        eventFlags = (PS3TMAPI.FileServingEventFlag)num;
        return result;
    }

    // Token: 0x060001EF RID: 495
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetCaseSensitiveFileServing")]
    private static extern PS3TMAPI.SNRESULT SetCaseSensitiveFileServingX86(int target, bool bOn, out bool bOldSetting);

    // Token: 0x060001F0 RID: 496
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetCaseSensitiveFileServing")]
    private static extern PS3TMAPI.SNRESULT SetCaseSensitiveFileServingX64(int target, bool bOn, out bool bOldSetting);

    // Token: 0x060001F1 RID: 497 RVA: 0x00006342 File Offset: 0x00004542
    public static PS3TMAPI.SNRESULT SetCaseSensitiveFileServing(int target, bool bOn, out bool bOldSetting)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetCaseSensitiveFileServingX64(target, bOn, out bOldSetting);
        }
        return PS3TMAPI.SetCaseSensitiveFileServingX86(target, bOn, out bOldSetting);
    }

    // Token: 0x060001F2 RID: 498
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterFTPEventHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterFTPEventHandlerX86(int target, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x060001F3 RID: 499
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterFTPEventHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterFTPEventHandlerX64(int target, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x060001F4 RID: 500 RVA: 0x0000635C File Offset: 0x0000455C
    public static PS3TMAPI.SNRESULT RegisterFTPEventHandler(int target, PS3TMAPI.FTPEventCallback callback, ref object userData)
    {
        if (callback == null)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.RegisterFTPEventHandlerX86(target, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3TMAPI.RegisterFTPEventHandlerX64(target, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.FtpCallbackAndUserData ftpCallbackAndUserData = new PS3TMAPI.FtpCallbackAndUserData();
            ftpCallbackAndUserData.m_callback = callback;
            ftpCallbackAndUserData.m_userData = userData;
            if (PS3TMAPI.ms_userFtpCallbacks == null)
            {
                PS3TMAPI.ms_userFtpCallbacks = new Dictionary<int, PS3TMAPI.FtpCallbackAndUserData>(1);
            }
            PS3TMAPI.ms_userFtpCallbacks[target] = ftpCallbackAndUserData;
        }
        return snresult;
    }

    // Token: 0x060001F5 RID: 501
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CancelFTPEvents")]
    private static extern PS3TMAPI.SNRESULT CancelFTPEventsX86(int target);

    // Token: 0x060001F6 RID: 502
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CancelFTPEvents")]
    private static extern PS3TMAPI.SNRESULT CancelFTPEventsX64(int target);

    // Token: 0x060001F7 RID: 503 RVA: 0x000063D8 File Offset: 0x000045D8
    public static PS3TMAPI.SNRESULT CancelFTPEvents(int target)
    {
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.CancelFTPEventsX86(target) : PS3TMAPI.CancelFTPEventsX64(target);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            if (PS3TMAPI.ms_userFtpCallbacks == null)
            {
                return PS3TMAPI.SNRESULT.SN_E_ERROR;
            }
            PS3TMAPI.ms_userFtpCallbacks.Remove(target);
        }
        return snresult;
    }

    // Token: 0x060001F8 RID: 504 RVA: 0x00006420 File Offset: 0x00004620
    private static void MarshalFTPEvent(int target, uint param, PS3TMAPI.SNRESULT result, uint length, IntPtr data)
    {
        PS3TMAPI.FTPNotification[] array = new PS3TMAPI.FTPNotification[0];
        if (length > 0U)
        {
            uint num = (uint)((ulong)length / (ulong)((long)Marshal.SizeOf(typeof(PS3TMAPI.FTPNotification))));
            array = new PS3TMAPI.FTPNotification[num];
            int num2 = 0;
            while ((long)num2 < (long)((ulong)num))
            {
                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.FTPNotification>(data, ref array[num2]);
                num2++;
            }
        }
        if (PS3TMAPI.ms_userFtpCallbacks == null)
        {
            return;
        }
        PS3TMAPI.ms_userFtpCallbacks[target].m_callback(target, result, array, PS3TMAPI.ms_userFtpCallbacks[target].m_userData);
    }

    // Token: 0x060001F9 RID: 505
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterFileTraceHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterFileTraceHandlerX86(int target, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x060001FA RID: 506
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterFileTraceHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterFileTraceHandlerX64(int target, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x060001FB RID: 507 RVA: 0x000064A4 File Offset: 0x000046A4
    public static PS3TMAPI.SNRESULT RegisterFileTraceHandler(int target, PS3TMAPI.FileTraceCallback callback, ref object userData)
    {
        if (callback == null)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.RegisterFileTraceHandlerX86(target, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3TMAPI.RegisterFileTraceHandlerX64(target, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.FileTraceCallbackAndUserData fileTraceCallbackAndUserData = new PS3TMAPI.FileTraceCallbackAndUserData();
            fileTraceCallbackAndUserData.m_callback = callback;
            fileTraceCallbackAndUserData.m_userData = userData;
            if (PS3TMAPI.ms_userFileTraceCallbacks == null)
            {
                PS3TMAPI.ms_userFileTraceCallbacks = new Dictionary<int, PS3TMAPI.FileTraceCallbackAndUserData>(1);
            }
            PS3TMAPI.ms_userFileTraceCallbacks[target] = fileTraceCallbackAndUserData;
        }
        return snresult;
    }

    // Token: 0x060001FC RID: 508
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnRegisterFileTraceHandler")]
    private static extern PS3TMAPI.SNRESULT UnregisterFileTraceHandlerX86(int target);

    // Token: 0x060001FD RID: 509
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnRegisterFileTraceHandler")]
    private static extern PS3TMAPI.SNRESULT UnregisterFileTraceHandlerX64(int target);

    // Token: 0x060001FE RID: 510 RVA: 0x00006520 File Offset: 0x00004720
    public static PS3TMAPI.SNRESULT UnregisterFileTraceHandler(int target)
    {
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.UnregisterFileTraceHandlerX86(target) : PS3TMAPI.UnregisterFileTraceHandlerX64(target);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            if (PS3TMAPI.ms_userFileTraceCallbacks == null)
            {
                return PS3TMAPI.SNRESULT.SN_E_ERROR;
            }
            PS3TMAPI.ms_userFileTraceCallbacks.Remove(target);
        }
        return snresult;
    }

    // Token: 0x060001FF RID: 511 RVA: 0x00006568 File Offset: 0x00004768
    private static void MarshalFileTraceEvent(int target, uint param, PS3TMAPI.SNRESULT result, uint length, IntPtr data)
    {
        PS3TMAPI.FileTraceEvent fileTraceEvent = default(PS3TMAPI.FileTraceEvent);
        IntPtr intPtr = data;
        uint num = 44U;
        if (length < num)
        {
            return;
        }
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.SerialID);
        int traceType = 0;
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<int>(intPtr, ref traceType);
        fileTraceEvent.TraceType = (PS3TMAPI.FileTraceType)traceType;
        int status = 0;
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<int>(intPtr, ref status);
        fileTraceEvent.Status = (PS3TMAPI.FileTraceNotificationStatus)status;
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.ProcessID);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.ThreadID);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.TimeBaseStartOfTrace);
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.TimeBase);
        uint num2 = 0U;
        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref num2);
        num += num2;
        if (length < num)
        {
            return;
        }
        fileTraceEvent.BackTraceData = new byte[num2];
        int num3 = 0;
        while ((long)num3 < (long)((ulong)num2))
        {
            intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<byte>(intPtr, ref fileTraceEvent.BackTraceData[num3]);
            num3++;
        }
        switch (fileTraceEvent.TraceType)
        {
            case PS3TMAPI.FileTraceType.GetBlockSize:
            case PS3TMAPI.FileTraceType.Stat:
            case PS3TMAPI.FileTraceType.WidgetStat:
            case PS3TMAPI.FileTraceType.Unlink:
            case PS3TMAPI.FileTraceType.WidgetUnlink:
            case PS3TMAPI.FileTraceType.RMDir:
            case PS3TMAPI.FileTraceType.WidgetRMDir:
                {
                    fileTraceEvent.LogData.LogType1 = default(PS3TMAPI.FileTraceLogType1);
                    uint num4 = 0U;
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref num4);
                    if (num4 > 0U)
                    {
                        fileTraceEvent.LogData.LogType1.Path = PS3TMAPI.Utf8ToString(intPtr, num4);
                    }
                    break;
                }
            case PS3TMAPI.FileTraceType.Rename:
            case PS3TMAPI.FileTraceType.WidgetRename:
                {
                    fileTraceEvent.LogData.LogType2 = default(PS3TMAPI.FileTraceLogType2);
                    uint num5 = 0U;
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref num5);
                    uint num6 = 0U;
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref num6);
                    if (num5 > 0U)
                    {
                        fileTraceEvent.LogData.LogType2.Path1 = PS3TMAPI.Utf8ToString(intPtr, num5);
                        intPtr = new IntPtr(intPtr.ToInt64() + (long)((ulong)num5));
                    }
                    if (num6 > 0U)
                    {
                        fileTraceEvent.LogData.LogType2.Path2 = PS3TMAPI.Utf8ToString(intPtr, num6);
                    }
                    break;
                }
            case PS3TMAPI.FileTraceType.Truncate:
            case PS3TMAPI.FileTraceType.TruncateNoAlloc:
            case PS3TMAPI.FileTraceType.Truncate2:
            case PS3TMAPI.FileTraceType.Truncate2NoInit:
                {
                    fileTraceEvent.LogData.LogType3 = default(PS3TMAPI.FileTraceLogType3);
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.LogData.LogType3.Arg);
                    uint num4 = 0U;
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref num4);
                    if (num4 > 0U)
                    {
                        fileTraceEvent.LogData.LogType3.Path = PS3TMAPI.Utf8ToString(intPtr, num4);
                    }
                    break;
                }
            case PS3TMAPI.FileTraceType.OpenDir:
            case PS3TMAPI.FileTraceType.WidgetOpenDir:
            case PS3TMAPI.FileTraceType.CHMod:
            case PS3TMAPI.FileTraceType.MkDir:
                {
                    fileTraceEvent.LogData.LogType4 = default(PS3TMAPI.FileTraceLogType4);
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType4.Mode);
                    uint num4 = 0U;
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref num4);
                    if (num4 > 0U)
                    {
                        fileTraceEvent.LogData.LogType4.Path = PS3TMAPI.Utf8ToString(intPtr, num4);
                    }
                    break;
                }
            case PS3TMAPI.FileTraceType.UTime:
                {
                    fileTraceEvent.LogData.LogType6 = default(PS3TMAPI.FileTraceLogType6);
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.LogData.LogType6.Arg1);
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.LogData.LogType6.Arg2);
                    uint num4 = 0U;
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref num4);
                    if (num4 > 0U)
                    {
                        fileTraceEvent.LogData.LogType6.Path = PS3TMAPI.Utf8ToString(intPtr, num4);
                    }
                    break;
                }
            case PS3TMAPI.FileTraceType.Open:
            case PS3TMAPI.FileTraceType.WidgetOpen:
                {
                    fileTraceEvent.LogData.LogType8 = default(PS3TMAPI.FileTraceLogType8);
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.FileTraceProcessInfo>(intPtr, ref fileTraceEvent.LogData.LogType8.ProcessInfo);
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType8.Arg1);
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType8.Arg2);
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType8.Arg3);
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType8.Arg4);
                    uint num7 = 0U;
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref num7);
                    uint num4 = 0U;
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref num4);
                    fileTraceEvent.LogData.LogType8.VArg = new byte[num7];
                    int num8 = 0;
                    while ((long)num8 < (long)((ulong)num7))
                    {
                        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<byte>(intPtr, ref fileTraceEvent.LogData.LogType8.VArg[num8]);
                        num8++;
                    }
                    if (num4 > 0U)
                    {
                        fileTraceEvent.LogData.LogType8.Path = PS3TMAPI.Utf8ToString(intPtr, num4);
                    }
                    break;
                }
            case PS3TMAPI.FileTraceType.Close:
            case PS3TMAPI.FileTraceType.CloseDir:
            case PS3TMAPI.FileTraceType.FSync:
            case PS3TMAPI.FileTraceType.ReadDir:
            case PS3TMAPI.FileTraceType.FStat:
            case PS3TMAPI.FileTraceType.FGetBlockSize:
                fileTraceEvent.LogData.LogType9 = default(PS3TMAPI.FileTraceLogType9);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.FileTraceProcessInfo>(intPtr, ref fileTraceEvent.LogData.LogType9.ProcessInfo);
                break;
            case PS3TMAPI.FileTraceType.Read:
            case PS3TMAPI.FileTraceType.Write:
            case PS3TMAPI.FileTraceType.GetDirEntries:
                fileTraceEvent.LogData.LogType10 = default(PS3TMAPI.FileTraceLogType10);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.FileTraceProcessInfo>(intPtr, ref fileTraceEvent.LogData.LogType10.ProcessInfo);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType10.Size);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.LogData.LogType10.Address);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType10.TxSize);
                break;
            case PS3TMAPI.FileTraceType.ReadOffset:
            case PS3TMAPI.FileTraceType.WriteOffset:
                fileTraceEvent.LogData.LogType11 = default(PS3TMAPI.FileTraceLogType11);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.FileTraceProcessInfo>(intPtr, ref fileTraceEvent.LogData.LogType11.ProcessInfo);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType11.Size);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.LogData.LogType11.Address);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.LogData.LogType11.Offset);
                PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType11.TxSize);
                break;
            case PS3TMAPI.FileTraceType.FTruncate:
            case PS3TMAPI.FileTraceType.FTruncateNoAlloc:
                fileTraceEvent.LogData.LogType12 = default(PS3TMAPI.FileTraceLogType12);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.FileTraceProcessInfo>(intPtr, ref fileTraceEvent.LogData.LogType12.ProcessInfo);
                PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.LogData.LogType12.TargetSize);
                break;
            case PS3TMAPI.FileTraceType.LSeek:
                fileTraceEvent.LogData.LogType13 = default(PS3TMAPI.FileTraceLogType13);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.FileTraceProcessInfo>(intPtr, ref fileTraceEvent.LogData.LogType13.ProcessInfo);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType13.Size);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.LogData.LogType13.Offset);
                PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(intPtr, ref fileTraceEvent.LogData.LogType13.CurPos);
                break;
            case PS3TMAPI.FileTraceType.SetIOBuffer:
                fileTraceEvent.LogData.LogType14 = default(PS3TMAPI.FileTraceLogType14);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.FileTraceProcessInfo>(intPtr, ref fileTraceEvent.LogData.LogType14.ProcessInfo);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType14.MaxSize);
                intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType14.Page);
                PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref fileTraceEvent.LogData.LogType14.ContainerID);
                break;
        }
        if (PS3TMAPI.ms_userFileTraceCallbacks == null)
        {
            return;
        }
        PS3TMAPI.ms_userFileTraceCallbacks[target].m_callback(target, result, fileTraceEvent, PS3TMAPI.ms_userFileTraceCallbacks[target].m_userData);
    }

    // Token: 0x06000200 RID: 512
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StartFileTrace")]
    private static extern PS3TMAPI.SNRESULT StartFileTraceX86(int target, uint processId, uint size, IntPtr filename);

    // Token: 0x06000201 RID: 513
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StartFileTrace")]
    private static extern PS3TMAPI.SNRESULT StartFileTraceX64(int target, uint processId, uint size, IntPtr filename);

    // Token: 0x06000202 RID: 514 RVA: 0x00006CD4 File Offset: 0x00004ED4
    public static PS3TMAPI.SNRESULT StartFileTrace(int target, uint processID, uint size, string filename)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(filename));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.StartFileTraceX64(target, processID, size, scopedGlobalHeapPtr.Get());
        }
        return PS3TMAPI.StartFileTraceX86(target, processID, size, scopedGlobalHeapPtr.Get());
    }

    // Token: 0x06000203 RID: 515
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StopFileTrace")]
    private static extern PS3TMAPI.SNRESULT StopFileTraceX86(int target, uint processId);

    // Token: 0x06000204 RID: 516
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StopFileTrace")]
    private static extern PS3TMAPI.SNRESULT StopFileTraceX64(int target, uint processId);

    // Token: 0x06000205 RID: 517 RVA: 0x00006D11 File Offset: 0x00004F11
    public static PS3TMAPI.SNRESULT StopFileTrace(int target, uint processID)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.StopFileTraceX64(target, processID);
        }
        return PS3TMAPI.StopFileTraceX86(target, processID);
    }

    // Token: 0x06000206 RID: 518
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3InstallPackage")]
    private static extern PS3TMAPI.SNRESULT InstallPackageX86(int target, IntPtr packagePath);

    // Token: 0x06000207 RID: 519
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3InstallPackage")]
    private static extern PS3TMAPI.SNRESULT InstallPackageX64(int target, IntPtr packagePath);

    // Token: 0x06000208 RID: 520 RVA: 0x00006D2C File Offset: 0x00004F2C
    public static PS3TMAPI.SNRESULT InstallPackage(int target, string packagePath)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(packagePath));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.InstallPackageX64(target, scopedGlobalHeapPtr.Get());
        }
        return PS3TMAPI.InstallPackageX86(target, scopedGlobalHeapPtr.Get());
    }

    // Token: 0x06000209 RID: 521
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UploadFile")]
    private static extern PS3TMAPI.SNRESULT UploadFileX86(int target, IntPtr source, IntPtr dest, out uint transactionId);

    // Token: 0x0600020A RID: 522
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UploadFile")]
    private static extern PS3TMAPI.SNRESULT UploadFileX64(int target, IntPtr source, IntPtr dest, out uint transactionId);

    // Token: 0x0600020B RID: 523 RVA: 0x00006D68 File Offset: 0x00004F68
    public static PS3TMAPI.SNRESULT UploadFile(int target, string source, string dest, out uint txID)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(source));
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(dest));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.UploadFileX64(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get(), out txID);
        }
        return PS3TMAPI.UploadFileX86(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get(), out txID);
    }

    // Token: 0x0600020C RID: 524 RVA: 0x00006DBC File Offset: 0x00004FBC
    private static IntPtr FileTransferInfoMarshalHelper(IntPtr dataPtr, ref PS3TMAPI.FileTransferInfo fileTransferInfo)
    {
        PS3TMAPI.FileTransferInfoPriv fileTransferInfoPriv = default(PS3TMAPI.FileTransferInfoPriv);
        dataPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.FileTransferInfoPriv>(dataPtr, ref fileTransferInfoPriv);
        fileTransferInfo.TransferID = fileTransferInfoPriv.TransferId;
        fileTransferInfo.Status = (PS3TMAPI.FileTransferStatus)fileTransferInfoPriv.Status;
        fileTransferInfo.Size = fileTransferInfoPriv.Size;
        fileTransferInfo.BytesRead = fileTransferInfoPriv.BytesRead;
        fileTransferInfo.SourcePath = PS3TMAPI.Utf8FixedSizeByteArrayToString(fileTransferInfoPriv.SourcePath);
        fileTransferInfo.DestinationPath = PS3TMAPI.Utf8FixedSizeByteArrayToString(fileTransferInfoPriv.DestinationPath);
        return dataPtr;
    }

    // Token: 0x0600020D RID: 525
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetFileTransferList")]
    private static extern PS3TMAPI.SNRESULT GetFileTransferListX86(int target, ref uint count, IntPtr fileTransferInfo);

    // Token: 0x0600020E RID: 526
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetFileTransferList")]
    private static extern PS3TMAPI.SNRESULT GetFileTransferListX64(int target, ref uint count, IntPtr fileTransferInfo);

    // Token: 0x0600020F RID: 527 RVA: 0x00006E34 File Offset: 0x00005034
    public static PS3TMAPI.SNRESULT GetFileTransferList(int target, out PS3TMAPI.FileTransferInfo[] fileTransfers)
    {
        fileTransfers = null;
        uint num = 0U;
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetFileTransferListX86(target, ref num, IntPtr.Zero) : PS3TMAPI.GetFileTransferListX64(target, ref num, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)((long)Marshal.SizeOf(typeof(PS3TMAPI.FileTransferInfoPriv)) * (long)((ulong)num))));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetFileTransferListX86(target, ref num, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetFileTransferListX64(target, ref num, scopedGlobalHeapPtr.Get()));
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        IntPtr dataPtr = scopedGlobalHeapPtr.Get();
        fileTransfers = new PS3TMAPI.FileTransferInfo[num];
        for (uint num2 = 0U; num2 < num; num2 += 1U)
        {
            dataPtr = PS3TMAPI.FileTransferInfoMarshalHelper(dataPtr, ref fileTransfers[(int)((UIntPtr)num2)]);
        }
        return snresult;
    }

    // Token: 0x06000210 RID: 528
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetFileTransferInfo")]
    private static extern PS3TMAPI.SNRESULT GetFileTransferInfoX86(int target, uint txId, IntPtr fileTransferInfoPtr);

    // Token: 0x06000211 RID: 529
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetFileTransferInfo")]
    private static extern PS3TMAPI.SNRESULT GetFileTransferInfoX64(int target, uint txId, IntPtr fileTransferInfoPtr);

    // Token: 0x06000212 RID: 530 RVA: 0x00006EF8 File Offset: 0x000050F8
    public static PS3TMAPI.SNRESULT GetFileTransferInfo(int target, uint txID, out PS3TMAPI.FileTransferInfo fileTransferInfo)
    {
        fileTransferInfo = default(PS3TMAPI.FileTransferInfo);
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PS3TMAPI.FileTransferInfoPriv))));
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetFileTransferInfoX86(target, txID, scopedGlobalHeapPtr.Get()) : PS3TMAPI.GetFileTransferInfoX64(target, txID, scopedGlobalHeapPtr.Get());
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.FileTransferInfoMarshalHelper(scopedGlobalHeapPtr.Get(), ref fileTransferInfo);
        }
        return snresult;
    }

    // Token: 0x06000213 RID: 531
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CancelFileTransfer")]
    private static extern PS3TMAPI.SNRESULT CancelFileTransferX86(int target, uint txID);

    // Token: 0x06000214 RID: 532
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CancelFileTransfer")]
    private static extern PS3TMAPI.SNRESULT CancelFileTransferX64(int target, uint txID);

    // Token: 0x06000215 RID: 533 RVA: 0x00006F60 File Offset: 0x00005160
    public static PS3TMAPI.SNRESULT CancelFileTransfer(int target, uint txID)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.CancelFileTransferX64(target, txID);
        }
        return PS3TMAPI.CancelFileTransferX86(target, txID);
    }

    // Token: 0x06000216 RID: 534
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RetryFileTransfer")]
    private static extern PS3TMAPI.SNRESULT RetryFileTransferX86(int target, uint txID, bool bForce);

    // Token: 0x06000217 RID: 535
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RetryFileTransfer")]
    private static extern PS3TMAPI.SNRESULT RetryFileTransferX64(int target, uint txID, bool bForce);

    // Token: 0x06000218 RID: 536 RVA: 0x00006F78 File Offset: 0x00005178
    public static PS3TMAPI.SNRESULT RetryFileTransfer(int target, uint txID, bool bForce)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.RetryFileTransferX64(target, txID, bForce);
        }
        return PS3TMAPI.RetryFileTransferX86(target, txID, bForce);
    }

    // Token: 0x06000219 RID: 537
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RemoveTransferItemsByStatus")]
    private static extern PS3TMAPI.SNRESULT RemoveTransferItemsByStatusX86(int target, uint filter);

    // Token: 0x0600021A RID: 538
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RemoveTransferItemsByStatus")]
    private static extern PS3TMAPI.SNRESULT SNPS3RemoveTransferItemsByStatusX64(int target, uint filter);

    // Token: 0x0600021B RID: 539 RVA: 0x00006F92 File Offset: 0x00005192
    public static PS3TMAPI.SNRESULT RemoveTransferItemsByStatus(int target, uint filter)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SNPS3RemoveTransferItemsByStatusX64(target, filter);
        }
        return PS3TMAPI.RemoveTransferItemsByStatusX86(target, filter);
    }

    // Token: 0x0600021C RID: 540 RVA: 0x00006FAC File Offset: 0x000051AC
    private static IntPtr DirEntryMarshalHelper(IntPtr dataPtr, ref PS3TMAPI.DirEntry dirEntry)
    {
        PS3TMAPI.DirEntryPriv dirEntryPriv = default(PS3TMAPI.DirEntryPriv);
        dataPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.DirEntryPriv>(dataPtr, ref dirEntryPriv);
        dirEntry.Type = (PS3TMAPI.DirEntryType)dirEntryPriv.Type;
        dirEntry.Mode = dirEntryPriv.Mode;
        dirEntry.AccessTime = dirEntryPriv.AccessTime;
        dirEntry.ModifiedTime = dirEntryPriv.ModifiedTime;
        dirEntry.CreateTime = dirEntryPriv.CreateTime;
        dirEntry.Size = dirEntryPriv.Size;
        dirEntry.Name = PS3TMAPI.Utf8FixedSizeByteArrayToString(dirEntryPriv.Name);
        return dataPtr;
    }

    // Token: 0x0600021D RID: 541
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDirectoryList")]
    private static extern PS3TMAPI.SNRESULT GetDirectoryListX86(int target, IntPtr directory, ref uint numDirEntries, IntPtr dirEntryList);

    // Token: 0x0600021E RID: 542
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDirectoryList")]
    private static extern PS3TMAPI.SNRESULT GetDirectoryListX64(int target, IntPtr directory, ref uint numDirEntries, IntPtr dirEntryList);

    // Token: 0x0600021F RID: 543 RVA: 0x0000702C File Offset: 0x0000522C
    public static PS3TMAPI.SNRESULT GetDirectoryList(int target, string directory, out PS3TMAPI.DirEntry[] dirEntries)
    {
        dirEntries = null;
        uint num = 0U;
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(directory));
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetDirectoryListX86(target, scopedGlobalHeapPtr.Get(), ref num, IntPtr.Zero) : PS3TMAPI.GetDirectoryListX64(target, scopedGlobalHeapPtr.Get(), ref num, IntPtr.Zero);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)(num * (uint)Marshal.SizeOf(typeof(PS3TMAPI.DirEntryPriv)))));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetDirectoryListX86(target, scopedGlobalHeapPtr.Get(), ref num, scopedGlobalHeapPtr2.Get()) : PS3TMAPI.GetDirectoryListX64(target, scopedGlobalHeapPtr.Get(), ref num, scopedGlobalHeapPtr2.Get()));
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            IntPtr dataPtr = scopedGlobalHeapPtr2.Get();
            dirEntries = new PS3TMAPI.DirEntry[num];
            int num2 = 0;
            while ((long)num2 < (long)((ulong)num))
            {
                dataPtr = PS3TMAPI.DirEntryMarshalHelper(dataPtr, ref dirEntries[num2]);
                num2++;
            }
        }
        return snresult;
    }

    // Token: 0x06000220 RID: 544 RVA: 0x00007110 File Offset: 0x00005310
    private static IntPtr DirEntryExMarshalHelper(IntPtr dataPtr, ref PS3TMAPI.DirEntryEx dirEntryEx)
    {
        PS3TMAPI.DirEntryExPriv dirEntryExPriv = default(PS3TMAPI.DirEntryExPriv);
        dataPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.DirEntryExPriv>(dataPtr, ref dirEntryExPriv);
        dirEntryEx.Type = (PS3TMAPI.DirEntryType)dirEntryExPriv.Type;
        dirEntryEx.Mode = dirEntryExPriv.Mode;
        dirEntryEx.AccessTimeUTC = dirEntryExPriv.AccessTimeUTC;
        dirEntryEx.ModifiedTimeUTC = dirEntryExPriv.ModifiedTimeUTC;
        dirEntryEx.CreateTimeUTC = dirEntryExPriv.CreateTimeUTC;
        dirEntryEx.Size = dirEntryExPriv.Size;
        dirEntryEx.Name = PS3TMAPI.Utf8FixedSizeByteArrayToString(dirEntryExPriv.Name);
        return dataPtr;
    }

    // Token: 0x06000221 RID: 545
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDirectoryListEx")]
    private static extern PS3TMAPI.SNRESULT GetDirectoryListExX86(int target, IntPtr dirPtr, ref uint numDirEntries, IntPtr dirEntryListEx, ref PS3TMAPI.TargetTimezone timeZone);

    // Token: 0x06000222 RID: 546
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetDirectoryListEx")]
    private static extern PS3TMAPI.SNRESULT GetDirectoryListExX64(int target, IntPtr dirPtr, ref uint numDirEntries, IntPtr dirEntryListEx, ref PS3TMAPI.TargetTimezone timeZone);

    // Token: 0x06000223 RID: 547 RVA: 0x00007190 File Offset: 0x00005390
    public static PS3TMAPI.SNRESULT GetDirectoryListEx(int target, string directory, out PS3TMAPI.DirEntryEx[] dirEntries, out PS3TMAPI.TargetTimezone timeZone)
    {
        dirEntries = null;
        timeZone = default(PS3TMAPI.TargetTimezone);
        uint num = 0U;
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(directory));
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.GetDirectoryListExX86(target, scopedGlobalHeapPtr.Get(), ref num, IntPtr.Zero, ref timeZone) : PS3TMAPI.GetDirectoryListExX64(target, scopedGlobalHeapPtr.Get(), ref num, IntPtr.Zero, ref timeZone);
        if (PS3TMAPI.FAILED(snresult))
        {
            return snresult;
        }
        int cb = (int)(num * (uint)Marshal.SizeOf(typeof(PS3TMAPI.DirEntryExPriv)));
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal(cb));
        snresult = (PS3TMAPI.Is32Bit() ? PS3TMAPI.GetDirectoryListExX86(target, scopedGlobalHeapPtr.Get(), ref num, scopedGlobalHeapPtr2.Get(), ref timeZone) : PS3TMAPI.GetDirectoryListExX64(target, scopedGlobalHeapPtr.Get(), ref num, scopedGlobalHeapPtr2.Get(), ref timeZone));
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            IntPtr dataPtr = scopedGlobalHeapPtr2.Get();
            dirEntries = new PS3TMAPI.DirEntryEx[num];
            int num2 = 0;
            while ((long)num2 < (long)((ulong)num))
            {
                dataPtr = PS3TMAPI.DirEntryExMarshalHelper(dataPtr, ref dirEntries[num2]);
                num2++;
            }
        }
        return snresult;
    }

    // Token: 0x06000224 RID: 548
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3MakeDirectory")]
    private static extern PS3TMAPI.SNRESULT MakeDirectoryX86(int target, IntPtr directory, uint mode);

    // Token: 0x06000225 RID: 549
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3MakeDirectory")]
    private static extern PS3TMAPI.SNRESULT MakeDirectoryX64(int target, IntPtr directory, uint mode);

    // Token: 0x06000226 RID: 550 RVA: 0x00007288 File Offset: 0x00005488
    public static PS3TMAPI.SNRESULT MakeDirectory(int target, string directory, uint mode)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(directory));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.MakeDirectoryX64(target, scopedGlobalHeapPtr.Get(), mode);
        }
        return PS3TMAPI.MakeDirectoryX86(target, scopedGlobalHeapPtr.Get(), mode);
    }

    // Token: 0x06000227 RID: 551
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Delete")]
    private static extern PS3TMAPI.SNRESULT DeleteFileX86(int target, IntPtr path);

    // Token: 0x06000228 RID: 552
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Delete")]
    private static extern PS3TMAPI.SNRESULT DeleteFileX64(int target, IntPtr path);

    // Token: 0x06000229 RID: 553 RVA: 0x000072C4 File Offset: 0x000054C4
    public static PS3TMAPI.SNRESULT DeleteFile(int target, string path)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(path));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.DeleteFileX64(target, scopedGlobalHeapPtr.Get());
        }
        return PS3TMAPI.DeleteFileX86(target, scopedGlobalHeapPtr.Get());
    }

    // Token: 0x0600022A RID: 554
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3DeleteEx")]
    private static extern PS3TMAPI.SNRESULT DeleteFileExX86(int target, IntPtr path, uint msTimeout);

    // Token: 0x0600022B RID: 555
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3DeleteEx")]
    private static extern PS3TMAPI.SNRESULT DeleteFileExX64(int target, IntPtr path, uint msTimeout);

    // Token: 0x0600022C RID: 556 RVA: 0x00007300 File Offset: 0x00005500
    public static PS3TMAPI.SNRESULT DeleteFileEx(int target, string path, uint msTimeout)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(path));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.DeleteFileExX64(target, scopedGlobalHeapPtr.Get(), msTimeout);
        }
        return PS3TMAPI.DeleteFileExX86(target, scopedGlobalHeapPtr.Get(), msTimeout);
    }

    // Token: 0x0600022D RID: 557
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Rename")]
    private static extern PS3TMAPI.SNRESULT RenameFileX86(int target, IntPtr source, IntPtr dest);

    // Token: 0x0600022E RID: 558
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3Rename")]
    private static extern PS3TMAPI.SNRESULT RenameFileX64(int target, IntPtr source, IntPtr dest);

    // Token: 0x0600022F RID: 559 RVA: 0x0000733C File Offset: 0x0000553C
    public static PS3TMAPI.SNRESULT RenameFile(int target, string source, string dest)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(source));
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(dest));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.RenameFileX64(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get());
        }
        return PS3TMAPI.RenameFileX86(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get());
    }

    // Token: 0x06000230 RID: 560
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3DownloadFile")]
    private static extern PS3TMAPI.SNRESULT DownloadFileX86(int target, IntPtr source, IntPtr dest, out uint transactionId);

    // Token: 0x06000231 RID: 561
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3DownloadFile")]
    private static extern PS3TMAPI.SNRESULT DownloadFileX64(int target, IntPtr source, IntPtr dest, out uint transactionId);

    // Token: 0x06000232 RID: 562 RVA: 0x00007390 File Offset: 0x00005590
    public static PS3TMAPI.SNRESULT DownloadFile(int target, string source, string dest, out uint txID)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(source));
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(dest));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.DownloadFileX64(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get(), out txID);
        }
        return PS3TMAPI.DownloadFileX86(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get(), out txID);
    }

    // Token: 0x06000233 RID: 563
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3DownloadDirectory")]
    private static extern PS3TMAPI.SNRESULT DownloadDirectoryX86(int target, IntPtr source, IntPtr dest, out uint lastTransactionId);

    // Token: 0x06000234 RID: 564
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3DownloadDirectory")]
    private static extern PS3TMAPI.SNRESULT DownloadDirectoryX64(int target, IntPtr source, IntPtr dest, out uint lastTransactionId);

    // Token: 0x06000235 RID: 565 RVA: 0x000073E4 File Offset: 0x000055E4
    public static PS3TMAPI.SNRESULT DownloadDirectory(int target, string source, string dest, out uint lastTxID)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(source));
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(dest));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.DownloadDirectoryX64(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get(), out lastTxID);
        }
        return PS3TMAPI.DownloadDirectoryX86(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get(), out lastTxID);
    }

    // Token: 0x06000236 RID: 566
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UploadDirectory")]
    private static extern PS3TMAPI.SNRESULT UploadDirectoryX86(int target, IntPtr source, IntPtr dest, out uint lastTransactionId);

    // Token: 0x06000237 RID: 567
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UploadDirectory")]
    private static extern PS3TMAPI.SNRESULT UploadDirectoryX64(int target, IntPtr source, IntPtr dest, out uint lastTransactionId);

    // Token: 0x06000238 RID: 568 RVA: 0x00007438 File Offset: 0x00005638
    public static PS3TMAPI.SNRESULT UploadDirectory(int target, string source, string dest, out uint lastTxID)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(source));
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(dest));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.UploadDirectoryX64(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get(), out lastTxID);
        }
        return PS3TMAPI.UploadDirectoryX86(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get(), out lastTxID);
    }

    // Token: 0x06000239 RID: 569
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StatTargetFile")]
    private static extern PS3TMAPI.SNRESULT StatTargetFileX86(int target, IntPtr file, IntPtr dirEntry);

    // Token: 0x0600023A RID: 570
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StatTargetFile")]
    private static extern PS3TMAPI.SNRESULT StatTargetFileX64(int target, IntPtr file, IntPtr dirEntry);

    // Token: 0x0600023B RID: 571 RVA: 0x0000748C File Offset: 0x0000568C
    public static PS3TMAPI.SNRESULT StatTargetFile(int target, string file, out PS3TMAPI.DirEntry dirEntry)
    {
        dirEntry = default(PS3TMAPI.DirEntry);
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(file));
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PS3TMAPI.DirEntryPriv))));
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.StatTargetFileX86(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get()) : PS3TMAPI.StatTargetFileX64(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get());
        PS3TMAPI.DirEntryMarshalHelper(scopedGlobalHeapPtr2.Get(), ref dirEntry);
        return result;
    }

    // Token: 0x0600023C RID: 572
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StatTargetFileEx")]
    private static extern PS3TMAPI.SNRESULT StatTargetFileExX86(int target, IntPtr file, IntPtr dirEntry, out PS3TMAPI.TargetTimezone timeZone);

    // Token: 0x0600023D RID: 573
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StatTargetFileEx")]
    private static extern PS3TMAPI.SNRESULT StatTargetFileExX64(int target, IntPtr file, IntPtr dirEntry, out PS3TMAPI.TargetTimezone timeZone);

    // Token: 0x0600023E RID: 574 RVA: 0x00007504 File Offset: 0x00005704
    public static PS3TMAPI.SNRESULT StatTargetFileEx(int target, string file, out PS3TMAPI.DirEntryEx dirEntryEx, out PS3TMAPI.TargetTimezone timeZone)
    {
        dirEntryEx = default(PS3TMAPI.DirEntryEx);
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(file));
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr2 = new PS3TMAPI.ScopedGlobalHeapPtr(Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PS3TMAPI.DirEntryExPriv))));
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.StatTargetFileExX86(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get(), out timeZone) : PS3TMAPI.StatTargetFileExX64(target, scopedGlobalHeapPtr.Get(), scopedGlobalHeapPtr2.Get(), out timeZone);
        PS3TMAPI.DirEntryExMarshalHelper(scopedGlobalHeapPtr2.Get(), ref dirEntryEx);
        return result;
    }

    // Token: 0x0600023F RID: 575
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CHMod")]
    private static extern PS3TMAPI.SNRESULT CHModX86(int target, IntPtr filePath, uint mode);

    // Token: 0x06000240 RID: 576
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CHMod")]
    private static extern PS3TMAPI.SNRESULT CHModX64(int target, IntPtr filePath, uint mode);

    // Token: 0x06000241 RID: 577 RVA: 0x0000757C File Offset: 0x0000577C
    public static PS3TMAPI.SNRESULT ChMod(int target, string filePath, PS3TMAPI.ChModFilePermission mode)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(filePath));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.CHModX64(target, scopedGlobalHeapPtr.Get(), (uint)mode);
        }
        return PS3TMAPI.CHModX86(target, scopedGlobalHeapPtr.Get(), (uint)mode);
    }

    // Token: 0x06000242 RID: 578
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetFileTime")]
    private static extern PS3TMAPI.SNRESULT SetFileTimeX86(int target, IntPtr filePath, ulong accessTime, ulong modifiedTime);

    // Token: 0x06000243 RID: 579
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetFileTime")]
    private static extern PS3TMAPI.SNRESULT SetFileTimeX64(int target, IntPtr filePath, ulong accessTime, ulong modifiedTime);

    // Token: 0x06000244 RID: 580 RVA: 0x000075B8 File Offset: 0x000057B8
    public static PS3TMAPI.SNRESULT SetFileTime(int target, string filePath, ulong accessTime, ulong modifiedTime)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(filePath));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetFileTimeX64(target, scopedGlobalHeapPtr.Get(), accessTime, modifiedTime);
        }
        return PS3TMAPI.SetFileTimeX86(target, scopedGlobalHeapPtr.Get(), accessTime, modifiedTime);
    }

    // Token: 0x06000245 RID: 581
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3InstallGameEx")]
    private static extern PS3TMAPI.SNRESULT InstallGameExX86(int target, IntPtr paramSfoPath, out IntPtr titleId, out IntPtr targetPath, out uint txId);

    // Token: 0x06000246 RID: 582
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3InstallGameEx")]
    private static extern PS3TMAPI.SNRESULT InstallGameExX64(int target, IntPtr paramSfoPath, out IntPtr titleId, out IntPtr targetPath, out uint txId);

    // Token: 0x06000247 RID: 583 RVA: 0x000075F8 File Offset: 0x000057F8
    public static PS3TMAPI.SNRESULT InstallGameEx(int target, string paramSFOPath, out string titleID, out string targetPath, out uint txID)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(paramSFOPath));
        IntPtr utf8Ptr;
        IntPtr utf8Ptr2;
        PS3TMAPI.SNRESULT result = PS3TMAPI.Is32Bit() ? PS3TMAPI.InstallGameExX86(target, scopedGlobalHeapPtr.Get(), out utf8Ptr, out utf8Ptr2, out txID) : PS3TMAPI.InstallGameExX64(target, scopedGlobalHeapPtr.Get(), out utf8Ptr, out utf8Ptr2, out txID);
        titleID = PS3TMAPI.Utf8ToString(utf8Ptr, uint.MaxValue);
        targetPath = PS3TMAPI.Utf8ToString(utf8Ptr2, uint.MaxValue);
        return result;
    }

    // Token: 0x06000248 RID: 584
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3FormatHDD")]
    private static extern PS3TMAPI.SNRESULT FormatHDDX86(int target, uint initRegistry);

    // Token: 0x06000249 RID: 585
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3FormatHDD")]
    private static extern PS3TMAPI.SNRESULT FormatHDDX64(int target, uint initRegistry);

    // Token: 0x0600024A RID: 586 RVA: 0x00007652 File Offset: 0x00005852
    public static PS3TMAPI.SNRESULT FormatHDD(int target, uint initRegistry)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.FormatHDDX64(target, initRegistry);
        }
        return PS3TMAPI.FormatHDDX86(target, initRegistry);
    }

    // Token: 0x0600024B RID: 587
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UninstallGame")]
    private static extern PS3TMAPI.SNRESULT UninstallGameX86(int target, IntPtr gameDirectory);

    // Token: 0x0600024C RID: 588
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UninstallGame")]
    private static extern PS3TMAPI.SNRESULT UninstallGameX64(int target, IntPtr gameDirectory);

    // Token: 0x0600024D RID: 589 RVA: 0x0000766C File Offset: 0x0000586C
    public static PS3TMAPI.SNRESULT UninstallGame(int target, string gameDirectory)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(gameDirectory));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.UninstallGameX64(target, scopedGlobalHeapPtr.Get());
        }
        return PS3TMAPI.UninstallGameX86(target, scopedGlobalHeapPtr.Get());
    }

    // Token: 0x0600024E RID: 590
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3WaitForFileTransfer")]
    private static extern PS3TMAPI.SNRESULT WaitForFileTransferX86(int target, uint txId, out PS3TMAPI.FileTransferNotificationType notificationType, uint msTimeout);

    // Token: 0x0600024F RID: 591
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3WaitForFileTransfer")]
    private static extern PS3TMAPI.SNRESULT WaitForFileTransferX64(int target, uint txId, out PS3TMAPI.FileTransferNotificationType notificationType, uint msTimeout);

    // Token: 0x06000250 RID: 592 RVA: 0x000076A8 File Offset: 0x000058A8
    public static PS3TMAPI.SNRESULT WaitForFileTransfer(int target, uint txID, out PS3TMAPI.FileTransferNotificationType notificationType, uint msTimeout)
    {
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.WaitForFileTransferX86(target, txID, out notificationType, msTimeout) : PS3TMAPI.WaitForFileTransferX64(target, txID, out notificationType, msTimeout);
    }

    // Token: 0x06000251 RID: 593
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3FSGetFreeSize")]
    private static extern PS3TMAPI.SNRESULT FSGetFreeSizeX86(int target, IntPtr fsDir, out uint blockSize, out ulong freeBlockCount);

    // Token: 0x06000252 RID: 594
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3FSGetFreeSize")]
    private static extern PS3TMAPI.SNRESULT FSGetFreeSizeX64(int target, IntPtr fsDir, out uint blockSize, out ulong freeBlockCount);

    // Token: 0x06000253 RID: 595 RVA: 0x000076D4 File Offset: 0x000058D4
    public static PS3TMAPI.SNRESULT FSGetFreeSize(int target, string fsDir, out uint blockSize, out ulong freeBlockCount)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(fsDir));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.FSGetFreeSizeX64(target, scopedGlobalHeapPtr.Get(), out blockSize, out freeBlockCount);
        }
        return PS3TMAPI.FSGetFreeSizeX86(target, scopedGlobalHeapPtr.Get(), out blockSize, out freeBlockCount);
    }

    // Token: 0x06000254 RID: 596
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetLogOptions")]
    private static extern PS3TMAPI.SNRESULT GetLogOptionsX86(out PS3TMAPI.LogCategory category);

    // Token: 0x06000255 RID: 597
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3GetLogOptions")]
    private static extern PS3TMAPI.SNRESULT GetLogOptionsX64(out PS3TMAPI.LogCategory category);

    // Token: 0x06000256 RID: 598 RVA: 0x00007714 File Offset: 0x00005914
    public static PS3TMAPI.SNRESULT GetLogOptions(out PS3TMAPI.LogCategory category)
    {
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.GetLogOptionsX86(out category) : PS3TMAPI.GetLogOptionsX64(out category);
    }

    // Token: 0x06000257 RID: 599
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetLogOptions")]
    private static extern PS3TMAPI.SNRESULT SetLogOptionsX86(PS3TMAPI.LogCategory category);

    // Token: 0x06000258 RID: 600
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetLogOptions")]
    private static extern PS3TMAPI.SNRESULT SetLogOptionsX64(PS3TMAPI.LogCategory category);

    // Token: 0x06000259 RID: 601 RVA: 0x00007738 File Offset: 0x00005938
    public static PS3TMAPI.SNRESULT SetLogOptions(PS3TMAPI.LogCategory category)
    {
        return PS3TMAPI.Is32Bit() ? PS3TMAPI.SetLogOptionsX86(category) : PS3TMAPI.SetLogOptionsX64(category);
    }

    // Token: 0x0600025A RID: 602
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnableInternalKick")]
    private static extern PS3TMAPI.SNRESULT EnableInternalKickX86(bool enable);

    // Token: 0x0600025B RID: 603
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3EnableInternalKick")]
    private static extern PS3TMAPI.SNRESULT EnableInternalKickX64(bool enable);

    // Token: 0x0600025C RID: 604 RVA: 0x0000775C File Offset: 0x0000595C
    public static PS3TMAPI.SNRESULT EnableInternalKick(bool bEnable)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.EnableInternalKickX64(bEnable);
        }
        return PS3TMAPI.EnableInternalKickX86(bEnable);
    }

    // Token: 0x0600025D RID: 605
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessOfflineFileTrace")]
    private static extern PS3TMAPI.SNRESULT ProcessOfflineFileTraceX86(int target, IntPtr path);

    // Token: 0x0600025E RID: 606
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ProcessOfflineFileTrace")]
    private static extern PS3TMAPI.SNRESULT ProcessOfflineFileTraceX64(int target, IntPtr path);

    // Token: 0x0600025F RID: 607 RVA: 0x00007774 File Offset: 0x00005974
    public static PS3TMAPI.SNRESULT ProcessOfflineFileTrace(int target, string path)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(path));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ProcessOfflineFileTraceX64(target, scopedGlobalHeapPtr.Get());
        }
        return PS3TMAPI.ProcessOfflineFileTraceX86(target, scopedGlobalHeapPtr.Get());
    }

    // Token: 0x06000260 RID: 608
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3BDTransferImage")]
    private static extern PS3TMAPI.SNRESULT BDTransferImageX86(int target, IntPtr sourceFileName, string destinationDevice, out uint transactionId);

    // Token: 0x06000261 RID: 609
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3BDTransferImage")]
    private static extern PS3TMAPI.SNRESULT BDTransferImageX64(int target, IntPtr sourceFileName, string destinationDevice, out uint transactionId);

    // Token: 0x06000262 RID: 610 RVA: 0x000077B0 File Offset: 0x000059B0
    public static PS3TMAPI.SNRESULT BDTransferImage(int target, string sourceFileName, string destinationDevice, out uint transactionId)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(sourceFileName));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.BDTransferImageX64(target, scopedGlobalHeapPtr.Get(), destinationDevice, out transactionId);
        }
        return PS3TMAPI.BDTransferImageX86(target, scopedGlobalHeapPtr.Get(), destinationDevice, out transactionId);
    }

    // Token: 0x06000263 RID: 611
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3BDInsert")]
    private static extern PS3TMAPI.SNRESULT BDInsertX86(int target, string deviceName);

    // Token: 0x06000264 RID: 612
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3BDInsert")]
    private static extern PS3TMAPI.SNRESULT BDInsertX64(int target, string deviceName);

    // Token: 0x06000265 RID: 613 RVA: 0x000077ED File Offset: 0x000059ED
    public static PS3TMAPI.SNRESULT BDInsert(int target, string deviceName)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.BDInsertX64(target, deviceName);
        }
        return PS3TMAPI.BDInsertX86(target, deviceName);
    }

    // Token: 0x06000266 RID: 614
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3BDEject")]
    private static extern PS3TMAPI.SNRESULT BDEjectX86(int target, string deviceName);

    // Token: 0x06000267 RID: 615
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3BDEject")]
    private static extern PS3TMAPI.SNRESULT BDEjectX64(int target, string deviceName);

    // Token: 0x06000268 RID: 616 RVA: 0x00007805 File Offset: 0x00005A05
    public static PS3TMAPI.SNRESULT BDEject(int target, string deviceName)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.BDEjectX64(target, deviceName);
        }
        return PS3TMAPI.BDEjectX86(target, deviceName);
    }

    // Token: 0x06000269 RID: 617
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3BDFormat")]
    private static extern PS3TMAPI.SNRESULT BDFormatX86(int target, string deviceName, uint formatMode);

    // Token: 0x0600026A RID: 618
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3BDFormat")]
    private static extern PS3TMAPI.SNRESULT BDFormatX64(int target, string deviceName, uint formatMode);

    // Token: 0x0600026B RID: 619 RVA: 0x0000781D File Offset: 0x00005A1D
    public static PS3TMAPI.SNRESULT BDFormat(int target, string deviceName, uint formatMode)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.BDFormatX64(target, deviceName, formatMode);
        }
        return PS3TMAPI.BDFormatX86(target, deviceName, formatMode);
    }

    // Token: 0x0600026C RID: 620
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3BDQuery")]
    private static extern PS3TMAPI.SNRESULT BDQueryX86(int target, string deviceName, ref PS3TMAPI.BDInfoPriv infoPriv);

    // Token: 0x0600026D RID: 621
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3BDQuery")]
    private static extern PS3TMAPI.SNRESULT BDQueryX64(int target, string deviceName, ref PS3TMAPI.BDInfoPriv infoPriv);

    // Token: 0x0600026E RID: 622 RVA: 0x00007838 File Offset: 0x00005A38
    public static PS3TMAPI.SNRESULT BDQuery(int target, string deviceName, ref PS3TMAPI.BDInfo info)
    {
        PS3TMAPI.BDInfoPriv bdinfoPriv = default(PS3TMAPI.BDInfoPriv);
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.BDQueryX86(target, deviceName, ref bdinfoPriv) : PS3TMAPI.BDQueryX64(target, deviceName, ref bdinfoPriv);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            info.bdemu_data_size = bdinfoPriv.bdemu_data_size;
            info.bdemu_total_entry = bdinfoPriv.bdemu_total_entry;
            info.bdemu_selected_index = bdinfoPriv.bdemu_selected_index;
            info.image_index = bdinfoPriv.image_index;
            info.image_type = bdinfoPriv.image_type;
            info.image_file_name = PS3TMAPI.Utf8FixedSizeByteArrayToString(bdinfoPriv.image_file_name);
            info.image_file_size = bdinfoPriv.image_file_size;
            info.image_product_code = PS3TMAPI.Utf8FixedSizeByteArrayToString(bdinfoPriv.image_product_code);
            info.image_producer = PS3TMAPI.Utf8FixedSizeByteArrayToString(bdinfoPriv.image_producer);
            info.image_author = PS3TMAPI.Utf8FixedSizeByteArrayToString(bdinfoPriv.image_author);
            info.image_date = PS3TMAPI.Utf8FixedSizeByteArrayToString(bdinfoPriv.image_date);
            info.image_sector_layer0 = bdinfoPriv.image_sector_layer0;
            info.image_sector_layer1 = bdinfoPriv.image_sector_layer1;
            info.image_memorandum = PS3TMAPI.Utf8FixedSizeByteArrayToString(bdinfoPriv.image_memorandum);
        }
        return snresult;
    }

    // Token: 0x0600026F RID: 623
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterTargetEventHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterTargetEventHandlerX86(int target, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x06000270 RID: 624
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3RegisterTargetEventHandler")]
    private static extern PS3TMAPI.SNRESULT RegisterTargetEventHandlerX64(int target, PS3TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    // Token: 0x06000271 RID: 625 RVA: 0x0000794C File Offset: 0x00005B4C
    public static PS3TMAPI.SNRESULT RegisterTargetEventHandler(int target, PS3TMAPI.TargetEventCallback callback, ref object userData)
    {
        if (callback == null)
        {
            return PS3TMAPI.SNRESULT.SN_E_BAD_PARAM;
        }
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.RegisterTargetEventHandlerX86(target, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3TMAPI.RegisterTargetEventHandlerX64(target, PS3TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            PS3TMAPI.TargetCallbackAndUserData targetCallbackAndUserData = new PS3TMAPI.TargetCallbackAndUserData();
            targetCallbackAndUserData.m_callback = callback;
            targetCallbackAndUserData.m_userData = userData;
            if (PS3TMAPI.ms_userTargetCallbacks == null)
            {
                PS3TMAPI.ms_userTargetCallbacks = new Dictionary<int, PS3TMAPI.TargetCallbackAndUserData>(1);
            }
            PS3TMAPI.ms_userTargetCallbacks[target] = targetCallbackAndUserData;
        }
        return snresult;
    }

    // Token: 0x06000272 RID: 626
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CancelTargetEvents")]
    private static extern PS3TMAPI.SNRESULT CancelTargetEventsX86(int target);

    // Token: 0x06000273 RID: 627
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3CancelTargetEvents")]
    private static extern PS3TMAPI.SNRESULT CancelTargetEventsX64(int target);

    // Token: 0x06000274 RID: 628 RVA: 0x000079C8 File Offset: 0x00005BC8
    public static PS3TMAPI.SNRESULT CancelTargetEvents(int target)
    {
        PS3TMAPI.SNRESULT snresult = PS3TMAPI.Is32Bit() ? PS3TMAPI.CancelTargetEventsX86(target) : PS3TMAPI.CancelTargetEventsX64(target);
        if (PS3TMAPI.SUCCEEDED(snresult))
        {
            if (PS3TMAPI.ms_userTargetCallbacks == null)
            {
                return PS3TMAPI.SNRESULT.SN_E_ERROR;
            }
            PS3TMAPI.ms_userTargetCallbacks.Remove(target);
        }
        return snresult;
    }

    // Token: 0x06000275 RID: 629 RVA: 0x00007A10 File Offset: 0x00005C10
    private static void MarshalTargetEvent(int target, uint param, PS3TMAPI.SNRESULT result, uint length, IntPtr data)
    {
        List<PS3TMAPI.TargetEvent> list = new List<PS3TMAPI.TargetEvent>();
        uint num = length;
        while (num > 0U)
        {
            PS3TMAPI.TargetEvent item = default(PS3TMAPI.TargetEvent);
            PS3TMAPI.TargetEventHdrPriv targetEventHdrPriv = default(PS3TMAPI.TargetEventHdrPriv);
            IntPtr intPtr = data;
            intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.TargetEventHdrPriv>(intPtr, ref targetEventHdrPriv);
            uint size = targetEventHdrPriv.Size;
            item.TargetID = targetEventHdrPriv.TargetID;
            item.Type = (PS3TMAPI.TargetEventType)targetEventHdrPriv.EventType;
            item.Type.GetType();
            PS3TMAPI.TargetEventType type = item.Type;
            switch (type)
            {
                case PS3TMAPI.TargetEventType.UnitStatusChange:
                    {
                        item.EventData = default(PS3TMAPI.TargetEventData);
                        PS3TMAPI.TGTEventUnitStatusChangeDataPriv tgteventUnitStatusChangeDataPriv = default(PS3TMAPI.TGTEventUnitStatusChangeDataPriv);
                        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.TGTEventUnitStatusChangeDataPriv>(intPtr, ref tgteventUnitStatusChangeDataPriv);
                        item.EventData.UnitStatusChangeData.Unit = (PS3TMAPI.UnitType)tgteventUnitStatusChangeDataPriv.Unit;
                        item.EventData.UnitStatusChangeData.Status = (PS3TMAPI.UnitStatus)tgteventUnitStatusChangeDataPriv.Status;
                        break;
                    }
                case PS3TMAPI.TargetEventType.ResetStarted:
                case PS3TMAPI.TargetEventType.ResetEnd:
                case (PS3TMAPI.TargetEventType)3U:
                case PS3TMAPI.TargetEventType.ModuleDoneRemove:
                case PS3TMAPI.TargetEventType.ModuleDoneResident:
                case PS3TMAPI.TargetEventType.ModuleStoppedRemove:
                case PS3TMAPI.TargetEventType.PowerStatusChange:
                case PS3TMAPI.TargetEventType.TTYStreamAdded:
                case PS3TMAPI.TargetEventType.TTYStreamDeleted:
                case (PS3TMAPI.TargetEventType)14U:
                case (PS3TMAPI.TargetEventType)15U:
                    break;
                case PS3TMAPI.TargetEventType.Details:
                    item.EventData = default(PS3TMAPI.TargetEventData);
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(intPtr, ref item.EventData.DetailsData.Flags);
                    break;
                case PS3TMAPI.TargetEventType.ModuleLoad:
                case PS3TMAPI.TargetEventType.ModuleRunning:
                case PS3TMAPI.TargetEventType.ModuleStopped:
                    item.EventData = default(PS3TMAPI.TargetEventData);
                    intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.TGTEventModuleEventData>(intPtr, ref item.EventData.ModuleEventData);
                    break;
                case PS3TMAPI.TargetEventType.BDIsotransferStarted:
                case PS3TMAPI.TargetEventType.BDIsotransferFinished:
                case PS3TMAPI.TargetEventType.BDFormatStarted:
                case PS3TMAPI.TargetEventType.BDFormatFinished:
                case PS3TMAPI.TargetEventType.BDMountStarted:
                case PS3TMAPI.TargetEventType.BDMountFinished:
                case PS3TMAPI.TargetEventType.BDUnmountStarted:
                case PS3TMAPI.TargetEventType.BDUnmountFinished:
                    {
                        item.EventData = default(PS3TMAPI.TargetEventData);
                        PS3TMAPI.TGTEventBDDataPriv tgteventBDDataPriv = default(PS3TMAPI.TGTEventBDDataPriv);
                        intPtr = PS3TMAPI.ReadDataFromUnmanagedIncPtr<PS3TMAPI.TGTEventBDDataPriv>(intPtr, ref tgteventBDDataPriv);
                        item.EventData.BdData.Source = PS3TMAPI.Utf8FixedSizeByteArrayToString(tgteventBDDataPriv.Source);
                        item.EventData.BdData.Destination = PS3TMAPI.Utf8FixedSizeByteArrayToString(tgteventBDDataPriv.Destination);
                        item.EventData.BdData.Result = tgteventBDDataPriv.Result;
                        break;
                    }
                default:
                    if (type == (PS3TMAPI.TargetEventType)2147483648U)
                    {
                        item.TargetSpecific = PS3TMAPI.MarshalTargetSpecificEvent(size, intPtr);
                    }
                    break;
            }
            list.Add(item);
            num -= size;
            data = new IntPtr(data.ToInt64() + (long)((ulong)size));
        }
        if (PS3TMAPI.ms_userTargetCallbacks == null)
        {
            return;
        }
        PS3TMAPI.ms_userTargetCallbacks[target].m_callback(target, result, list.ToArray(), PS3TMAPI.ms_userTargetCallbacks[target].m_userData);
    }

    // Token: 0x06000276 RID: 630 RVA: 0x00007C88 File Offset: 0x00005E88
    private static PS3TMAPI.TargetSpecificEvent MarshalTargetSpecificEvent(uint eventSize, IntPtr data)
    {
        PS3TMAPI.TargetSpecificEvent result = default(PS3TMAPI.TargetSpecificEvent);
        PS3TMAPI.TargetSpecificData data2 = default(PS3TMAPI.TargetSpecificData);
        uint num = 0U;
        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref result.CommandID);
        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref result.RequestID);
        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref num);
        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref result.ProcessID);
        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref result.Result);
        int num2 = Marshal.ReadInt32(data, 0);
        data = new IntPtr(data.ToInt64() + (long)Marshal.SizeOf(num2));
        data2.Type = (PS3TMAPI.TargetSpecificEventType)num2;
        int num3 = 20;
        PS3TMAPI.TargetSpecificEventType type = data2.Type;
        if (type <= PS3TMAPI.TargetSpecificEventType.DAInitialised)
        {
            if (type <= PS3TMAPI.TargetSpecificEventType.SPUThreadStopEx)
            {
                switch (type)
                {
                    case PS3TMAPI.TargetSpecificEventType.ProcessCreate:
                        data2.PPUProcessCreate = default(PS3TMAPI.PPUProcessCreateData);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.PPUProcessCreate.ParentProcessID);
                        if ((ulong)num - (ulong)((long)num3) - 4UL > 0UL)
                        {
                            data2.PPUProcessCreate.Filename = PS3TMAPI.Utf8ToString(data, uint.MaxValue);
                        }
                        break;
                    case PS3TMAPI.TargetSpecificEventType.ProcessExit:
                        data2.PPUProcessExit = default(PS3TMAPI.PPUProcessExitData);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUProcessExit.ExitCode);
                        break;
                    case PS3TMAPI.TargetSpecificEventType.ProcessKill:
                    case PS3TMAPI.TargetSpecificEventType.ProcessExitSpawn:
                    case (PS3TMAPI.TargetSpecificEventType)4U:
                    case (PS3TMAPI.TargetSpecificEventType)5U:
                    case (PS3TMAPI.TargetSpecificEventType)6U:
                    case (PS3TMAPI.TargetSpecificEventType)7U:
                    case (PS3TMAPI.TargetSpecificEventType)8U:
                    case (PS3TMAPI.TargetSpecificEventType)9U:
                    case (PS3TMAPI.TargetSpecificEventType)10U:
                    case (PS3TMAPI.TargetSpecificEventType)11U:
                    case (PS3TMAPI.TargetSpecificEventType)12U:
                    case (PS3TMAPI.TargetSpecificEventType)13U:
                    case (PS3TMAPI.TargetSpecificEventType)14U:
                    case (PS3TMAPI.TargetSpecificEventType)15U:
                    case (PS3TMAPI.TargetSpecificEventType)29U:
                    case (PS3TMAPI.TargetSpecificEventType)30U:
                    case (PS3TMAPI.TargetSpecificEventType)31U:
                        break;
                    case PS3TMAPI.TargetSpecificEventType.PPUExcTrap:
                    case PS3TMAPI.TargetSpecificEventType.PPUExcPrevInt:
                    case PS3TMAPI.TargetSpecificEventType.PPUExcIllInst:
                    case PS3TMAPI.TargetSpecificEventType.PPUExcTextHtabMiss:
                    case PS3TMAPI.TargetSpecificEventType.PPUExcTextSlbMiss:
                    case PS3TMAPI.TargetSpecificEventType.PPUExcDataHtabMiss:
                    case PS3TMAPI.TargetSpecificEventType.PPUExcFloat:
                    case PS3TMAPI.TargetSpecificEventType.PPUExcDataSlbMiss:
                    case PS3TMAPI.TargetSpecificEventType.PPUExcDabrMatch:
                    case PS3TMAPI.TargetSpecificEventType.PPUExcStop:
                    case PS3TMAPI.TargetSpecificEventType.PPUExcStopInit:
                        data2.PPUException = default(PS3TMAPI.PPUExceptionData);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUException.ThreadID);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.PPUException.HWThreadNumber);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUException.PC);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUException.SP);
                        break;
                    case PS3TMAPI.TargetSpecificEventType.PPUExcAlignment:
                        data2.PPUAlignmentException = default(PS3TMAPI.PPUAlignmentExceptionData);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUAlignmentException.ThreadID);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.PPUAlignmentException.HWThreadNumber);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUAlignmentException.DSISR);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUAlignmentException.DAR);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUAlignmentException.PC);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUAlignmentException.SP);
                        break;
                    case PS3TMAPI.TargetSpecificEventType.PPUExcDataMAT:
                        data2.PPUDataMatException = default(PS3TMAPI.PPUDataMatExceptionData);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUDataMatException.ThreadID);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.PPUDataMatException.HWThreadNumber);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUDataMatException.DSISR);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUDataMatException.DAR);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUDataMatException.PC);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUDataMatException.SP);
                        break;
                    case PS3TMAPI.TargetSpecificEventType.PPUThreadCreate:
                        data2.PPUThreadCreate = default(PS3TMAPI.PPUThreadCreateData);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUThreadCreate.ThreadID);
                        break;
                    case PS3TMAPI.TargetSpecificEventType.PPUThreadExit:
                        data2.PPUThreadExit = default(PS3TMAPI.PPUThreadExitData);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PPUThreadExit.ThreadID);
                        break;
                    default:
                        switch (type)
                        {
                            case PS3TMAPI.TargetSpecificEventType.SPUThreadStart:
                                data2.SPUThreadStart = default(PS3TMAPI.SPUThreadStartData);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.SPUThreadStart.ThreadGroupID);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.SPUThreadStart.ThreadID);
                                if ((ulong)num - (ulong)((long)num3) - 8UL > 0UL)
                                {
                                    data2.SPUThreadStart.ElfFilename = PS3TMAPI.Utf8ToString(data, uint.MaxValue);
                                }
                                break;
                            case PS3TMAPI.TargetSpecificEventType.SPUThreadStop:
                            case PS3TMAPI.TargetSpecificEventType.SPUThreadStopInit:
                                data2.SPUThreadStop = default(PS3TMAPI.SPUThreadStopData);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.SPUThreadStop.ThreadGroupID);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.SPUThreadStop.ThreadID);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.SPUThreadStop.PC);
                                num2 = Marshal.ReadInt32(data, 0);
                                data = new IntPtr(data.ToInt64() + (long)Marshal.SizeOf(num2));
                                data2.SPUThreadStop.Reason = (PS3TMAPI.SPUThreadStopReason)num2;
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.SPUThreadStop.SP);
                                break;
                            case PS3TMAPI.TargetSpecificEventType.SPUThreadGroupDestroy:
                                data2.SPUThreadGroupDestroyData = default(PS3TMAPI.SPUThreadGroupDestroyData);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.SPUThreadGroupDestroyData.ThreadGroupID);
                                break;
                            case PS3TMAPI.TargetSpecificEventType.SPUThreadStopEx:
                                data2.SPUThreadStopEx = default(PS3TMAPI.SPUThreadStopExData);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.SPUThreadStopEx.ThreadGroupID);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.SPUThreadStopEx.ThreadID);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.SPUThreadStopEx.PC);
                                num2 = Marshal.ReadInt32(data, 0);
                                data = new IntPtr(data.ToInt64() + (long)Marshal.SizeOf(num2));
                                data2.SPUThreadStopEx.Reason = (PS3TMAPI.SPUThreadStopReason)num2;
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.SPUThreadStopEx.SP);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.SPUThreadStopEx.MFCDSISR);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.SPUThreadStopEx.MFCDSIPR);
                                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.SPUThreadStopEx.MFCDAR);
                                break;
                        }
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case PS3TMAPI.TargetSpecificEventType.PRXLoad:
                        data2.PRXLoad = default(PS3TMAPI.NotifyPRXLoadData);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PRXLoad.PPUThreadID);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.PRXLoad.PRXID);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PRXLoad.Timestamp);
                        break;
                    case PS3TMAPI.TargetSpecificEventType.PRXUnload:
                        data2.PRXUnload = default(PS3TMAPI.NotifyPRXUnloadData);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PRXUnload.PPUThreadID);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.PRXUnload.PRXID);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.PRXUnload.Timestamp);
                        break;
                    default:
                        if (type != PS3TMAPI.TargetSpecificEventType.DAInitialised)
                        {
                        }
                        break;
                }
            }
        }
        else if (type <= PS3TMAPI.TargetSpecificEventType.InstallPackagePath)
        {
            if (type != PS3TMAPI.TargetSpecificEventType.Footswitch)
            {
                switch (type)
                {
                    case PS3TMAPI.TargetSpecificEventType.InstallPackageProgress:
                        data2.InstallPackageProgress = default(PS3TMAPI.InstallPackageProgress);
                        data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref data2.InstallPackageProgress.Percent);
                        break;
                    case PS3TMAPI.TargetSpecificEventType.InstallPackagePath:
                        data2.InstallPackagePath = default(PS3TMAPI.InstallPackagePath);
                        data2.InstallPackagePath.Path = PS3TMAPI.Utf8ToString(data, 1024U);
                        break;
                }
            }
            else
            {
                data2.Footswitch = default(PS3TMAPI.FootswitchData);
                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.Footswitch.EventSource);
                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.Footswitch.EventData1);
                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.Footswitch.EventData2);
                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.Footswitch.EventData3);
                data = PS3TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref data2.Footswitch.Reserved);
            }
        }
        else
        {
            switch (type)
            {
                case PS3TMAPI.TargetSpecificEventType.CoreDumpComplete:
                    data2.CoreDumpComplete = default(PS3TMAPI.CoreDumpComplete);
                    data2.CoreDumpComplete.Filename = PS3TMAPI.Utf8ToString(data, 1024U);
                    break;
                case PS3TMAPI.TargetSpecificEventType.CoreDumpStart:
                    data2.CoreDumpStart = default(PS3TMAPI.CoreDumpStart);
                    data2.CoreDumpStart.Filename = PS3TMAPI.Utf8ToString(data, 1024U);
                    break;
                default:
                    if (type != (PS3TMAPI.TargetSpecificEventType)4026531855U)
                    {
                    }
                    break;
            }
        }
        result.Data = data2;
        return result;
    }

    // Token: 0x06000277 RID: 631 RVA: 0x00008420 File Offset: 0x00006620
    private static void EventHandlerWrapper(int target, PS3TMAPI.EventType type, uint param, PS3TMAPI.SNRESULT result, uint length, IntPtr data, IntPtr userData)
    {
        switch (type)
        {
            case PS3TMAPI.EventType.TTY:
                PS3TMAPI.MarshalTTYEvent(target, param, result, length, data);
                return;
            case PS3TMAPI.EventType.Target:
                PS3TMAPI.MarshalTargetEvent(target, param, result, length, data);
                return;
            case PS3TMAPI.EventType.System:
                break;
            case PS3TMAPI.EventType.FTP:
                PS3TMAPI.MarshalFTPEvent(target, param, result, length, data);
                return;
            case PS3TMAPI.EventType.PadCapture:
                PS3TMAPI.MarshalPadCaptureEvent(target, param, result, length, data);
                return;
            case PS3TMAPI.EventType.FileTrace:
                PS3TMAPI.MarshalFileTraceEvent(target, param, result, length, data);
                return;
            case PS3TMAPI.EventType.PadPlayback:
                PS3TMAPI.MarshalPadPlaybackEvent(target, param, result, length, data);
                return;
            case PS3TMAPI.EventType.Server:
                PS3TMAPI.MarshalServerEvent(target, param, result, length, data);
                break;
            default:
                return;
        }
    }

    // Token: 0x06000278 RID: 632
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SearchForTargets")]
    private static extern PS3TMAPI.SNRESULT SearchForTargetsX86(string ipAddressFrom, string ipAddressTo, PS3TMAPI.SearchTargetsCallbackPriv callback, IntPtr userData, int port);

    // Token: 0x06000279 RID: 633
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SearchForTargets")]
    private static extern PS3TMAPI.SNRESULT SearchForTargetsX64(string ipAddressFrom, string ipAddressTo, PS3TMAPI.SearchTargetsCallbackPriv callback, IntPtr userData, int port);

    // Token: 0x0600027A RID: 634 RVA: 0x000084B4 File Offset: 0x000066B4
    public static PS3TMAPI.SNRESULT SearchForTargets(string ipAddressFrom, string ipAddressTo, PS3TMAPI.SearchTargetsCallback callback, object userData, int port)
    {
        PS3TMAPI.SearchForTargetsCallbackHandler value = new PS3TMAPI.SearchForTargetsCallbackHandler(callback, userData);
        PS3TMAPI.SearchTargetsCallbackPriv callback2 = new PS3TMAPI.SearchTargetsCallbackPriv(PS3TMAPI.SearchForTargetsCallbackHandler.SearchForTargetsCallback);
        GCHandle value2 = GCHandle.Alloc(value);
        IntPtr userData2 = GCHandle.ToIntPtr(value2);
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SearchForTargetsX64(ipAddressFrom, ipAddressTo, callback2, userData2, port);
        }
        return PS3TMAPI.SearchForTargetsX86(ipAddressFrom, ipAddressTo, callback2, userData2, port);
    }

    // Token: 0x0600027B RID: 635
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StopSearchForTargets")]
    private static extern PS3TMAPI.SNRESULT StopSearchForTargetsX86();

    // Token: 0x0600027C RID: 636
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3StopSearchForTargets")]
    private static extern PS3TMAPI.SNRESULT StopSearchForTargetsX64();

    // Token: 0x0600027D RID: 637 RVA: 0x00008502 File Offset: 0x00006702
    public static PS3TMAPI.SNRESULT StopSearchForTargets()
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.StopSearchForTargetsX64();
        }
        return PS3TMAPI.StopSearchForTargetsX86();
    }

    // Token: 0x0600027E RID: 638
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3IsScanning")]
    private static extern PS3TMAPI.SNRESULT IsScanningX86();

    // Token: 0x0600027F RID: 639
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3IsScanning")]
    private static extern PS3TMAPI.SNRESULT IsScanningX64();

    // Token: 0x06000280 RID: 640 RVA: 0x00008516 File Offset: 0x00006716
    public static PS3TMAPI.SNRESULT IsScanning()
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.IsScanningX64();
        }
        return PS3TMAPI.IsScanningX86();
    }

    // Token: 0x06000281 RID: 641
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3IsValidResolution")]
    private static extern PS3TMAPI.SNRESULT IsValidResolutionX86(uint monitorType, uint startupResolution);

    // Token: 0x06000282 RID: 642
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3IsValidResolution")]
    private static extern PS3TMAPI.SNRESULT IsValidResolutionX64(uint monitorType, uint startupResolution);

    // Token: 0x06000283 RID: 643 RVA: 0x0000852A File Offset: 0x0000672A
    public static PS3TMAPI.SNRESULT IsValidResolution(uint monitorType, uint startupResolution)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.IsValidResolutionX64(monitorType, startupResolution);
        }
        return PS3TMAPI.IsValidResolutionX86(monitorType, startupResolution);
    }

    // Token: 0x06000284 RID: 644
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDisplaySettings")]
    private static extern PS3TMAPI.SNRESULT SetDisplaySettingsX86(int target, IntPtr executable, uint monitorType, uint connectorType, uint startupResolution, bool HDCP, bool resetAfter);

    // Token: 0x06000285 RID: 645
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3SetDisplaySettings")]
    private static extern PS3TMAPI.SNRESULT SetDisplaySettingsX64(int target, IntPtr executable, uint monitorType, uint connectorType, uint startupResolution, bool HDCP, bool resetAfter);

    // Token: 0x06000286 RID: 646 RVA: 0x00008544 File Offset: 0x00006744
    public static PS3TMAPI.SNRESULT SetDisplaySettings(int target, string executable, uint monitorType, uint connectorType, uint startupResolution, bool HDCP, bool resetAfter)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(executable));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.SetDisplaySettingsX64(target, scopedGlobalHeapPtr.Get(), monitorType, connectorType, startupResolution, HDCP, resetAfter);
        }
        return PS3TMAPI.SetDisplaySettingsX86(target, scopedGlobalHeapPtr.Get(), monitorType, connectorType, startupResolution, HDCP, resetAfter);
    }

    // Token: 0x06000287 RID: 647
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3MapFileSystem")]
    private static extern PS3TMAPI.SNRESULT MapFileSystemX86(char driveLetter);

    // Token: 0x06000288 RID: 648
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3MapFileSystem")]
    private static extern PS3TMAPI.SNRESULT MapFileSystemX64(char driveLetter);

    // Token: 0x06000289 RID: 649 RVA: 0x0000858D File Offset: 0x0000678D
    public static PS3TMAPI.SNRESULT MapFileSystem(char driveLetter)
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.MapFileSystemX64(driveLetter);
        }
        return PS3TMAPI.MapFileSystemX86(driveLetter);
    }

    // Token: 0x0600028A RID: 650
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnmapFileSystem")]
    private static extern PS3TMAPI.SNRESULT UnmapFileSystemX86();

    // Token: 0x0600028B RID: 651
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3UnmapFileSystem")]
    private static extern PS3TMAPI.SNRESULT UnmapFileSystemX64();

    // Token: 0x0600028C RID: 652 RVA: 0x000085A3 File Offset: 0x000067A3
    public static PS3TMAPI.SNRESULT UnmapFileSystem()
    {
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.UnmapFileSystemX64();
        }
        return PS3TMAPI.UnmapFileSystemX86();
    }

    // Token: 0x0600028D RID: 653
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ImportTargetSettings")]
    private static extern PS3TMAPI.SNRESULT ImportTargetSettingsX86(int target, IntPtr szFileName);

    // Token: 0x0600028E RID: 654
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ImportTargetSettings")]
    private static extern PS3TMAPI.SNRESULT ImportTargetSettingsX64(int target, IntPtr szFileName);

    // Token: 0x0600028F RID: 655 RVA: 0x000085B8 File Offset: 0x000067B8
    public static PS3TMAPI.SNRESULT ImportTargetSettings(int target, string fileName)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(fileName));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ImportTargetSettingsX64(target, scopedGlobalHeapPtr.Get());
        }
        return PS3TMAPI.ImportTargetSettingsX86(target, scopedGlobalHeapPtr.Get());
    }

    // Token: 0x06000290 RID: 656
    [DllImport("PS3TMAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ExportTargetSettings")]
    private static extern PS3TMAPI.SNRESULT ExportTargetSettingsX86(int target, IntPtr szFileName);

    // Token: 0x06000291 RID: 657
    [DllImport("PS3TMAPIX64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNPS3ExportTargetSettings")]
    private static extern PS3TMAPI.SNRESULT ExportTargetSettingsX64(int target, IntPtr szFileName);

    // Token: 0x06000292 RID: 658 RVA: 0x000085F4 File Offset: 0x000067F4
    public static PS3TMAPI.SNRESULT ExportTargetSettings(int target, string fileName)
    {
        PS3TMAPI.ScopedGlobalHeapPtr scopedGlobalHeapPtr = new PS3TMAPI.ScopedGlobalHeapPtr(PS3TMAPI.AllocUtf8FromString(fileName));
        if (!PS3TMAPI.Is32Bit())
        {
            return PS3TMAPI.ExportTargetSettingsX64(target, scopedGlobalHeapPtr.Get());
        }
        return PS3TMAPI.ExportTargetSettingsX86(target, scopedGlobalHeapPtr.Get());
    }

    // Token: 0x06000293 RID: 659 RVA: 0x00008630 File Offset: 0x00006830
    private static IntPtr WriteDataToUnmanagedIncPtr<T>(T storage, IntPtr unmanagedBuf)
    {
        bool fDeleteOld = false;
        Marshal.StructureToPtr(storage, unmanagedBuf, fDeleteOld);
        return new IntPtr(unmanagedBuf.ToInt64() + (long)Marshal.SizeOf(storage));
    }

    // Token: 0x06000294 RID: 660 RVA: 0x00008665 File Offset: 0x00006865
    private static IntPtr ReadDataFromUnmanagedIncPtr<T>(IntPtr unmanagedBuf, ref T storage)
    {
        storage = (T)((object)Marshal.PtrToStructure(unmanagedBuf, typeof(T)));
        return new IntPtr(unmanagedBuf.ToInt64() + (long)Marshal.SizeOf(storage));
    }

    // Token: 0x06000295 RID: 661 RVA: 0x000086A0 File Offset: 0x000068A0
    private static IntPtr ReadAnsiStringFromUnmanagedIncPtr(IntPtr unmanagedBuf, ref string inputString)
    {
        inputString = Marshal.PtrToStringAnsi(unmanagedBuf);
        return new IntPtr(unmanagedBuf.ToInt64() + (long)inputString.Length + 1L);
    }

    // Token: 0x06000296 RID: 662 RVA: 0x000086C4 File Offset: 0x000068C4
    private static IntPtr AllocUtf8FromString(string wcharString)
    {
        if (wcharString == null)
        {
            return IntPtr.Zero;
        }
        byte[] bytes = Encoding.UTF8.GetBytes(wcharString);
        IntPtr intPtr = Marshal.AllocHGlobal(bytes.Length + 1);
        Marshal.Copy(bytes, 0, intPtr, bytes.Length);
        Marshal.WriteByte((IntPtr)(intPtr.ToInt64() + (long)bytes.Length), 0);
        return intPtr;
    }

    // Token: 0x06000297 RID: 663 RVA: 0x00008714 File Offset: 0x00006914
    private unsafe static string Utf8ToString(IntPtr utf8Ptr, uint maxLength)
    {
        if (utf8Ptr == IntPtr.Zero)
        {
            return "";
        }
        byte* ptr = (byte*)((void*)utf8Ptr);
        int num = 0;
        while (*ptr != 0 && (long)num < (long)((ulong)maxLength))
        {
            ptr++;
            num++;
        }
        byte[] array = new byte[num];
        Marshal.Copy(utf8Ptr, array, 0, num);
        return Encoding.UTF8.GetString(array);
    }

    // Token: 0x06000298 RID: 664 RVA: 0x00008770 File Offset: 0x00006970
    private static string Utf8FixedSizeByteArrayToString(byte[] byteArray)
    {
        if (byteArray == null)
        {
            return "";
        }
        int num = 0;
        foreach (byte b in byteArray)
        {
            if (b == 0)
            {
                break;
            }
            num++;
        }
        byte[] array = new byte[num];
        Buffer.BlockCopy(byteArray, 0, array, 0, num);
        return Encoding.UTF8.GetString(array);
    }

    // Token: 0x04000001 RID: 1
    public const int InvalidTarget = -1;

    // Token: 0x04000002 RID: 2
    public const int DefaultTarget = -2;

    // Token: 0x04000003 RID: 3
    public const uint AllTTYStreams = 4294967295U;

    // Token: 0x04000004 RID: 4
    public const uint DefaultProcessPriority = 999U;

    // Token: 0x04000005 RID: 5
    public const uint DefaultProtocolPriority = 128U;

    // Token: 0x04000006 RID: 6
    private static PS3TMAPI.EnumerateTargetsExCallbackPriv ms_enumTargetsExCallbackPriv = new PS3TMAPI.EnumerateTargetsExCallbackPriv(PS3TMAPI.EnumTargetsExPriv);

    // Token: 0x04000007 RID: 7
    [ThreadStatic]
    private static PS3TMAPI.EnumerateTargetsExCallback ms_enumTargetsExCallback = null;

    // Token: 0x04000008 RID: 8
    [ThreadStatic]
    private static object ms_enumTargetsExUserData = null;

    // Token: 0x04000009 RID: 9
    [ThreadStatic]
    private static PS3TMAPI.ServerEventCallback ms_serverEventCallback = null;

    // Token: 0x0400000A RID: 10
    [ThreadStatic]
    private static object ms_serverEventUserData = null;

    // Token: 0x0400000B RID: 11
    [ThreadStatic]
    private static Dictionary<PS3TMAPI.TTYChannel, PS3TMAPI.TTYCallbackAndUserData> ms_userTtyCallbacks = null;

    // Token: 0x0400000C RID: 12
    [ThreadStatic]
    private static Dictionary<int, PS3TMAPI.PadPlaybackCallbackAndUserData> ms_userPadPlaybackCallbacks = null;

    // Token: 0x0400000D RID: 13
    [ThreadStatic]
    private static Dictionary<int, PS3TMAPI.PadCaptureCallbackAndUserData> ms_userPadCaptureCallbacks = null;

    // Token: 0x0400000E RID: 14
    private static PS3TMAPI.CustomProtocolCallbackPriv ms_customProtoCallbackPriv = new PS3TMAPI.CustomProtocolCallbackPriv(PS3TMAPI.CustomProtocolHandler);

    // Token: 0x0400000F RID: 15
    [ThreadStatic]
    private static Dictionary<PS3TMAPI.CustomProtocolId, PS3TMAPI.CusProtoCallbackAndUserData> ms_userCustomProtoCallbacks = null;

    // Token: 0x04000010 RID: 16
    [ThreadStatic]
    private static Dictionary<int, PS3TMAPI.FtpCallbackAndUserData> ms_userFtpCallbacks = null;

    // Token: 0x04000011 RID: 17
    [ThreadStatic]
    private static Dictionary<int, PS3TMAPI.FileTraceCallbackAndUserData> ms_userFileTraceCallbacks = null;

    // Token: 0x04000012 RID: 18
    [ThreadStatic]
    private static Dictionary<int, PS3TMAPI.TargetCallbackAndUserData> ms_userTargetCallbacks = null;

    // Token: 0x04000013 RID: 19
    private static PS3TMAPI.HandleEventCallbackPriv ms_eventHandlerWrapper = new PS3TMAPI.HandleEventCallbackPriv(PS3TMAPI.EventHandlerWrapper);

    // Token: 0x02000003 RID: 3
    public enum SNRESULT
    {
        // Token: 0x04000015 RID: 21
        SN_S_OK,
        // Token: 0x04000016 RID: 22
        SN_S_PENDING,
        // Token: 0x04000017 RID: 23
        SN_S_NO_MSG = 3,
        // Token: 0x04000018 RID: 24
        SN_S_TM_VERSION,
        // Token: 0x04000019 RID: 25
        SN_S_REPLACED,
        // Token: 0x0400001A RID: 26
        SN_S_NO_ACTION,
        // Token: 0x0400001B RID: 27
        SN_S_TARGET_STILL_REGISTERED,
        // Token: 0x0400001C RID: 28
        SN_E_ERROR = -2147483648,
        // Token: 0x0400001D RID: 29
        SN_E_NOT_IMPL = -1,
        // Token: 0x0400001E RID: 30
        SN_E_TM_NOT_RUNNING = -2,
        // Token: 0x0400001F RID: 31
        SN_E_BAD_TARGET = -3,
        // Token: 0x04000020 RID: 32
        SN_E_NOT_CONNECTED = -4,
        // Token: 0x04000021 RID: 33
        SN_E_COMMS_ERR = -5,
        // Token: 0x04000022 RID: 34
        SN_E_TM_COMMS_ERR = -6,
        // Token: 0x04000023 RID: 35
        SN_E_TIMEOUT = -7,
        // Token: 0x04000024 RID: 36
        SN_E_HOST_NOT_FOUND = -8,
        // Token: 0x04000025 RID: 37
        SN_E_TARGET_IN_USE = -9,
        // Token: 0x04000026 RID: 38
        SN_E_LOAD_ELF_FAILED = -10,
        // Token: 0x04000027 RID: 39
        SN_E_BAD_UNIT = -11,
        // Token: 0x04000028 RID: 40
        SN_E_OUT_OF_MEM = -12,
        // Token: 0x04000029 RID: 41
        SN_E_NOT_LISTED = -13,
        // Token: 0x0400002A RID: 42
        SN_E_TM_VERSION = -14,
        // Token: 0x0400002B RID: 43
        SN_E_DLL_NOT_INITIALISED = -15,
        // Token: 0x0400002C RID: 44
        SN_E_TARGET_RUNNING = -17,
        // Token: 0x0400002D RID: 45
        SN_E_BAD_MEMSPACE = -18,
        // Token: 0x0400002E RID: 46
        SN_E_NO_TARGETS = -19,
        // Token: 0x0400002F RID: 47
        SN_E_NO_SEL = -20,
        // Token: 0x04000030 RID: 48
        SN_E_BAD_PARAM = -21,
        // Token: 0x04000031 RID: 49
        SN_E_BUSY = -22,
        // Token: 0x04000032 RID: 50
        SN_E_DECI_ERROR = -23,
        // Token: 0x04000033 RID: 51
        SN_E_EXISTING_CALLBACK = -24,
        // Token: 0x04000034 RID: 52
        SN_E_INSUFFICIENT_DATA = -25,
        // Token: 0x04000035 RID: 53
        SN_E_DATA_TOO_LONG = -26,
        // Token: 0x04000036 RID: 54
        SN_E_DEPRECATED = -27,
        // Token: 0x04000037 RID: 55
        SN_E_BAD_ALIGN = -28,
        // Token: 0x04000038 RID: 56
        SN_E_FILE_ERROR = -29,
        // Token: 0x04000039 RID: 57
        SN_E_NOT_SUPPORTED_IN_SDK_VERSION = -30,
        // Token: 0x0400003A RID: 58
        SN_E_LOAD_MODULE_FAILED = -31,
        // Token: 0x0400003B RID: 59
        SN_E_LICENSE_ERROR = -32,
        // Token: 0x0400003C RID: 60
        SN_E_CHECK_TARGET_CONFIGURATION = -33,
        // Token: 0x0400003D RID: 61
        SN_E_MODULE_NOT_FOUND = -34,
        // Token: 0x0400003E RID: 62
        SN_E_CONNECT_TO_GAMEPORT_FAILED = -35,
        // Token: 0x0400003F RID: 63
        SN_E_COMMAND_CANCELLED = -36,
        // Token: 0x04000040 RID: 64
        SN_E_PROTOCOL_ALREADY_REGISTERED = -37,
        // Token: 0x04000041 RID: 65
        SN_E_CONNECTED = -38,
        // Token: 0x04000042 RID: 66
        SN_E_COMMS_EVENT_MISMATCHED_ERR = -39
    }

    // Token: 0x02000004 RID: 4
    public enum ConnectStatus
    {
        // Token: 0x04000044 RID: 68
        Connected,
        // Token: 0x04000045 RID: 69
        Connecting,
        // Token: 0x04000046 RID: 70
        NotConnected,
        // Token: 0x04000047 RID: 71
        InUse,
        // Token: 0x04000048 RID: 72
        Unavailable
    }

    // Token: 0x02000005 RID: 5
    // (Invoke) Token: 0x0600029C RID: 668
    public delegate int EnumerateTargetsCallback(int target);

    // Token: 0x02000006 RID: 6
    // (Invoke) Token: 0x060002A0 RID: 672
    public delegate int EnumerateTargetsExCallback(int target, object userData);

    // Token: 0x02000007 RID: 7
    // (Invoke) Token: 0x060002A4 RID: 676
    private delegate int EnumerateTargetsExCallbackPriv(int target, IntPtr unused);

    // Token: 0x02000008 RID: 8
    [Flags]
    public enum BootParameter : ulong
    {
        // Token: 0x0400004A RID: 74
        Default = 0UL,
        // Token: 0x0400004B RID: 75
        SystemMode = 17UL,
        // Token: 0x0400004C RID: 76
        ReleaseMode = 1UL,
        // Token: 0x0400004D RID: 77
        DebugMode = 16UL,
        // Token: 0x0400004E RID: 78
        MemSizeConsole = 2UL,
        // Token: 0x0400004F RID: 79
        BluRayEmuOff = 4UL,
        // Token: 0x04000050 RID: 80
        HDDSpeedBluRayEmu = 8UL,
        // Token: 0x04000051 RID: 81
        BluRayEmuUSB = 32UL,
        // Token: 0x04000052 RID: 82
        HostFSTarget = 64UL,
        // Token: 0x04000053 RID: 83
        DualNIC = 128UL
    }

    // Token: 0x02000009 RID: 9
    [Flags]
    public enum BootParameterMask : ulong
    {
        // Token: 0x04000055 RID: 85
        BootMode = 17UL,
        // Token: 0x04000056 RID: 86
        Memsize = 2UL,
        // Token: 0x04000057 RID: 87
        BlurayEmulation = 4UL,
        // Token: 0x04000058 RID: 88
        HDDSpeed = 8UL,
        // Token: 0x04000059 RID: 89
        BlurayEmuSelect = 32UL,
        // Token: 0x0400005A RID: 90
        HostFS = 64UL,
        // Token: 0x0400005B RID: 91
        NIC = 128UL,
        // Token: 0x0400005C RID: 92
        All = 255UL
    }

    // Token: 0x0200000A RID: 10
    [Flags]
    public enum ResetParameter : ulong
    {
        // Token: 0x0400005E RID: 94
        Soft = 0UL,
        // Token: 0x0400005F RID: 95
        Hard = 1UL,
        // Token: 0x04000060 RID: 96
        Quick = 2UL,
        // Token: 0x04000061 RID: 97
        ResetEx = 9223372036854775808UL
    }

    // Token: 0x0200000B RID: 11
    [Flags]
    public enum ResetParameterMask : ulong
    {
        // Token: 0x04000063 RID: 99
        All = 9223372036854775811UL
    }

    // Token: 0x0200000C RID: 12
    [Flags]
    public enum SystemParameter : ulong
    {
        // Token: 0x04000065 RID: 101
        TargetModel60GB = 281474976710656UL,
        // Token: 0x04000066 RID: 102
        TargetModel20GB = 562949953421312UL,
        // Token: 0x04000067 RID: 103
        ReleaseCheckMode = 140737488355328UL
    }

    // Token: 0x0200000D RID: 13
    [Flags]
    public enum SystemParameterMask : ulong
    {
        // Token: 0x04000069 RID: 105
        TargetModel = 71776119061217280UL,
        // Token: 0x0400006A RID: 106
        ReleaseCheck = 140737488355328UL,
        // Token: 0x0400006B RID: 107
        All = 71916856549572608UL
    }

    // Token: 0x0200000E RID: 14
    [Flags]
    public enum TargetInfoFlag : uint
    {
        // Token: 0x0400006D RID: 109
        TargetID = 1U,
        // Token: 0x0400006E RID: 110
        Name = 2U,
        // Token: 0x0400006F RID: 111
        Info = 4U,
        // Token: 0x04000070 RID: 112
        HomeDir = 8U,
        // Token: 0x04000071 RID: 113
        FileServingDir = 16U,
        // Token: 0x04000072 RID: 114
        Boot = 32U
    }

    // Token: 0x0200000F RID: 15
    public struct TargetInfo
    {
        // Token: 0x04000073 RID: 115
        public PS3TMAPI.TargetInfoFlag Flags;

        // Token: 0x04000074 RID: 116
        public int Target;

        // Token: 0x04000075 RID: 117
        public string Name;

        // Token: 0x04000076 RID: 118
        public string Type;

        // Token: 0x04000077 RID: 119
        public string Info;

        // Token: 0x04000078 RID: 120
        public string HomeDir;

        // Token: 0x04000079 RID: 121
        public string FSDir;

        // Token: 0x0400007A RID: 122
        public PS3TMAPI.BootParameter Boot;
    }

    // Token: 0x02000010 RID: 16
    private struct TargetInfoPriv
    {
        // Token: 0x0400007B RID: 123
        public PS3TMAPI.TargetInfoFlag Flags;

        // Token: 0x0400007C RID: 124
        public int Target;

        // Token: 0x0400007D RID: 125
        public IntPtr Name;

        // Token: 0x0400007E RID: 126
        public IntPtr Type;

        // Token: 0x0400007F RID: 127
        public IntPtr Info;

        // Token: 0x04000080 RID: 128
        public IntPtr HomeDir;

        // Token: 0x04000081 RID: 129
        public IntPtr FSDir;

        // Token: 0x04000082 RID: 130
        public PS3TMAPI.BootParameter Boot;
    }

    // Token: 0x02000011 RID: 17
    public struct TargetType
    {
        // Token: 0x04000083 RID: 131
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Type;

        // Token: 0x04000084 RID: 132
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Description;
    }

    // Token: 0x02000012 RID: 18
    [StructLayout(LayoutKind.Sequential)]
    public class TCPIPConnectProperties
    {
        // Token: 0x04000085 RID: 133
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string IPAddress;

        // Token: 0x04000086 RID: 134
        public uint Port;
    }

    // Token: 0x02000013 RID: 19
    public enum ServerEventType : uint
    {
        // Token: 0x04000088 RID: 136
        TargetAdded,
        // Token: 0x04000089 RID: 137
        TargetDeleted,
        // Token: 0x0400008A RID: 138
        DefaultTargetChanged
    }

    // Token: 0x02000014 RID: 20
    // (Invoke) Token: 0x060002A9 RID: 681
    public delegate void ServerEventCallback(int target, PS3TMAPI.SNRESULT res, PS3TMAPI.ServerEventType eventType, object userData);

    // Token: 0x02000015 RID: 21
    private struct ServerEventHeader
    {
        // Token: 0x0400008B RID: 139
        public uint size;

        // Token: 0x0400008C RID: 140
        public uint targetID;

        // Token: 0x0400008D RID: 141
        public PS3TMAPI.ServerEventType eventType;
    }

    // Token: 0x02000016 RID: 22
    [Flags]
    public enum SystemInfoFlag : uint
    {
        // Token: 0x0400008F RID: 143
        SDKVersion = 1U,
        // Token: 0x04000090 RID: 144
        TimebaseFreq = 2U,
        // Token: 0x04000091 RID: 145
        RTSDKVersion = 4U,
        // Token: 0x04000092 RID: 146
        TotalSystemMem = 8U,
        // Token: 0x04000093 RID: 147
        AvailableSysMem = 16U,
        // Token: 0x04000094 RID: 148
        DCMBufferSize = 32U
    }

    // Token: 0x02000017 RID: 23
    public struct SystemInfo
    {
        // Token: 0x04000095 RID: 149
        public uint CellSDKVersion;

        // Token: 0x04000096 RID: 150
        public ulong TimebaseFrequency;

        // Token: 0x04000097 RID: 151
        public uint CellRuntimeSDKVersion;

        // Token: 0x04000098 RID: 152
        public uint TotalSystemMemory;

        // Token: 0x04000099 RID: 153
        public uint AvailableSystemMemory;

        // Token: 0x0400009A RID: 154
        public uint DCMBufferSize;
    }

    // Token: 0x02000018 RID: 24
    [Flags]
    public enum ExtraLoadFlag : ulong
    {
        // Token: 0x0400009C RID: 156
        EnableLv2ExceptionHandler = 1UL,
        // Token: 0x0400009D RID: 157
        EnableRemotePlay = 2UL,
        // Token: 0x0400009E RID: 158
        EnableGCMDebug = 4UL,
        // Token: 0x0400009F RID: 159
        LoadLibprofSPRXAutomatically = 8UL,
        // Token: 0x040000A0 RID: 160
        EnableCoreDump = 16UL,
        // Token: 0x040000A1 RID: 161
        EnableAccForRemotePlay = 32UL,
        // Token: 0x040000A2 RID: 162
        EnableHUDRSXTools = 64UL,
        // Token: 0x040000A3 RID: 163
        EnableMAT = 128UL,
        // Token: 0x040000A4 RID: 164
        EnableMiscSettings = 9223372036854775808UL,
        // Token: 0x040000A5 RID: 165
        GameAttributeInviteMessage = 256UL,
        // Token: 0x040000A6 RID: 166
        GameAttributeCustomMessage = 512UL,
        // Token: 0x040000A7 RID: 167
        LoadingPatch = 4096UL
    }

    // Token: 0x02000019 RID: 25
    [Flags]
    public enum ExtraLoadFlagMask : ulong
    {
        // Token: 0x040000A9 RID: 169
        GameAttributeMessageMask = 3840UL,
        // Token: 0x040000AA RID: 170
        All = 9223372036854783999UL,
        // Token: 0x040000AB RID: 171
        OverrideTVGUIMask = 9223372036854775808UL
    }

    // Token: 0x0200001A RID: 26
    public struct TTYStream
    {
        // Token: 0x040000AC RID: 172
        public uint Index;

        // Token: 0x040000AD RID: 173
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Name;
    }

    // Token: 0x0200001B RID: 27
    private enum SNPS3_TM_TIMEOUT
    {
        // Token: 0x040000AF RID: 175
        DEFAULT_TIMEOUT,
        // Token: 0x040000B0 RID: 176
        RESET_TIMEOUT,
        // Token: 0x040000B1 RID: 177
        CONNECT_TIMEOUT,
        // Token: 0x040000B2 RID: 178
        LOAD_TIMEOUT,
        // Token: 0x040000B3 RID: 179
        GET_STATUS_TIMEOUT,
        // Token: 0x040000B4 RID: 180
        RECONNECT_TIMEOUT,
        // Token: 0x040000B5 RID: 181
        GAMEPORT_TIMEOUT,
        // Token: 0x040000B6 RID: 182
        GAMEEXIT_TIMEOUT
    }

    // Token: 0x0200001C RID: 28
    public enum TimeoutType
    {
        // Token: 0x040000B8 RID: 184
        Default,
        // Token: 0x040000B9 RID: 185
        Reset,
        // Token: 0x040000BA RID: 186
        Connect,
        // Token: 0x040000BB RID: 187
        Load,
        // Token: 0x040000BC RID: 188
        GetStatus,
        // Token: 0x040000BD RID: 189
        Reconnect,
        // Token: 0x040000BE RID: 190
        GamePort,
        // Token: 0x040000BF RID: 191
        GameExit
    }

    // Token: 0x0200001D RID: 29
    // (Invoke) Token: 0x060002AD RID: 685
    public delegate void TTYCallback(int target, uint streamID, PS3TMAPI.SNRESULT res, string data, object userData);

    // Token: 0x0200001E RID: 30
    // (Invoke) Token: 0x060002B1 RID: 689
    public delegate void TTYCallbackRaw(int target, uint streamID, PS3TMAPI.SNRESULT res, byte[] data, object userData);

    // Token: 0x0200001F RID: 31
    private class TTYCallbackAndUserData
    {
        // Token: 0x040000C0 RID: 192
        public PS3TMAPI.TTYCallback m_callback;

        // Token: 0x040000C1 RID: 193
        public object m_userData;

        // Token: 0x040000C2 RID: 194
        public PS3TMAPI.TTYCallbackRaw m_callbackRaw;

        // Token: 0x040000C3 RID: 195
        public object m_userDataRaw;
    }

    // Token: 0x02000020 RID: 32
    private struct TTYChannel
    {
        // Token: 0x060002B5 RID: 693 RVA: 0x0000885E File Offset: 0x00006A5E
        public TTYChannel(int target, uint channel)
        {
            this.Target = target;
            this.Channel = channel;
        }

        // Token: 0x040000C4 RID: 196
        public readonly int Target;

        // Token: 0x040000C5 RID: 197
        public readonly uint Channel;
    }

    // Token: 0x02000021 RID: 33
    public enum UnitType
    {
        // Token: 0x040000C7 RID: 199
        PPU,
        // Token: 0x040000C8 RID: 200
        SPU,
        // Token: 0x040000C9 RID: 201
        SPURAW
    }

    // Token: 0x02000022 RID: 34
    public enum UnitStatus : uint
    {
        // Token: 0x040000CB RID: 203
        Unknown,
        // Token: 0x040000CC RID: 204
        Running,
        // Token: 0x040000CD RID: 205
        Stopped,
        // Token: 0x040000CE RID: 206
        Signalled,
        // Token: 0x040000CF RID: 207
        Resetting,
        // Token: 0x040000D0 RID: 208
        Missing,
        // Token: 0x040000D1 RID: 209
        Reset,
        // Token: 0x040000D2 RID: 210
        NotConnected,
        // Token: 0x040000D3 RID: 211
        Connected,
        // Token: 0x040000D4 RID: 212
        StatusChange
    }

    // Token: 0x02000023 RID: 35
    [Flags]
    public enum LoadFlag : uint
    {
        // Token: 0x040000D6 RID: 214
        EnableDebugging = 1U,
        // Token: 0x040000D7 RID: 215
        UseELFPriority = 256U,
        // Token: 0x040000D8 RID: 216
        UseELFStackSize = 512U,
        // Token: 0x040000D9 RID: 217
        WaitBDMounted = 8192U,
        // Token: 0x040000DA RID: 218
        PPUNotDebug = 65536U,
        // Token: 0x040000DB RID: 219
        SPUNotDebug = 131072U,
        // Token: 0x040000DC RID: 220
        IgnoreDefaults = 2147483648U,
        // Token: 0x040000DD RID: 221
        ParamSFOUseELFDir = 1048576U,
        // Token: 0x040000DE RID: 222
        ParamSFOUseCustomDir = 2097152U
    }

    // Token: 0x02000024 RID: 36
    public enum ProcessStatus : uint
    {
        // Token: 0x040000E0 RID: 224
        Creating = 1U,
        // Token: 0x040000E1 RID: 225
        Ready,
        // Token: 0x040000E2 RID: 226
        Exited
    }

    // Token: 0x02000025 RID: 37
    public struct ProcessInfoHdr
    {
        // Token: 0x040000E3 RID: 227
        public PS3TMAPI.ProcessStatus Status;

        // Token: 0x040000E4 RID: 228
        public uint NumPPUThreads;

        // Token: 0x040000E5 RID: 229
        public uint NumSPUThreads;

        // Token: 0x040000E6 RID: 230
        public uint ParentProcessID;

        // Token: 0x040000E7 RID: 231
        public ulong MaxMemorySize;

        // Token: 0x040000E8 RID: 232
        public string ELFPath;
    }

    // Token: 0x02000026 RID: 38
    public struct ProcessInfo
    {
        // Token: 0x040000E9 RID: 233
        public PS3TMAPI.ProcessInfoHdr Hdr;

        // Token: 0x040000EA RID: 234
        public ulong[] ThreadIDs;
    }

    // Token: 0x02000027 RID: 39
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ExtraProcessInfo
    {
        // Token: 0x040000EB RID: 235
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public uint[] PPUGUIDs;
    }

    // Token: 0x02000028 RID: 40
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ProcessLoadParams
    {
        // Token: 0x040000EC RID: 236
        public ulong Version;

        // Token: 0x040000ED RID: 237
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public ulong[] Data;
    }

    // Token: 0x02000029 RID: 41
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ProcessLoadInfo
    {
        // Token: 0x040000EE RID: 238
        public uint InfoValid;

        // Token: 0x040000EF RID: 239
        public uint DebugFlags;

        // Token: 0x040000F0 RID: 240
        public PS3TMAPI.ProcessLoadParams LoadInfo;
    }

    // Token: 0x0200002A RID: 42
    public struct ModuleInfoHdr
    {
        // Token: 0x040000F1 RID: 241
        public string Name;

        // Token: 0x040000F2 RID: 242
        public sbyte[] Version;

        // Token: 0x040000F3 RID: 243
        public uint Attribute;

        // Token: 0x040000F4 RID: 244
        public uint StartEntry;

        // Token: 0x040000F5 RID: 245
        public uint StopEntry;

        // Token: 0x040000F6 RID: 246
        public string ELFName;

        // Token: 0x040000F7 RID: 247
        public uint NumSegments;
    }

    // Token: 0x0200002B RID: 43
    private struct ModuleInfoHdrPriv
    {
        // Token: 0x040000F8 RID: 248
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        public byte[] Name;

        // Token: 0x040000F9 RID: 249
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public sbyte[] Version;

        // Token: 0x040000FA RID: 250
        public uint Attribute;

        // Token: 0x040000FB RID: 251
        public uint StartEntry;

        // Token: 0x040000FC RID: 252
        public uint StopEntry;

        // Token: 0x040000FD RID: 253
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] ELFName;

        // Token: 0x040000FE RID: 254
        public uint NumSegments;
    }

    // Token: 0x0200002C RID: 44
    public struct PRXSegment
    {
        // Token: 0x040000FF RID: 255
        public ulong Base;

        // Token: 0x04000100 RID: 256
        public ulong FileSize;

        // Token: 0x04000101 RID: 257
        public ulong MemSize;

        // Token: 0x04000102 RID: 258
        public ulong Index;

        // Token: 0x04000103 RID: 259
        public ulong ELFType;
    }

    // Token: 0x0200002D RID: 45
    public struct ModuleInfo
    {
        // Token: 0x04000104 RID: 260
        public PS3TMAPI.ModuleInfoHdr Hdr;

        // Token: 0x04000105 RID: 261
        public PS3TMAPI.PRXSegment[] Segments;
    }

    // Token: 0x0200002E RID: 46
    public struct PRXSegmentEx
    {
        // Token: 0x04000106 RID: 262
        public ulong Base;

        // Token: 0x04000107 RID: 263
        public ulong FileSize;

        // Token: 0x04000108 RID: 264
        public ulong MemSize;

        // Token: 0x04000109 RID: 265
        public ulong Index;

        // Token: 0x0400010A RID: 266
        public ulong ELFType;

        // Token: 0x0400010B RID: 267
        public ulong Flags;

        // Token: 0x0400010C RID: 268
        public ulong Align;
    }

    // Token: 0x0200002F RID: 47
    public struct ModuleInfoEx
    {
        // Token: 0x0400010D RID: 269
        public PS3TMAPI.ModuleInfoHdr Hdr;

        // Token: 0x0400010E RID: 270
        public PS3TMAPI.PRXSegmentEx[] Segments;
    }

    // Token: 0x02000030 RID: 48
    public struct MSELFInfo
    {
        // Token: 0x0400010F RID: 271
        public ulong MSELFFileOffset;

        // Token: 0x04000110 RID: 272
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] Reserved;
    }

    // Token: 0x02000031 RID: 49
    public struct ExtraModuleInfo
    {
        // Token: 0x04000111 RID: 273
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public uint[] PPUGUIDs;
    }

    // Token: 0x02000032 RID: 50
    public enum PPUThreadState
    {
        // Token: 0x04000113 RID: 275
        Idle,
        // Token: 0x04000114 RID: 276
        Runnable,
        // Token: 0x04000115 RID: 277
        OnProc,
        // Token: 0x04000116 RID: 278
        Sleep,
        // Token: 0x04000117 RID: 279
        Suspended,
        // Token: 0x04000118 RID: 280
        SleepSuspended,
        // Token: 0x04000119 RID: 281
        Stop,
        // Token: 0x0400011A RID: 282
        Zombie,
        // Token: 0x0400011B RID: 283
        Deleted
    }

    // Token: 0x02000033 RID: 51
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct PPUThreadInfoPriv
    {
        // Token: 0x0400011C RID: 284
        public ulong ThreadID;

        // Token: 0x0400011D RID: 285
        public uint Priority;

        // Token: 0x0400011E RID: 286
        public uint State;

        // Token: 0x0400011F RID: 287
        public ulong StackAddress;

        // Token: 0x04000120 RID: 288
        public ulong StackSize;

        // Token: 0x04000121 RID: 289
        public uint ThreadNameLen;
    }

    // Token: 0x02000034 RID: 52
    public struct PPUThreadInfo
    {
        // Token: 0x04000122 RID: 290
        public ulong ThreadID;

        // Token: 0x04000123 RID: 291
        public uint Priority;

        // Token: 0x04000124 RID: 292
        public PS3TMAPI.PPUThreadState State;

        // Token: 0x04000125 RID: 293
        public ulong StackAddress;

        // Token: 0x04000126 RID: 294
        public ulong StackSize;

        // Token: 0x04000127 RID: 295
        public string ThreadName;
    }

    // Token: 0x02000035 RID: 53
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct PPUThreadInfoExPriv
    {
        // Token: 0x04000128 RID: 296
        public ulong ThreadId;

        // Token: 0x04000129 RID: 297
        public uint Priority;

        // Token: 0x0400012A RID: 298
        public uint BasePriority;

        // Token: 0x0400012B RID: 299
        public uint State;

        // Token: 0x0400012C RID: 300
        public ulong StackAddress;

        // Token: 0x0400012D RID: 301
        public ulong StackSize;

        // Token: 0x0400012E RID: 302
        public uint ThreadNameLen;
    }

    // Token: 0x02000036 RID: 54
    public struct PPUThreadInfoEx
    {
        // Token: 0x0400012F RID: 303
        public ulong ThreadID;

        // Token: 0x04000130 RID: 304
        public uint Priority;

        // Token: 0x04000131 RID: 305
        public uint BasePriority;

        // Token: 0x04000132 RID: 306
        public PS3TMAPI.PPUThreadState State;

        // Token: 0x04000133 RID: 307
        public ulong StackAddress;

        // Token: 0x04000134 RID: 308
        public ulong StackSize;

        // Token: 0x04000135 RID: 309
        public string ThreadName;
    }

    // Token: 0x02000037 RID: 55
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SpuThreadInfoPriv
    {
        // Token: 0x04000136 RID: 310
        public uint ThreadGroupId;

        // Token: 0x04000137 RID: 311
        public uint ThreadId;

        // Token: 0x04000138 RID: 312
        public uint FilenameLen;

        // Token: 0x04000139 RID: 313
        public uint ThreadNameLen;
    }

    // Token: 0x02000038 RID: 56
    public struct SPUThreadInfo
    {
        // Token: 0x0400013A RID: 314
        public uint ThreadGroupID;

        // Token: 0x0400013B RID: 315
        public uint ThreadID;

        // Token: 0x0400013C RID: 316
        public string Filename;

        // Token: 0x0400013D RID: 317
        public string ThreadName;
    }

    // Token: 0x02000039 RID: 57
    [Flags]
    public enum ELFStackSize : uint
    {
        // Token: 0x0400013F RID: 319
        Stack32k = 32U,
        // Token: 0x04000140 RID: 320
        Stack64k = 64U,
        // Token: 0x04000141 RID: 321
        Stack96k = 96U,
        // Token: 0x04000142 RID: 322
        Stack128k = 128U,
        // Token: 0x04000143 RID: 323
        Stack256k = 256U,
        // Token: 0x04000144 RID: 324
        Stack512k = 512U,
        // Token: 0x04000145 RID: 325
        Stack1024k = 1024U,
        // Token: 0x04000146 RID: 326
        StackDefault = 64U
    }

    // Token: 0x0200003A RID: 58
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct DebugThreadControlInfoPriv
    {
        // Token: 0x04000147 RID: 327
        public ulong ControlFlags;

        // Token: 0x04000148 RID: 328
        public uint NumEntries;
    }

    // Token: 0x0200003B RID: 59
    public struct ControlKeywordEntry
    {
        // Token: 0x04000149 RID: 329
        public uint MatchConditionFlags;

        // Token: 0x0400014A RID: 330
        public string Keyword;
    }

    // Token: 0x0200003C RID: 60
    public struct DebugThreadControlInfo
    {
        // Token: 0x0400014B RID: 331
        public ulong ControlFlags;

        // Token: 0x0400014C RID: 332
        public PS3TMAPI.ControlKeywordEntry[] ControlKeywords;
    }

    // Token: 0x0200003D RID: 61
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct ProcessTreeBranchPriv
    {
        // Token: 0x0400014D RID: 333
        public uint ProcessId;

        // Token: 0x0400014E RID: 334
        public PS3TMAPI.ProcessStatus ProcessState;

        // Token: 0x0400014F RID: 335
        public uint NumPpuThreads;

        // Token: 0x04000150 RID: 336
        public uint NumSpuThreadGroups;

        // Token: 0x04000151 RID: 337
        public ushort ProcessFlags;

        // Token: 0x04000152 RID: 338
        public ushort RawSPU;

        // Token: 0x04000153 RID: 339
        public IntPtr PpuThreadStatuses;

        // Token: 0x04000154 RID: 340
        public IntPtr SpuThreadGroupStatuses;
    }

    // Token: 0x0200003E RID: 62
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PPUThreadStatus
    {
        // Token: 0x04000155 RID: 341
        public ulong ThreadID;

        // Token: 0x04000156 RID: 342
        public PS3TMAPI.PPUThreadState ThreadState;
    }

    // Token: 0x0200003F RID: 63
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SPUThreadGroupStatus
    {
        // Token: 0x04000157 RID: 343
        public uint ThreadGroupID;

        // Token: 0x04000158 RID: 344
        public PS3TMAPI.SPUThreadGroupState ThreadGroupState;
    }

    // Token: 0x02000040 RID: 64
    public struct ProcessTreeBranch
    {
        // Token: 0x04000159 RID: 345
        public uint ProcessID;

        // Token: 0x0400015A RID: 346
        public PS3TMAPI.ProcessStatus ProcessState;

        // Token: 0x0400015B RID: 347
        public ushort ProcessFlags;

        // Token: 0x0400015C RID: 348
        public ushort RawSPU;

        // Token: 0x0400015D RID: 349
        public PS3TMAPI.PPUThreadStatus[] PPUThreadStatuses;

        // Token: 0x0400015E RID: 350
        public PS3TMAPI.SPUThreadGroupStatus[] SPUThreadGroupStatuses;
    }

    // Token: 0x02000041 RID: 65
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SpuThreadGroupInfoPriv
    {
        // Token: 0x0400015F RID: 351
        public uint ThreadGroupId;

        // Token: 0x04000160 RID: 352
        public uint State;

        // Token: 0x04000161 RID: 353
        public uint Priority;

        // Token: 0x04000162 RID: 354
        public uint NumThreads;

        // Token: 0x04000163 RID: 355
        public uint ThreadGroupNameLen;
    }

    // Token: 0x02000042 RID: 66
    public enum SPUThreadGroupState : uint
    {
        // Token: 0x04000165 RID: 357
        NotConfigured,
        // Token: 0x04000166 RID: 358
        Configured,
        // Token: 0x04000167 RID: 359
        Ready,
        // Token: 0x04000168 RID: 360
        Waiting,
        // Token: 0x04000169 RID: 361
        Suspended,
        // Token: 0x0400016A RID: 362
        WaitingSuspended,
        // Token: 0x0400016B RID: 363
        Running,
        // Token: 0x0400016C RID: 364
        Stopped
    }

    // Token: 0x02000043 RID: 67
    public struct SPUThreadGroupInfo
    {
        // Token: 0x0400016D RID: 365
        public uint ThreadGroupID;

        // Token: 0x0400016E RID: 366
        public PS3TMAPI.SPUThreadGroupState State;

        // Token: 0x0400016F RID: 367
        public uint Priority;

        // Token: 0x04000170 RID: 368
        public string GroupName;

        // Token: 0x04000171 RID: 369
        public uint[] ThreadIDs;
    }

    // Token: 0x02000044 RID: 68
    public enum MemoryCompressionLevel : uint
    {
        // Token: 0x04000173 RID: 371
        None,
        // Token: 0x04000174 RID: 372
        BestSpeed,
        // Token: 0x04000175 RID: 373
        BestCompression = 9U,
        // Token: 0x04000176 RID: 374
        Default = 4294967295U
    }

    // Token: 0x02000045 RID: 69
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VirtualMemoryArea
    {
        // Token: 0x04000177 RID: 375
        public ulong Address;

        // Token: 0x04000178 RID: 376
        public ulong Flags;

        // Token: 0x04000179 RID: 377
        public ulong VSize;

        // Token: 0x0400017A RID: 378
        public ulong Options;

        // Token: 0x0400017B RID: 379
        public ulong PageFaultPPU;

        // Token: 0x0400017C RID: 380
        public ulong PageFaultSPU;

        // Token: 0x0400017D RID: 381
        public ulong PageIn;

        // Token: 0x0400017E RID: 382
        public ulong PageOut;

        // Token: 0x0400017F RID: 383
        public ulong PMemTotal;

        // Token: 0x04000180 RID: 384
        public ulong PMemUsed;

        // Token: 0x04000181 RID: 385
        public ulong Time;

        // Token: 0x04000182 RID: 386
        public ulong[] Pages;
    }

    // Token: 0x02000046 RID: 70
    public struct SyncPrimitiveCounts
    {
        // Token: 0x04000183 RID: 387
        public uint NumMutexes;

        // Token: 0x04000184 RID: 388
        public uint NumConditionVariables;

        // Token: 0x04000185 RID: 389
        public uint NumRWLocks;

        // Token: 0x04000186 RID: 390
        public uint NumLWMutexes;

        // Token: 0x04000187 RID: 391
        public uint NumEventQueues;

        // Token: 0x04000188 RID: 392
        public uint NumSemaphores;

        // Token: 0x04000189 RID: 393
        public uint NumLWConditionVariables;

        // Token: 0x0400018A RID: 394
        public uint NumEventFlag;
    }

    // Token: 0x02000047 RID: 71
    public struct MutexAttr
    {
        // Token: 0x0400018B RID: 395
        public uint Protocol;

        // Token: 0x0400018C RID: 396
        public uint Recursive;

        // Token: 0x0400018D RID: 397
        public uint PShared;

        // Token: 0x0400018E RID: 398
        public uint Adaptive;

        // Token: 0x0400018F RID: 399
        public ulong Key;

        // Token: 0x04000190 RID: 400
        public uint Flags;

        // Token: 0x04000191 RID: 401
        public string Name;
    }

    // Token: 0x02000048 RID: 72
    public struct MutexInfo
    {
        // Token: 0x04000192 RID: 402
        public uint ID;

        // Token: 0x04000193 RID: 403
        public PS3TMAPI.MutexAttr Attribute;

        // Token: 0x04000194 RID: 404
        public ulong OwnerThreadID;

        // Token: 0x04000195 RID: 405
        public uint LockCounter;

        // Token: 0x04000196 RID: 406
        public uint ConditionRefCounter;

        // Token: 0x04000197 RID: 407
        public uint ConditionVarID;

        // Token: 0x04000198 RID: 408
        public uint NumWaitAllThreads;

        // Token: 0x04000199 RID: 409
        public ulong[] WaitingThreads;
    }

    // Token: 0x02000049 RID: 73
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct LwMutexInfoPriv
    {
        // Token: 0x0400019A RID: 410
        public uint Id;

        // Token: 0x0400019B RID: 411
        public PS3TMAPI.LWMutexAttr Attribute;

        // Token: 0x0400019C RID: 412
        public ulong OwnerThreadId;

        // Token: 0x0400019D RID: 413
        public uint LockCounter;

        // Token: 0x0400019E RID: 414
        public uint NumWaitingThreads;

        // Token: 0x0400019F RID: 415
        public uint NumWaitAllThreads;
    }

    // Token: 0x0200004A RID: 74
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LWMutexAttr
    {
        // Token: 0x040001A0 RID: 416
        public uint Protocol;

        // Token: 0x040001A1 RID: 417
        public uint Recursive;

        // Token: 0x040001A2 RID: 418
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    // Token: 0x0200004B RID: 75
    public struct LWMutexInfo
    {
        // Token: 0x040001A3 RID: 419
        public uint ID;

        // Token: 0x040001A4 RID: 420
        public PS3TMAPI.LWMutexAttr Attribute;

        // Token: 0x040001A5 RID: 421
        public ulong OwnerThreadID;

        // Token: 0x040001A6 RID: 422
        public uint LockCounter;

        // Token: 0x040001A7 RID: 423
        public uint NumWaitAllThreads;

        // Token: 0x040001A8 RID: 424
        public ulong[] WaitingThreads;
    }

    // Token: 0x0200004C RID: 76
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct ConditionVarInfoPriv
    {
        // Token: 0x040001A9 RID: 425
        public uint Id;

        // Token: 0x040001AA RID: 426
        public PS3TMAPI.ConditionVarAttr Attribute;

        // Token: 0x040001AB RID: 427
        public uint MutexId;

        // Token: 0x040001AC RID: 428
        public uint NumWaitingThreads;

        // Token: 0x040001AD RID: 429
        public uint NumWaitAllThreads;
    }

    // Token: 0x0200004D RID: 77
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ConditionVarAttr
    {
        // Token: 0x040001AE RID: 430
        public uint PShared;

        // Token: 0x040001AF RID: 431
        public ulong Key;

        // Token: 0x040001B0 RID: 432
        public uint Flags;

        // Token: 0x040001B1 RID: 433
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    // Token: 0x0200004E RID: 78
    public struct ConditionVarInfo
    {
        // Token: 0x040001B2 RID: 434
        public uint ID;

        // Token: 0x040001B3 RID: 435
        public PS3TMAPI.ConditionVarAttr Attribute;

        // Token: 0x040001B4 RID: 436
        public uint MutexID;

        // Token: 0x040001B5 RID: 437
        public uint NumWaitAllThreads;

        // Token: 0x040001B6 RID: 438
        public ulong[] WaitingThreads;
    }

    // Token: 0x0200004F RID: 79
    private struct LwConditionVarInfoPriv
    {
        // Token: 0x040001B7 RID: 439
        public uint Id;

        // Token: 0x040001B8 RID: 440
        public PS3TMAPI.LWConditionVarAttr Attribute;

        // Token: 0x040001B9 RID: 441
        public uint LwMutexId;

        // Token: 0x040001BA RID: 442
        public uint NumWaitingThreads;

        // Token: 0x040001BB RID: 443
        public uint NumWaitAllThreads;
    }

    // Token: 0x02000050 RID: 80
    public struct LWConditionVarAttr
    {
        // Token: 0x040001BC RID: 444
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    // Token: 0x02000051 RID: 81
    public struct LWConditionVarInfo
    {
        // Token: 0x040001BD RID: 445
        public uint ID;

        // Token: 0x040001BE RID: 446
        public PS3TMAPI.LWConditionVarAttr Attribute;

        // Token: 0x040001BF RID: 447
        public uint LWMutexID;

        // Token: 0x040001C0 RID: 448
        public uint NumWaitAllThreads;

        // Token: 0x040001C1 RID: 449
        public ulong[] WaitingThreads;
    }

    // Token: 0x02000052 RID: 82
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct RwLockInfoPriv
    {
        // Token: 0x040001C2 RID: 450
        public uint Id;

        // Token: 0x040001C3 RID: 451
        public PS3TMAPI.RWLockAttr Attribute;

        // Token: 0x040001C4 RID: 452
        public ulong OwnerThreadId;

        // Token: 0x040001C5 RID: 453
        public uint NumWaitingReadThreads;

        // Token: 0x040001C6 RID: 454
        public uint NumWaitAllReadThreads;

        // Token: 0x040001C7 RID: 455
        public uint NumWaitingWriteThreads;

        // Token: 0x040001C8 RID: 456
        public uint NumWaitAllWriteThreads;
    }

    // Token: 0x02000053 RID: 83
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RWLockAttr
    {
        // Token: 0x040001C9 RID: 457
        public uint Protocol;

        // Token: 0x040001CA RID: 458
        public uint PShared;

        // Token: 0x040001CB RID: 459
        public ulong Key;

        // Token: 0x040001CC RID: 460
        public uint Flags;

        // Token: 0x040001CD RID: 461
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    // Token: 0x02000054 RID: 84
    public struct RWLockInfo
    {
        // Token: 0x040001CE RID: 462
        public uint ID;

        // Token: 0x040001CF RID: 463
        public PS3TMAPI.RWLockAttr Attribute;

        // Token: 0x040001D0 RID: 464
        public ulong OwnerThreadID;

        // Token: 0x040001D1 RID: 465
        public uint NumWaitingReadThreads;

        // Token: 0x040001D2 RID: 466
        public uint NumWaitAllReadThreads;

        // Token: 0x040001D3 RID: 467
        public uint NumWaitingWriteThreads;

        // Token: 0x040001D4 RID: 468
        public uint NumWaitAllWriteThreads;

        // Token: 0x040001D5 RID: 469
        public ulong[] WaitingThreads;
    }

    // Token: 0x02000055 RID: 85
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SemaphoreInfoPriv
    {
        // Token: 0x040001D6 RID: 470
        public uint Id;

        // Token: 0x040001D7 RID: 471
        public PS3TMAPI.SemaphoreAttr Attribute;

        // Token: 0x040001D8 RID: 472
        public uint MaxValue;

        // Token: 0x040001D9 RID: 473
        public uint CurrentValue;

        // Token: 0x040001DA RID: 474
        public uint NumWaitingThreads;

        // Token: 0x040001DB RID: 475
        public uint NumWaitAllThreads;
    }

    // Token: 0x02000056 RID: 86
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SemaphoreAttr
    {
        // Token: 0x040001DC RID: 476
        public uint Protocol;

        // Token: 0x040001DD RID: 477
        public uint PShared;

        // Token: 0x040001DE RID: 478
        public ulong Key;

        // Token: 0x040001DF RID: 479
        public uint Flags;

        // Token: 0x040001E0 RID: 480
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    // Token: 0x02000057 RID: 87
    public struct SemaphoreInfo
    {
        // Token: 0x040001E1 RID: 481
        public uint ID;

        // Token: 0x040001E2 RID: 482
        public PS3TMAPI.SemaphoreAttr Attribute;

        // Token: 0x040001E3 RID: 483
        public uint MaxValue;

        // Token: 0x040001E4 RID: 484
        public uint CurrentValue;

        // Token: 0x040001E5 RID: 485
        public uint NumWaitAllThreads;

        // Token: 0x040001E6 RID: 486
        public ulong[] WaitingThreads;
    }

    // Token: 0x02000058 RID: 88
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct EventQueueInfoPriv
    {
        // Token: 0x040001E7 RID: 487
        public uint Id;

        // Token: 0x040001E8 RID: 488
        public PS3TMAPI.EventQueueAttr Attribute;

        // Token: 0x040001E9 RID: 489
        public ulong Key;

        // Token: 0x040001EA RID: 490
        public uint Size;

        // Token: 0x040001EB RID: 491
        public uint NumWaitingThreads;

        // Token: 0x040001EC RID: 492
        public uint NumWaitAllThreads;

        // Token: 0x040001ED RID: 493
        public uint NumReadableEvQueue;

        // Token: 0x040001EE RID: 494
        public uint NumReadableAllEvQueue;

        // Token: 0x040001EF RID: 495
        public IntPtr WaitingThreadIds;

        // Token: 0x040001F0 RID: 496
        public IntPtr QueueEntries;
    }

    // Token: 0x02000059 RID: 89
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventQueueAttr
    {
        // Token: 0x040001F1 RID: 497
        public uint Protocol;

        // Token: 0x040001F2 RID: 498
        public uint Type;

        // Token: 0x040001F3 RID: 499
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    // Token: 0x0200005A RID: 90
    public struct SystemEvent
    {
        // Token: 0x040001F4 RID: 500
        public ulong Source;

        // Token: 0x040001F5 RID: 501
        public ulong Data1;

        // Token: 0x040001F6 RID: 502
        public ulong Data2;

        // Token: 0x040001F7 RID: 503
        public ulong Data3;
    }

    // Token: 0x0200005B RID: 91
    public struct EventQueueInfo
    {
        // Token: 0x040001F8 RID: 504
        public uint ID;

        // Token: 0x040001F9 RID: 505
        public PS3TMAPI.EventQueueAttr Attribute;

        // Token: 0x040001FA RID: 506
        public ulong Key;

        // Token: 0x040001FB RID: 507
        public uint Size;

        // Token: 0x040001FC RID: 508
        public uint NumWaitAllThreads;

        // Token: 0x040001FD RID: 509
        public uint NumReadableAllEvQueue;

        // Token: 0x040001FE RID: 510
        public ulong[] WaitingThreadIDs;

        // Token: 0x040001FF RID: 511
        public PS3TMAPI.SystemEvent[] QueueEntries;
    }

    // Token: 0x0200005C RID: 92
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventFlagWaitThread
    {
        // Token: 0x04000200 RID: 512
        public ulong ID;

        // Token: 0x04000201 RID: 513
        public ulong BitPattern;

        // Token: 0x04000202 RID: 514
        public uint Mode;
    }

    // Token: 0x0200005D RID: 93
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventFlagAttr
    {
        // Token: 0x04000203 RID: 515
        public uint Protocol;

        // Token: 0x04000204 RID: 516
        public uint PShared;

        // Token: 0x04000205 RID: 517
        public ulong Key;

        // Token: 0x04000206 RID: 518
        public uint Flags;

        // Token: 0x04000207 RID: 519
        public uint Type;

        // Token: 0x04000208 RID: 520
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    // Token: 0x0200005E RID: 94
    public struct EventFlagInfo
    {
        // Token: 0x04000209 RID: 521
        public uint ID;

        // Token: 0x0400020A RID: 522
        public PS3TMAPI.EventFlagAttr Attribute;

        // Token: 0x0400020B RID: 523
        public ulong BitPattern;

        // Token: 0x0400020C RID: 524
        public uint NumWaitAllThreads;

        // Token: 0x0400020D RID: 525
        public PS3TMAPI.EventFlagWaitThread[] WaitingThreads;
    }

    // Token: 0x0200005F RID: 95
    public enum PowerStatus
    {
        // Token: 0x0400020F RID: 527
        Off,
        // Token: 0x04000210 RID: 528
        On,
        // Token: 0x04000211 RID: 529
        Suspended,
        // Token: 0x04000212 RID: 530
        Unknown,
        // Token: 0x04000213 RID: 531
        SwitchingOn
    }

    // Token: 0x02000060 RID: 96
    public struct UserMemoryStats
    {
        // Token: 0x04000214 RID: 532
        public uint CreatedSharedMemorySize;

        // Token: 0x04000215 RID: 533
        public uint AttachedSharedMemorySize;

        // Token: 0x04000216 RID: 534
        public uint ProcessLocalMemorySize;

        // Token: 0x04000217 RID: 535
        public uint ProcessLocalTextSize;

        // Token: 0x04000218 RID: 536
        public uint PRXTextSize;

        // Token: 0x04000219 RID: 537
        public uint PRXDataSize;

        // Token: 0x0400021A RID: 538
        public uint MiscMemorySize;
    }

    // Token: 0x02000061 RID: 97
    public struct GamePortIPAddressData
    {
        // Token: 0x0400021B RID: 539
        public uint ReturnValue;

        // Token: 0x0400021C RID: 540
        public uint IPAddress;

        // Token: 0x0400021D RID: 541
        public uint SubnetMask;

        // Token: 0x0400021E RID: 542
        public uint BroadcastAddress;
    }

    // Token: 0x02000062 RID: 98
    [Flags]
    public enum RSXProfilingFlag : ulong
    {
        // Token: 0x04000220 RID: 544
        UseRSXProfilingTools = 1UL,
        // Token: 0x04000221 RID: 545
        UseFullHUDFeatures = 2UL
    }

    // Token: 0x02000063 RID: 99
    [Flags]
    public enum CoreDumpFlag : ulong
    {
        // Token: 0x04000223 RID: 547
        ToDevMS = 1UL,
        // Token: 0x04000224 RID: 548
        ToAppHome = 2UL,
        // Token: 0x04000225 RID: 549
        ToDevUSB = 4UL,
        // Token: 0x04000226 RID: 550
        ToDevHDD0 = 8UL,
        // Token: 0x04000227 RID: 551
        DisablePPUExceptionDetection = 36028797018963968UL,
        // Token: 0x04000228 RID: 552
        DisableSPUExceptionDetection = 18014398509481984UL,
        // Token: 0x04000229 RID: 553
        DisableRSXExceptionDetection = 9007199254740992UL,
        // Token: 0x0400022A RID: 554
        DisableFootSwitchDetection = 4503599627370496UL,
        // Token: 0x0400022B RID: 555
        DisableMemoryDump = 3489660928UL,
        // Token: 0x0400022C RID: 556
        EnableRestartProcess = 32768UL,
        // Token: 0x0400022D RID: 557
        EnableKeepRunningHandler = 8192UL
    }

    // Token: 0x02000064 RID: 100
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ScatteredWrite
    {
        // Token: 0x0400022E RID: 558
        public uint Address;

        // Token: 0x0400022F RID: 559
        public byte[] Data;
    }

    // Token: 0x02000065 RID: 101
    public enum MATCondition : byte
    {
        // Token: 0x04000231 RID: 561
        Transparent,
        // Token: 0x04000232 RID: 562
        Write,
        // Token: 0x04000233 RID: 563
        ReadWrite,
        // Token: 0x04000234 RID: 564
        Error
    }

    // Token: 0x02000066 RID: 102
    public struct MATRange
    {
        // Token: 0x04000235 RID: 565
        public uint StartAddress;

        // Token: 0x04000236 RID: 566
        public uint Size;

        // Token: 0x04000237 RID: 567
        public PS3TMAPI.MATCondition[] PageConditions;
    }

    // Token: 0x02000067 RID: 103
    public enum PadPlaybackResponse : uint
    {
        // Token: 0x04000239 RID: 569
        Ok,
        // Token: 0x0400023A RID: 570
        InvalidPacket = 2147549186U,
        // Token: 0x0400023B RID: 571
        InsufficientMemory = 2147549188U,
        // Token: 0x0400023C RID: 572
        Busy = 2147549194U,
        // Token: 0x0400023D RID: 573
        NoDev = 2147549229U
    }

    // Token: 0x02000068 RID: 104
    // (Invoke) Token: 0x060002B7 RID: 695
    public delegate void PadPlaybackCallback(int target, PS3TMAPI.SNRESULT res, PS3TMAPI.PadPlaybackResponse playbackResult, object userData);

    // Token: 0x02000069 RID: 105
    private class PadPlaybackCallbackAndUserData
    {
        // Token: 0x0400023E RID: 574
        public PS3TMAPI.PadPlaybackCallback m_callback;

        // Token: 0x0400023F RID: 575
        public object m_userData;
    }

    // Token: 0x0200006A RID: 106
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PadData
    {
        // Token: 0x04000240 RID: 576
        public uint TimeHi;

        // Token: 0x04000241 RID: 577
        public uint TimeLo;

        // Token: 0x04000242 RID: 578
        public uint Reserved0;

        // Token: 0x04000243 RID: 579
        public uint Reserved1;

        // Token: 0x04000244 RID: 580
        public byte Port;

        // Token: 0x04000245 RID: 581
        public byte PortStatus;

        // Token: 0x04000246 RID: 582
        public byte Length;

        // Token: 0x04000247 RID: 583
        public byte Reserved2;

        // Token: 0x04000248 RID: 584
        public uint Reserved3;

        // Token: 0x04000249 RID: 585
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public short[] buttons;
    }

    // Token: 0x0200006B RID: 107
    // (Invoke) Token: 0x060002BC RID: 700
    public delegate void PadCaptureCallback(int target, PS3TMAPI.SNRESULT res, PS3TMAPI.PadData[] padData, object userData);

    // Token: 0x0200006C RID: 108
    private class PadCaptureCallbackAndUserData
    {
        // Token: 0x0400024A RID: 586
        public PS3TMAPI.PadCaptureCallback m_callback;

        // Token: 0x0400024B RID: 587
        public object m_userData;
    }

    // Token: 0x0200006D RID: 109
    [Flags]
    public enum VRAMCaptureFlag : ulong
    {
        // Token: 0x0400024D RID: 589
        Enabled = 1UL,
        // Token: 0x0400024E RID: 590
        Disabled = 0UL
    }

    // Token: 0x0200006E RID: 110
    public class VRAMInfo
    {
        // Token: 0x0400024F RID: 591
        public ulong BPAddress;

        // Token: 0x04000250 RID: 592
        public ulong TopAddressPointer;

        // Token: 0x04000251 RID: 593
        public uint Width;

        // Token: 0x04000252 RID: 594
        public uint Height;

        // Token: 0x04000253 RID: 595
        public uint Pitch;

        // Token: 0x04000254 RID: 596
        public byte Colour;
    }

    // Token: 0x0200006F RID: 111
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct VramInfoPriv
    {
        // Token: 0x04000255 RID: 597
        public ulong BpAddress;

        // Token: 0x04000256 RID: 598
        public ulong TopAddressPointer;

        // Token: 0x04000257 RID: 599
        public uint Width;

        // Token: 0x04000258 RID: 600
        public uint Height;

        // Token: 0x04000259 RID: 601
        public uint Pitch;

        // Token: 0x0400025A RID: 602
        public byte Colour;
    }

    // Token: 0x02000070 RID: 112
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PS3Protocol
    {
        // Token: 0x0400025B RID: 603
        public uint Protocol;

        // Token: 0x0400025C RID: 604
        public uint Port;

        // Token: 0x0400025D RID: 605
        public uint LPARDesc;
    }

    // Token: 0x02000071 RID: 113
    private struct PS3ProtocolPriv
    {
        // Token: 0x060002C1 RID: 705 RVA: 0x00008886 File Offset: 0x00006A86
        public PS3ProtocolPriv(uint protocol, uint port)
        {
            this.Protocol = port;
            this.Port = protocol;
        }

        // Token: 0x0400025E RID: 606
        public readonly uint Protocol;

        // Token: 0x0400025F RID: 607
        public readonly uint Port;
    }

    // Token: 0x02000072 RID: 114
    private struct CustomProtocolId
    {
        // Token: 0x060002C2 RID: 706 RVA: 0x00008896 File Offset: 0x00006A96
        public CustomProtocolId(int target, PS3TMAPI.PS3ProtocolPriv protocol)
        {
            this.Target = target;
            this.Protocol = protocol;
        }

        // Token: 0x04000260 RID: 608
        public readonly int Target;

        // Token: 0x04000261 RID: 609
        public readonly PS3TMAPI.PS3ProtocolPriv Protocol;
    }

    // Token: 0x02000073 RID: 115
    // (Invoke) Token: 0x060002C4 RID: 708
    private delegate void CustomProtocolCallbackPriv(int target, PS3TMAPI.PS3Protocol protocol, IntPtr unmanagedBuf, uint length, IntPtr userData);

    // Token: 0x02000074 RID: 116
    // (Invoke) Token: 0x060002C8 RID: 712
    public delegate void CustomProtocolCallback(int target, PS3TMAPI.PS3Protocol protocol, byte[] data, object userData);

    // Token: 0x02000075 RID: 117
    private class CusProtoCallbackAndUserData
    {
        // Token: 0x04000262 RID: 610
        public PS3TMAPI.CustomProtocolCallback m_callback;

        // Token: 0x04000263 RID: 611
        public object m_userData;
    }

    // Token: 0x02000076 RID: 118
    [Flags]
    public enum FileServingEventFlag : ulong
    {
        // Token: 0x04000265 RID: 613
        Create = 1UL,
        // Token: 0x04000266 RID: 614
        Close = 4UL,
        // Token: 0x04000267 RID: 615
        Read = 8UL,
        // Token: 0x04000268 RID: 616
        Write = 16UL,
        // Token: 0x04000269 RID: 617
        Seek = 32UL,
        // Token: 0x0400026A RID: 618
        Delete = 64UL,
        // Token: 0x0400026B RID: 619
        Rename = 128UL,
        // Token: 0x0400026C RID: 620
        SetAttr = 256UL,
        // Token: 0x0400026D RID: 621
        GetAttr = 512UL,
        // Token: 0x0400026E RID: 622
        SetTime = 1024UL,
        // Token: 0x0400026F RID: 623
        MKDir = 2048UL,
        // Token: 0x04000270 RID: 624
        RMDir = 4096UL,
        // Token: 0x04000271 RID: 625
        OpenDir = 8192UL,
        // Token: 0x04000272 RID: 626
        CloseDir = 16384UL,
        // Token: 0x04000273 RID: 627
        ReadDir = 32768UL,
        // Token: 0x04000274 RID: 628
        Truncate = 65536UL,
        // Token: 0x04000275 RID: 629
        FGetAttr64 = 131072UL,
        // Token: 0x04000276 RID: 630
        GetAttr64 = 262144UL,
        // Token: 0x04000277 RID: 631
        All = 524285UL
    }

    // Token: 0x02000077 RID: 119
    public enum FileTransferNotificationType : uint
    {
        // Token: 0x04000279 RID: 633
        Progress,
        // Token: 0x0400027A RID: 634
        Finish,
        // Token: 0x0400027B RID: 635
        Skipped,
        // Token: 0x0400027C RID: 636
        Cancelled,
        // Token: 0x0400027D RID: 637
        Error,
        // Token: 0x0400027E RID: 638
        Pending,
        // Token: 0x0400027F RID: 639
        Unknown,
        // Token: 0x04000280 RID: 640
        RefreshList = 2147483648U
    }

    // Token: 0x02000078 RID: 120
    public struct FTPNotification
    {
        // Token: 0x04000281 RID: 641
        public PS3TMAPI.FileTransferNotificationType Type;

        // Token: 0x04000282 RID: 642
        public uint TransferID;

        // Token: 0x04000283 RID: 643
        public ulong BytesTransferred;
    }

    // Token: 0x02000079 RID: 121
    // (Invoke) Token: 0x060002CD RID: 717
    public delegate void FTPEventCallback(int target, PS3TMAPI.SNRESULT res, PS3TMAPI.FTPNotification[] ftpNotifications, object userData);

    // Token: 0x0200007A RID: 122
    private class FtpCallbackAndUserData
    {
        // Token: 0x04000284 RID: 644
        public PS3TMAPI.FTPEventCallback m_callback;

        // Token: 0x04000285 RID: 645
        public object m_userData;
    }

    // Token: 0x0200007B RID: 123
    public enum FileTraceType
    {
        // Token: 0x04000287 RID: 647
        GetBlockSize = 1,
        // Token: 0x04000288 RID: 648
        Stat,
        // Token: 0x04000289 RID: 649
        WidgetStat,
        // Token: 0x0400028A RID: 650
        Unlink,
        // Token: 0x0400028B RID: 651
        WidgetUnlink,
        // Token: 0x0400028C RID: 652
        RMDir,
        // Token: 0x0400028D RID: 653
        WidgetRMDir,
        // Token: 0x0400028E RID: 654
        Rename = 14,
        // Token: 0x0400028F RID: 655
        WidgetRename,
        // Token: 0x04000290 RID: 656
        Truncate = 18,
        // Token: 0x04000291 RID: 657
        TruncateNoAlloc,
        // Token: 0x04000292 RID: 658
        Truncate2,
        // Token: 0x04000293 RID: 659
        Truncate2NoInit,
        // Token: 0x04000294 RID: 660
        OpenDir = 24,
        // Token: 0x04000295 RID: 661
        WidgetOpenDir,
        // Token: 0x04000296 RID: 662
        CHMod,
        // Token: 0x04000297 RID: 663
        MkDir,
        // Token: 0x04000298 RID: 664
        UTime = 29,
        // Token: 0x04000299 RID: 665
        Open = 33,
        // Token: 0x0400029A RID: 666
        WidgetOpen,
        // Token: 0x0400029B RID: 667
        Close,
        // Token: 0x0400029C RID: 668
        CloseDir,
        // Token: 0x0400029D RID: 669
        FSync,
        // Token: 0x0400029E RID: 670
        ReadDir,
        // Token: 0x0400029F RID: 671
        FStat,
        // Token: 0x040002A0 RID: 672
        FGetBlockSize,
        // Token: 0x040002A1 RID: 673
        Read = 47,
        // Token: 0x040002A2 RID: 674
        Write,
        // Token: 0x040002A3 RID: 675
        GetDirEntries,
        // Token: 0x040002A4 RID: 676
        ReadOffset,
        // Token: 0x040002A5 RID: 677
        WriteOffset,
        // Token: 0x040002A6 RID: 678
        FTruncate,
        // Token: 0x040002A7 RID: 679
        FTruncateNoAlloc,
        // Token: 0x040002A8 RID: 680
        LSeek = 56,
        // Token: 0x040002A9 RID: 681
        SetIOBuffer,
        // Token: 0x040002AA RID: 682
        OfflineEnd = 9999
    }

    // Token: 0x0200007C RID: 124
    public enum FileTraceNotificationStatus
    {
        // Token: 0x040002AC RID: 684
        Processed,
        // Token: 0x040002AD RID: 685
        Received,
        // Token: 0x040002AE RID: 686
        Waiting,
        // Token: 0x040002AF RID: 687
        Processing,
        // Token: 0x040002B0 RID: 688
        Suspended,
        // Token: 0x040002B1 RID: 689
        Finished
    }

    // Token: 0x0200007D RID: 125
    public struct FileTraceLogData
    {
        // Token: 0x040002B2 RID: 690
        public PS3TMAPI.FileTraceLogType1 LogType1;

        // Token: 0x040002B3 RID: 691
        public PS3TMAPI.FileTraceLogType2 LogType2;

        // Token: 0x040002B4 RID: 692
        public PS3TMAPI.FileTraceLogType3 LogType3;

        // Token: 0x040002B5 RID: 693
        public PS3TMAPI.FileTraceLogType4 LogType4;

        // Token: 0x040002B6 RID: 694
        public PS3TMAPI.FileTraceLogType6 LogType6;

        // Token: 0x040002B7 RID: 695
        public PS3TMAPI.FileTraceLogType8 LogType8;

        // Token: 0x040002B8 RID: 696
        public PS3TMAPI.FileTraceLogType9 LogType9;

        // Token: 0x040002B9 RID: 697
        public PS3TMAPI.FileTraceLogType10 LogType10;

        // Token: 0x040002BA RID: 698
        public PS3TMAPI.FileTraceLogType11 LogType11;

        // Token: 0x040002BB RID: 699
        public PS3TMAPI.FileTraceLogType12 LogType12;

        // Token: 0x040002BC RID: 700
        public PS3TMAPI.FileTraceLogType13 LogType13;

        // Token: 0x040002BD RID: 701
        public PS3TMAPI.FileTraceLogType14 LogType14;
    }

    // Token: 0x0200007E RID: 126
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType1
    {
        // Token: 0x040002BE RID: 702
        public string Path;
    }

    // Token: 0x0200007F RID: 127
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType2
    {
        // Token: 0x040002BF RID: 703
        public string Path1;

        // Token: 0x040002C0 RID: 704
        public string Path2;
    }

    // Token: 0x02000080 RID: 128
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType3
    {
        // Token: 0x040002C1 RID: 705
        public ulong Arg;

        // Token: 0x040002C2 RID: 706
        public string Path;
    }

    // Token: 0x02000081 RID: 129
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType4
    {
        // Token: 0x040002C3 RID: 707
        public uint Mode;

        // Token: 0x040002C4 RID: 708
        public string Path;
    }

    // Token: 0x02000082 RID: 130
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType6
    {
        // Token: 0x040002C5 RID: 709
        public ulong Arg1;

        // Token: 0x040002C6 RID: 710
        public ulong Arg2;

        // Token: 0x040002C7 RID: 711
        public string Path;
    }

    // Token: 0x02000083 RID: 131
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceProcessInfo
    {
        // Token: 0x040002C8 RID: 712
        public ulong VFSID;

        // Token: 0x040002C9 RID: 713
        public ulong FD;
    }

    // Token: 0x02000084 RID: 132
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType8
    {
        // Token: 0x040002CA RID: 714
        public PS3TMAPI.FileTraceProcessInfo ProcessInfo;

        // Token: 0x040002CB RID: 715
        public uint Arg1;

        // Token: 0x040002CC RID: 716
        public uint Arg2;

        // Token: 0x040002CD RID: 717
        public uint Arg3;

        // Token: 0x040002CE RID: 718
        public uint Arg4;

        // Token: 0x040002CF RID: 719
        public byte[] VArg;

        // Token: 0x040002D0 RID: 720
        public string Path;
    }

    // Token: 0x02000085 RID: 133
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType9
    {
        // Token: 0x040002D1 RID: 721
        public PS3TMAPI.FileTraceProcessInfo ProcessInfo;
    }

    // Token: 0x02000086 RID: 134
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType10
    {
        // Token: 0x040002D2 RID: 722
        public PS3TMAPI.FileTraceProcessInfo ProcessInfo;

        // Token: 0x040002D3 RID: 723
        public uint Size;

        // Token: 0x040002D4 RID: 724
        public ulong Address;

        // Token: 0x040002D5 RID: 725
        public uint TxSize;
    }

    // Token: 0x02000087 RID: 135
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType11
    {
        // Token: 0x040002D6 RID: 726
        public PS3TMAPI.FileTraceProcessInfo ProcessInfo;

        // Token: 0x040002D7 RID: 727
        public uint Size;

        // Token: 0x040002D8 RID: 728
        public ulong Address;

        // Token: 0x040002D9 RID: 729
        public ulong Offset;

        // Token: 0x040002DA RID: 730
        public uint TxSize;
    }

    // Token: 0x02000088 RID: 136
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType12
    {
        // Token: 0x040002DB RID: 731
        public PS3TMAPI.FileTraceProcessInfo ProcessInfo;

        // Token: 0x040002DC RID: 732
        public ulong TargetSize;
    }

    // Token: 0x02000089 RID: 137
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType13
    {
        // Token: 0x040002DD RID: 733
        public PS3TMAPI.FileTraceProcessInfo ProcessInfo;

        // Token: 0x040002DE RID: 734
        public uint Size;

        // Token: 0x040002DF RID: 735
        public ulong Offset;

        // Token: 0x040002E0 RID: 736
        public ulong CurPos;
    }

    // Token: 0x0200008A RID: 138
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType14
    {
        // Token: 0x040002E1 RID: 737
        public PS3TMAPI.FileTraceProcessInfo ProcessInfo;

        // Token: 0x040002E2 RID: 738
        public uint MaxSize;

        // Token: 0x040002E3 RID: 739
        public uint Page;

        // Token: 0x040002E4 RID: 740
        public uint ContainerID;
    }

    // Token: 0x0200008B RID: 139
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceEvent
    {
        // Token: 0x040002E5 RID: 741
        public ulong SerialID;

        // Token: 0x040002E6 RID: 742
        public PS3TMAPI.FileTraceType TraceType;

        // Token: 0x040002E7 RID: 743
        public PS3TMAPI.FileTraceNotificationStatus Status;

        // Token: 0x040002E8 RID: 744
        public uint ProcessID;

        // Token: 0x040002E9 RID: 745
        public uint ThreadID;

        // Token: 0x040002EA RID: 746
        public ulong TimeBaseStartOfTrace;

        // Token: 0x040002EB RID: 747
        public ulong TimeBase;

        // Token: 0x040002EC RID: 748
        public byte[] BackTraceData;

        // Token: 0x040002ED RID: 749
        public PS3TMAPI.FileTraceLogData LogData;
    }

    // Token: 0x0200008C RID: 140
    // (Invoke) Token: 0x060002D2 RID: 722
    public delegate void FileTraceCallback(int target, PS3TMAPI.SNRESULT res, PS3TMAPI.FileTraceEvent fileTraceEvent, object userData);

    // Token: 0x0200008D RID: 141
    private class FileTraceCallbackAndUserData
    {
        // Token: 0x040002EE RID: 750
        public PS3TMAPI.FileTraceCallback m_callback;

        // Token: 0x040002EF RID: 751
        public object m_userData;
    }

    // Token: 0x0200008E RID: 142
    public enum FileTransferStatus : uint
    {
        // Token: 0x040002F1 RID: 753
        Pending = 1U,
        // Token: 0x040002F2 RID: 754
        Failed,
        // Token: 0x040002F3 RID: 755
        Succeeded = 4U,
        // Token: 0x040002F4 RID: 756
        Skipped = 8U,
        // Token: 0x040002F5 RID: 757
        InProgress = 16U,
        // Token: 0x040002F6 RID: 758
        Cancelled = 32U
    }

    // Token: 0x0200008F RID: 143
    public struct FileTransferInfo
    {
        // Token: 0x040002F7 RID: 759
        public uint TransferID;

        // Token: 0x040002F8 RID: 760
        public PS3TMAPI.FileTransferStatus Status;

        // Token: 0x040002F9 RID: 761
        public string SourcePath;

        // Token: 0x040002FA RID: 762
        public string DestinationPath;

        // Token: 0x040002FB RID: 763
        public ulong Size;

        // Token: 0x040002FC RID: 764
        public ulong BytesRead;
    }

    // Token: 0x02000090 RID: 144
    private struct FileTransferInfoPriv
    {
        // Token: 0x040002FD RID: 765
        public uint TransferId;

        // Token: 0x040002FE RID: 766
        public uint Status;

        // Token: 0x040002FF RID: 767
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
        public byte[] SourcePath;

        // Token: 0x04000300 RID: 768
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1056)]
        public byte[] DestinationPath;

        // Token: 0x04000301 RID: 769
        public ulong Size;

        // Token: 0x04000302 RID: 770
        public ulong BytesRead;
    }

    // Token: 0x02000091 RID: 145
    public struct Time
    {
        // Token: 0x04000303 RID: 771
        private int Sec;

        // Token: 0x04000304 RID: 772
        private int Min;

        // Token: 0x04000305 RID: 773
        private int Hour;

        // Token: 0x04000306 RID: 774
        private int MDay;

        // Token: 0x04000307 RID: 775
        private int Mon;

        // Token: 0x04000308 RID: 776
        private int Year;

        // Token: 0x04000309 RID: 777
        private int WDay;

        // Token: 0x0400030A RID: 778
        private int YDay;

        // Token: 0x0400030B RID: 779
        private int IsDST;
    }

    // Token: 0x02000092 RID: 146
    public enum DirEntryType : uint
    {
        // Token: 0x0400030D RID: 781
        Unknown,
        // Token: 0x0400030E RID: 782
        Directory,
        // Token: 0x0400030F RID: 783
        Regular,
        // Token: 0x04000310 RID: 784
        Symlink
    }

    // Token: 0x02000093 RID: 147
    public struct DirEntry
    {
        // Token: 0x04000311 RID: 785
        public PS3TMAPI.DirEntryType Type;

        // Token: 0x04000312 RID: 786
        public uint Mode;

        // Token: 0x04000313 RID: 787
        public PS3TMAPI.Time AccessTime;

        // Token: 0x04000314 RID: 788
        public PS3TMAPI.Time ModifiedTime;

        // Token: 0x04000315 RID: 789
        public PS3TMAPI.Time CreateTime;

        // Token: 0x04000316 RID: 790
        public ulong Size;

        // Token: 0x04000317 RID: 791
        public string Name;
    }

    // Token: 0x02000094 RID: 148
    private struct DirEntryPriv
    {
        // Token: 0x04000318 RID: 792
        public uint Type;

        // Token: 0x04000319 RID: 793
        public uint Mode;

        // Token: 0x0400031A RID: 794
        public PS3TMAPI.Time AccessTime;

        // Token: 0x0400031B RID: 795
        public PS3TMAPI.Time ModifiedTime;

        // Token: 0x0400031C RID: 796
        public PS3TMAPI.Time CreateTime;

        // Token: 0x0400031D RID: 797
        public ulong Size;

        // Token: 0x0400031E RID: 798
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] Name;
    }

    // Token: 0x02000095 RID: 149
    public struct DirEntryEx
    {
        // Token: 0x0400031F RID: 799
        public PS3TMAPI.DirEntryType Type;

        // Token: 0x04000320 RID: 800
        public uint Mode;

        // Token: 0x04000321 RID: 801
        public ulong AccessTimeUTC;

        // Token: 0x04000322 RID: 802
        public ulong ModifiedTimeUTC;

        // Token: 0x04000323 RID: 803
        public ulong CreateTimeUTC;

        // Token: 0x04000324 RID: 804
        public ulong Size;

        // Token: 0x04000325 RID: 805
        public string Name;
    }

    // Token: 0x02000096 RID: 150
    private struct DirEntryExPriv
    {
        // Token: 0x04000326 RID: 806
        public uint Type;

        // Token: 0x04000327 RID: 807
        public uint Mode;

        // Token: 0x04000328 RID: 808
        public ulong AccessTimeUTC;

        // Token: 0x04000329 RID: 809
        public ulong ModifiedTimeUTC;

        // Token: 0x0400032A RID: 810
        public ulong CreateTimeUTC;

        // Token: 0x0400032B RID: 811
        public ulong Size;

        // Token: 0x0400032C RID: 812
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] Name;
    }

    // Token: 0x02000097 RID: 151
    public struct TargetTimezone
    {
        // Token: 0x0400032D RID: 813
        public int Timezone;

        // Token: 0x0400032E RID: 814
        public int DST;
    }

    // Token: 0x02000098 RID: 152
    public enum ChModFilePermission : uint
    {
        // Token: 0x04000330 RID: 816
        ReadWrite = 384U,
        // Token: 0x04000331 RID: 817
        ReadOnly = 256U
    }

    // Token: 0x02000099 RID: 153
    public enum LogCategory : uint
    {
        // Token: 0x04000333 RID: 819
        Off,
        // Token: 0x04000334 RID: 820
        All = 4294967295U
    }

    // Token: 0x0200009A RID: 154
    public struct BDInfo
    {
        // Token: 0x04000335 RID: 821
        public uint bdemu_data_size;

        // Token: 0x04000336 RID: 822
        public byte bdemu_total_entry;

        // Token: 0x04000337 RID: 823
        public byte bdemu_selected_index;

        // Token: 0x04000338 RID: 824
        public byte image_index;

        // Token: 0x04000339 RID: 825
        public byte image_type;

        // Token: 0x0400033A RID: 826
        public string image_file_name;

        // Token: 0x0400033B RID: 827
        public ulong image_file_size;

        // Token: 0x0400033C RID: 828
        public string image_product_code;

        // Token: 0x0400033D RID: 829
        public string image_producer;

        // Token: 0x0400033E RID: 830
        public string image_author;

        // Token: 0x0400033F RID: 831
        public string image_date;

        // Token: 0x04000340 RID: 832
        public uint image_sector_layer0;

        // Token: 0x04000341 RID: 833
        public uint image_sector_layer1;

        // Token: 0x04000342 RID: 834
        public string image_memorandum;
    }

    // Token: 0x0200009B RID: 155
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct BDInfoPriv
    {
        // Token: 0x04000343 RID: 835
        public uint bdemu_data_size;

        // Token: 0x04000344 RID: 836
        public byte bdemu_total_entry;

        // Token: 0x04000345 RID: 837
        public byte bdemu_selected_index;

        // Token: 0x04000346 RID: 838
        public byte image_index;

        // Token: 0x04000347 RID: 839
        public byte image_type;

        // Token: 0x04000348 RID: 840
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] image_file_name;

        // Token: 0x04000349 RID: 841
        public ulong image_file_size;

        // Token: 0x0400034A RID: 842
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] image_product_code;

        // Token: 0x0400034B RID: 843
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] image_producer;

        // Token: 0x0400034C RID: 844
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] image_author;

        // Token: 0x0400034D RID: 845
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] image_date;

        // Token: 0x0400034E RID: 846
        public uint image_sector_layer0;

        // Token: 0x0400034F RID: 847
        public uint image_sector_layer1;

        // Token: 0x04000350 RID: 848
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] image_memorandum;
    }

    // Token: 0x0200009C RID: 156
    public enum TargetEventType : uint
    {
        // Token: 0x04000352 RID: 850
        UnitStatusChange,
        // Token: 0x04000353 RID: 851
        ResetStarted,
        // Token: 0x04000354 RID: 852
        ResetEnd,
        // Token: 0x04000355 RID: 853
        Details = 4U,
        // Token: 0x04000356 RID: 854
        ModuleLoad,
        // Token: 0x04000357 RID: 855
        ModuleRunning,
        // Token: 0x04000358 RID: 856
        ModuleDoneRemove,
        // Token: 0x04000359 RID: 857
        ModuleDoneResident,
        // Token: 0x0400035A RID: 858
        ModuleStopped,
        // Token: 0x0400035B RID: 859
        ModuleStoppedRemove,
        // Token: 0x0400035C RID: 860
        PowerStatusChange,
        // Token: 0x0400035D RID: 861
        TTYStreamAdded,
        // Token: 0x0400035E RID: 862
        TTYStreamDeleted,
        // Token: 0x0400035F RID: 863
        BDIsotransferStarted = 16U,
        // Token: 0x04000360 RID: 864
        BDIsotransferFinished,
        // Token: 0x04000361 RID: 865
        BDFormatStarted,
        // Token: 0x04000362 RID: 866
        BDFormatFinished,
        // Token: 0x04000363 RID: 867
        BDMountStarted,
        // Token: 0x04000364 RID: 868
        BDMountFinished,
        // Token: 0x04000365 RID: 869
        BDUnmountStarted,
        // Token: 0x04000366 RID: 870
        BDUnmountFinished,
        // Token: 0x04000367 RID: 871
        TargetSpecific = 2147483648U
    }

    // Token: 0x0200009D RID: 157
    public struct TGTEventUnitStatusChangeData
    {
        // Token: 0x04000368 RID: 872
        public PS3TMAPI.UnitType Unit;

        // Token: 0x04000369 RID: 873
        public PS3TMAPI.UnitStatus Status;
    }

    // Token: 0x0200009E RID: 158
    private struct TGTEventUnitStatusChangeDataPriv
    {
        // Token: 0x0400036A RID: 874
        public int Unit;

        // Token: 0x0400036B RID: 875
        public uint Status;
    }

    // Token: 0x0200009F RID: 159
    public struct TGTEventDetailsData
    {
        // Token: 0x0400036C RID: 876
        public uint Flags;
    }

    // Token: 0x020000A0 RID: 160
    public struct TGTEventModuleEventData
    {
        // Token: 0x0400036D RID: 877
        public uint Unit;

        // Token: 0x0400036E RID: 878
        public uint ModuleID;
    }

    // Token: 0x020000A1 RID: 161
    public struct TGTEventBDData
    {
        // Token: 0x0400036F RID: 879
        public uint Result;

        // Token: 0x04000370 RID: 880
        public string Source;

        // Token: 0x04000371 RID: 881
        public string Destination;
    }

    // Token: 0x020000A2 RID: 162
    private struct TGTEventBDDataPriv
    {
        // Token: 0x04000372 RID: 882
        public uint Result;

        // Token: 0x04000373 RID: 883
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] Source;

        // Token: 0x04000374 RID: 884
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] Destination;
    }

    // Token: 0x020000A3 RID: 163
    public struct TargetEventData
    {
        // Token: 0x04000375 RID: 885
        public PS3TMAPI.TGTEventUnitStatusChangeData UnitStatusChangeData;

        // Token: 0x04000376 RID: 886
        public PS3TMAPI.TGTEventDetailsData DetailsData;

        // Token: 0x04000377 RID: 887
        public PS3TMAPI.TGTEventModuleEventData ModuleEventData;

        // Token: 0x04000378 RID: 888
        public PS3TMAPI.TGTEventBDData BdData;
    }

    // Token: 0x020000A4 RID: 164
    public struct TargetEvent
    {
        // Token: 0x04000379 RID: 889
        public uint TargetID;

        // Token: 0x0400037A RID: 890
        public PS3TMAPI.TargetEventType Type;

        // Token: 0x0400037B RID: 891
        public PS3TMAPI.TargetEventData EventData;

        // Token: 0x0400037C RID: 892
        public PS3TMAPI.TargetSpecificEvent TargetSpecific;
    }

    // Token: 0x020000A5 RID: 165
    // (Invoke) Token: 0x060002D7 RID: 727
    public delegate void TargetEventCallback(int target, PS3TMAPI.SNRESULT res, PS3TMAPI.TargetEvent[] targetEventList, object userData);

    // Token: 0x020000A6 RID: 166
    private class TargetCallbackAndUserData
    {
        // Token: 0x0400037D RID: 893
        public PS3TMAPI.TargetEventCallback m_callback;

        // Token: 0x0400037E RID: 894
        public object m_userData;
    }

    // Token: 0x020000A7 RID: 167
    private struct TargetEventHdrPriv
    {
        // Token: 0x0400037F RID: 895
        public uint Size;

        // Token: 0x04000380 RID: 896
        public uint TargetID;

        // Token: 0x04000381 RID: 897
        public uint EventType;
    }

    // Token: 0x020000A8 RID: 168
    public enum TargetSpecificEventType : uint
    {
        // Token: 0x04000383 RID: 899
        ProcessCreate,
        // Token: 0x04000384 RID: 900
        ProcessExit,
        // Token: 0x04000385 RID: 901
        ProcessKill,
        // Token: 0x04000386 RID: 902
        ProcessExitSpawn,
        // Token: 0x04000387 RID: 903
        PPUExcTrap = 16U,
        // Token: 0x04000388 RID: 904
        PPUExcPrevInt,
        // Token: 0x04000389 RID: 905
        PPUExcAlignment,
        // Token: 0x0400038A RID: 906
        PPUExcIllInst,
        // Token: 0x0400038B RID: 907
        PPUExcTextHtabMiss,
        // Token: 0x0400038C RID: 908
        PPUExcTextSlbMiss,
        // Token: 0x0400038D RID: 909
        PPUExcDataHtabMiss,
        // Token: 0x0400038E RID: 910
        PPUExcFloat,
        // Token: 0x0400038F RID: 911
        PPUExcDataSlbMiss,
        // Token: 0x04000390 RID: 912
        PPUExcDabrMatch,
        // Token: 0x04000391 RID: 913
        PPUExcStop,
        // Token: 0x04000392 RID: 914
        PPUExcStopInit,
        // Token: 0x04000393 RID: 915
        PPUExcDataMAT,
        // Token: 0x04000394 RID: 916
        PPUThreadCreate = 32U,
        // Token: 0x04000395 RID: 917
        PPUThreadExit,
        // Token: 0x04000396 RID: 918
        SPUThreadStart = 48U,
        // Token: 0x04000397 RID: 919
        SPUThreadStop,
        // Token: 0x04000398 RID: 920
        SPUThreadStopInit,
        // Token: 0x04000399 RID: 921
        SPUThreadGroupDestroy,
        // Token: 0x0400039A RID: 922
        SPUThreadStopEx,
        // Token: 0x0400039B RID: 923
        RawNotify = 4026531855U,
        // Token: 0x0400039C RID: 924
        PRXLoad = 64U,
        // Token: 0x0400039D RID: 925
        PRXUnload,
        // Token: 0x0400039E RID: 926
        DAInitialised = 96U,
        // Token: 0x0400039F RID: 927
        Footswitch = 112U,
        // Token: 0x040003A0 RID: 928
        InstallPackageProgress = 128U,
        // Token: 0x040003A1 RID: 929
        InstallPackagePath,
        // Token: 0x040003A2 RID: 930
        CoreDumpStart = 257U,
        // Token: 0x040003A3 RID: 931
        CoreDumpComplete = 256U
    }

    // Token: 0x020000A9 RID: 169
    public struct PPUProcessCreateData
    {
        // Token: 0x040003A4 RID: 932
        public uint ParentProcessID;

        // Token: 0x040003A5 RID: 933
        public string Filename;
    }

    // Token: 0x020000AA RID: 170
    public struct PPUProcessExitData
    {
        // Token: 0x040003A6 RID: 934
        public ulong ExitCode;
    }

    // Token: 0x020000AB RID: 171
    public struct PPUExceptionData
    {
        // Token: 0x040003A7 RID: 935
        public ulong ThreadID;

        // Token: 0x040003A8 RID: 936
        public uint HWThreadNumber;

        // Token: 0x040003A9 RID: 937
        public ulong PC;

        // Token: 0x040003AA RID: 938
        public ulong SP;
    }

    // Token: 0x020000AC RID: 172
    public struct PPUAlignmentExceptionData
    {
        // Token: 0x040003AB RID: 939
        public ulong ThreadID;

        // Token: 0x040003AC RID: 940
        public uint HWThreadNumber;

        // Token: 0x040003AD RID: 941
        public ulong DSISR;

        // Token: 0x040003AE RID: 942
        public ulong DAR;

        // Token: 0x040003AF RID: 943
        public ulong PC;

        // Token: 0x040003B0 RID: 944
        public ulong SP;
    }

    // Token: 0x020000AD RID: 173
    public struct PPUDataMatExceptionData
    {
        // Token: 0x040003B1 RID: 945
        public ulong ThreadID;

        // Token: 0x040003B2 RID: 946
        public uint HWThreadNumber;

        // Token: 0x040003B3 RID: 947
        public ulong DSISR;

        // Token: 0x040003B4 RID: 948
        public ulong DAR;

        // Token: 0x040003B5 RID: 949
        public ulong PC;

        // Token: 0x040003B6 RID: 950
        public ulong SP;
    }

    // Token: 0x020000AE RID: 174
    public struct PPUThreadCreateData
    {
        // Token: 0x040003B7 RID: 951
        public ulong ThreadID;
    }

    // Token: 0x020000AF RID: 175
    public struct PPUThreadExitData
    {
        // Token: 0x040003B8 RID: 952
        public ulong ThreadID;
    }

    // Token: 0x020000B0 RID: 176
    public struct SPUThreadStartData
    {
        // Token: 0x040003B9 RID: 953
        public uint ThreadGroupID;

        // Token: 0x040003BA RID: 954
        public uint ThreadID;

        // Token: 0x040003BB RID: 955
        public string ElfFilename;
    }

    // Token: 0x020000B1 RID: 177
    public enum SPUThreadStopReason : uint
    {
        // Token: 0x040003BD RID: 957
        NoException,
        // Token: 0x040003BE RID: 958
        DMAAlignment,
        // Token: 0x040003BF RID: 959
        DMACommand,
        // Token: 0x040003C0 RID: 960
        Error = 4U,
        // Token: 0x040003C1 RID: 961
        MFCFIR = 8U,
        // Token: 0x040003C2 RID: 962
        MFCSegment = 16U,
        // Token: 0x040003C3 RID: 963
        MFCStorage = 32U,
        // Token: 0x040003C4 RID: 964
        NoValue = 64U,
        // Token: 0x040003C5 RID: 965
        StopCall = 256U,
        // Token: 0x040003C6 RID: 966
        StopDCall = 512U,
        // Token: 0x040003C7 RID: 967
        Halt = 1024U
    }

    // Token: 0x020000B2 RID: 178
    public struct SPUThreadStopData
    {
        // Token: 0x040003C8 RID: 968
        public uint ThreadGroupID;

        // Token: 0x040003C9 RID: 969
        public uint ThreadID;

        // Token: 0x040003CA RID: 970
        public uint PC;

        // Token: 0x040003CB RID: 971
        public PS3TMAPI.SPUThreadStopReason Reason;

        // Token: 0x040003CC RID: 972
        public uint SP;
    }

    // Token: 0x020000B3 RID: 179
    public struct SPUThreadStopExData
    {
        // Token: 0x040003CD RID: 973
        public uint ThreadGroupID;

        // Token: 0x040003CE RID: 974
        public uint ThreadID;

        // Token: 0x040003CF RID: 975
        public uint PC;

        // Token: 0x040003D0 RID: 976
        public PS3TMAPI.SPUThreadStopReason Reason;

        // Token: 0x040003D1 RID: 977
        public uint SP;

        // Token: 0x040003D2 RID: 978
        public ulong MFCDSISR;

        // Token: 0x040003D3 RID: 979
        public ulong MFCDSIPR;

        // Token: 0x040003D4 RID: 980
        public ulong MFCDAR;
    }

    // Token: 0x020000B4 RID: 180
    public struct SPUThreadGroupDestroyData
    {
        // Token: 0x040003D5 RID: 981
        public uint ThreadGroupID;
    }

    // Token: 0x020000B5 RID: 181
    public struct NotifyPRXLoadData
    {
        // Token: 0x040003D6 RID: 982
        public ulong PPUThreadID;

        // Token: 0x040003D7 RID: 983
        public uint PRXID;

        // Token: 0x040003D8 RID: 984
        public ulong Timestamp;
    }

    // Token: 0x020000B6 RID: 182
    public struct NotifyPRXUnloadData
    {
        // Token: 0x040003D9 RID: 985
        public ulong PPUThreadID;

        // Token: 0x040003DA RID: 986
        public uint PRXID;

        // Token: 0x040003DB RID: 987
        public ulong Timestamp;
    }

    // Token: 0x020000B7 RID: 183
    public struct FootswitchData
    {
        // Token: 0x040003DC RID: 988
        public ulong EventSource;

        // Token: 0x040003DD RID: 989
        public ulong EventData1;

        // Token: 0x040003DE RID: 990
        public ulong EventData2;

        // Token: 0x040003DF RID: 991
        public ulong EventData3;

        // Token: 0x040003E0 RID: 992
        public ulong Reserved;
    }

    // Token: 0x020000B8 RID: 184
    public struct InstallPackageProgress
    {
        // Token: 0x040003E1 RID: 993
        public uint Percent;
    }

    // Token: 0x020000B9 RID: 185
    public struct InstallPackagePath
    {
        // Token: 0x040003E2 RID: 994
        public string Path;
    }

    // Token: 0x020000BA RID: 186
    public struct CoreDumpComplete
    {
        // Token: 0x040003E3 RID: 995
        public string Filename;
    }

    // Token: 0x020000BB RID: 187
    public struct CoreDumpStart
    {
        // Token: 0x040003E4 RID: 996
        public string Filename;
    }

    // Token: 0x020000BC RID: 188
    public struct TargetSpecificData
    {
        // Token: 0x040003E5 RID: 997
        public PS3TMAPI.TargetSpecificEventType Type;

        // Token: 0x040003E6 RID: 998
        public PS3TMAPI.PPUProcessCreateData PPUProcessCreate;

        // Token: 0x040003E7 RID: 999
        public PS3TMAPI.PPUProcessExitData PPUProcessExit;

        // Token: 0x040003E8 RID: 1000
        public PS3TMAPI.PPUExceptionData PPUException;

        // Token: 0x040003E9 RID: 1001
        public PS3TMAPI.PPUAlignmentExceptionData PPUAlignmentException;

        // Token: 0x040003EA RID: 1002
        public PS3TMAPI.PPUDataMatExceptionData PPUDataMatException;

        // Token: 0x040003EB RID: 1003
        public PS3TMAPI.PPUThreadCreateData PPUThreadCreate;

        // Token: 0x040003EC RID: 1004
        public PS3TMAPI.PPUThreadExitData PPUThreadExit;

        // Token: 0x040003ED RID: 1005
        public PS3TMAPI.SPUThreadStartData SPUThreadStart;

        // Token: 0x040003EE RID: 1006
        public PS3TMAPI.SPUThreadStopData SPUThreadStop;

        // Token: 0x040003EF RID: 1007
        public PS3TMAPI.SPUThreadStopExData SPUThreadStopEx;

        // Token: 0x040003F0 RID: 1008
        public PS3TMAPI.SPUThreadGroupDestroyData SPUThreadGroupDestroyData;

        // Token: 0x040003F1 RID: 1009
        public PS3TMAPI.NotifyPRXLoadData PRXLoad;

        // Token: 0x040003F2 RID: 1010
        public PS3TMAPI.NotifyPRXUnloadData PRXUnload;

        // Token: 0x040003F3 RID: 1011
        public PS3TMAPI.FootswitchData Footswitch;

        // Token: 0x040003F4 RID: 1012
        public PS3TMAPI.InstallPackageProgress InstallPackageProgress;

        // Token: 0x040003F5 RID: 1013
        public PS3TMAPI.InstallPackagePath InstallPackagePath;

        // Token: 0x040003F6 RID: 1014
        public PS3TMAPI.CoreDumpStart CoreDumpStart;

        // Token: 0x040003F7 RID: 1015
        public PS3TMAPI.CoreDumpComplete CoreDumpComplete;
    }

    // Token: 0x020000BD RID: 189
    public struct TargetSpecificEvent
    {
        // Token: 0x040003F8 RID: 1016
        public uint CommandID;

        // Token: 0x040003F9 RID: 1017
        public uint RequestID;

        // Token: 0x040003FA RID: 1018
        public uint ProcessID;

        // Token: 0x040003FB RID: 1019
        public uint Result;

        // Token: 0x040003FC RID: 1020
        public PS3TMAPI.TargetSpecificData Data;
    }

    // Token: 0x020000BE RID: 190
    private enum EventType
    {
        // Token: 0x040003FE RID: 1022
        TTY = 100,
        // Token: 0x040003FF RID: 1023
        Target,
        // Token: 0x04000400 RID: 1024
        System,
        // Token: 0x04000401 RID: 1025
        FTP,
        // Token: 0x04000402 RID: 1026
        PadCapture,
        // Token: 0x04000403 RID: 1027
        FileTrace,
        // Token: 0x04000404 RID: 1028
        PadPlayback,
        // Token: 0x04000405 RID: 1029
        Server
    }

    // Token: 0x020000BF RID: 191
    // (Invoke) Token: 0x060002DC RID: 732
    private delegate void HandleEventCallbackPriv(int target, PS3TMAPI.EventType type, uint param, PS3TMAPI.SNRESULT result, uint length, IntPtr data, IntPtr userData);

    // Token: 0x020000C0 RID: 192
    // (Invoke) Token: 0x060002E0 RID: 736
    public delegate void SearchTargetsCallback(string name, string type, PS3TMAPI.TCPIPConnectProperties ConnectInfo, object userData);

    // Token: 0x020000C1 RID: 193
    // (Invoke) Token: 0x060002E4 RID: 740
    private delegate void SearchTargetsCallbackPriv(IntPtr name, IntPtr type, IntPtr connectInfo, IntPtr userData);

    // Token: 0x020000C2 RID: 194
    private class SearchForTargetsCallbackHandler
    {
        // Token: 0x060002E7 RID: 743 RVA: 0x000088C8 File Offset: 0x00006AC8
        public static void SearchForTargetsCallback(IntPtr namePtr, IntPtr typePtr, IntPtr connectInfoPtr, IntPtr userDataPtr)
        {
            PS3TMAPI.SearchForTargetsCallbackHandler searchForTargetsCallbackHandler = (PS3TMAPI.SearchForTargetsCallbackHandler)GCHandle.FromIntPtr(userDataPtr).Target;
            PS3TMAPI.TCPIPConnectProperties tcpipconnectProperties = null;
            if (connectInfoPtr != IntPtr.Zero)
            {
                tcpipconnectProperties = new PS3TMAPI.TCPIPConnectProperties();
                Marshal.PtrToStructure(connectInfoPtr, tcpipconnectProperties);
            }
            string text = PS3TMAPI.Utf8ToString(namePtr, uint.MaxValue);
            if (text == "")
            {
                text = null;
            }
            string type = PS3TMAPI.Utf8ToString(typePtr, uint.MaxValue);
            searchForTargetsCallbackHandler.m_SearchForTargetCallback(text, type, tcpipconnectProperties, searchForTargetsCallbackHandler.m_UserData);
        }

        // Token: 0x060002E8 RID: 744 RVA: 0x0000893A File Offset: 0x00006B3A
        public SearchForTargetsCallbackHandler(PS3TMAPI.SearchTargetsCallback callback, object userData)
        {
            this.m_SearchForTargetCallback = callback;
            this.m_UserData = userData;
        }

        // Token: 0x04000406 RID: 1030
        private PS3TMAPI.SearchTargetsCallback m_SearchForTargetCallback;

        // Token: 0x04000407 RID: 1031
        private object m_UserData;
    }

    // Token: 0x020000C3 RID: 195
    private class ScopedGlobalHeapPtr
    {
        // Token: 0x060002E9 RID: 745 RVA: 0x00008950 File Offset: 0x00006B50
        public ScopedGlobalHeapPtr(IntPtr intPtr)
        {
            this.m_intPtr = intPtr;
        }

        // Token: 0x060002EA RID: 746 RVA: 0x0000896C File Offset: 0x00006B6C
        ~ScopedGlobalHeapPtr()
        {
            if (this.m_intPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.m_intPtr);
            }
        }

        // Token: 0x060002EB RID: 747 RVA: 0x000089B0 File Offset: 0x00006BB0
        public IntPtr Get()
        {
            return this.m_intPtr;
        }

        // Token: 0x04000408 RID: 1032
        private IntPtr m_intPtr = IntPtr.Zero;
    }
}
