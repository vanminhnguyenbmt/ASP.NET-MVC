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
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [RegularExpression(@"^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        public string Phone { get; set; }
        public string Sex { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đăng nhập")]
        public string Account { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}