using Delivery.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Delivery.Controllers
{
    public class BestellungController : Controller
    {
        // GET: Order
        private SqlConnection con;
        public ActionResult Index()
        {
            BestellungManagement dbhandle = new BestellungManagement();
            ModelState.Clear();
            return View(dbhandle.GetBestellungInfo());
          

        }
        

        [HttpPost]
        public ActionResult Save_Bestellung(List<Client> clients, Bestellung bs, BestellungViewModel vm)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            try
            {
                if (ModelState.IsValid)
                {
                    BestellungManagement bdb = new BestellungManagement();
                    if (bdb.AddBestellung(bs,vm))
                    {
                        
                        //ModelState.Clear();
                        return RedirectToAction("Index", "Bestellung");
                    }
                }
                else { 

                var errors1 = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                }
              
                return View();
            }
            catch
            {
                return JavaScript("alert('Order is Wrong ! Please verify your data !!')");
            }

        }
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DeliveryCon"].ToString();
            con = new SqlConnection(constring);

        }
    

        public JsonResult SendMailToUser()
        {

            UserDetails user = new UserDetails();
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["DeliveryCon"].ToString()))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("select First_Name, Last_Name, Adresse, Email, Phone, Date, Time, Nbre_P, Items from Bestellung where BestellungId=(Select MAX(BestellungId) From Bestellung)", c))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    user = DataTableToUserDetails(dt);
                }
            }
            //Format mail body
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table>");
            sb.AppendFormat("<tr><td>First Name :</td><td>{0}</td></tr>", user.FirstName.Trim());
            sb.AppendFormat("<tr><td>Last Name :</td><td>{0}</td></tr>", user.LastName.Trim());
            sb.AppendFormat("<tr><td>Address :</td><td>{0}</td></tr>", user.Address.Trim());
            sb.AppendFormat("<tr><td>Email :</td><td>{0}</td></tr>", user.Email.Trim());
            sb.AppendFormat("<tr><td>Phone :</td><td>{0}</td></tr>", user.Phone.Trim());
            sb.AppendFormat("<tr><td>Date :</td><td>{0}</td></tr>", user.Date);
            sb.AppendFormat("<tr><td>Time :</td><td>{0}</td></tr>", user.Time.Trim());
            sb.AppendFormat("<tr><td>Number of persons :</td><td>{0}</td></tr>", user.Nbre_P);
            sb.AppendFormat("<tr><td>Items :</td><td>{0}</td></tr>", user.Items.Trim());
            sb.Append("</table>");
            
            
            bool result = false;
            result = SendEmail(user.Email, "Delivery : Your Order", sb.ToString());
            return Json(result, JsonRequestBehavior.AllowGet);
           

        }
        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        private static UserDetails DataTableToUserDetails(DataTable dt)
        {
            var userDetail = (from rw in dt.AsEnumerable()
                              select new UserDetails()
                              {
                                  FirstName = Convert.ToString(rw["First_Name"]),
                                  LastName = Convert.ToString(rw["Last_Name"]),
                                  Address = Convert.ToString(rw["Adresse"]),
                                  Email = Convert.ToString(rw["Email"]),
                                  Phone = Convert.ToString(rw["Phone"]),
                                  Date = Convert.ToDateTime(rw["Date"]),
                                  Time = Convert.ToString(rw["Time"]),
                                  Nbre_P = Convert.ToInt32(rw["Nbre_P"]),
                                  Items = Convert.ToString(rw["Items"])
                              }).FirstOrDefault();

            return userDetail;
        }
    }

}