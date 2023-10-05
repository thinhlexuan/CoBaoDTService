using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class BangNhatKy
    {
        [Key]        
        public string TenBang { get; set; }        
    }
    public partial class NhatKy
    {
        [Key]
        public long ID { get; set; }
        public string TenBang { get; set; }
        public string NoiDung { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
    }
   
}
