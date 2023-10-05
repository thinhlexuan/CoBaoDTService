using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class DSNLDinhMuc
    {
        [Key]
        public long ID { get; set; }
        public string MaDV { get; set; }
        public string LoaiMayID { get; set; }
        public short? CongTacId { get; set; }         
        public string GhiChu { get; set; }
        public decimal? DinhMuc { get; set; }
        public string DonVi { get; set; }
        public DateTime NgayHL { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
}
