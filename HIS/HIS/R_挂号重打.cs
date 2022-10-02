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
    public partial class R_挂号重打 : Form
    {
        string strCon;
        string t_server, t_uid, t_password;
        string t_dbuser;

        public R_挂号重打(string server, string uid, string password, string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void R_挂号重打_Load(object sender, EventArgs e)
        {
            update_dgv1();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            update_dgv1();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            update_dgv1();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//点击挂号单列表
        {
            update_dgv2();
            update_dgv3();
        }

        private void button1_Click(object sender, EventArgs e)//按钮——查询
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select * from 挂号单打印 where 挂号单号= '{0}' or 姓名 = '{1}'";
                strCmd = string.Format(strCmd, textBox1.Text, textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            update_dgv2();
            update_dgv3();
            count_amount();
        }

        private void button2_Click(object sender, EventArgs e)//按钮——挂号单打印
        {
            if(dataGridView1.RowCount!=0)
            {
                R_挂号单打印 F_print_1 = new R_挂号单打印(t_server, t_uid, t_password, dataGridView1.CurrentRow.Cells[0].Value.ToString(),t_dbuser);
                F_print_1.StartPosition = FormStartPosition.CenterScreen;
                F_print_1.ShowDialog();
            }           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            count_amount();
        }







        public void update_dgv1()
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select * from 挂号单打印 where 就诊日期 >= '{0}' and 就诊日期 <= '{1}'";
                strCmd = string.Format(strCmd, dateTimePicker1.Value, dateTimePicker2.Value);
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                if(dataGridView2.Rows.Count != 0)
                {
                    dataGridView2.DataSource = null;
                    dataGridView2.Rows.Clear();
                }
                if (dataGridView3.Rows.Count != 0)
                {
                    dataGridView3.DataSource = null;
                    dataGridView3.Rows.Clear();
                }
            }
            else
            {
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0]; // 关键
                update_dgv2();
                update_dgv3();
            }
            count_amount();
        }

        public void update_dgv2()//更新处方信息
        {
            if(dataGridView1.Rows.Count==0&&dataGridView2.Rows.Count!=0)
            {
                dataGridView2.DataSource = null;
                dataGridView2.Rows.Clear();
                return;
            }
            else if(dataGridView1.Rows.Count!=0)
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string strCmd = "select 药品名称,用法,频率,用药时间,单次用量,总量 from 西药处方表 where 挂号单号= '{0}'";
                    strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value);
                    SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                }
            }
            count_amount();
        }

        public void update_dgv3()
        {
            if(dataGridView1.Rows.Count==0 && dataGridView3.Rows.Count != 0)
            {
                dataGridView3.DataSource = null;
                dataGridView3.Rows.Clear();
                return;
            }
            else if(dataGridView1.Rows.Count!=0)
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string strCmd = "select distinct 申请单号,申请时间 from 检验申请单 where 挂号单号= '{0}'";
                    strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value);
                    SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dataGridView3.DataSource = ds.Tables[0].DefaultView;
                }
            }
            count_amount();
        }

        public void count_amount()
        {
            decimal d1 = 0;
            decimal d2 = 0;
            decimal d3 = 0;
            if (dataGridView1.Rows.Count==0)//挂号费
            {
                d1 = 0;
                label7.Text = "0.00";
            }
            else
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string strCmd = "select 挂号费 from 挂号单打印 where 挂号单号='{0}'";
                    strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value);
                    SqlCommand cmd = new SqlCommand(strCmd, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    d1 = reader.GetDecimal(0);
                    label7.Text = d1.ToString();
                }
            }

            if(dataGridView2.Rows.Count==0)//处方费
            {
                d2 = 0;
                label8.Text = "0.00";
            }
            else
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string strCmd = "select sum(单价) from 西药处方打印  where 挂号单号='{0}'";
                    strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value);
                    SqlCommand cmd = new SqlCommand(strCmd, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    d2 = reader.GetDecimal(0);
                    label8.Text = d2.ToString();
                }
            }

            if(dataGridView3.Rows.Count==0)
            {
                d3 = 0;
                label9.Text = "0.00";
            }
            else
            {
                for (int i = 0; i < dataGridView3.Rows.Count; i++)//检验费
                {
                    using (SqlConnection con = new SqlConnection(strCon))
                    {
                        con.Open();
                        string strCmd = "select sum(价格) from 检验申请单打印  where 申请单号='{0}'";
                        strCmd = string.Format(strCmd, dataGridView3.Rows[i].Cells[0].Value);
                        SqlCommand cmd = new SqlCommand(strCmd, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        d3 += reader.GetDecimal(0);
                        label9.Text = d3.ToString();
                    }
                }
            }
            
            label11.Text = Convert.ToString(d1 + d2 + d3);
        }






    }
}
