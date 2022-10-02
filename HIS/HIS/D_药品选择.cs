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
    public partial class D_药品选择 : Form
    {
        string strCon;
        string t_server, t_uid, t_password;
        string t_dbuser;

        public D_药品选择(string server, string uid, string password, string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void D_药品_Load(object sender, EventArgs e)
        {
            update_datagridview1();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
        }

        string[] drug_info = new string[4];
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//点击选择药品
        {
            drug_info[0] = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            drug_info[1] = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            drug_info[2] = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            drug_info[3] = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            this.Close();
        }






        public void update_datagridview1() 
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select * from 西药信息表";
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }            
        }

        public string[] get_drug_info()
        {
            return drug_info;
        }
    }
}
