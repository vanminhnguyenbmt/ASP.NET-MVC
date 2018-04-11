using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Common
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
    }
}