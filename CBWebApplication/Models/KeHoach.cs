using System;
using System.ComponentModel.DataAnnotations;

namespace CBWebApplication.Models
{
    public class LoaiKeHoach
    {
        [Key]
        public short MaLoai { get; set; }
        public string SoTT { get; set; }
        public string TenLoai { get; set; }
        public string DonVi { get; set; }
    }
    public class KeHoach
    {
        [Key]
        public long ID { get; set; }
        public short MaLoai { get; set; }
        public string NhomKH { get; set; }
        public short KyKH { get; set; }
        public short NamKH { get; set; }
        public decimal YV { get; set; }
        public decimal HN { get; set; }
        public decimal VIN { get; set; }
        public decimal DN { get; set; }
        public decimal SG { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public class KeHoachView
    {
        [Key]
        public long ID { get; set; }
        public short MaLoai { get; set; }
        public string SoTT { get; set; }
        public string TenLoai { get; set; }
        public string DonVi { get; set; }
        public string NhomKH { get; set; }
        public short KyKH { get; set; }
        public short NamKH { get; set; }
        public decimal YV { get; set; }
        public decimal HN { get; set; }
        public decimal VIN { get; set; }
        public decimal DN { get; set; }
        public decimal SG { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public class ViewBcKeHoach
    {
        [Key]
        public string DvcbID { get; set; }
        public short? CongTacID { get; set; }
        public decimal? KMChinh { get; set; }
        public decimal? KMPhuTro { get; set; }        
        public decimal? TKMChinh { get; set; }
        public decimal? TKMPhuTro { get; set; }        
        public int? GioDon { get; set; }
    }
}
