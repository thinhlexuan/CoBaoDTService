using System;
using System.ComponentModel.DataAnnotations;

namespace CBWebApplication.Models
{
    public partial class DauMay
    {
        [Key]
        public int ID { get; set; }
        public string DauMayID { get; set; }
        public string LoaiMayID { get; set; }
        public string MaCTSoHuu { get; set; }
        public string MaCTQuanLy { get; set; }
        public DateTime? NgayHL { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class ViewDauMay
    {
        [Key]
        public int ID { get; set; }
        public string DauMayID { get; set; }
        public string LoaiMayID { get; set; }
        public string MaCTSoHuu { get; set; }
        public string TenCTSoHuu { get; set; }
        public string MaCTQuanLy { get; set; }
        public string TenCTQuanLy { get; set; }
        public DateTime? NgayHL { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
}
