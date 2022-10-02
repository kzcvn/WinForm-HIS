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
    public partial class D_检验申请单打印 : Form
    {
        string strCon;
        string t_server, t_uid, t_password;
        string t_i_num;//申请单号
        string t_dbuser;
        public D_检验申请单打印(string server, string uid, string password,string i_num, string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;
            t_i_num = i_num;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void D_检验申请单打印_Load(object sender, EventArgs e)
        {
            string name = null;//姓名
            string gender = null;//性别
            int age = 0;//年龄
                        //申请单号：t_i_num
            string diagnosis = null;//诊断
            string doctor = null;//申请医生
            DateTime request_date = DateTime.Now;//申请时间
            string items = null;//检验项目
            double amount = 0;//合计
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select distinct 姓名,性别,出生日期,诊断,申请医生,申请时间 from 检验申请单打印 where 申请单号='{0}'";
                strCmd = string.Format(strCmd, t_i_num);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    name = reader.GetString(0);
                    gender = reader.GetString(1);
                    age = Convert.ToInt32(DateTime.Now.ToString("yyyy")) - Convert.ToInt32(reader.GetDateTime(2).ToString("yyyy"));
                    diagnosis = reader.GetString(3);
                    doctor = reader.GetString(4);
                    request_date = reader.GetDateTime(5);
                }
            }
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select  检验项目名称,价格 from 检验申请单打印 where 申请单号='{0}'";
                strCmd = string.Format(strCmd, t_i_num);
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items += reader.GetString(0) + '\n';
                    amount += Convert.ToDouble(reader.GetDecimal(1));
                }
            }
            DataTable dt = new DataTable();

            //定义本地数据表的列，名称应跟之前所建的testDataTable表中列相同。
            dt.Columns.Add("姓名", typeof(String));
            dt.Columns.Add("性别", typeof(String));
            dt.Columns.Add("年龄", typeof(int));
            dt.Columns.Add("申请单号", typeof(String));
            dt.Columns.Add("诊断", typeof(String));
            dt.Columns.Add("申请医生", typeof(String));
            dt.Columns.Add("申请时间", typeof(DateTime));
            dt.Columns.Add("检验项目", typeof(String));
            dt.Columns.Add("合计", typeof(Double));

            DataRow row = dt.NewRow();
            row[0] = name;
            row[1] = gender;
            row[2] = age;
            row[3] = t_i_num;
            row[4] = diagnosis;
            row[5] = doctor;
            row[6] = request_date;
            row[7] = items;
            row[8] = amount;

            dt.Rows.Add(row);

            this.reportViewer1.LocalReport.ReportPath = @"..\..\检验申请单.rdlc";
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            this.reportViewer1.RefreshReport();
        }
    }
}
