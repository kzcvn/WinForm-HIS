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
    public partial class D_医生工作台 : Form
    {
        string strCon;
        string t_server, t_uid, t_password;
        string t_dbuser;

        public D_医生工作台(string server, string uid, string password, string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void D1_Load(object sender, EventArgs e)
        {
            update_datagridview1();
        }

        private void button2_Click(object sender, EventArgs e)//按钮——接诊
        {
            if(radioButton1.Checked && dataGridView1.Rows.Count>0)
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string strCmd = "update 挂号表 set 状态='就诊中' where 挂号单号='{0}'";
                    strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value);
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = strCmd;
                    cmd.ExecuteNonQuery();
                }
                update_datagridview1();               
            }           
        }

        private void button3_Click(object sender, EventArgs e)//按钮——完诊
        {
            if(radioButton2.Checked && dataGridView1.Rows.Count>0)
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string strCmd = "update 挂号表 set 状态='完诊' where 挂号单号='{0}'";
                    strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value);
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = strCmd;
                    cmd.ExecuteNonQuery();
                }
                update_datagridview1();
                if(dataGridView1.Rows.Count==0)
                {
                    panel5.Enabled = false;
                }
            }           
        }

        private void button1_Click(object sender, EventArgs e)//按钮——查找
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 挂号单号,姓名,时间段,序号,状态 from 挂号单打印 where 医生工号='{0}' and 挂号单号='{1}' or 医生工号='{0}' and 姓名='{2}' ";
                strCmd = string.Format(strCmd,t_uid,textBox1.Text,textBox2.Text);
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if(ds.Tables[0].Rows.Count==0)
                {
                    return;
                }
                if(Convert.ToString(ds.Tables[0].Rows[0][4]) == "候诊")
                {
                    radioButton1.Checked = true;
                }
                if (Convert.ToString(ds.Tables[0].Rows[0][4]) == "就诊中")
                {
                    radioButton2.Checked = true;
                }
                ds.Tables[0].Columns.Remove("状态");
                dataGridView1.DataSource = ds.Tables[0].DefaultView;   
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)//点击病人列表——选择病人
        {
            update_tabPage1(dataGridView1.CurrentRow.Cells[0].Value.ToString());//更新tp1（病历）
            update_datagridview2();                                             //更新tp2（处方）
            update_dgv3();
        }


                                                                  //————————————————————————tp1
  
        private void button4_Click(object sender, EventArgs e)//tp1按钮——保存病历
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "if not exists (select 1 from 病历表 where 挂号单号 = '{0}')" +
                    " INSERT INTO 病历表(挂号单号,主诉,现病史,既往史,体格检查,辅助检验检查,诊断,医嘱) VALUES('{0}', '{1}','{2}', '{3}','{4}', '{5}', '{6}','{7}')" +
                    "else UPDATE 病历表 SET 主诉 = '{1}',现病史 = '{2}',既往史 = '{3}',体格检查 = '{4}',辅助检验检查 = '{5}',诊断 = '{6}',医嘱 = '{7}' WHERE 挂号单号 = '{0}'";
                strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value,richTextBox1.Text,richTextBox2.Text,richTextBox3.Text,richTextBox4.Text,richTextBox5.Text,richTextBox6.Text,richTextBox7.Text);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("保存成功");
        }
                                                                  //————————————————————————tp2 
   
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)//tp2西药处方编辑
        {
            if(dataGridView2.CurrentCell.ColumnIndex==0)//选择药品
            {
                D_药品选择 f_D_药品 = new D_药品选择(t_server, t_uid, t_password, t_dbuser);
                f_D_药品.StartPosition = FormStartPosition.Manual;
                f_D_药品.Location = dataGridView2.PointToScreen(new Point(0, (dataGridView2.CurrentRow.Index+1) * dataGridView2.RowTemplate.Height + dataGridView2.ColumnHeadersHeight));
                f_D_药品.ShowDialog();
                dataGridView2.CurrentRow.Cells[0].Value = f_D_药品.get_drug_info()[0];//名称
                dataGridView2.CurrentRow.Cells[1].Value = f_D_药品.get_drug_info()[1];//规格
                dataGridView2.CurrentRow.Cells[7].Value = f_D_药品.get_drug_info()[2];//单位
                dataGridView2.CurrentRow.Cells[8].Value = f_D_药品.get_drug_info()[3];//单价
                radioButton2.Focus();
            }
            if (dataGridView2.CurrentCell.ColumnIndex == 2|| dataGridView2.CurrentCell.ColumnIndex == 3|| dataGridView2.CurrentCell.ColumnIndex == 4)//选择用法、频率、用药时间
            {
                D_西药用法选择 f_D_西药用法选择 = new D_西药用法选择(t_server, t_uid, t_password,dataGridView2.CurrentCell.ColumnIndex, t_dbuser);
                f_D_西药用法选择.StartPosition = FormStartPosition.Manual;
                f_D_西药用法选择.Location = dataGridView2.PointToScreen(new Point((dataGridView2.CurrentCell.ColumnIndex * dataGridView2.Columns[0].Width)+dataGridView2.RowHeadersWidth, (dataGridView2.CurrentRow.Index + 1) * dataGridView2.RowTemplate.Height + dataGridView2.ColumnHeadersHeight));
                f_D_西药用法选择.ShowDialog();
                dataGridView2.CurrentCell.Value = f_D_西药用法选择.get_text();
                radioButton2.Focus();
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)//tp2计算每一列总金额
        {
            for(int i=0; i<dataGridView2.Rows.Count;i++)
            {
                dataGridView2.Rows[i].Cells[9].Value = Convert.ToDouble(dataGridView2.Rows[i].Cells[6].Value) * Convert.ToDouble(dataGridView2.Rows[i].Cells[8].Value);
            }    
        }

        private void button8_Click(object sender, EventArgs e)//tp2按钮——新增
        {
            dataGridView2.Rows.Add(1);
        }

        private void button9_Click(object sender, EventArgs e)//tp2按钮——删除
        {
            if(dataGridView2.Rows.Count==0)
            {
                return;
            }
            int index = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows.RemoveAt(index);
        }

        private void button6_Click(object sender, EventArgs e)//tp2按钮——保存西药处方
        {
            if(dataGridView2.Rows.Count==0)//还未编辑直接保存
            {
                return;
            }
            using (SqlConnection con = new SqlConnection(strCon))//还未书写病历
            {
                con.Open();
                string strCmd = "select count(*) from 病历表 where 挂号单号= '{0}'";
                strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if(reader.GetInt32(0)==0)
                {
                    MessageBox.Show("请先书写病历");
                    return;
                }
            }
            using (SqlConnection con = new SqlConnection(strCon))//
            {
                con.Open();
                string strCmd = "delete from 西药处方表 where 挂号单号= '{0}'";
                strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                cmd.ExecuteNonQuery();
            }
            for (int i=0;i<dataGridView2.Rows.Count;i++)
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string strCmd = " INSERT INTO 西药处方表(挂号单号,药品名称,用法,频率,用药时间,单次用量,总量) VALUES('{0}', '{1}','{2}', '{3}','{4}', '{5}', '{6}')";                       
                    string s1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();//挂号单号
                    string s2 = dataGridView2.Rows[i].Cells[0].Value.ToString();//药品名称
                    string s3 = dataGridView2.Rows[i].Cells[2].Value.ToString();//用法
                    string s4 = dataGridView2.Rows[i].Cells[3].Value.ToString();//频率
                    string s5 = dataGridView2.Rows[i].Cells[4].Value.ToString();//用药时间
                    string s6 = dataGridView2.Rows[i].Cells[5].Value.ToString();//单次用量
                    string s7 = dataGridView2.Rows[i].Cells[6].Value.ToString();//总量
                    strCmd = string.Format(strCmd, s1, s2, s3, s4, s5, s6, s7);
                    SqlCommand cmd = new SqlCommand(strCmd, con);
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("处方保存成功");
        }


        private void button7_Click(object sender, EventArgs e)//tp2按钮——打印西药处方
        {
            D_西药处方打印 f_D_西药处方打印 = new D_西药处方打印(t_server, t_uid, t_password, dataGridView1.CurrentRow.Cells[0].Value.ToString(), t_dbuser);
            f_D_西药处方打印.StartPosition = FormStartPosition.CenterScreen;
            f_D_西药处方打印.ShowDialog();
        }

                                                                //————————————————————————tp3

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            update_dgv4();
        }

        private void button10_Click(object sender, EventArgs e)//tp3按钮——新开申请
        {
            using (SqlConnection con = new SqlConnection(strCon))//还未书写病历
            {
                con.Open();
                string strCmd = "select count(*) from 病历表 where 挂号单号= '{0}'";
                strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.GetInt32(0) == 0)
                {
                    MessageBox.Show("请先书写病历");
                    return;
                }
            }
            D_检验项目选择 f_D_检验项目选择 = new D_检验项目选择(t_server, t_uid, t_password, t_dbuser);
            f_D_检验项目选择.StartPosition = FormStartPosition.CenterScreen;
            f_D_检验项目选择.ShowDialog();
            string s = "I" + GetRandomString(5);
            for(int i=0;f_D_检验项目选择.get_items()[i]!=null;i++)
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string strCmd = "INSERT INTO 检验申请单(挂号单号,申请单号,检验项目名称,申请时间) VALUES('{0}', '{1}','{2}', '{3}')";
                    strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value,s,f_D_检验项目选择.get_items()[i],DateTime.Now);
                    SqlCommand cmd = new SqlCommand(strCmd, con);
                    cmd.ExecuteNonQuery();
                }
            }
            update_dgv3();
        }

        private void button11_Click(object sender, EventArgs e)//tp3按钮——作废申请 
        {
            if(dataGridView3.Rows.Count==0)
            {
                return;
            }
            string message = "确定要作废该申请单吗？";
            string title = "提示";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string strCmd = "delete from 检验申请单 where 申请单号='{0}'";
                    strCmd = string.Format(strCmd, dataGridView3.CurrentRow.Cells[0].Value);
                    SqlCommand cmd = new SqlCommand(strCmd, con);
                    cmd.ExecuteNonQuery();
                }
            }
            update_dgv3();        
        }

        private void button12_Click(object sender, EventArgs e)//tp3按钮——打印检验申请单 
        {
            if(dataGridView3.Rows.Count==0)
            {
                return;
            }
            D_检验申请单打印 f_D_检验申请单打印 = new D_检验申请单打印(t_server, t_uid, t_password, dataGridView3.CurrentRow.Cells[0].Value.ToString(), t_dbuser);
            f_D_检验申请单打印.StartPosition = FormStartPosition.CenterScreen;
            f_D_检验申请单打印.ShowDialog();
        }










        private void update_datagridview1()//更新病人列表
        {
            if(radioButton1.Checked)
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string strCmd = "select 挂号单号,姓名,时间段,序号 from 挂号单打印 where 状态='候诊'and 医生工号='{0}' order by 时间段,序号";
                    strCmd = string.Format(strCmd, t_uid);//只显示当前登录的医生的病人
                    SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0].DefaultView;
                }
                panel5.Enabled = false;
            } 
            if(radioButton2.Checked)
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string strCmd = "select 挂号单号,姓名,时间段,序号 from 挂号单打印 where 状态='就诊中' and 医生工号='{0}' order by 时间段,序号";
                    strCmd = string.Format(strCmd, t_uid);
                    SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0].DefaultView;
                }
                if(dataGridView1.Rows.Count>0)
                {
                    panel5.Enabled = true;
                }              
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

                                                                         //————————————————————tp1

        public void clear_tabPage1()//tp1清空病历
        {
            richTextBox1.Text = null;
            richTextBox2.Text = null;
            richTextBox3.Text = null;
            richTextBox4.Text = null;
            richTextBox5.Text = null;
            richTextBox6.Text = null;
            richTextBox7.Text = null;
        }

        public void update_tabPage1(string r_num)//tp1更新病历信息
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 姓名,性别,挂号科室,出生日期,医生 from 挂号单打印 where 挂号单号='{0}'";
                strCmd = string.Format(strCmd, r_num);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label8.Text = reader.GetString(0);
                    label9.Text = reader.GetString(1);
                    label11.Text = reader.GetString(2);
                    label10.Text = Convert.ToString(Convert.ToInt32(DateTime.Now.ToString("yyyy")) - Convert.ToInt32(reader.GetDateTime(3).ToString("yyyy")));
                    label20.Text = reader.GetString(4);
                    label22.Text = DateTime.Now.ToString("yyyy年MM月dd日");
                }
            }
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select * from 病历表 where 挂号单号='{0}'";
                strCmd = string.Format(strCmd, r_num);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    richTextBox1.Text = reader.GetString(1);
                    richTextBox2.Text = reader.GetString(2);
                    richTextBox3.Text = reader.GetString(3);
                    richTextBox4.Text = reader.GetString(4);
                    richTextBox5.Text = reader.GetString(5);
                    richTextBox6.Text = reader.GetString(6);
                    richTextBox7.Text = reader.GetString(7);
                }
                else
                    clear_tabPage1();
            }
        }


                                                                    //——————————————————————tp2

        public void update_datagridview2()//tp2更新当前病人的西药处方信息
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 药品名称,规格,用法,频率,用药时间,单次用量,总量,单位,单价 from 西药处方打印 where 挂号单号='{0}'";
                strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                int i = 0;
                dataGridView2.Rows.Clear();
                while (reader.Read())
                {
                    dataGridView2.Rows.Add(1);
                    dataGridView2.Rows[i].Cells[0].Value = reader.GetValue(0);
                    dataGridView2.Rows[i].Cells[1].Value = reader.GetValue(1);
                    dataGridView2.Rows[i].Cells[2].Value = reader.GetValue(2);
                    dataGridView2.Rows[i].Cells[3].Value = reader.GetValue(3);
                    dataGridView2.Rows[i].Cells[4].Value = reader.GetValue(4);
                    dataGridView2.Rows[i].Cells[5].Value = reader.GetValue(5);
                    dataGridView2.Rows[i].Cells[6].Value = reader.GetValue(6);
                    dataGridView2.Rows[i].Cells[7].Value = reader.GetValue(7);
                    dataGridView2.Rows[i].Cells[8].Value = reader.GetValue(8);
                    i++;
                }
            }
        }


                                                              //——————————————————————————tp3


        public void update_dgv3()//tp3更新当前病人的检验申请单列表
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select distinct 申请单号,申请科室,申请医生,申请时间 from 检验申请单打印 where 挂号单号='{0}'";
                strCmd = string.Format(strCmd, dataGridView1.CurrentRow.Cells[0].Value);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                dataGridView3.Rows.Clear();
                while (reader.Read())
                {
                    dataGridView3.Rows.Add(1);
                    dataGridView3.Rows[i].Cells[0].Value = reader.GetValue(0);
                    dataGridView3.Rows[i].Cells[1].Value = reader.GetValue(1);
                    dataGridView3.Rows[i].Cells[2].Value = reader.GetValue(2);
                    dataGridView3.Rows[i].Cells[3].Value = reader.GetValue(3);
                    i++;
                }
            }
            if (dataGridView3.Rows.Count == 0)
            {
                dataGridView4.Rows.Clear();
            }
            else
            {
                dataGridView3.Rows[0].Selected = true;
                dataGridView3.CurrentCell = dataGridView3.Rows[0].Cells[0]; // 关键
                update_dgv4();
            }                
        }

        public void update_dgv4()//tp3更新选中的检验申请单中的项目
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 检验项目名称,样本类型,价格,结果 from 检验申请单打印 where 申请单号='{0}'";
                strCmd = string.Format(strCmd, dataGridView3.CurrentRow.Cells[0].Value);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                int i = 0;
                dataGridView4.Rows.Clear();
                while (reader.Read())
                {
                    dataGridView4.Rows.Add(1);
                    dataGridView4.Rows[i].Cells[0].Value = reader.GetValue(0);
                    dataGridView4.Rows[i].Cells[1].Value = reader.GetValue(1);
                    dataGridView4.Rows[i].Cells[2].Value = reader.GetValue(2);
                    dataGridView4.Rows[i].Cells[3].Value = reader.GetValue(3);
                    i++;
                }
            }
        }


    }
}
