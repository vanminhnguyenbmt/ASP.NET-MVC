using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanDongHo.Areas.Admin.Models
{
    public class PromotionViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string MAKM { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string TENKM { get; set; }
    }
}