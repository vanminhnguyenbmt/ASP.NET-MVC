using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.ViewModel
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}