using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class SGKhuDoan
    {
        [Key]
        public long ID { get; set; }
        public string KhuDoanID { get; set; }
        public int? GaXP { get; set; }
        public string Tuyen { get; set; }
        public int? GaDT1 { get; set; }
        public string Tuyen1 { get; set; }
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
    public partial class SGNLDinhMuc
    {
        [Key]
        public long ID { get; set; }
        public string LoaiMayID { get; set; }
        public string KhuDoan { get; set; }
        public string LoaiTau { get; set; }
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
    public partial class SGHSTan
    {
        [Key]
        public long ID { get; set; }
        public string LoaiMayID { get; set; }
        public decimal? TanMin { get; set; }
        public decimal? TanMax { get; set; }
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
    public partial class SGXuatDT
    {
        [Key]
        public long socb { get; set; }
        public string sohieucb { get; set; }
        public string lmay { get; set; }
        public string dmay { get; set; }
        public string madv { get; set; }
        public string maso { get; set; }
        public int doi { get; set; }
        public string sdb1 { get; set; }
        public string ten1 { get; set; }
        public string sdb2 { get; set; }
        public string ten2 { get; set; }
        public string sdb3 { get; set; }
        public string ten3 { get; set; }
        public string sdb4 { get; set; }
        public string ten4 { get; set; }
        public string sdb5 { get; set; }
        public string ten5 { get; set; }
        public string sdb6 { get; set; }
        public string ten6 { get; set; }
        public string tau { get; set; }
        public short ctac { get; set; }
        public string ctacp { get; set; }
        public short tchat { get; set; }
        public string kdoan { get; set; }
        public decimal slbt { get; set; }
        public decimal sllh { get; set; }
        public decimal slsd { get; set; }
        public decimal slbs { get; set; }
        public decimal sltt { get; set; }
        public decimal sltc { get; set; }
        public decimal ca3 { get; set; }
        public decimal hscb { get; set; }
        public decimal bunl { get; set; }
        public decimal lsl1 { get; set; }
        public decimal slpt { get; set; }
        public string nlanh { get; set; }
        public string nlieu { get; set; }
        public bool thnl { get; set; }
        public bool thbt { get; set; }
        public bool phnl { get; set; }
        public bool phpt { get; set; }
        public short pdoan { get; set; }
        public string gaxp { get; set; }
        public string gakt { get; set; }
        public DateTime daycb { get; set; }
        public string ngaylaptau { get; set; }
        public string galaptau { get; set; }
        public string tgltau { get; set; }
        public string glb { get; set; }
        public string gnm { get; set; }
        public string grk { get; set; }
        public string gvk { get; set; }
        public string ggm { get; set; }
        public string gxb { get; set; }
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
        public string dgbd { get; set; }      
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
        public decimal srcg { get; set; }       
        public decimal l_d1 { get; set; }
        public decimal l_d2 { get; set; }
        public decimal l_d3 { get; set; }
        public decimal t_d1 { get; set; }
        public decimal t_d2 { get; set; }
        public decimal t_d3 { get; set; }
        public decimal c_d1 { get; set; }
        public decimal c_d2 { get; set; }
        public decimal c_d3 { get; set; }
        public decimal slrkm { get; set; }
        public decimal slrkn { get; set; }
        public decimal tgtnm { get; set; }
        public decimal tgtnn { get; set; }
        public decimal bulo { get; set; }
        public decimal tylebulo { get; set; }
        public decimal dthicong { get; set; }
        public decimal bunbd { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
    }
}
