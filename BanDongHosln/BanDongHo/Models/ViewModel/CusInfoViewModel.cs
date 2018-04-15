using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BanDongHo.Models.Models;

namespace BanDongHo.Models.ViewModel
{
    public class CusInfoViewModel
    {
        public string DiaChiGiao { get; set; }
        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"^(\d{4,15})$", ErrorMessage = "*{0} không hợp lệ")]
        public string Sdt { get; set; }
        public string MoTa { get; set; }
        public Cart cart { get; set; }
    }
}