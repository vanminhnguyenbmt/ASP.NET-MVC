//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BanDongHo.Domain.DataContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHITIETSANPHAM
    {
        public string MASP { get; set; }
        public string MASIZE { get; set; }
        public string MAMAU { get; set; }
        public string MALOAISP { get; set; }
        public Nullable<int> SOLUONG { get; set; }
        public Nullable<decimal> DONGIA { get; set; }
    
        public virtual LOAISANPHAM LOAISANPHAM { get; set; }
        public virtual MAUSAC MAUSAC { get; set; }
        public virtual SIZE SIZE { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}