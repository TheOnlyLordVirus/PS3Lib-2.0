using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PS3ManagerAPI
{
    // Token: 0x02000004 RID: 4
    public sealed partial class ConnectDialog : Form
    {
        // Token: 0x0600000C RID: 12 RVA: 0x0000275E File Offset: 0x0000095E
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Token: 0x0600000D RID: 13 RVA: 0x00002780 File Offset: 0x00000980
        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ConnectDialog));
            this.label1 = new Label();
            this.txtIp = new TextBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.txtPort = new TextBox();
            this.label2 = new Label();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 26);
            this.label1.Name = "label1";
            this.label1.Size = new Size(23, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP: ";
            this.txtIp.Location = new Point(45, 23);
            this.txtIp.MaxLength = 16;
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new Size(116, 20);
            this.txtIp.TabIndex = 1;
            this.txtIp.Text = "127.0.0.1";
            this.txtIp.TextAlign = HorizontalAlignment.Center;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(118, 58);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 21);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Connect";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(203, 58);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 21);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.txtPort.Enabled = false;
            this.txtPort.Location = new Point(223, 23);
            this.txtPort.MaxLength = 5;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new Size(55, 20);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "7887";
            this.txtPort.TextAlign = HorizontalAlignment.Center;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(174, 26);
            this.label2.Name = "label2";
            this.label2.Size = new Size(43, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "PORT: ";
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(292, 85);
            base.ControlBox = false;
            base.Controls.Add(this.txtPort);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtIp);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ConnectDialog";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Connection with PS3 Manager API";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        // Token: 0x0400000C RID: 12
        private IContainer components;

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
    }
}
