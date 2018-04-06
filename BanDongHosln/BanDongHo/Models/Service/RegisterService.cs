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
        const string ID_DEFAULT = "KH00001";
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

        public bool RegisterAccount(Register register)
        {
            string makh = "";
            BANDONGHOEntities db = new BANDONGHOEntities();
            /* Thêm Tài khoản */

            /* Thêm khách hàng */
            #region Tạo mã KH
            // lấy mã khách hàng lớn nhất hiện tại
            KHACHHANG kh = (from k in db.KHACHHANGs
                            orderby k.MAKH descending
                            select k).SingleOrDefault();
            if (kh == null)
            {
                makh = ID_DEFAULT;
            }
            else
            {
                //cắt 2 ký tự đầu
                int numberId;
                if (!Int32.TryParse(makh.Substring(2), out numberId))
                {
                    makh = ID_DEFAULT;
                }
                else
                {
                    numberId++;
                    string newId = "KH";
                    for (int i = 0; i < 5 - numberId.ToString().Length; i++)
                    {
                        newId += "0";
                    }
                    newId += numberId.ToString();
                    makh = newId;
                }
            }
            #endregion
            // tạo mới khách hàng
            KHACHHANG customer = new KHACHHANG { MAKH = makh, HOTEN = register.FirstName + register.LastName, DIACHI = "", SDT = register.Phone, GIOITINH = register.Sex };
            // Thêm khách hàng và db
            db.KHACHHANGs.Add(customer);
            db.SaveChanges();
            return true;
        }
    }
}