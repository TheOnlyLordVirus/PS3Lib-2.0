using PS3ManagerAPI;
using System;
using System.Windows.Forms;

namespace PS3Lib
{
    public partial class LogDialog : Form
    {
        // Token: 0x04000007 RID: 7
        private PS3MAPI PS3MAPI;

        // Token: 0x06000005 RID: 5 RVA: 0x00002450 File Offset: 0x00000650
        public LogDialog()
        {
            InitializeComponent();
        }

        // Token: 0x06000006 RID: 6 RVA: 0x0000245E File Offset: 0x0000065E
        public LogDialog(PS3MAPI MyPS3MAPI) : this()
        {
            this.PS3MAPI = MyPS3MAPI;
        }

        // Token: 0x06000007 RID: 7 RVA: 0x0000246D File Offset: 0x0000066D
        private void LogDialog_Refresh(object sender, EventArgs e)
        {
            if (this.PS3MAPI != null)
            {
                this.tB_Log.Text = this.PS3MAPI.Log;
            }
        }

        // Token: 0x06000008 RID: 8 RVA: 0x0000248D File Offset: 0x0000068D
        private void button1_Click(object sender, EventArgs e)
        {
            base.Hide();
        }
    }
}
