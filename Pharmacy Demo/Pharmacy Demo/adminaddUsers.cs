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
    public partial class adminaddUsers : UserControl
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nulln\Documents\Pharmacy.mdf;Integrated Security=True;Connect Timeout=30";
        public adminaddUsers()
        {
            InitializeComponent();
            displayAddUsers();
        }

        public void displayAddUsers()
        {
            adminAddUsersData aauData = new adminAddUsersData();
            List<adminAddUsersData> listData = aauData.listAddUsersData();

            dataGridView1.DataSource = listData;
        }

        private void add_userAddbtn_Click(object sender, EventArgs e)
        {
            if (add_Username.Text == "" || add_Pass.Text == "" || add_Role.SelectedIndex == -1 || add_status.SelectedIndex == -1)
            {
                MessageBox.Show("Empty feild!!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string checkUsername = "SELECT * FROM USERSP WHERE username = @usern";
                        using (SqlCommand checkUsern = new SqlCommand(checkUsername, con))
                        {
                            checkUsern.Parameters.AddWithValue("@usern", add_Username.Text.Trim());

                            SqlDataAdapter adapter = new SqlDataAdapter(checkUsern);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                string temUsern = add_Username.Text.Substring(0, 1).ToUpper() + add_Username.Text.Substring(1);
                                MessageBox.Show(temUsern + " already exists", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else
                            {
                                string insertData = "INSERT INTO USERSP (username, password, role, status, date_register)" + " VALUES(@usern, @pass, @role,@status,@date)";

                                using (SqlCommand cmd = new SqlCommand(insertData, con))
                                {
                                    cmd.Parameters.AddWithValue("@usern", add_Username.Text.Trim());
                                    cmd.Parameters.AddWithValue("@pass", add_Pass.Text.Trim());
                                    cmd.Parameters.AddWithValue("@role", add_Role.SelectedItem.ToString());
                                    cmd.Parameters.AddWithValue("@status", add_status.SelectedItem.ToString());
                                    DateTime today = DateTime.Today;
                                    cmd.Parameters.AddWithValue("@date", today);
                                    cmd.ExecuteNonQuery();
                                    clearFeild();
                                    MessageBox.Show("Added Successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            displayAddUsers();


        }

        public void clearFeild()
        {
            add_Username.Text = "";
            add_Pass.Text = "";
            add_Role.SelectedIndex = -1;
            add_status.SelectedIndex = -1;
        }

        private void add_userClearbtn_Click(object sender, EventArgs e)
        {
            clearFeild();
        }

        private void add_userUpdatebtn_Click(object sender, EventArgs e)
        {
            if (add_Username.Text == "" || add_Pass.Text == "" || add_Role.SelectedIndex == -1 || add_status.SelectedIndex == -1)
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
                        string upUser = "UPDATE USERSP SET username = @usern, password=@pass, role=@role, status=@status WHERE id=@id";
                        using (SqlCommand cmd = new SqlCommand(upUser, con))
                        {
                            cmd.Parameters.AddWithValue("@usern", add_Username.Text.Trim());
                            cmd.Parameters.AddWithValue("@pass", add_Pass.Text.Trim());
                            cmd.Parameters.AddWithValue("@role", add_Role.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@status", add_status.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@id", getID);
                            cmd.ExecuteNonQuery();
                            clearFeild();
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
            displayAddUsers();

        }
        private int getID = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            getID = Convert.ToInt32(row.Cells[0].Value);
            add_Username.Text = row.Cells[1].Value.ToString();
            add_Pass.Text = row.Cells[2].Value.ToString();
            add_Role.SelectedItem = row.Cells[3].Value.ToString();
            add_status.SelectedItem = row.Cells[4].Value.ToString();

        }

        private void add_userDeletebtn_Click(object sender, EventArgs e)
        {
            if (getID == 0)
            {
                MessageBox.Show("Please select a user to delete", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string deleteQuery = "DELETE FROM USERSP WHERE id = @id";
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@id", getID);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("User deleted successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clearFeild();
                                displayAddUsers(); // Refresh the user list after deletion
                            }
                            else
                            {
                                MessageBox.Show("No user found with the specified ID", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
