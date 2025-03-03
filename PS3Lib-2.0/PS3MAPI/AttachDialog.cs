using PS3ManagerAPI;
using System.Windows.Forms;

namespace PS3Lib
{
    public partial class AttachDialog : Form
    {
        public AttachDialog(PS3MAPI MyPS3MAPI)
        {
            InitializeComponent();
            this.comboBox1.Items.Clear();
            foreach (uint num in MyPS3MAPI.Process.GetPidProcesses())
            {
                if (num == 0U)
                {
                    break;
                }
                this.comboBox1.Items.Add(MyPS3MAPI.Process.GetName(num));
            }
            this.comboBox1.SelectedIndex = 0;
        }
    }
}
