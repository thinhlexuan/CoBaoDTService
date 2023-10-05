using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class ThongKeDM
    {
        [Key]
        public string DauMayID { get; set; }
        public string LoaiMayID { get; set; }
        public string DonViID { get; set; }
        public string DonViName { get; set; }
    }

    public partial class ThongKeSL
    {
        [Key]
        public string DauMayID { get; set; }
        public string LoaiMayID { get; set; }
        public DateTime NhanMay { get; set; }
        public int GioDM { get; set; }
        public decimal KMChinh { get; set; }
        public decimal TKMChinh { get; set; }
        public decimal NLTieuThu { get; set; }
    }
}
