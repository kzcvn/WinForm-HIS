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
using Microsoft.Reporting.WinForms;

namespace HIS
{
    public partial class D_西药处方打印 : Form
    {
        string strCon;
        string t_server, t_uid, t_password;
        string t_r_num;//挂号单号
        string t_dbuser;
        public D_西药处方打印(string server, string uid, string password,string r_num, string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;
            t_r_num = r_num;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void D_西药处方打印_Load(object sender, EventArgs e)
        {
            string name = null;//姓名
            string gender = null;//性别
            string department = null;//挂号科室
            string doctor = null;//医生
            DateTime date = DateTime.Now;
            string diagnosis = null;//诊断
            string recipe = null;//处方信息
            int age = 0;//年龄
            double amount = 0;//总金额
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 姓名,性别,挂号科室,医生,就诊日期,诊断 from 挂号单打印,病历表 where 挂号单打印.挂号单号='{0}' and 挂号单打印.挂号单号=病历表.挂号单号";
                strCmd = string.Format(strCmd, t_r_num);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    name = reader.GetString(0);
                    gender = reader.GetString(1);
                    department = reader.GetString(2);
                    doctor = reader.GetString(3);
                    date = reader.GetDateTime(4);
                    diagnosis = reader.GetString(5);
                }
            }
            using (SqlConnection con = new SqlConnection(strCon))//处方信息
            {
                con.Open();
                string strCmd = "select 药品名称,规格,总量,单位,单次用量,频率,用法 from 西药处方打印 where 挂号单号='{0}'";
                strCmd = string.Format(strCmd, t_r_num);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    recipe = recipe + reader.GetString(0) + "                              " + reader.GetString(1) + "*" + reader.GetString(2) + reader.GetString(3) + "\n"
                            + "        Sig.   " + reader.GetString(4) + "   " + reader.GetString(5) + "   " + reader.GetString(6) + "\n";
                }
            }
            using (SqlConnection con = new SqlConnection(strCon))//求年龄
            {
                con.Open();
                string strCmd = "select 出生日期 from 挂号单打印 where 挂号单号 = '{0}'";
                strCmd = string.Format(strCmd, t_r_num);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = strCmd;
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();//每次读一行
                int birth_year = Convert.ToInt32(reader.GetDateTime(0).ToString("yyyy"));
                age = Convert.ToInt32(DateTime.Now.ToString("yyyy")) - birth_year;
            }
            using (SqlConnection con = new SqlConnection(strCon))//求总金额
            {
                con.Open();
                string strCmd = "select 总量,单价 from 西药处方打印 where 挂号单号='{0}'";
                strCmd = string.Format(strCmd, t_r_num);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    amount += Convert.ToDouble(reader.GetString(0)) * Convert.ToDouble(reader.GetDecimal(1));
                }
            }

            DataTable dt = new DataTable();

            //定义本地数据表的列，名称应跟之前所建的testDataTable表中列相同。
            dt.Columns.Add("挂号单号", typeof(String));
            dt.Columns.Add("姓名", typeof(String));
            dt.Columns.Add("性别", typeof(String));
            dt.Columns.Add("挂号科室", typeof(String));
            dt.Columns.Add("医生", typeof(String));
            dt.Columns.Add("就诊日期", typeof(DateTime));
            dt.Columns.Add("诊断", typeof(String));
            dt.Columns.Add("处方信息", typeof(String));
            dt.Columns.Add("年龄", typeof(int));
            dt.Columns.Add("总金额", typeof(double));

            DataRow row = dt.NewRow();
            row[0] = t_r_num;
            row[1] = name;
            row[2] = gender;
            row[3] = department;
            row[4] = doctor;
            row[5] = date;
            row[6] = diagnosis;
            row[7] = recipe;
            row[8] = age;
            row[9] = amount;
            dt.Rows.Add(row);

            //设置本地报表，使程序与之前所建的testReport.rdlc报表文件进行绑定.
            this.reportViewer1.LocalReport.ReportPath = @"..\..\西药处方.rdlc";
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));


            this.reportViewer1.RefreshReport();
        }




    }
}
