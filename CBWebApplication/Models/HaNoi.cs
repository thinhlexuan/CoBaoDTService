using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class HNKhuDoan
    {
        [Key]
        public long ID { get; set; }
        public string LoaiMayID { get; set; }
        public string CongTac { get; set; }
        public string KhuDoanID { get; set; }
        public string KhuDoanName { get; set; }
        public int? GaXP { get; set; }
        public string Tuyen { get; set; }
        public int? GaDT1 { get; set; }
        public string Tuyen1 { get; set; }
        public int? GaDT2 { get; set; }
        public string Tuyen2 { get; set; }
        public int? GaDT3 { get; set; }
        public string Tuyen3 { get; set; }
        public int? GaKT { get; set; }       
        public decimal? Km { get; set; }
        public DateTime NgayHL { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
        public string CacGa { get; set; }
    }
    public partial class HNNLDinhMuc
    {
        [Key]
        public long ID { get; set; }
        public string LoaiMayID { get; set; }
        public short STT { get; set; }       
        public decimal? TanMin { get; set; }
        public decimal? TanMax { get; set; }
        public decimal? DinhMuc { get; set; }
        public decimal? HeSo { get; set; }
        public decimal? HeSoC { get; set; }
        public string DonVi { get; set; }
        public DateTime NgayHL { get; set; }
        public string CongTac { get; set; }
        public string KhuDoan { get; set; }
        public string LoaiTau { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class HNNLPDinhMuc
    {
        [Key]
        public long ID { get; set; }
        public string LoaiMayID { get; set; }
        public short STT { get; set; }
        public decimal? DinhMuc { get; set; }
        public string DonVi { get; set; }
        public DateTime NgayHL { get; set; }
        public string CongTac { get; set; }
        public string DienGiai { get; set; }
        public string KhuDoan { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class HNDMDinhMuc
    {
        [Key]
        public long ID { get; set; }
        public string LoaiMayID { get; set; }
        public short? DauMoID { get; set; }
        public string DauMoName { get; set; }
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
    public partial class HNPhieuThuong
    {
        [Key]
        public long ID { get; set; }
        public string LoaiPhieu { get; set; }
        public string MacTau { get; set; }
        public int GaID { get; set; }
        public string GaName { get; set; }       
        public decimal? DonGia { get; set; }
        public string DonVi { get; set; }
        public DateTime NgayHL { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class HNXuatDT
    {
        [Key]
        public string socb { get; set; }        
        public string lmay { get; set; }
        public string dmay { get; set; }       
        public string sdb1 { get; set; }
        public string ten1 { get; set; }
        public string sdb2 { get; set; }
        public string ten2 { get; set; }
        public string sdb3 { get; set; }
        public string ten3 { get; set; }       
        public string mtau { get; set; }
        public short ctac { get; set; }
        public string ctacp { get; set; }
        public short tchat { get; set; }
        public string kdoan { get; set; }
        public decimal slbt { get; set; }
        public decimal sllh { get; set; }
        public decimal sldl_d { get; set; }
        public decimal sldl_m { get; set; }
        public decimal slbs { get; set; }
        public decimal sltt { get; set; }
        public decimal sltc { get; set; }
        public decimal slpt { get; set; }       
        public string nlanh { get; set; }
        public string nlieu { get; set; }
        public bool thnl { get; set; }
        public bool thbt { get; set; }
        public bool phnl { get; set; }
        public bool phpt { get; set; }
        public short pdoan { get; set; }
        public string gaxp { get; set; }
        public DateTime daycb { get; set; }      
        public decimal dgkb { get; set; }
        public decimal dgdm { get; set; }
        public decimal dgdn { get; set; }
        public decimal dgkm { get; set; }
        public decimal dgkn { get; set; }
        public decimal dgqd { get; set; }
        public decimal giqv { get; set; }
        public decimal gilh { get; set; }
        public decimal gidt { get; set; }
        public decimal dgxp { get; set; }
        public decimal dgdd { get; set; }       
        public decimal dgcc { get; set; }
        public decimal dnxp { get; set; }
        public decimal dndd { get; set; }
        public decimal dncc { get; set; }
        public decimal slrk { get; set; }
        public decimal kmch { get; set; }
        public decimal kmdw { get; set; }
        public decimal kmgh { get; set; }
        public decimal kmdy { get; set; }
        public decimal tkch { get; set; }
        public decimal tkdw { get; set; }
        public decimal tkgh { get; set; }
        public decimal tkdy { get; set; }
        public string l_nt { get; set; }
        public string l_bg { get; set; }
        public string l_gz { get; set; }
        public decimal l_dc { get; set; }
        public decimal l_tl { get; set; }
        public decimal l_gt { get; set; }
        public decimal t_nt { get; set; }
        public decimal t_bg { get; set; }
        public decimal t_gz { get; set; }
        public decimal t_dc { get; set; }
        public decimal t_tl { get; set; }
        public decimal t_gt { get; set; }
        public decimal c_nt { get; set; }
        public decimal c_bg { get; set; }
        public decimal c_gz { get; set; }
        public decimal c_dc { get; set; }
        public decimal c_tl { get; set; }
        public decimal c_gt { get; set; }
        public decimal slrkm { get; set; }
        public decimal slrkn { get; set; }
        public decimal tgtnm { get; set; }
        public decimal tgtnn { get; set; }
        public string maql { get; set; }
        public string q { get; set; }
        public string lldg { get; set; }
        public DateTime dayxp { get; set; }
        public string dtau { get; set; }
        public string mghep { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
    }
}
