using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductCategoryViewModel
    {
        [Required]
        public string MALOAISP { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên loại sản phẩm")]
        public string TENLOAISP { get; set; }
    }
}