using System;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using PS3Lib2.Attributes;
using PS3Lib2.Extentions;
using PS3Lib2.Interfaces;

namespace DemoTool;

public partial class ConnectDialog : Form
{
    private const int EM_SETCUEBANNER = 0x1501;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

    private readonly IPlaystationApi _playstationApi;

    public ConnectDialog(IPlaystationApi api)
    {
        InitializeComponent();
        SendMessage(ipAddressTextBox.Handle, EM_SETCUEBANNER, 0, "Playstation Ip Address...");
        _playstationApi = api;
    }

    private void connectButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IPAddress.TryParse(ipAddressTextBox.Text, out _))
                throw new Exception("Please enter a valid Ip!");

            if (!_playstationApi.Connect(ipAddressTextBox.Text))
                throw new Exception("Failed to connect!");

            PlaystationApiSupportAttribute<IPlaystationApi> supportedMethods = 
                    _playstationApi.GetApiSupportAttribute();

            if (supportedMethods.SupportedMethods.Contains("RingBuzzer"))
                _playstationApi.RingBuzzer();

            if (supportedMethods.SupportedMethods.Contains("VshNotify"))
                _playstationApi.VshNotify("Demo tool connected to playstation 3!");

            this.Close();
        }

        catch (Exception Ex)
        {
            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
