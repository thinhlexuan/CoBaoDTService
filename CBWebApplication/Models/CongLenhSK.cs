using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class CongLenhSK
    {
        [Key]
        public long ID { get; set; }
        public string TuyenID { get; set; }
        public string KhuDoan { get; set; }
        public decimal? DocHC { get; set; }
        public decimal? D4H { get; set; }
        public decimal? D5H { get; set; }
        public decimal? D8E { get; set; }
        public decimal? D9E { get; set; }
        public decimal? D10H { get; set; }
        public decimal? D11H { get; set; }
        public decimal? D12E { get; set; }
        public decimal? D13E { get; set; }
        public decimal? D14Er { get; set; }
        public decimal? D18E { get; set; }
        public decimal? D19E { get; set; }
        public decimal? D19Er { get; set; }
        public decimal? D20E { get; set; }
        public string GhiChu { get; set; }
        public int? GaXP { get; set; }
        public decimal? KmXP { get; set; }
        public int? GaKT { get; set; }
        public decimal? KmKT { get; set; }        
        public DateTime NgayHL { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }   
}
