﻿namespace DemoTool;

public sealed partial class ConnectDialog
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.connectButton = new System.Windows.Forms.Button();
            this.consoleInfoListBox = new System.Windows.Forms.ListBox();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.connectButton.Location = new System.Drawing.Point(18, 424);
            this.connectButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(364, 70);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // consoleInfoListBox
            // 
            this.consoleInfoListBox.Enabled = false;
            this.consoleInfoListBox.FormattingEnabled = true;
            this.consoleInfoListBox.ItemHeight = 20;
            this.consoleInfoListBox.Location = new System.Drawing.Point(18, 18);
            this.consoleInfoListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.consoleInfoListBox.Name = "consoleInfoListBox";
            this.consoleInfoListBox.Size = new System.Drawing.Size(362, 364);
            this.consoleInfoListBox.TabIndex = 2;
            this.consoleInfoListBox.SelectedIndexChanged += new System.EventHandler(this.ConsoleInfoListBox_SelectedIndexChanged);
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressTextBox.ForeColor = System.Drawing.Color.Red;
            this.ipAddressTextBox.Location = new System.Drawing.Point(18, 390);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(364, 26);
            this.ipAddressTextBox.TabIndex = 3;
            this.ipAddressTextBox.TextChanged += new System.EventHandler(this.IpAddressTextBox_TextChanged);
            // 
            // ConnectDialog
            // 
            this.AcceptButton = this.connectButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 508);
            this.Controls.Add(this.ipAddressTextBox);
            this.Controls.Add(this.consoleInfoListBox);
            this.Controls.Add(this.connectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "ConnectDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Api Connection";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.Button connectButton;
    private System.Windows.Forms.ListBox consoleInfoListBox;
    private System.Windows.Forms.TextBox ipAddressTextBox;
}