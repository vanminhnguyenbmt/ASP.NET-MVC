using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BanDongHo.Areas.Admin.Models
{
    public class PromotionDetailViewModel
    {
        [Required(ErrorMessage = "")]
        public string MACTKM { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn mã khuyến mãi")]
        public string MAKM { get; set; }
        [Required(ErrorMessage = "vui lòng chọn mã sản phẩm")]
        public int MASP { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày tháng bắt đầu")]
        [DataType(DataType.DateTime,ErrorMessage ="")]
        public DateTime NGAYBATDAUKM { get; set; }
        [Required(ErrorMessage = "")]
        [DataType(DataType.DateTime, ErrorMessage = "Vui lòng chọn ngày tháng kết thúc")]
        public DateTime NGAYKETTHUCKM { get; set; }
        [Required(ErrorMessage = "vui lòng chọn phần trăm khuyến mãi")]
        public int PHANTRAMKM { get; set; }
    }
}