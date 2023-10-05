using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class LyTrinh
    {
        [Key]
        public long ID { get; set; }
        public string TuyenID { get; set; }
        public string TuyenName { get; set; }       
        public int? GaID { get; set; }
        public string TenGa { get; set; }
        public decimal? Km { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class DmLyTrinh
    {
        [Key]
        public string TuyenId { get; set; }
        public string TuyenName { get; set; }
        public int? GaDiId { get; set; }
        public string GaDiName { get; set; }
        public decimal? GaDiKM { get; set; }
        public int? GaDenId { get; set; }
        public string GaDenName { get; set; }
        public decimal? GaDenKM { get; set; }
        public string Chieu { get; set; }
    }
}
