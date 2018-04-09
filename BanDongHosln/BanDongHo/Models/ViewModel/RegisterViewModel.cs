using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn cần nhập tên")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập tên")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng")]
        public string Phone { get; set; }
        public string Sex { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đăng nhập")]
        public string Account { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; }
    }
}