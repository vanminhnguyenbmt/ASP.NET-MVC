using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BanDongHo.Areas.Admin.Models
{
    public class PromotionDetailViewModel
    {
        
        [Required(ErrorMessage = "Vui lòng chọn khuyến mãi")]
        public string MAKM { get; set; }

        [Required(ErrorMessage = "vui lòng chọn sản phẩm")]
        public int MASP { get; set; }
      
        [Required(ErrorMessage = "vui lòng chọn phần trăm khuyến mãi")]
        public int PHANTRAMKM { get; set; }
    }
}