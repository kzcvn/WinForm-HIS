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
    public partial class R_诊疗卡管理 : Form
    {
        string strCon;
        string t_server, t_uid, t_password;
        string t_dbuser;

        public R_诊疗卡管理(string server, string uid, string password,string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void R_诊疗卡管理_Load(object sender, EventArgs e)
        {
            update_dgv1();
        }

        private void button3_Click(object sender, EventArgs e)//按钮——刷新
        {
            update_dgv1();
        }


        private void button1_Click(object sender, EventArgs e)//按钮——搜索
        {
            int card_id = 0;
            int.TryParse(textBox1.Text, out card_id);
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select * from 病人基本信息表 where 诊疗卡号={0} or 姓名='{1}'";
                strCmd = string.Format(strCmd,card_id, textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
        }

        private void button2_Click(object sender, EventArgs e)//按钮——修改信息
        {
            R_病人基本信息修改 f_D_病人基本信息修改 = new R_病人基本信息修改(t_server, t_uid, t_password,dataGridView1.CurrentRow.Cells[0].Value.ToString(), t_dbuser);
            f_D_病人基本信息修改.StartPosition = FormStartPosition.CenterScreen;
            f_D_病人基本信息修改.ShowDialog();
        }




        public void update_dgv1()
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select * from 病人基本信息表";
                //strCmd = string.Format(strCmd, );
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
        }




    }
}
