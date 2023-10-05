using CBWebApplication.Context;
using CBWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapSCCBsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public CapSCCBsController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetCapSCCB")]
        public async Task<ActionResult<IEnumerable<CapSCCB>>> GetCapSCCB(string madv, string loaimayid, string daumayid, DateTime ngaybd, DateTime ngaykt)
        {
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        where cb.NgayCB >= ngaybd && cb.NgayCB < ngaykt
                        select new
                        {
                            DauMayID = (string)cb.DauMayID,
                            LoaiMayID = (string)cb.LoaiMayID,
                            DvdmID = (string)cb.DvdmID,
                            DungKB = (int)dt.DungKB,
                            DungTD = (int)(ct.DungDM + ct.DungDN + ct.DungKhoDM + ct.DungKhoDN),
                            DungKG = (int)(ct.DungXP + ct.DungDD + ct.DungQD + ct.DungKT),
                            Don = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                            KMChinh = (decimal)ct.KMChinh,
                            KMDon = (decimal)ct.KMDon,
                            KMGhep = (decimal)ct.KMGhep,
                            KMDay = (decimal)ct.KMDay,
                        };
            if (madv != "TCT")
                query = query.Where(x => x.DvdmID == madv);
            if (loaimayid != "ALL")
                query = query.Where(x => x.LoaiMayID == loaimayid);
            if (daumayid != "ALL")
                query = query.Where(x => x.DauMayID == daumayid);
            return await (from x in query
                          group x by new { x.DauMayID, x.LoaiMayID, x.DvdmID } into g
                          select new CapSCCB
                          {
                              DauMayID = g.Key.DauMayID,
                              LoaiMayID = g.Key.LoaiMayID,
                              DvdmID = g.Key.DvdmID,
                              DungKB = g.Sum(x => x.DungKB),
                              DungTD = g.Sum(x => x.DungTD),
                              DungKG = g.Sum(x => x.DungKG),
                              Don = g.Sum(x => x.Don),
                              KMChinh = g.Sum(x => x.KMChinh),
                              KMDon = g.Sum(x => x.KMDon),
                              KMGhep = g.Sum(x => x.KMGhep),
                              KMDay = g.Sum(x => x.KMDay)
                          }).ToListAsync();
        }
    }
}
