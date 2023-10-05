using System;
using System.ComponentModel.DataAnnotations;

namespace CBWebApplication.Models
{
    public partial class VIKhuDoan
    {
        [Key]
        public long ID { get; set; }           
        public string KhuDoanID { get; set; }
        public string TinhChat { get; set; }
        public int? GaXP { get; set; }
        public string Tuyen { get; set; }
        public int? GaDT1 { get; set; }
        public string Tuyen1 { get; set; }
        public int? GaDT2 { get; set; }
        public string Tuyen2 { get; set; }
        public int? GaDT3 { get; set; }
        public string Tuyen3 { get; set; }
        public int? GaKT { get; set; }
        public string CacGa { get; set; }
        public decimal? Km { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class VINLDinhMuc
    {
        [Key]
        public long ID { get; set; }
        public string LoaiMayID { get; set; }
        public string KhuDoan { get; set; }
        public string LoaiTau { get; set; }
        public decimal? DinhMuc { get; set; }
        public string DonVi { get; set; }
        public decimal? DinhMucDon { get; set; }
        public decimal? TyLeDon { get; set; }
        public string DonViDon { get; set; }
        public DateTime NgayHL { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class VINLDDinhMuc
    {
        [Key]
        public long ID { get; set; }
        public string KhuDoan { get; set; }
        public string MayChinhID { get; set; }
        public string MayPhuID { get; set; }
        public int? MayChinhTL { get; set; }
        public decimal? MayChinhDM { get; set; }
        public int? MayPhuTL { get; set; }
        public decimal? MayPhuDM { get; set; }
        public decimal? Km { get; set; }
        public string DonVi { get; set; }       
        public DateTime NgayHL { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class VIHSTan
    {
        [Key]
        public long ID { get; set; }
        public string LoaiMayID { get; set; }
        public decimal? TanMin { get; set; }
        public decimal? TanMax { get; set; }
        public decimal? HeSo { get; set; }       
        public DateTime NgayHL { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class VIDMDinhMuc
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

    public partial class VIXuatDT
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
        public string sdb4 { get; set; }
        public string ten4 { get; set; }
        public string tau { get; set; }
        public string cty { get; set; }
        public short ctac { get; set; }
        public string ctacp { get; set; }
        public short tchat { get; set; }
        public string kdoan { get; set; }
        public decimal slbt { get; set; }
        public decimal sll1 { get; set; }
        public decimal sll2 { get; set; }
        public decimal slsd { get; set; }
        public decimal slbs { get; set; }
        public decimal sltt { get; set; }
        public decimal sltc { get; set; }
        public decimal slpt { get; set; }
        public string kho1 { get; set; }
        public string kho2 { get; set; }
        public string nlieu { get; set; }
        public bool thnl { get; set; }
        public bool thbt { get; set; }
        public bool phnl { get; set; }
        public bool phpt { get; set; }
        public short pdoan { get; set; }
        public string gaxp { get; set; }
        public DateTime daycb { get; set; }
        public string dgkb { get; set; }
        public decimal dgdm { get; set; }
        public decimal dgdn { get; set; }
        public decimal tgtnm { get; set; }
        public decimal tgtnn { get; set; }
        public decimal dgkm { get; set; }
        public decimal dgkn { get; set; }
        public decimal dgqd { get; set; }
        public decimal giqv { get; set; }
        public decimal gilh { get; set; }
        public decimal gidt { get; set; }
        public decimal dgxp { get; set; }
        public decimal dgdd { get; set; }
        public decimal dgcc { get; set; }
        public string tglg { get; set; }
        public decimal dnxp { get; set; }
        public decimal dndd { get; set; }
        public decimal dncc { get; set; }
        public decimal slrk { get; set; }
        public decimal kmch { get; set; }
        public decimal kmdw { get; set; }
        public decimal kmgh { get; set; }
        public decimal kmng { get; set; }
        public decimal kmdy { get; set; }
        public decimal tkch { get; set; }
        public decimal tkdw { get; set; }
        public decimal tkgh { get; set; }
        public decimal tkdy { get; set; }
        public decimal tkdd { get; set; }
        public string k1bt { get; set; }
        public string k2bt { get; set; }       
        public decimal l1d1 { get; set; }
        public decimal l2d1 { get; set; }
        public decimal l1d2 { get; set; }
        public decimal l2d2 { get; set; }
        public decimal l3d1 { get; set; }
        public decimal l3d2 { get; set; }
        public decimal t_d1 { get; set; }
        public decimal t_d2 { get; set; }
        public decimal t_d3 { get; set; }
        public decimal c_d1 { get; set; }
        public decimal c_d2 { get; set; }
        public decimal c_d3 { get; set; }       
        public decimal slrkm { get; set; }
        public decimal slrkn { get; set; }
        public decimal gioa { get; set; }
        public decimal giod { get; set; }
        public decimal dgnl { get; set; }       
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
    }
}
