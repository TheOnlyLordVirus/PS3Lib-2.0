using System;
using System.Windows.Forms;

using PS3Lib2;
using PS3Lib2.Capi;
using PS3Lib2.Tmapi;
using PS3Lib2.PS3Mapi;
using PS3Lib2.Interfaces;

namespace DemoTool;

public partial class PlaystationApiDemoForm : Form
{
    private IPlaystationApi? CurrentApi { get; set; }

    public PlaystationApiDemoForm()
    {
        InitializeComponent();
    }

    private void Connect_Button_Click(object sender, EventArgs e)
        => Internal_Connect(CurrentApi);

    private void Attach_Button_Click(object sender, EventArgs e)
    {

    }

    private void Read_Button_Click(object sender, EventArgs e)
    {

    }

    private void Write_Button_Click(object sender, EventArgs e)
    {

    }

    private void Internal_Connect(IPlaystationApi api)
    {
        if (api is null)
            return;

        var ConnectForm = new ConnectDialog(api);

        ConnectForm.ShowDialog();
    }

    private void CCAPI_CheckedChanged(object sender, EventArgs e)
    {
        if (CurrentApi is not null)
            CurrentApi.Dispose();

        CurrentApi = new CCAPI_Wrapper();
    }

    private void tmapiRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (CurrentApi is not null)
            CurrentApi.Dispose();

        CurrentApi = new TMAPI_Wrapper();
    }

    private void ps3mapiRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (CurrentApi is not null)
            CurrentApi.Dispose();

        CurrentApi = new PS3MAPI_Wrapper();
    }
}
