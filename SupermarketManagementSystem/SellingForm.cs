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
    public partial class SellingForm : Form
    {
        public SellingForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\karya\Documents\smarketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select ProdName,ProdPrice from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void populateBills()
        {
            Con.Open();
            string query = "select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillDGV.DataSource = ds.Tables[0];
            Con.Close();
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
            catCb.ValueMember = "catName";
            catCb.DataSource = dt;
            Con.Close();

            Con.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
            populateBills();
            fillcombo();
            SellerNameLbl.Text = Form1.SellerName;
        }

        
        private void ProdDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProdDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                ProdDGV.CurrentRow.Selected = true;
                ProdName.Text = ProdDGV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                ProdPrice.Text = ProdDGV.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DateLbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        int GrdTotal = 0, n = 0;

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(BillId.Text == "")
            {
                MessageBox.Show("Missing Bill Id");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BillTbl values(" + BillId.Text + ", '" + SellerNameLbl.Text + "','" + DateLbl.Text + "'," + AmtLbl.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Added Successfully");
                    Con.Close();
                    populateBills();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK) 
            {
                printDocument1.Print();
            }
        }

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("KARYO SUPERMARKET", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Bill Id : " + BillDGV.Rows[0].Cells[0].FormattedValue.ToString(), new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, new Point(100,70));
            e.Graphics.DrawString("Seller Name : " + BillDGV.Rows[0].Cells[1].FormattedValue.ToString(), new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, new Point(100, 100));
            e.Graphics.DrawString("Date : " + BillDGV.Rows[0].Cells[2].FormattedValue.ToString(), new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, new Point(100, 130));
            e.Graphics.DrawString("Total Amount : " + BillDGV.Rows[0].Cells[3].FormattedValue.ToString(), new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, new Point(100, 160));
            e.Graphics.DrawString("KaryaCode ", new Font("Century Gothic", 20, FontStyle.Italic), Brushes.Red, new Point(230,230));
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        private void ProdDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdName.Text = ProdDGV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            ProdPrice.Text = ProdDGV.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            
        }

        private void BillDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void button9_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "select ProdName,ProdPrice from ProductTbl where ProdCat = '" + catCb.SelectedValue.ToString();
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda); 
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ProdName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(ProdName.Text == "" || ProdPrice.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                int total = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQty.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(OrderDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ProdName.Text;
                newRow.Cells[2].Value = ProdPrice.Text;
                newRow.Cells[3].Value = ProdQty.Text;
                newRow.Cells[4].Value = total;
                OrderDGV.Rows.Add(newRow);
                n++;
                GrdTotal = GrdTotal + total;
                AmtLbl.Text = "" + GrdTotal;
               
            }
           
        }
    }
}
