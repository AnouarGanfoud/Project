using Delivery.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;


namespace Delivery.Models
{
    public class BestellungManagement
    {
        
        public bool AddBestellung(Bestellung bs, BestellungViewModel vm)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(Connection()))
                {
                    using (SqlCommand cmd = new SqlCommand("AddNewBestellung", con))
                    {

                        var ts = new Bestellung();
                        foreach (var item in vm.Items)
                        {
                            ts.Items += item + "/";
                        }
                        ts.Items = ts.Items.Substring(0, ts.Items.Length - 1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Date", bs.Date);
            cmd.Parameters.AddWithValue("@Time", bs.Time);
            cmd.Parameters.AddWithValue("@Nbre_P", Convert.ToInt32(bs.Nbre_P));
            cmd.Parameters.AddWithValue("@Items", ts.Items);
            cmd.Parameters.AddWithValue("@Adresse", bs.Address);
            cmd.Parameters.AddWithValue("@Phone", bs.Phone);
            cmd.Parameters.AddWithValue("@Email", bs.Email);
            cmd.Parameters.AddWithValue("@First_Name", bs.FirstName);
            cmd.Parameters.AddWithValue("@Last_Name", bs.LastName);
          
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
            catch(Exception ex)
    {
                string e = ex.Message;
                return false;
            }
        }

      

        private static string Connection()
        {
            return ConfigurationManager.ConnectionStrings["deliverycon"].ToString();
        }

        public List<Bestellung> GetBestellungInfo()
        {
       
            List<Bestellung> Bestellunglist = new List<Bestellung>();
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetBestellungInfo", con))
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    con.Open();
                    sd.Fill(dt);
                    con.Close();


                    foreach (DataRow dr in dt.Rows)
                    {
                        Bestellunglist.Add(
                            new Bestellung
                            {
                                //BestellungId = Convert.ToInt32(dr["Id"]),
                                Date = Convert.ToDateTime(dr["Date"]),
                                Time = Convert.ToString(dr["Time"]),
                                Nbre_P = Convert.ToInt32(dr["Nbre_P"]),
                                Items = Convert.ToString(dr["Items"]),
                                Address = Convert.ToString(dr["Adresse"]),
                                FirstName = Convert.ToString(dr["First_Name"]),
                                LastName = Convert.ToString(dr["Last_Name"]),
                                Email = Convert.ToString(dr["Email"]),
                                Phone = Convert.ToString(dr["Phone"])

                            });
                    }
                    return Bestellunglist;
                }
            }
        }
    }
}