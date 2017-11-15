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
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            ClientManagement dbhandle = new ClientManagement();
            ModelState.Clear();
            return View(dbhandle.GetClientsInfo());
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Client cmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClientManagement cdb = new ClientManagement();
                    if (cdb.AddClient(cmodel))
                    {
                        
                        ViewBag.Message = "Client Details Added Successfully";
                        ModelState.Clear();
                    }
                }
                return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            ClientManagement cdb = new ClientManagement();
            return View(cdb.GetClientsInfo().Find(cmodel => cmodel.ClientId == id));
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Client cmodel)
        {
            try
            {
                ClientManagement cdb = new ClientManagement();
                cdb.UpdateDetails(cmodel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            
            return View();
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Client cmodel)
        {
            try
            {
                ClientManagement cdb = new ClientManagement();
                if (cdb.DeleteClient(id))
                {
                    ViewBag.AlertMsg = "Client Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Profil()
        {
            ClientManagement dbhandle = new ClientManagement();
            ModelState.Clear();
            return View(dbhandle.GetClientsInfo());
        }

        private static string Connection()
        {
            return ConfigurationManager.ConnectionStrings["Deliverycon"].ToString();
        }


        private bool DoesEmailExist(string email, int Id)
        {
           
            var q = "select TOP 1 Email from Client where Email=@Email AND Id<>@CltId";

            //using (var c = new SqlConnection(conStr))
            using (SqlConnection con = new SqlConnection(Connection()))
            {
                {
                    using (var cmd = new SqlCommand(q, con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@CltId", Id);
                        var r = cmd.ExecuteReader();
                        return r.HasRows;
                    }
                }
            }
        }

        public JsonResult IsClientExist(string Email, int Id)
        {
            var r = DoesEmailExist(Email,Id);
            return Json(!r, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }



    }
}
