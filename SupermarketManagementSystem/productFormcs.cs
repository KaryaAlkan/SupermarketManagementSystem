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
    public partial class productFormcs : Form
    {
        public productFormcs()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\karya\Documents\smarketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fillcombo()
        {
            //This method will bind the combobox with the database
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CatName from CategoryTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            CatCb.ValueMember = "catName";
            CatCb.DataSource = dt;
            searchCb.ValueMember = "catName";
            searchCb.DataSource = dt;
            Con.Close();

            Con.Close();
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void productFormcs_Load(object sender, EventArgs e)
        {
            fillcombo();
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            categoryForm cat = new categoryForm();
            cat.Show();
            this.Hide();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into ProductTbl values(" + ProdId.Text + ", '" + ProdName.Text + "'," + ProdQty.Text + ","+ProdPrice.Text+",'"+CatCb.SelectedValue.ToString()+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully");
                Con.Close();
                populate();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                ProdId.Text = dataGridView1.Rows[e.RowIndex].Cells["ProdId"].FormattedValue.ToString();
                ProdName.Text = dataGridView1.Rows[e.RowIndex].Cells["ProdName"].FormattedValue.ToString();
                ProdQty.Text = dataGridView1.Rows[e.RowIndex].Cells["ProdQty"].FormattedValue.ToString();
                ProdPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["ProdPrice"].FormattedValue.ToString();
                CatCb.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["ProdCat"].FormattedValue.ToString();
            }
        }

        private void CatCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdId.Text == "" || ProdName.Text == "" || ProdQty.Text == "" || ProdPrice.Text == "" || CatCb.SelectedValue.ToString() == null)
                {
                    MessageBox.Show("Missing information.");
                }
                else
                {
                    Con.Open();
                    string query = "Update ProductTbl set ProdName ='" + ProdName.Text + "',ProdQty = '" + ProdQty.Text + "',ProdPrice = '" + ProdPrice.Text + "',ProdCat = '" + CatCb.SelectedValue.ToString() +
                        "' where ProdId = " + ProdId.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Successfully Updated.");
                    Con.Close();
                    populate();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdId.Text == "")
                {
                    MessageBox.Show("Select The Product to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "Delete from ProductTbl where ProdId = " + ProdId.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted Successfully");
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

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "select * from ProductTbl where ProdCat = '" + searchCb.SelectedValue.ToString();
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
            
        }
    }
}
