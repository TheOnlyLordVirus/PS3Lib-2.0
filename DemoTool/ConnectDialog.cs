﻿using System;
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
        InitializeComponent();
        Utilities.SendMessage(ipAddressTextBox.Handle, Utilities.EM_SETCUEBANNER, 0, "Playstation Ip Address...");
        _playstationApi = api;
    }

    private void ConnectButton_Click(object ___, EventArgs __)
    {
        try
        {
            if (!IPAddress.TryParse(ipAddressTextBox.Text, out _))
                throw new Exception("Please enter a valid Ip!");

            if (!_playstationApi.Connect(ipAddressTextBox.Text))
                throw new Exception("Failed to connect!");

            IEnumerable<string> supportedMethods = 
                _playstationApi
                .GetSupportedMethods();

            if (supportedMethods.Contains("RingBuzzer"))
                _playstationApi.RingBuzzer();

            if (supportedMethods.Contains("VshNotify"))
                _playstationApi.VshNotify("Demo tool connected to playstation 3!");


            MessageBox.Show("Demo Tool has successfully connected to your playstation 3!");

            this.Close();
        }

        catch (Exception Ex)
        {
            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
