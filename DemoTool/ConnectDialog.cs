using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

using PS3Lib2;
using PS3Lib2.Extentions;

namespace DemoTool;

public sealed partial class ConnectDialog : Form
{
    private readonly IPlaystationApi _playstationApi;

    public ConnectDialog(IPlaystationApi api)
    {
        _playstationApi = api;

        Internal_PrivateInitComponent();
    }

    // InitializeComponent with the close button and the consoleinfo.
    private void Internal_PrivateInitComponent()
    {
        InitializeComponent();

        closeButton = new Button()
        {
            Visible = true,
            Enabled = true,
            Size = new System.Drawing.Size(0, 0)
        };

        closeButton.Click += (object _, EventArgs _) => this.Close();

        this.CancelButton = closeButton;
        this.CancelButton.DialogResult = DialogResult.Cancel;

        Utilities.SendMessage(consoleInfoListBox.Handle, Utilities.EM_SETCUEBANNER, 0, "Playstation Ip Address...");

        consoleInfoListBox.Items.Clear();

        consoleInfoListBox.DataSource =
            _playstationApi
                .ConsolesInfo
                    .Select(x => new ConsoleListBoxItem()
                    {
                        ConsoleInfo = x
                    }).ToList();
    }

    private void ConnectButton_Click(object ___, EventArgs __)
    {
        if (consoleInfoListBox.SelectedItems.Count < 0)
            return;

        if (consoleInfoListBox.SelectedItems[0] is not ConsoleListBoxItem consoleListBoxItem)
            return;

        if (!IPAddress.TryParse(consoleListBoxItem.ConsoleInfo.ConsoleIp, out _))
            throw new Exception("Please enter a valid Ip!");

        if (!_playstationApi.Connect(consoleListBoxItem.ConsoleInfo.ConsoleIp))
            throw new Exception("Failed to connect!");

        this.Close();
    }

    private void ConsoleInfoListBox_SelectedIndexChanged(object ___, EventArgs __)
    {
        if (consoleInfoListBox.SelectedIndex == -1)
            return;

        if (!connectButton.Enabled)
            connectButton.Enabled = true;
    }
}