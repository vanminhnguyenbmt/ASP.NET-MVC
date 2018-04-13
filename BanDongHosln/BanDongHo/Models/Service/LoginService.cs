using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;
using BanDongHo.Models.ViewModel;
using BanDongHo.Common;

namespace BanDongHo.Models.Service
{
    public class LoginService
    {
        BANDONGHOEntities db = null;
        public LoginService()
        {
            db = new BANDONGHOEntities();
        }

        /// <summary>
        /// Service lấy username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public TAIKHOAN GetById(string userName)
        {
            return db.TAIKHOANs.SingleOrDefault(n => n.TENDN == userName);
        }

        /// <summary>
        /// Service kiểm tra username và password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public int Login(string userName, string passWord)
        {
            var result = db.TAIKHOANs.SingleOrDefault(n => n.TENDN == userName);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if (result.TRANGTHAI == false)
                {
                    return -1;
                }
                else
                {
                    if (result.MATKHAU == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }
    }
}