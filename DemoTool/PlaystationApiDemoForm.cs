using System;
using System.Windows.Forms;

using PS3Lib2;
using PS3Lib2.Capi;
using PS3Lib2.Tmapi;
using PS3Lib2.PS3Mapi;
using PS3Lib2.Interfaces;

#nullable enable

namespace DemoTool;

public sealed partial class PlaystationApiDemoForm : Form
{
    private IPlaystationApi? CurrentApi { get; set; }

    // Minecraft Godmode.
    private const uint _godModeAddress = 0x004B2021;
    private const byte _godModeEnable = 0x80;
    private const byte _godModeDisable = 0x20;

    private const string _godModeEnableString = "God Mode is Enabled!";
    private const string _godModeDisableString = "God Mode is Disabled!";

    public PlaystationApiDemoForm()
    {
        InitializeComponent();
    }

    private void Connect_Button_Click(object sender, EventArgs e)
        => Internal_Connect(CurrentApi);

    private void Internal_Connect(IPlaystationApi? api)
    {
        if (api is null)
            return;

        var ConnectForm = new ConnectDialog(api);

        ConnectForm.ShowDialog();
    }

    private void Attach_Button_Click(object sender, EventArgs e)
    {
        Internal_DisplayExeceptions(() =>
        {
            if (!CurrentApi!.AttachGameProcess())
                throw new Exception("Failed to AttachGameProcess!");
        });
    }

    private void Read_Button_Click(object sender, EventArgs e)
    {
        Internal_DisplayExeceptions(() =>
        {
            byte currentValue = CurrentApi!.ReadMemoryU8(_godModeAddress);

            string curState = currentValue is _godModeEnable ? 
                                _godModeEnableString : _godModeDisableString;

            MessageBox.Show(curState, "God Mode Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });
    }

    private void Write_Button_Click(object sender, EventArgs e)
    {
        Internal_DisplayExeceptions(() =>
        {
            // Godmode Toggle Example.
            byte currentValue = CurrentApi!.ReadMemoryU8(_godModeAddress);

            CurrentApi
                .WriteMemoryU8
                (
                    _godModeAddress, 
                    currentValue is _godModeEnable ? 
                    _godModeEnable : _godModeDisable
                );
        });
    }



    private void CCAPI_CheckedChanged(object sender, EventArgs e)
    {
        Internal_DisplayExeceptions(() =>
        {
            CurrentApi?.Dispose();

            CurrentApi = new CCAPI_Wrapper();
        });
    }

    private void tmapiRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        Internal_DisplayExeceptions(() =>
        {
            CurrentApi?.Dispose();

            CurrentApi = new TMAPI_Wrapper();
        });
    }

    private void ps3mapiRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        Internal_DisplayExeceptions(() =>
        {
            CurrentApi?.Dispose();

            CurrentApi = new PS3MAPI_Wrapper();
        });
    }


    private void Internal_DisplayExeceptions(Action throwableAction)
    {
        try
        {
            if (CurrentApi is null)
                throw new Exception("No API is selected. CurrentApi is null.");

            throwableAction.Invoke();
        }

        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
