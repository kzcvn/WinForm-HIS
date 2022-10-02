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
    public partial class R_病人基本信息修改 : Form
    {
        string strCon;
        string t_server, t_uid, t_password;
        string t_dbuser;
        string t_card_id;//诊疗卡号

        public R_病人基本信息修改(string server, string uid, string password,string card_id,string dbuser)
        {
            t_card_id = card_id;
            t_server = server; t_uid = uid; t_password = password;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void D_病人基本信息修改_Load(object sender, EventArgs e)
        {
            textBox1.Text = t_card_id;
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 姓名,性别,出生日期,身份证号,电话,联系人,联系人电话,住址,卡内余额 from 病人基本信息表 where 诊疗卡号={0}";
                strCmd = string.Format(strCmd, t_card_id);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox2.Text = reader.GetString(0);//姓名
                    comboBox1.Text = reader.GetString(1);//性别
                    dateTimePicker1.Value = reader.GetDateTime(2);//出生日期
                    textBox3.Text = reader.GetString(3);//身份证号
                    textBox4.Text = reader.GetString(4);//电话
                    textBox5.Text = reader.IsDBNull(5) ? "" : reader.GetString(5);//联系人
                    textBox6.Text = reader.IsDBNull(6) ? "" : reader.GetString(6);//联系人电话
                    textBox7.Text = reader.IsDBNull(7) ? "" : reader.GetString(7);//住址
                    textBox8.Text = reader.GetDecimal(8).ToString();//卡内余额
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)//按钮——保存
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("请先完善信息", "提示", MessageBoxButtons.OK);
                return;
            }
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "UPDATE 病人基本信息表 SET 姓名 = '{0}', 性别 = '{1}',出生日期 = '{2}',身份证号 = '{3}',电话 = '{4}',联系人 = '{5}',联系人电话 = '{6}',住址 = '{7}' WHERE 诊疗卡号 = {8}; ";              
                strCmd = string.Format(strCmd, textBox2.Text, comboBox1.Text, dateTimePicker1.Value, textBox3.Text,textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, t_card_id);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                cmd.ExecuteNonQuery();             
            }
            MessageBox.Show("修改成功");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)//按钮——关闭
        {
            this.Close();
        }



    }
}
