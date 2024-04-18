using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Demo
{
    internal class addAdmincategoryData
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nulln\Documents\Pharmacy.mdf;Integrated Security=True;Connect Timeout=30";

        public int ID {  get; set; }
        public string Category { get; set; }
        public string Status { get; set; }

        public string Date { get; set; }
        public List<addAdmincategoryData> listAddcategoriesData()
        {
            List <addAdmincategoryData>listdata = new List <addAdmincategoryData> ();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open ();
                string selectdata = "SELECT * FROM catagories";
                using (SqlCommand cmd = new SqlCommand(selectdata, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader ();

                    while (reader.Read ())
                    {
                        addAdmincategoryData data = new addAdmincategoryData ();
                        data.ID = (int)reader["id"];
                        data.Category = reader["category"].ToString();
                        data.Status = reader["status"].ToString();
                        data.Date = ((DateTime)reader["date_insert"]).ToString("MM/dd/yy");

                        listdata.Add (data);
                    }
                }
            }

                return listdata; 
        }
    }
}
