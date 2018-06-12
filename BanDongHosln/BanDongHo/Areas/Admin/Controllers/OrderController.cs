using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Areas.Admin.Models;
using BanDongHo.Common;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Controllers
{
    
    public class OrderController : Controller
    {
        OrderService orderService = new OrderService();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
            OrderViewModel orderViewModel = new OrderViewModel();

            orderViewModel.ListProduct = orderService.LoadAllProduct();
            orderViewModel.ListOrder = orderService.getAllOrder().ToList();
            int totalRecord = orderViewModel.ListOrder.Count;
            orderViewModel.TotalPage = (int)(Math.Ceiling(((double)totalRecord / 10)));

            return View(orderViewModel);
        }

        [HttpGet]
        public String TakeListOrderLimit(int pageIndex)
        {
            int startPageIndex = (pageIndex - 1) * 10;

            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.ListOrder = orderService.getAllOrder().Skip(startPageIndex).Take(10).ToList();

            String html = "";
            foreach (var item in orderViewModel.ListOrder)
            {
                html += "<tr>";
                html += "<td data-tennguoinhan='" + item.KHACHHANG.TENKH + "'>" + item.KHACHHANG.TENKH + "</td>";
                html += "<td data-sodt='" + item.SDT + "'>" + item.SDT + "</td>";
                html += "<td data-diachi='" + item.DIACHIGIAO + "'>" + item.DIACHIGIAO + "</td>";
                html += "<td data-tinhtrang='" + item.TRANGTHAI + "'>" + item.TRANGTHAI + "</td>";
                html += "<td data-ngaymua='" + item.NGAYDAT + "'>" + item.NGAYDAT + "</td>";
                html += "<td data-ngaygiao='" + item.NGAYGIAO + "'>" + item.NGAYGIAO + "</td>";
                html += "<td data-tongtien='" + (int)item.TONGTIEN + "'>" + String.Format("{0:0,0}", item.TONGTIEN) + " VNĐ</td>";
                html += "<td data-id='" + item.MADH + "' data-idCus='" + item.MAKH + "'><a class='btn btn-success btn-capnhathoadon'>Cập nhật</a></td>";
                html += "</tr>";
            }
            return html;
        }

        [HttpGet]
        public String LoadOrderDetail(int id)
        {         
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.ListOrderDetail = orderService.LoadOrderDetail(id);

            String html = "";
            foreach (var item in orderViewModel.ListOrderDetail)
            {
                html += "<tr>";
                html += "<th>";
                html += "Tên sản phẩm : <input disabled style='margin:5px; padding:5px; width:60%' data-masp='" + item.MASP + "' name='mangsanpham[]' type="
                        + "text' value='" + item.SANPHAM.TENSP + "' />";
                html += "</th>";

                html += "<th>";
                html += "Số lượng : <input disabled data-masp='" + item.MASP + "' style='margin:5px; padding:5px; width:60%' name='mangsoluong[]' type='text' value='" + item.SOLUONG + "' />";
                html += "<a class='btn btn-danger btnxoachitiethoadon'>Xóa</a>";
                html += "</th>";
                html += "</tr>";
            }
            return html;
        }

        [HttpGet]
        public String UpdateOrder(OrderViewModel orderViewModel)
        {
            //xử lý số lượng đặt không được vượt quá tồn kho
            int count = orderViewModel.mangmasp.Count();
            bool kiemtra = false;
            String chuoithongbao = "";

            if(!orderViewModel.tinhtrang.Equals("đã hủy"))
            {
                for (int i = 0; i < count; i++)
                {
                    int masp = orderViewModel.mangmasp[i];
                    int soluong = orderViewModel.mangsoluong[i];
                    String tensp = orderViewModel.mangtensp[i];

                    int? soluongtonkho = orderService.TakeQuantityProduct(masp);

                    if (soluongtonkho < soluong)
                    {
                        chuoithongbao += "Quá số lượng tồn kho: "
                                + "Tên sản phẩm: '" + tensp + "' # Số lượng: '" + (soluongtonkho - soluong) + "'";
                        kiemtra = true;
                    }
                }
            }

            //xử lý tiếp khi thỏa mãn số lượng tồn kho
            if (!kiemtra)
            {
                //Cập nhập khách hàng trong hóa đơn
                orderService.UpdateCustomer(orderViewModel);

                //Tính lại tổng tiền hóa đơn kèm theo giá khuyến mãi nếu có
                double? totalMoney = 0;
                for (int i = 0; i < count; i++)
                {
                    int masp = orderViewModel.mangmasp[i];
                    int soluong = orderViewModel.mangsoluong[i];

                    //Lấy ra phần trăm khuyến mãi của sản phẩm nếu có
                    int phantramkm = BanDongHo.Models.Service.PromotionService.GetPromotion(masp);
                    SANPHAM sanpham = orderService.GetProductByID(masp);

                    totalMoney += soluong * sanpham.DONGIA * ((float)(100 - phantramkm) / 100);
                }

                //Tiến hành cập nhập hóa đơn và chi tiết hóa đơn
                if (orderViewModel.tinhtrang.Equals("chờ kiểm duyệt") || orderViewModel.tinhtrang.Equals("hoàn thành"))
                {
                    //Cập nhập thông tin hóa đơn
                    orderService.UpdateOrder(orderViewModel, totalMoney);
                    //Xóa hết chi tiết hóa đơn của hóa đơn hiện tại
                    orderService.DeleteDetailOrder(orderViewModel);

                    //Lấy ra tất cả chi tiết hóa đơn có trong hóa đơn để thêm vào CSDL
                    for (int i = 0; i < count; i++)
                    {
                        int masp = orderViewModel.mangmasp[i];
                        int soluong = orderViewModel.mangsoluong[i];
                        //Thêm vào những chi tiết hóa đơn có trong hóa đơn
                        orderService.InsertDetailOrder(orderViewModel.mahd, masp, soluong);
                    }
                    chuoithongbao = "Cập nhập thành công";
                }
                else if (orderViewModel.tinhtrang.Equals("đã hủy"))
                {
                    //Cập nhập thông tin hóa đơn
                    orderService.UpdateOrder(orderViewModel, totalMoney);

                    for (int i = 0; i < count; i++)
                    {
                        int masp = orderViewModel.mangmasp[i];
                        int soluong = orderViewModel.mangsoluong[i];

                        //Cộng lại số lượng sản phẩm đã mua vào số lượng tồn kho
                        orderService.UpdateQuantityProduct(masp, soluong, true);
                    }
                    chuoithongbao = "Cập nhập thành công";
                }
                else if (orderViewModel.tinhtrang.Equals("đang giao hàng"))
                {
                    //Cập nhập thông tin hóa đơn
                    orderService.UpdateOrder(orderViewModel, totalMoney);

                    for (int i = 0; i < count; i++)
                    {
                        int masp = orderViewModel.mangmasp[i];
                        int soluong = orderViewModel.mangsoluong[i];

                        //Trừ số lượng sản phẩm tồn kho
                        orderService.UpdateQuantityProduct(masp, soluong, false);
                    }
                    chuoithongbao = "Cập nhập thành công";
                }
            }

            return chuoithongbao;
        }
    }
}