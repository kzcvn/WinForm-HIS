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
    public partial class D_检验项目选择 : Form
    {
        string strCon;
        string t_server, t_uid, t_password;
        string t_dbuser;
        public D_检验项目选择(string server, string uid, string password, string dbuser)
        {
            t_server = server; t_uid = uid; t_password = password;
            t_dbuser = dbuser;
            strCon = "server=" + server + ";database=HIS;user id=" + dbuser + ";password=" + password;
            InitializeComponent();
        }

        private void D_检验项目选择_Load(object sender, EventArgs e)
        {
            update_dgv1();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)//双击选择检验项目
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView1.CurrentRow.Cells[0].Value == dataGridView2.Rows[i].Cells[0].Value)
                {
                    MessageBox.Show("无法重复选择同一项目");
                    return;
                }
            }
            int index = dataGridView2.Rows.Add();
            dataGridView2.Rows[index].Cells[0].Value = dataGridView1.CurrentRow.Cells[0].Value;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)//退选
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Column2" && e.RowIndex >= 0)
            {
                dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
            }
        }

        string[] items = new string[100];
        private void button2_Click(object sender, EventArgs e)//按钮——确定
        {
            if(dataGridView2.Rows.Count==0)
            {
                MessageBox.Show("还未选择项目");
                return;
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                items[i] = dataGridView2.Rows[i].Cells[0].Value.ToString();
            }
            MessageBox.Show("申请成功");
            this.Close();
        }









        public void update_dgv1()
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strCmd = "select * from 检验项目表 ";
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
        }
      
        public string[] get_items()
        {     
            return items;
        }

    }
}
