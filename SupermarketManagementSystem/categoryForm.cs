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

namespace SupermarketManagementSystem
{
    public partial class categoryForm : Form
    {
        public categoryForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\karya\Documents\smarketdb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string query = "select * from CategoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CatDVG.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into CategoryTbl values("+CatIdTb.Text+", '"+CatNameTb.Text+"','"+CatDescTb.Text+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully");
                Con.Close();
                populate();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

       
        private void categoryForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CatDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }


        private void button2_Click(object sender, EventArgs e)
        {
            productFormcs prod = new productFormcs();
            prod.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if(CatIdTb.Text == "")
                {
                    MessageBox.Show("Select The Category to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "Delete from CategoryTbl where CatId = " + CatIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted Successfully");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void CatDVG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          if(CatDVG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                CatDVG.CurrentRow.Selected = true;
                CatIdTb.Text = CatDVG.Rows[e.RowIndex].Cells["CatId"].FormattedValue.ToString();
                CatNameTb.Text = CatDVG.Rows[e.RowIndex].Cells["CatName"].FormattedValue.ToString();
                CatDescTb.Text = CatDVG.Rows[e.RowIndex].Cells["CatDesc"].FormattedValue.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if(CatIdTb.Text == "" || CatNameTb.Text == "" || CatDescTb.Text == "")
                {
                    MessageBox.Show("Missing information.");
                }
                else
                {
                    Con.Open();
                    string query = "Update CategoryTbl set CatName ='" + CatNameTb.Text + "',CatDesc = '" + CatDescTb.Text + "' where CatId = " + CatIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Successfully Updated.");
                    Con.Close();
                    populate();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sellerForm seller = new sellerForm();
            seller.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
