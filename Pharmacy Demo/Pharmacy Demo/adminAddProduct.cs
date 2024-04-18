using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy_Demo
{
    public partial class adminAddProduct : UserControl
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nulln\Documents\Pharmacy.mdf;Integrated Security=True;Connect Timeout=30";
        public adminAddProduct()
        {
            InitializeComponent();
            displayCategories();
        }
        public void displayCategories()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string selectCategories = "SELECT * FROM catagories WHERE status = 'Active'";

                using (SqlCommand cmd = new SqlCommand(selectCategories, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        prdtcategory.Items.Add(reader["category"]);
                        prdtStatus.Items.Add(reader["status"]);
                    }
                }
            }
        }
        private void prd_addBtn_Click(object sender, EventArgs e)
        {
            if (prdtID.Text == "" || prdtName.Text == "" || prdtcategory.SelectedIndex == -1 || prdtPrice.Text == "" || prdtStock.Text == "" || prdtStatus.SelectedIndex == -1 || pictureBox1.Image == null)
            {

            }

        }

        private void prd_importbtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg;*.png|*.jpg;*.png)";
                string imagePath = "";

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    pictureBox1.ImageLocation = imagePath;
                }
            }catch(Exception ex)
            {
                MessageBox.Show("ERROR: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
