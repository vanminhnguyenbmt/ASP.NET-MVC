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
        [Required(ErrorMessage = "Vui lòng chọn ngày tháng bắt đầu")]
        [DataType(DataType.DateTime, ErrorMessage = "")]
        public DateTime NGAYBD { get; set; }
        [Required(ErrorMessage = "")]
        [DataType(DataType.DateTime, ErrorMessage = "Vui lòng chọn ngày tháng kết thúc")]
        public DateTime NGAYKT { get; set; }
    }
}