using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS
{
    public partial class DW : Form
    {
        string t_server, t_uid, t_password;
        string t_dbuser;

        public DW(string server, string uid, string password, string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;
            t_dbuser = dbuser;
            InitializeComponent();
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Login f_login = new Login();
            f_login.StartPosition = FormStartPosition.CenterScreen;
            f_login.Show();
            this.Close();
        }

        private void 医生工作台ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            D_医生工作台 f_d1 = new D_医生工作台(t_server, t_uid, t_password,t_dbuser);//实例化FormChild子窗体
            bool isOpened = false;//定义子窗体打开标记，默认值为false
            foreach (Form form in this.MdiChildren)//循环MDI中的所有子窗体
            {
                if (f_d1.Name == form.Name)//若该子窗体已被打开
                {
                    form.Activate();//激活该窗体
                    form.WindowState = FormWindowState.Maximized;
                    isOpened = true;//设置子窗体的打开标记为true
                    f_d1.Dispose();//销毁formChild实例
                    break;
                }
            }
            if (!isOpened)//若该子窗体未打开，则显示该子窗体
            {
                f_d1.MdiParent = this;
                //fr1_1.MaximizeBox = false;
                //fr1_1.MinimizeBox = false;
                f_d1.WindowState = FormWindowState.Maximized;
                f_d1.Show();
                f_d1.Dock = DockStyle.Fill;
            }
        }


    }
}
