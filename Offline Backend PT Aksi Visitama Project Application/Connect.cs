using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Offline_Backend_PT_Aksi_Visitama_Project_Application
{
    class Connect
    {
        SqlConnection con;

        public Connect()
        {
            con = new SqlConnection(@"Server = (local);" + "Database = AksiVisitama; Integrated Security = true");
        }

        public DataTable getTabel(String query)
        {
            if (con.State != System.Data.ConnectionState.Closed)
            {
                con.Close();
            }
            con.Open();
            SqlDataAdapter tabel = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            tabel.Fill(dt);
            con.Close();
            return dt;
        }
        public void executeUpdate(String query)
        {
            if (con.State != System.Data.ConnectionState.Closed)
            {
                con.Close();
            }

            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            com.ExecuteNonQuery();
            //con.Close();
        }

        public string getAutoGenerate(String query)
        {
            String newID = "";
            DataTable dt = getTabel(query);

            int pecah = 0;
            String oneofChar = "";
            if (dt.Rows.Count == -1)
            {
                pecah = 1;
            }
            else
            {
                String ID = Convert.ToString(dt.Rows[0][0]);
                oneofChar = ID.Substring(0, 1);
                int.TryParse(ID.Substring(1), out pecah);
                pecah += 1;
            }

            if (pecah <= 9)
            {
                newID = oneofChar + "000" + pecah;
            }
            else if (pecah >= 10 && pecah <= 99)
            {
                newID = oneofChar + "00" + pecah;
            }
            else if (pecah >= 100 && pecah <= 999)
            {
                newID = oneofChar + "0" + pecah;
            }
            else if (pecah >= 1000 && pecah <= 9999)
            {
                newID = oneofChar + pecah;
            }
            return newID;
        }
    }
}
