using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Delivery.Models
{
    public class ClientManagement
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["deliverycon"].ToString();
            con = new SqlConnection(constring);
        }

        // **************** ADD NEW Client *********************
        public bool AddClient(Client cmodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewClients", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", cmodel.FirstName);
            cmd.Parameters.AddWithValue("@LastName", cmodel.LastName);
            cmd.Parameters.AddWithValue("@Email", cmodel.Email);
            cmd.Parameters.AddWithValue("@Phone", cmodel.Phone);
            cmd.Parameters.AddWithValue("@Address", cmodel.Address);
            cmd.Parameters.AddWithValue("@Password", cmodel.Password);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********** VIEW CLIENT DETAILS ********************
        public List<Client> GetClientsInfo()
        {
            connection();
            List<Client> clientlist = new List<Client>();

            SqlCommand cmd = new SqlCommand("GetClientsInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                clientlist.Add(
                    new Client
                    {
                        //ClientId = Convert.ToInt32(dr["Id"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Email = Convert.ToString(dr["Email"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        Address = Convert.ToString(dr["Address"]),
                        Password = Convert.ToString(dr["Password"])
                    });
            }
            return clientlist;
        }

        // ***************** UPDATE CLIENT DETAILS *********************
        public bool UpdateDetails(Client cmodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateClientsInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CltId", cmodel.ClientId);
            cmd.Parameters.AddWithValue("@FirstName", cmodel.FirstName);
            cmd.Parameters.AddWithValue("@Lastname", cmodel.LastName);
            cmd.Parameters.AddWithValue("@Email", cmodel.Email);
            cmd.Parameters.AddWithValue("@Phone", cmodel.Phone);
            cmd.Parameters.AddWithValue("@Address", cmodel.Address);
            cmd.Parameters.AddWithValue("@Password", cmodel.Password);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********************** DELETE CLIENT DETAILS *******************
        public bool DeleteClient(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteClient", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CltId", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}