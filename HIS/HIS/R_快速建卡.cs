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
    public partial class R_快速建卡 : Form
    {
        string t_server, t_uid, t_password;
        string t_dbuser;
        string strCon;
        public R_快速建卡(string server, string uid, string password,string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void R1_2_Load(object sender, EventArgs e)//加载建卡窗口并自动生成卡号
        {
            using (SqlConnection con = new SqlConnection(strCon))//第一次建卡
            {
                con.Open();
                string strCmd = "select * from 病人基本信息表";
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = strCmd;
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    textBox1.Text = Convert.ToString(66600001);
                    return;
                }
            }
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select max(诊疗卡号) from 病人基本信息表";
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = strCmd;
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                textBox1.Text = (reader.GetInt32(0) + 1).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)//按钮——立即建卡
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("请先完善信息", "提示", MessageBoxButtons.OK);
                return;
            }
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "insert into 病人基本信息表(诊疗卡号, 姓名, 性别, 身份证号, 出生日期, 电话,卡内余额,卡状态) values({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}')";
                DateTime birthdate = dateTimePicker1.Value;//出生日期
                strCmd = string.Format(strCmd, Convert.ToInt32(textBox1.Text), textBox2.Text, comboBox1.Text, textBox3.Text, birthdate, textBox4.Text, 0.00, "正常");
                SqlCommand cmd2 = new SqlCommand(strCmd, con);
                cmd2.ExecuteNonQuery();
            }
            MessageBox.Show("建卡成功", "提示", MessageBoxButtons.OK);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)//按钮——取消
        {
            this.Close();
        }



    }
}
