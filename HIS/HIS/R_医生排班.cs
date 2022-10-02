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
    public partial class R_医生排班 : Form
    {
        int t_doc_jnum;
        DateTime t_dp_date;
        string strCon;
        string t_server, t_uid, t_password;
        string t_dbuser;

        public R_医生排班(int doc_jnum,DateTime dp_date, string server, string uid, string password, string dbuser)
        {
            t_doc_jnum = doc_jnum;t_dp_date = dp_date;
            t_server = server; t_uid = uid; t_password = password;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void R1_3_Load(object sender, EventArgs e)
        {         
            update_datagridview1();
            datagridview1_addButton();
            update_datagridview1();//0123——>1230
            update_label();
        }

        string time;//存储挂号时间段
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//点击——>选择挂号时间段，将返回值存入time
        {
            if(dataGridView1.CurrentRow.Cells[3].Value.ToString() == "0")
            {
                MessageBox.Show("该时间段已满诊，请选择其他时间段", "提示", MessageBoxButtons.OK);
                return;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btn选择" && e.RowIndex >= 0)
            {
                time = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                this.Close();
            }
        }







        public void update_datagridview1()//函数——更新datagridview1
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 午别,时间段,剩余号数 from 医生排班表 where 医生工号={0} and 日期 = '{1}'";
                strCmd = string.Format(strCmd, t_doc_jnum, t_dp_date);
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.Rows[0].Selected = false;
                }
                dataGridView1.RowHeadersVisible = false;
            }
            dataGridView1.AllowUserToAddRows = false;
        }

        public void datagridview1_addButton() // 函数——datagridview1添加选择按钮
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "btn选择";
            btn.HeaderText = "操作";
            btn.DefaultCellStyle.NullValue = "选择";
            dataGridView1.Columns.Add(btn);
            //dataGridView1.RowTemplate.Height = 40;
            //btn.Width = 70;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Padding newPadding = new Padding(15, 4, 15, 4);
            btn.DefaultCellStyle.Padding = newPadding;
            btn.DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        public void update_label() //函数——初始化label1、2
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 姓名,职称 from 医生基本信息表 where 工号={0}";
                strCmd = string.Format(strCmd, t_doc_jnum);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = strCmd;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())//每次读一行
                {
                    label1.Text = reader.GetString(0);
                    label2.Text = reader.GetString(1);
                }
            }
        }

        public string get_time()  //返回挂号时间段
        {
            return time;
        }



    }
}
