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
    public partial class sellerForm : Form
    {
        public sellerForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\karya\Documents\smarketdb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string query = "select * from SellerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            categoryForm cat = new categoryForm();
            cat.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            productFormcs prod = new productFormcs();
            prod.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into SellerTbl values(" + SellerId.Text + ",'"
                    + SellerName.Text + "'," + SellerAge.Text + "," + SellerPhone.Text +
                    ",'" + SellerPass.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller Added Successfully.");
                Con.Close();
                populate();
                Con.Close();
                populate();
                SellerId.Text = "";
                SellerName.Text = "";
                SellerAge.Text = "";
                SellerPhone.Text = "";
                SellerPass.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if(SellerId.Text == "" || SellerName.Text == "" || SellerAge.Text == "" 
                    || SellerPhone.Text == "" || SellerPass.Text == "")
                {
                    MessageBox.Show("Missing information.");
                }
                else
                {
                    Con.Open();
                    string query = "update SellerTbl set SellerName = '" + SellerName.Text +
                        "',SellerAge = '" + SellerAge.Text + "',SellerPhone = '" + SellerPhone.Text +
                        "',SellerPass = '" + SellerPass.Text + "'where SellerId = " + SellerId.Text + ";";
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

        private void sellerForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void SellerDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(SellerDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                SellerDGV.CurrentRow.Selected = true;
                SellerId.Text = SellerDGV.Rows[e.RowIndex].Cells["SellerId"].FormattedValue.ToString();
                SellerName.Text = SellerDGV.Rows[e.RowIndex].Cells["SellerName"].FormattedValue.ToString();
                SellerAge.Text = SellerDGV.Rows[e.RowIndex].Cells["SellerAge"].FormattedValue.ToString();
                SellerPhone.Text = SellerDGV.Rows[e.RowIndex].Cells["SellerPhone"].FormattedValue.ToString();
                SellerPass.Text = SellerDGV.Rows[e.RowIndex].Cells["SellerPass"].FormattedValue.ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (SellerId.Text == "")
                {
                    MessageBox.Show("Select the Seller to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from SellerTbl where SellerId = " + SellerId.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted Successfully.");
                    Con.Close();
                    populate();
                    Con.Close();
                    populate();
                    SellerId.Text = "";
                    SellerName.Text = "";
                    SellerAge.Text = "";
                    SellerPhone.Text = "";
                    SellerPass.Text = "";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SellerId.Text = "";
            SellerName.Text = "";
            SellerAge.Text = "";
            SellerPhone.Text = "";
            SellerPass.Text = "";
        }
    }
}
