using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CBWebApplication.Models
{
    public partial class HeSoQdnl
    {
        [Key]
        public long ID { get; set; }
        public string MaDv { get; set; }       
        public int Thang { get; set; }        
        public int Nam { get; set; }
        public decimal HesoLit { get; set; }
        public decimal HesoKg { get; set; }
        public decimal NhietDo { get; set; }
    }
}
