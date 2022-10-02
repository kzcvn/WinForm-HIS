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
    public partial class Regis : Form
    {
        string t_server, t_uid, t_password;
        string t_dbuser;

        public Regis(string server, string uid, string password, string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password; t_dbuser = dbuser;
            InitializeComponent();
        }


        private void Regis_Load(object sender, EventArgs e)
        {
            
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login f_login = new Login();
            f_login.StartPosition = FormStartPosition.CenterScreen;
            f_login.Show();
            this.Close();
        }

        private void 诊疗卡管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            R_诊疗卡管理 f_R_诊疗卡管理 = new R_诊疗卡管理(t_server, t_uid, t_password, t_dbuser);//实例化FormChild子窗体           
            bool isOpened = false;//定义子窗体打开标记，默认值为false
            foreach (Form form in this.MdiChildren)//循环MDI中的所有子窗体
            {
                if (f_R_诊疗卡管理.Name == form.Name)//若该子窗体已被打开
                {
                    form.Activate();//激活该窗体
                    form.WindowState = FormWindowState.Maximized;
                    isOpened = true;//设置子窗体的打开标记为true
                    f_R_诊疗卡管理.Dispose();//销毁formChild实例
                    break;
                }
            }
            if (!isOpened)//若该子窗体未打开，则显示该子窗体
            {
                f_R_诊疗卡管理.MdiParent = this;
                f_R_诊疗卡管理.WindowState = FormWindowState.Maximized;
                f_R_诊疗卡管理.Show();
                f_R_诊疗卡管理.Dock = DockStyle.Fill;
            }
        }

        private void 当日挂号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            R_门急诊挂号 fr1_1 = new R_门急诊挂号(t_server, t_uid, t_password,t_dbuser);//实例化FormChild子窗体
            bool isOpened = false;//定义子窗体打开标记，默认值为false
            foreach (Form form in this.MdiChildren)//循环MDI中的所有子窗体
            {
                if (fr1_1.Name == form.Name)//若该子窗体已被打开
                {
                    form.Activate();//激活该窗体
                    form.WindowState = FormWindowState.Maximized;
                    isOpened = true;//设置子窗体的打开标记为true
                    fr1_1.Dispose();//销毁formChild实例
                    break;
                }
            }
            if (!isOpened)//若该子窗体未打开，则显示该子窗体
            {
                fr1_1.MdiParent = this;
                //fr1_1.MaximizeBox = false;
                //fr1_1.MinimizeBox = false;
                fr1_1.WindowState = FormWindowState.Maximized;
                fr1_1.Show();
                fr1_1.Dock = DockStyle.Fill;
            }
        }

        private void 挂号重打ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            R_挂号重打 fr_挂号重打 = new R_挂号重打(t_server, t_uid, t_password,t_dbuser);//实例化FormChild子窗体
            bool isOpened = false;//定义子窗体打开标记，默认值为false
            foreach (Form form in this.MdiChildren)//循环MDI中的所有子窗体
            {
                if (fr_挂号重打.Name == form.Name)//若该子窗体已被打开
                {
                    form.Activate();//激活该窗体
                    form.WindowState = FormWindowState.Maximized;
                    isOpened = true;//设置子窗体的打开标记为true
                    fr_挂号重打.Dispose();//销毁formChild实例
                    break;
                }
            }
            if (!isOpened)//若该子窗体未打开，则显示该子窗体
            {
                fr_挂号重打.MdiParent = this;
                //fr1_1.MaximizeBox = false;
                //fr1_1.MinimizeBox = false;
                fr_挂号重打.WindowState = FormWindowState.Maximized;
                fr_挂号重打.Show();
                fr_挂号重打.Dock = DockStyle.Fill;
            }
        }




    }
}
