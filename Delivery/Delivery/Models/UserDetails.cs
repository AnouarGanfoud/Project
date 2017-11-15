using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivery.Models
{
    public class UserDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int Nbre_P { get; set; }
        public string Items { get; set; }
    }
}