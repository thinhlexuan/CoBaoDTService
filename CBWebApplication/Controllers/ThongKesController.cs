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
    public class ThongKesController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public ThongKesController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetDauMay")]
        public async Task<ActionResult<IEnumerable<ThongKeDM>>> GetDauMay(string DonVi)
        {
            var query = (from dm in _context.DMDauMay select dm);
            if (!string.IsNullOrWhiteSpace(DonVi))
                query = query.Where(x => x.MaCtquanLy.Contains(DonVi));
            await query.ToListAsync();

            return await (from dm in query
                          join dv in _context.DonViDM on dm.MaCtquanLy equals dv.MaDV
                          group dm by new { dm.DauMaySo, dm.PhanLoai, dm.MaCtquanLy, dv.TenDV } into g
                          select new ThongKeDM
                          {
                              DauMayID = g.Key.DauMaySo,
                              LoaiMayID = g.Key.PhanLoai,
                              DonViID = g.Key.MaCtquanLy,
                              DonViName = g.Key.TenDV
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetSoLieu")]
        public async Task<ActionResult<IEnumerable<ThongKeSL>>> GetSoLieu(string DonVi, DateTime NgayBD, DateTime NgayKT)
        {
            DateTime _ngayKT = NgayKT.AddDays(1);
            var query = (from cb in _context.CoBao where cb.NhanMay >= NgayBD && cb.NhanMay < _ngayKT select cb);
            if (!string.IsNullOrWhiteSpace(DonVi))
                query = query.Where(x => x.DvdmID.Contains(DonVi));
            await query.ToListAsync();
            return await (from cb in query
                          join ct in _context.DoanThongCT on cb.CoBaoID equals ct.DoanThongID
                          group ct by new { cb.DauMayID,cb.LoaiMayID, cb.NhanMay } into g
                          select new ThongKeSL
                          {
                              DauMayID = g.Key.DauMayID,
                              LoaiMayID = g.Key.LoaiMayID,
                              NhanMay = g.Key.NhanMay,
                              GioDM = g.Sum(x => x.QuayVong + x.DungDM + x.DungDN + x.DungKhoDM + x.DungKhoDN),
                              KMChinh = g.Sum(x => x.KMChinh),
                              TKMChinh = g.Sum(x => x.TKMChinh),
                              NLTieuThu = g.Sum(x => x.TieuThu)
                          }).ToListAsync();
        }

    }
}
