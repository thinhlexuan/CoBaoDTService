using System.ComponentModel.DataAnnotations;

namespace CBWebApplication.Models
{
    public class CapSCCB
    {
        [Key]
        public string DauMayID { get; set; }
        public string LoaiMayID { get; set; }
        public string DvdmID { get; set; }
        public int DungKB { get; set; }
        public int DungTD { get; set; }
        public int DungKG { get; set; }
        public int Don { get; set; }
        public decimal KMChinh { get; set; }
        public decimal KMDon { get; set; }
        public decimal KMGhep { get; set; }
        public decimal KMDay { get; set; }
    }
}
