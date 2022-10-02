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
    public partial class D_西药用法选择 : Form
    {
        string strCon;
        string t_server, t_uid, t_password;
        int t_column_index;
        string t_dbuser;

        public D_西药用法选择(string server, string uid, string password,int column_index, string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;t_column_index = column_index;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void D_西药用法选择_Load(object sender, EventArgs e)
        {
            update_datagridview1();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            this.Close();
        }




        public void update_datagridview1()
        {
            string tablename = null;
            if (t_column_index == 2)
            {
                tablename = "给药途径表";
            }
            else if(t_column_index == 3)
            {
                tablename = "给药频次表";
            }
            else if (t_column_index == 4)
            {
                tablename = "给药时间表";
            }
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select * from {0}";
                strCmd = string.Format(strCmd, tablename);
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
        }

        string text;
        public string get_text()
        {
            return text;
        }
    }
}
