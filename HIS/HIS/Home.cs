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
    public partial class Home : Form
    {
        string t_server, t_uid, t_password;
        string t_dbuser;

        private void button2_Click(object sender, EventArgs e)//打开医生工作站子系统
        {
            DW f_dw = new DW(t_server, t_uid, t_password, t_dbuser);
            f_dw.StartPosition = FormStartPosition.CenterScreen;
            f_dw.Show();
            this.Close();
        }

        public Home(string server, string uid, string password)
        {
            t_server = server; t_uid = uid; t_password = password;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//打开门诊挂号子系统
        {
            Regis freg = new Regis(t_server, t_uid, t_password,t_dbuser);
            freg.StartPosition = FormStartPosition.CenterScreen;
            freg.Show();
            this.Close();
        }
    }
}
