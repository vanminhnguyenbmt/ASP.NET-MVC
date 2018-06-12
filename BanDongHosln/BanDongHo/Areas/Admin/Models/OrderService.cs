using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;
using System.Data.Entity.Migrations;

namespace BanDongHo.Areas.Admin.Models
{
    public class OrderService
    {
        BANDONGHOEntities db;

        public OrderService()
        {
            db = new BANDONGHOEntities();
        }
        public IEnumerable<DONHANG> getAllOrder()
        {          
            return db.DONHANGs.OrderByDescending(n => n.MADH);
        }

        public List<CHITIETDONHANG> LoadOrderDetail(int mahd)
        {
            return db.CHITIETDONHANGs.Where(n => n.MADH == mahd).ToList();
        }

        public List<SANPHAM> LoadAllProduct()
        {
            return db.SANPHAMs.ToList();
        }

        public int? TakeQuantityProduct(int masp)
        {
            return db.SANPHAMs.Where(n => n.MASP == masp).FirstOrDefault().SOLUONG;
        }

        public void UpdateCustomer(OrderViewModel orderViewModel)
        {
            using (var db = new BANDONGHOEntities())
            {
                var result = db.KHACHHANGs.Where(n => n.MAKH == orderViewModel.makh).FirstOrDefault();
                if(result != null)
                {
                    try
                    {
                        result.DIACHI = orderViewModel.diachi;
                        result.TENKH = orderViewModel.tennguoinhan;
                        result.SDT = orderViewModel.sodt;

                        db.KHACHHANGs.AddOrUpdate(result);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        public SANPHAM GetProductByID(int masp)
        {
            return db.SANPHAMs.Where(n => n.MASP == masp).FirstOrDefault();
        }

        public void UpdateOrder(OrderViewModel orderViewModel, double? totalMoney)
        {
            using (var db = new BANDONGHOEntities())
            {
                var result = db.DONHANGs.Where(n => n.MADH == orderViewModel.mahd).FirstOrDefault();
                if (result != null)
                {
                    try
                    {
                        result.TRANGTHAI = orderViewModel.tinhtrang;
                        result.DIACHIGIAO = orderViewModel.diachi;
                        result.SDT = orderViewModel.sodt;
                        result.NGAYDAT = DateTime.Parse(orderViewModel.ngaymua);
                        result.NGAYGIAO = DateTime.Parse(orderViewModel.ngaygiao);
                        result.TONGTIEN = totalMoney;

                        db.DONHANGs.AddOrUpdate(result);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        public void DeleteDetailOrder(OrderViewModel orderViewModel)
        {
            using (var db = new BANDONGHOEntities())
            {
                var result = db.CHITIETDONHANGs.Where(n => n.MADH == orderViewModel.mahd);
                if (result != null)
                {
                    try
                    {
                        foreach (var item in result)
                        {
                            db.CHITIETDONHANGs.Remove(item);
                        }
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        public void InsertDetailOrder(int mahd, int masp, int soluong)
        {
            using (var db = new BANDONGHOEntities())
            {
                CHITIETDONHANG chitietdonhang = new CHITIETDONHANG { MADH = mahd, MASP = masp, SOLUONG = soluong  };

                try
                {
                    db.CHITIETDONHANGs.Add(chitietdonhang);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public int HandleQuantityProduct(int masp, int soluong, bool kiemtra)
        {
            int? soluongtonkho = GetProductByID(masp).SOLUONG;
            if(kiemtra)
            {
                soluongtonkho += soluong;
            }
            else
            {
                soluongtonkho -= soluong;
            }
            return soluongtonkho.Value;
        }

        public void UpdateQuantityProduct(int masp, int soluong, bool kiemtra)
        {
            using (var db = new BANDONGHOEntities())
            {
                int soluongmoi = HandleQuantityProduct(masp, soluong, kiemtra);
                var result = db.SANPHAMs.Where(n => n.MASP == masp).FirstOrDefault();
                if (result != null)
                {
                    try
                    {
                        result.SOLUONG = soluongmoi;

                        db.SANPHAMs.AddOrUpdate(result);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
    }
}