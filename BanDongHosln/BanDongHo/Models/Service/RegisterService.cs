using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;
using System.Text.RegularExpressions;
using BanDongHo.Models.ViewModel;

namespace BanDongHo.Models.Service
{
    public class RegisterService : IRegisterSercive
    {
        const int KH_DEFAULT = 1;
        const int TK_DEFAULT = 1;
        public bool isExistAccount(string account)
        {
                // get TK => tk.TenDN == account
                // if tk ==null return true ? false;
                BANDONGHOEntities db = new BANDONGHOEntities();
                TAIKHOAN taikhoan = (from tk in db.TAIKHOANs
                                     where tk.TENDN.Equals(account)
                                     select tk).SingleOrDefault();
                if (taikhoan != null)
                {
                    return true;
                }
                return false;
        }

        public bool isPasswordAccount(string password)
        {
            return Regex.IsMatch(password, @"\w");
        }

        public void RegisterAccount(Register register)
        {
            int makh, matk;
            BANDONGHOEntities db = new BANDONGHOEntities();
            /* Thêm Tài khoản */
            TAIKHOAN tk = (from TK in db.TAIKHOANs
                            orderby TK.MATK descending
                            select TK).SingleOrDefault();
            if (tk == null)
            {
                matk = TK_DEFAULT;
            }
            else
            {
                int numberTK = tk.MATK;
                numberTK++;
                matk = numberTK;
            }

            /* Thêm khách hàng */
            #region Tạo mã KH
            // lấy mã khách hàng lớn nhất hiện tại
            KHACHHANG kh = (from k in db.KHACHHANGs
                            orderby k.MAKH descending
                            select k).SingleOrDefault();
            if (kh == null)
            {
                makh = KH_DEFAULT;
            }
            else
            {
                int numberkh = kh.MAKH;
                numberkh++;
                makh = numberkh;
            }
            #endregion
            // tạo mới khách hàng
            TAIKHOAN taikhoan = new TAIKHOAN { TENDN = register.Account, MATKHAU = register.Password, MALOAITK = "LK00002" };
            KHACHHANG customer = new KHACHHANG {  MATK = matk,TENKH = register.FirstName + register.LastName, DIACHI = "", SDT = register.Phone, GIOITINH = register.Sex, EMAIL = register.Email };
            // Thêm khách hàng và db
            db.TAIKHOANs.Add(taikhoan);
            db.KHACHHANGs.Add(customer);
            db.SaveChanges();
        }
    }
}