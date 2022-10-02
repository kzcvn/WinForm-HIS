using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HIS
{
    public partial class Login : Form
    {
        string strCon;
        string server, dbuser, password;
        string uid;

        public Login()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            server = "";
            dbuser = "";
            password = "";
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("用户名不能为空!", "登录提示", MessageBoxButtons.OK);
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("密码不能为空!", "登录提示", MessageBoxButtons.OK);
                return;
            }
            uid = textBox1.Text;
          
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "";
                if (uid[0] == '1')
                    strCmd = "select 密码 from 挂号员账号表 where 挂号员工号={0}";
                else if (uid[0] == '4') 
                    strCmd = "select 密码 from 医生账号表 where 工号={0}";
                strCmd = string.Format(strCmd, uid);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = strCmd;
                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())//每次读一行
                {
                    if (reader.GetString(0) == textBox2.Text)
                    {
                        if (uid[0] == '1')
                        {
                            Regis f_rigis = new Regis(server, uid, password, dbuser);
                            f_rigis.StartPosition = FormStartPosition.CenterScreen;
                            f_rigis.Show();
                            this.Hide();
                            con.Close();
                        }
                        else if (uid[0] == '4')
                        {
                            DW f_DW = new DW(server, uid, password, dbuser);
                            f_DW.StartPosition = FormStartPosition.CenterScreen;
                            f_DW.Show();
                            this.Hide();
                            con.Close();
                        }
                    }
                    else
                        MessageBox.Show("密码错误!", "登录提示", MessageBoxButtons.OK);
                }
                 else
                    MessageBox.Show("用户不存在!", "登录提示", MessageBoxButtons.OK);
            }
        }



    }
}
