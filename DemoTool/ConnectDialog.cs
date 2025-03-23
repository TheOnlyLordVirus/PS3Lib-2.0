using System;
using System.Linq;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using PS3Lib2;
using PS3Lib2.Extentions;

namespace DemoTool;

public sealed partial class ConnectDialog : Form
{
    private const string _ipv4RegEx = "^((25[0-5]|(2[0-4]|1\\d|[1-9]|)\\d)\\.?\\b){4}$";
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

        // Placeholder text in form elements.
        Utilities.SendMessage(consoleInfoListBox.Handle, Utilities.EM_SETCUEBANNER, 0, "Playstation Ip Address...");
        Utilities.SendMessage(ipAddressTextBox.Handle, Utilities.EM_SETCUEBANNER, 0, "Playstation Ip Address...");

        closeButton = new Button()
        {
            Visible = true,
            Enabled = true,
            Size = new System.Drawing.Size(0, 0)
        };

        // Define a close button for the esc key
        closeButton.Click += (object _, EventArgs _) => this.Close();
        this.CancelButton = closeButton;
        this.CancelButton.DialogResult = DialogResult.Cancel;

        var supportedMethods = _playstationApi.GetSupportedMethods();
        if (!supportedMethods.Contains("get_ConsolesInfo"))
            return;
            
        consoleInfoListBox.Items.Clear();
        consoleInfoListBox.DataSource =
            _playstationApi
                .ConsolesInfo
                    .Select(x => new ConsoleListBoxItem()
                    {
                        ConsoleInfo = x
                    }).ToList();

        consoleInfoListBox.SelectedIndex = -1;
        consoleInfoListBox.Enabled = true;
    }

    private void ConnectButton_Click(object ___, EventArgs __)
    {
        if (!Regex.IsMatch(ipAddressTextBox.Text, _ipv4RegEx))
            throw new Exception("Please enter a valid Ip!");

        if (!_playstationApi.Connect(ipAddressTextBox.Text))
            throw new Exception("Failed to connect!");

        this.Close();
    }

    private void ConsoleInfoListBox_SelectedIndexChanged(object ___, EventArgs __)
    {
        if (consoleInfoListBox.SelectedIndex == -1)
        {
            ipAddressTextBox.Text = string.Empty;
            return;
        }
            
        if (consoleInfoListBox.SelectedItems.Count < 0)
            return;

        if (consoleInfoListBox.SelectedItems[0] is not ConsoleListBoxItem consoleListBoxItem)
            return;

        ipAddressTextBox.Text = consoleListBoxItem.ConsoleInfo.ConsoleIp;
    }

    private void IpAddressTextBox_TextChanged(object sender, EventArgs e)
    {
        ipAddressTextBox.ForeColor = Regex.IsMatch(ipAddressTextBox.Text, _ipv4RegEx) ? Color.Green : Color.Red;
    }
}