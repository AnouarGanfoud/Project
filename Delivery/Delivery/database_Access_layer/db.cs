using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Delivery.database_Access_layer
{



    public class db
    {
      



        public int Admin_Login(string Email, string Password)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DeliveryCon"].ConnectionString)) { 
            int res = 0;
            SqlCommand com = new SqlCommand("SP_Login", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Email", Email);
            com.Parameters.AddWithValue("@Password", Password);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "@Isvalid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);
            con.Open();
            com.ExecuteNonQuery();
            res = Convert.ToInt32(oblogin.Value);
            return res;
            }
        }
    }
}