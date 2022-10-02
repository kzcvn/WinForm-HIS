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
    public partial class R_门急诊挂号 : Form
    {
        string strCon;
        string t_server, t_uid, t_password;
        string t_dbuser;

        public R_门急诊挂号(string server, string uid, string password,string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void R1_1_Load(object sender, EventArgs e)
        {
            update_comb2();
            update_datagridview2();
            datagridview2_addButton();
        }

        private void button1_Click(object sender, EventArgs e)//按钮——查询
        {
            if (textBox1.Text == "")
            {
                clear_patient_info();//没有输入直接查询
                return;
            }
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 姓名,性别,出生日期,身份证号,电话 from 病人基本信息表 where 诊疗卡号 = {0}";
                strCmd = string.Format(strCmd,textBox1.Text);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    textBox2.Text = reader.GetString(0);
                    comboBox1.Text = reader.GetString(1);
                    textBox3.Text = reader.GetDateTime(2).ToString("yyyy-MM-dd");
                    textBox5.Text = reader.GetString(3);
                    textBox6.Text = reader.GetString(4);
                    textBox4.Text = Convert.ToString(Convert.ToInt32(DateTime.Now.ToString("yyyy")) -Convert.ToInt32(reader.GetDateTime(2).ToString("yyyy")));
                }
                else
                {
                    clear_patient_info();//输入诊疗卡号不存在
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)//按钮——快速建卡
        {
            R_快速建卡 fR1_2 = new R_快速建卡(t_server, t_uid, t_password,t_dbuser);
            fR1_2.StartPosition = FormStartPosition.CenterScreen;
            fR1_2.ShowDialog();
            fR1_2.StartPosition = FormStartPosition.CenterScreen;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)//点击——>选择科室类别,并更新科室名
        {
            comboBox3.Items.Clear();
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 科室名 from 科室表 where 科室类别 = '{0}'";
                strCmd = string.Format(strCmd, comboBox2.Text);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = strCmd;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())//每次读一行
                {
                    comboBox3.Items.Add(reader.GetValue(0).ToString());
                }
            }
            comboBox3.SelectedIndex = 0;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)//点击——>选择具体科室，并更新医生排班
        {
            update_datagridview2();
            clear_regis_info();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)//点击——>选择日期，更新医生排班
        {
            update_datagridview2();
        }

        private void button3_Click(object sender, EventArgs e)//按钮——前一天
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-1);
        }

        private void button4_Click(object sender, EventArgs e)//按钮——后一天
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
        }

       
        DateTime s_date;//看诊日期
        string s_time;//看诊时间段
        int r_doc_jnum;//挂号医生工号 
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)//按钮——选择
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "btn选择" && e.RowIndex >= 0)
            {
                int doc_jnum = Convert.ToInt32(dataGridView2.CurrentRow.Cells[1].Value.ToString());
                DateTime dp_date = dateTimePicker1.Value;

                R_医生排班 f_r1_3 = new R_医生排班(doc_jnum,dp_date,t_server,t_uid,t_password, t_dbuser);
                f_r1_3.StartPosition = FormStartPosition.CenterScreen;
                f_r1_3.ShowDialog();

                string time = f_r1_3.get_time();
                if(time!=null)
                {
                    s_date = dp_date;
                    s_time = time;
                    r_doc_jnum = doc_jnum;
                    textBox8.Text = dateTimePicker1.Text + " " + time;
                    comboBox4.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                    textBox10.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                    textBox9.Text = get_price().ToString();
                }  
            }
        }

        private void button5_Click(object sender, EventArgs e)//按钮——确认挂号
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入正确的诊疗卡号","提示", MessageBoxButtons.OK);
                return;
            }
            if(comboBox4.Text=="")
            {
                MessageBox.Show("请选择挂号时间及医生", "提示", MessageBoxButtons.OK);
                return;
            }  
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 身份证号 from 病人基本信息表 where 诊疗卡号={0}";
                strCmd = string.Format(strCmd, Convert.ToInt32(textBox1.Text));
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = strCmd;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.GetString(0) != textBox5.Text)
                    {
                        MessageBox.Show("诊疗卡号与患者信息不匹配，请点击查询", "提示", MessageBoxButtons.OK);
                        return;
                    }                     
                }
                else
                {
                    MessageBox.Show("请输入正确的诊疗卡号", "提示", MessageBoxButtons.OK);
                    return;
                }                                    
            }
            string r_num = DateTime.Now.ToString("yyyyMMdd") + GetRandomString(7);//挂号单号
            string r_time = DateTime.Now.ToString();//挂号时间
            int card_id = Convert.ToInt32(textBox1.Text);//病人诊疗卡号
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "insert into 挂号表(挂号单号,挂号时间,看诊日期,看诊时间,病人诊疗卡号,医生工号,挂号科室名,状态,序号) values('{0}','{1}','{2}','{3}',{4},{5},'{6}','{7}',{8})";
                strCmd = string.Format(strCmd, r_num, r_time, s_date, s_time, card_id, r_doc_jnum, comboBox3.Text,"候诊",0);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                cmd.ExecuteNonQuery();
            }
            R_挂号单打印 F_print_1 = new R_挂号单打印(t_server, t_uid, t_password,r_num,t_dbuser);//显示打印挂号单界面
            F_print_1.StartPosition = FormStartPosition.CenterScreen;
            F_print_1.ShowDialog();
            clear_patient_info();
            clear_regis_info();
            update_datagridview2();
            textBox1.Text = null;
        }












        public void clear_patient_info()//函数——清空患者基本信息
        {
            textBox2.Text = null;
            comboBox1.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
        }

        public void clear_regis_info()//函数——清空挂号信息
        {
            comboBox4.Text = null;
            textBox10.Text = null;
            textBox8.Text = null;
            textBox9.Text = null;
        }

        public void update_comb2()//函数——初始化科室类别（comboBox2）
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select distinct 科室类别 from 科室表";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())//每次读一行
                {
                    comboBox2.Items.Add(reader.GetValue(0).ToString());
                }
            }
        }

        public void update_datagridview2()  //函数——更新医生排班信息（datagridview2）
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 工号,姓名,职称,sum(剩余号数)剩余号数 from 医生基本信息表,医生排班表 where 医生基本信息表.工号=医生排班表.医生工号 and 所属科室名='{0}' and 日期 = '{1}' group by 工号,姓名,职称 ";
                strCmd = string.Format(strCmd, comboBox3.Text,dateTimePicker1.Value);
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0].DefaultView;
                dataGridView2.RowHeadersVisible = false;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView2.Rows[0].Selected = false;
                }
            }
            dataGridView2.AllowUserToAddRows = false;
        }

        public void datagridview2_addButton() // 函数——datagridview2添加选择按钮
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "btn选择";
            btn.HeaderText = "操作";
            btn.DefaultCellStyle.NullValue = "选择";
            dataGridView2.Columns.Add(btn);
            dataGridView2.RowTemplate.Height = 40;    
            btn.Width = 70;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Padding newPadding = new Padding(10, 10, 10, 10);
            btn.DefaultCellStyle.Padding = newPadding;
            btn.DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        public decimal get_price() //函数——查询挂号费
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 价格 from 收费项目表 where 类别='{0}' and 名称='挂号费'";
                strCmd = string.Format(strCmd, comboBox4.Text);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = strCmd;
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();//每次读一行
                return reader.GetDecimal(0);
            }
        }

        public static string GetRandomString(int iLength)//函数——生成指定长度的随机数
        {
            string buffer = "0123456789";// 随机字符中也可以为汉字（任何）
            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            int range = buffer.Length;
            for (int i = 0; i < iLength; i++)
            {
                sb.Append(buffer.Substring(r.Next(range), 1));
            }
            return sb.ToString();
        }

    }
}
