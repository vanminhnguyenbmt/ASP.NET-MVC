using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductBrandViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string TENTH { get; set; }
        public string HINHTH { get; set; }
    }
}