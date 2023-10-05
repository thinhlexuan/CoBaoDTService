using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class DNKhuDoan
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

    public partial class DNNLDinhMuc
    {
        [Key]
        public long ID { get; set; }
        public string KhuDoan { get; set; }
        public string LoaiMay { get; set; }
        public string LoaiTau { get; set; }
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
    public partial class DNNLDinhMucTemp
    {
        [Key]
        public long ID { get; set; }
        public string KhuDoan { get; set; }
        public string LoaiMay { get; set; }
        public string LoaiTau { get; set; }
        public decimal? TanMin { get; set; }
        public decimal? TanMax { get; set; }
        public decimal? DinhMuc { get; set; }

    }
    public partial class DNXuatDT
    {
        [Key]
        public string socb { get; set; }
        public string madm { get; set; }
        public short mact { get; set; }
        public string ramxe { get; set; }
        public string doimay { get; set; }
        public string cv { get; set; }
        public string makd { get; set; }
        public string vr { get; set; }
        public DateTime ngaydi { get; set; }
        public string giodi { get; set; }
        public string mactau { get; set; }
        public short matc { get; set; }        
        public string cb_c { get; set; }
        public string chinh { get; set; }
        public string cb_g { get; set; }
        public string ghep { get; set; }
        public string cb_d1 { get; set; }
        public string day1 { get; set; }
        public string cb_d2 { get; set; }
        public string day2 { get; set; }
        public string kep { get; set; }
        public string don2 { get; set; }
        public decimal solanrk { get; set; }
        public decimal km { get; set; }
        public decimal tkmdm { get; set; }
        public decimal toaxe { get; set; }
        public decimal kmtoaxe { get; set; }
        public decimal tan { get; set; }
        public decimal tkm { get; set; }
        public decimal tanqd { get; set; }
        public decimal tkmqd { get; set; }
        public decimal donxp { get; set; }
        public decimal dondd { get; set; }
        public decimal doncc { get; set; }
        public decimal dungdd { get; set; }
        public decimal dungddnl { get; set; }
        public decimal dungxp { get; set; }
        public decimal dungxpnl { get; set; }
        public decimal dungcc { get; set; }
        public decimal dungccnl { get; set; }
        public decimal dscdm { get; set; }
        public decimal dscdn { get; set; }
        public decimal lh { get; set; }
        public decimal qvlh { get; set; }
        public decimal dungdn { get; set; }
        public decimal dungdm { get; set; }
        public decimal dctlmdm { get; set; }
        public decimal dctlmdmnl { get; set; }
        public decimal dctlmdn { get; set; }
        public decimal dctlmdnnl { get; set; }
        public decimal nldung { get; set; }
        public decimal nldon { get; set; }
        public decimal nltanso { get; set; }
        public decimal nlrvkho { get; set; }
        public decimal nltc { get; set; }
        public decimal nltt { get; set; }
        public string cty { get; set; }
        public decimal gogio { get; set; }
        public decimal nlrc { get; set; }
        public string gaxp { get; set; }
        public DateTime ngayxp { get; set; }
        public string mkep { get; set; }
        public decimal tmkep { get; set; }
        public decimal tkmnguoi { get; set; }
        public decimal nltt15 { get; set; }
        public string dcham { get; set; }
        public decimal nldcham { get; set; }
        public string makdphu { get; set; }       
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
    }
}
