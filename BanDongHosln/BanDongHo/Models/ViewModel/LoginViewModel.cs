using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Bạn cần nhập tài khoản")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
}