using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductCategoryViewModel
    {
        [Required(ErrorMessage = "")]
        public string MALOAISP { get; set; }
        [Required(ErrorMessage ="")]
        public string TENLOAISP { get; set; }
    }
}