using PS3ManagerAPI;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PS3Lib
{
    partial class ConnectDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP: ";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(82, 42);
            this.txtIp.Margin = new System.Windows.Forms.Padding(11, 11, 11, 11);
            this.txtIp.MaxLength = 16;
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(209, 29);
            this.txtIp.TabIndex = 1;
            this.txtIp.Text = "127.0.0.1";
            this.txtIp.TextAlign = HorizontalAlignment.Center;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(216, 107);
            this.btnOK.Margin = new System.Windows.Forms.Padding(11, 11, 11, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(138, 39);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Connect";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(372, 107);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(11, 11, 11, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 39);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtPort
            // 
            this.txtPort.Enabled = false;
            this.txtPort.Location = new System.Drawing.Point(409, 42);
            this.txtPort.Margin = new System.Windows.Forms.Padding(11, 11, 11, 11);
            this.txtPort.MaxLength = 5;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(98, 29);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "7887";
            this.txtPort.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(11, 11, 11, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "PORT: ";
            // 
            // ConnectDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(535, 157);
            this.ControlBox = false;
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(11, 11, 11, 11);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectDialog";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Connection with PS3 Manager API";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Token: 0x0400000D RID: 13
        private Label label1;

        // Token: 0x0400000E RID: 14
        private Button btnOK;

        // Token: 0x0400000F RID: 15
        private Button btnCancel;

        // Token: 0x04000010 RID: 16
        protected internal TextBox txtIp;

        // Token: 0x04000011 RID: 17
        protected internal TextBox txtPort;

        // Token: 0x04000012 RID: 18
        private Label label2;

        #endregion
    }
}