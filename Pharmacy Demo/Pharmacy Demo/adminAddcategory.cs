using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.ActiveDirectory;

namespace Pharmacy_Demo
{
    public partial class adminAddcategory : UserControl
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nulln\Documents\Pharmacy.mdf;Integrated Security=True;Connect Timeout=30";
        public adminAddcategory()
        {
            InitializeComponent();
            displayCAtegoriesData();
        }

        public void displayCAtegoriesData()
        {
            addAdmincategoryData aadata = new addAdmincategoryData();
            List<addAdmincategoryData> listData = aadata.listAddcategoriesData();

            dataGridView2.DataSource = listData;
        }

        private void add_catAddbtn_Click(object sender, EventArgs e)
        {
            if (add_Catname.Text == "" || add_Catstatus.SelectedIndex == -1)
            {
                MessageBox.Show("Empty Fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string insertCate = "INSERT INTO catagories (category, status, date_insert) VALUES (@category, @status, @date)";
                        using (SqlCommand cmd = new SqlCommand(insertCate, con))
                        {
                            cmd.Parameters.AddWithValue("@category", add_Catname.Text.Trim());
                            cmd.Parameters.AddWithValue("@status", add_Catstatus.SelectedItem.ToString());

                            DateTime today = DateTime.Today;
                            cmd.Parameters.AddWithValue("@date", today);

                            cmd.ExecuteNonQuery();
                            Clearfield();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            displayCAtegoriesData();

        }

        public void Clearfield()
        {
            add_Catname.Text = "";
            add_Catstatus.SelectedIndex = -1;
        }

        private void add_catClearbtn_Click(object sender, EventArgs e)
        {
            Clearfield();
        }

        private void add_catUpdatebtn_Click(object sender, EventArgs e)
        {
            if (add_Catname.Text == "" || add_Catstatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string upUser = "UPDATE catagories SET category = @category,status=@status WHERE id=@id";
                        using (SqlCommand cmd = new SqlCommand(upUser, con))
                        {
                            cmd.Parameters.AddWithValue("@category", add_Catname.Text.Trim());
                            cmd.Parameters.AddWithValue("@status", add_Catstatus.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@id", getId);
                            cmd.ExecuteNonQuery();
                            Clearfield();
                            MessageBox.Show("Added Successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            displayCAtegoriesData();

        }

        private int getId = 0;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

            getId = Convert.ToInt32(row.Cells[0].Value);
            add_Catname.Text = row.Cells[1].Value.ToString();
            add_Catstatus.Text = row.Cells[2].Value.ToString();

        }

        private void add_catDeletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string deleteQuery = "DELETE FROM catagories WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", getId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No category found with the specified ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            displayCAtegoriesData();
        }
    }
}
