using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SupermarketManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void circularButton1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UnameTb.Text = "";
            PassTb.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("Enter the UserName and Password");
            }
            else
            {
                if (RoleCb.SelectedIndex >-1)
                {
                    if (RoleCb.SelectedItem.ToString() == "ADMIN")
                    {
                        if (UnameTb.Text == "Admin" && PassTb.Text == "Admin")
                        {
                            productFormcs prod = new productFormcs();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If you are the Admin, Enter the correct Id and Password");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your In The Seller Section");
                    }

                }
               
                else
                {
                    MessageBox.Show("Select A Role");
                }
            }
        }

        private void RoleCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        private void RoleCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }
    }
}
