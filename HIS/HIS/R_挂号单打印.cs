using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;

namespace HIS
{
    public partial class R_挂号单打印 : Form
    {
        string strCon;
        string t_server, t_uid, t_password;
        string t_dbuser;
        string t_r_num;
        public R_挂号单打印(string server, string uid, string password,string r_num,string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;
            t_dbuser = dbuser;
            t_r_num = r_num;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void print_1_Load(object sender, EventArgs e)
        {
            int age;//年龄
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 出生日期 from 挂号单打印 where 挂号单号 = '{0}'";
                strCmd = string.Format(strCmd,t_r_num);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = strCmd;
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();//每次读一行
                int birth_year = Convert.ToInt32(reader.GetDateTime(0).ToString("yyyy"));
                age = Convert.ToInt32(DateTime.Now.ToString("yyyy")) - birth_year;
            }

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select 挂号单号,姓名,性别,出生日期,挂号科室,号别,医生,就诊日期,时间段,挂号费,挂号时间,状态,序号 from 挂号单打印 where 挂号单号='{0}'";
                strCmd = string.Format(strCmd, t_r_num);
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ds.Tables[0].Columns.Add("年龄", typeof(int));
                ds.Tables[0].Rows[0][13] = age;

                this.reportViewer1.LocalReport.ReportPath = @"..\..\挂号单.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                this.reportViewer1.RefreshReport();
            }
        }



    }
}
