using Delivery.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Delivery.Controllers
{
    
        public class HomeController : Controller
        {
            private SqlConnection con;

            public ActionResult Index()
            {
                return View();
            }

       
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DeliveryCon"].ToString();
            con = new SqlConnection(constring);
            
        }

        
       [HttpGet]
        public ActionResult Login()
            {
                return View();
            }

        database_Access_layer.db dblayer = new database_Access_layer.db();

       
        [HttpPost]
        public ActionResult Login(FormCollection fc, string LastName, string Email)
        {
          
            int res = dblayer.Admin_Login(fc["Email"], fc["Password"]);
            if (res == 1)
            {
                Session["currentUser"] = Email;
                string z = Email;
                connection();
                con.Open();
                SqlCommand command = new SqlCommand("select Email from Client", con);
               
                List<string> result = new List<string>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        result.Add(reader.GetString(0));
                    con.Close();
                }
                foreach (string x in result)
                {
                    if (x == z)
                    {

                        SqlCommand command2 = new SqlCommand($"select LastName from Client WHERE Email= '{x}' ", con);
                        con.Open();
                        string y = command2.ExecuteScalar().ToString();
                        con.Close();
                        Session["currentUser"] = y;
                       
                    }
                }
             
                return RedirectToAction("Profil", "Client");
                Session.RemoveAll();
               
              
            }
            else {

                TempData["msg"] = " Email or Password is wrong !";
                return RedirectToAction("Index", "Home");

            }
            
        }
        

            private Client GetClient(Client cmodel)
            {
                connection();

                SqlCommand cmd = new SqlCommand("select password,Id,FirstName from clients where Email = @Email", con); 
                cmd.Parameters.Add(new SqlParameter("Email", cmodel.Email));
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                string password = null;
                int id = 0;
                string firstname = null;

                if (reader.Read())
                {
                    password = reader["Password"].ToString();
                    id = (int)reader["Id"];
                    firstname = reader["FirstName"].ToString();
                }

                if (string.IsNullOrEmpty(password)) 
                {
                    return null;
                }

                if (password == cmodel.Password) 
                {
                    return new Client { Email = cmodel.Email, ClientId = id, Password = password, FirstName = firstname };
                }

                return null;
            }
        
    }

        
}