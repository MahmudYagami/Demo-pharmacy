using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Pharmacy_Demo
{
    internal class adminAddUsersData
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nulln\Documents\Pharmacy.mdf;Integrated Security=True;Connect Timeout=30";
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }

        public List<adminAddUsersData> listAddUsersData()
        {
            List<adminAddUsersData> listData = new List<adminAddUsersData>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string selectData = "SELECT * FROM USERSP";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                adminAddUsersData aauData = new adminAddUsersData();
                                aauData.ID = (int)dr["ID"];
                                aauData.Username = dr["username"].ToString();
                                aauData.Password = dr["password"].ToString();
                                aauData.Role = dr["role"].ToString();
                                aauData.Status = dr["status"].ToString();
                                aauData.Date = ((DateTime)dr["date_register"]).ToString("MM/dd/yyyy");

                                listData.Add(aauData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
                // Optionally, you can rethrow the exception or log it for further analysis
                throw;
            }

            return listData;
        }

    }

}
