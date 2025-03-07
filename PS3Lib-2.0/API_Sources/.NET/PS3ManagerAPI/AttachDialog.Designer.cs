using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PS3ManagerAPI
{
    // Token: 0x02000002 RID: 2
    public sealed partial class AttachDialog : Form
    {
        // Token: 0x06000003 RID: 3 RVA: 0x000020CD File Offset: 0x000002CD
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(AttachDialog));
            this.label1 = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.comboBox1 = new ComboBox();
            this.btnRefresh = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PROCESS: ";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(89, 58);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(108, 21);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Attach selected";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(203, 58);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 21);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(89, 18);
            this.comboBox1.MaxDropDownItems = 16;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(189, 21);
            this.comboBox1.TabIndex = 4;
            this.btnRefresh.DialogResult = DialogResult.Retry;
            this.btnRefresh.Location = new Point(12, 58);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(71, 21);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(292, 85);
            base.ControlBox = false;
            base.Controls.Add(this.btnRefresh);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AttachDialog";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Attach process with PS3 Manager API";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        // Token: 0x04000001 RID: 1
        private IContainer components;

        // Token: 0x04000002 RID: 2
        private Label label1;

        // Token: 0x04000003 RID: 3
        private Button btnOK;

        // Token: 0x04000004 RID: 4
        private Button btnCancel;

        // Token: 0x04000005 RID: 5
        private Button btnRefresh;

        // Token: 0x04000006 RID: 6
        protected internal ComboBox comboBox1;
    }
}
