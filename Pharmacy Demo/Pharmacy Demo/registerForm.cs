using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy_Demo
{
    public partial class registerForm : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nulln\Documents\Pharmacy.mdf;Integrated Security=True;Connect Timeout=30";
        public registerForm()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Form1 ft = new Form1();
            this.Hide();
            ft.Show();
        }

        private void Show_pass_CheckedChanged(object sender, EventArgs e)
        {
            if (Show_pass.Checked)
            {
                reg_Passbox.PasswordChar = '\0';
                reg_ConfirmPass.PasswordChar = '\0';
            }
            else
            {
                reg_Passbox.PasswordChar = '*';
                reg_ConfirmPass.PasswordChar = '*';
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Regis_Click(object sender, EventArgs e)
        {
            if(reg_Username.Text =="" || reg_Passbox.Text=="" || reg_ConfirmPass.Text == "")
            {
                MessageBox.Show("Emtpy fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string checkUsernQuery = "SELECT * FROM USERSP WHERE username = @usern";//retrive table data by username value

                    using (SqlCommand checkUsern = new SqlCommand(checkUsernQuery, connection))//to represent a SQL statement or stored procedure to execute against a SQL Server database
                    {
                        checkUsern.Parameters.AddWithValue("@usern", reg_Username.Text.Trim());//add username to the tabel

                        SqlDataAdapter adp = new SqlDataAdapter(checkUsern);// bridge between a DataSet and a data source for retrieving and saving data
                        DataTable table = new DataTable();

                        adp.Fill(table);//filling datatable

                        if (table.Rows.Count != 0)
                        {
                            string temUsern = reg_Username.Text.Substring(0,1).ToUpper() + reg_Username.Text.Substring(1);
                            MessageBox.Show(temUsern + " is Already exiting!!!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       
                        }else if(reg_Passbox.Text.Length<8)
                        {
                            MessageBox.Show("Invalid password, at least 8 characters are needed!!!","Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if(reg_Passbox.Text != reg_ConfirmPass.Text)
                        {
                            MessageBox.Show("Password Doesn't match", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        else
                        {
                            string insertData = "INSERT INTO USERSP (username, password, role, status, date_register)" + " VALUES(@usern, @pass, @role,@status,@date)";

                            using (SqlCommand cmd = new SqlCommand(insertData, connection))
                            {
                                cmd.Parameters.AddWithValue("@usern", reg_Username.Text.Trim());
                                cmd.Parameters.AddWithValue("@pass", reg_Passbox.Text.Trim());
                                cmd.Parameters.AddWithValue("@role", "Cashier");
                                cmd.Parameters.AddWithValue("@status", "Active");

                                DateTime today= DateTime.Today;
                                cmd.Parameters.AddWithValue("@date", today);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Register Success", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                Form1 loginFrm = new Form1();
                                this.Hide();
                                loginFrm.Show();
                   

                            }

                        }
                    }

                }
            }
        }
    }
}
