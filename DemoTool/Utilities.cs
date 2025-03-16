using System;
using System.Runtime.InteropServices;

namespace DemoTool;

internal static class Utilities
{
    public const int EM_SETCUEBANNER = 0x1501;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
}
