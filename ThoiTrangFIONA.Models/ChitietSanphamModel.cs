//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanSinhTo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChitietSanphamModel
    {
        public long PK_iChitietSanphamID { get; set; }
        public long FK_iSanphamID { get; set; }
        public long FK_iNguyenlieuID { get; set; }
        public double fSoluong { get; set; }
        public string sGhichu { get; set; }
    
        public virtual NguyenlieuModel tblNguyenlieu { get; set; }
        public virtual SanphamModel tblSanpham { get; set; }
    }
}
