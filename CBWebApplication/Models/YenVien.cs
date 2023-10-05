using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class YVKhuDoan
    {
        [Key]
        public long ID { get; set; }
        public string LoaiMayID { get; set; }
        public string CongTac { get; set; }
        public string KhuDoanID { get; set; }        
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

    public partial class YVNLDinhMuc
    {
        [Key]
        public long ID { get; set; }
        public string LoaiMayID { get; set; }
        public string CongTac { get; set; }
        public string KhuDoan { get; set; }
        public decimal? TanMin { get; set; }
        public decimal? TanMax { get; set; }
        public decimal? DinhMuc { get; set; }
        public decimal? HeSo { get; set; }
        public string DonVi { get; set; }
        public DateTime NgayHL { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }

    public partial class YVNLPDinhMuc
    {
        [Key]
        public long ID { get; set; }
        public string LoaiMayID { get; set; }
        public string CongTac { get; set; }
        public string DienGiai { get; set; }
        public string KhuDoan { get; set; }        
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

    public partial class YVDMDinhMuc
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

    public partial class YVXuatDT
    {
        [Key]
        public string SOCB { get; set; }
        public string LMAY { get; set; }
        public string DMAY { get; set; }
        public string SDB1 { get; set; }
        public string TEN1 { get; set; }
        public string SDB2 { get; set; }
        public string TEN2 { get; set; }
        public string SDB3 { get; set; }
        public string TEN3 { get; set; }
        public string MTAU { get; set; }
        public short CTAC { get; set; }
        public string CTACP { get; set; }
        public short TCHAT { get; set; }
        public string MDOAN { get; set; }
        public string KDOAN { get; set; }
        public decimal SLBT { get; set; }
        public decimal SLLH { get; set; }
        public decimal SLDL_D { get; set; }
        public decimal SLDL_M { get; set; }
        public decimal SLBS { get; set; }
        public decimal SLTT { get; set; }
        public decimal SLTC { get; set; }
        public decimal SLPT { get; set; }
        public string NLANH { get; set; }
        public string NLIEU { get; set; }
        public bool THNL { get; set; }
        public bool THBT { get; set; }
        public bool PHNL { get; set; }
        public bool PHBT { get; set; }
        public short PDOAN { get; set; }       
        public string GAXP { get; set; }
        public DateTime DAYCB { get; set; }
        public decimal DGKB { get; set; }
        public decimal DGDM { get; set; }
        public decimal DGDN { get; set; }
        public decimal DGKM { get; set; }
        public decimal DGKN { get; set; }
        public decimal DGQD { get; set; }
        public decimal GIQV { get; set; }
        public decimal GILH { get; set; }
        public decimal GIDT { get; set; }
        public decimal DGXP { get; set; }
        public decimal DGDD { get; set; }
        public decimal DGCC { get; set; }
        public decimal DNXP { get; set; }
        public decimal DNDD { get; set; }
        public decimal DNCC { get; set; }
        public decimal SLRK { get; set; }
        public decimal KMCH { get; set; }
        public decimal KMDW { get; set; }
        public decimal KMGH { get; set; }
        public decimal KMDY { get; set; }
        public decimal TKCH { get; set; }
        public decimal TKDW { get; set; }
        public decimal TKGH { get; set; }
        public decimal TKDY { get; set; }
        public string L_TN { get; set; }
        public string L_BG { get; set; }
        public string L_GZ { get; set; }
        public decimal L_DC { get; set; }
        public decimal L_TL { get; set; }
        public decimal L_GT { get; set; }
        public decimal T_TN { get; set; }
        public decimal T_BG { get; set; }
        public decimal T_GZ { get; set; }
        public decimal T_DC { get; set; }
        public decimal T_TL { get; set; }
        public decimal T_GT { get; set; }
        public decimal C_TN { get; set; }
        public decimal C_BG { get; set; }
        public decimal C_GZ { get; set; }
        public decimal C_DC { get; set; }
        public decimal C_TL { get; set; }
        public decimal C_GT { get; set; }
        public decimal SLRKM { get; set; }
        public decimal SLRKN { get; set; }
        public decimal TGTNM { get; set; }
        public decimal TGTNN { get; set; }
        public string MAQL { get; set; }
        public string Q { get; set; }
        public string LLDG { get; set; }
        public string CTY { get; set; }
        public DateTime DAY_LT { get; set; }
        public string DTAU { get; set; }
        public string GA_LT { get; set; }       
    }
}
