using CBWebApplication.Context;
using CBWebApplication.Models;
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
    public class DanhMucsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public DanhMucsController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetCongTac")]
        public async Task<ActionResult<IEnumerable<CongTac>>> GetCongTac()
        {
            return await _context.CongTac.ToListAsync();
        }

        [HttpGet]
        [Route("GetLoaiTau")]
        public async Task<ActionResult<IEnumerable<LoaiTau>>> GetLoaiTau()
        {
            return await _context.LoaiTau.ToListAsync();
        }

        [HttpGet]
        [Route("GetDMMacTau")]
        public async Task<ActionResult<IEnumerable<DMMacTau>>> GetDMMacTau()
        {  
            return await (from x in _context.CoBaoCT
                          group x by new { x.MacTauID, x.CongTacID, x.CongTacName, x.CongTyID,x.CongTyName,x.TuyenID,x.TuyenName } into g
                          select new DMMacTau
                          {
                              MacTauID = g.Key.MacTauID,
                              LoaiTauID = g.Key.CongTacID,
                              LoaiTauName = g.Key.CongTacName,
                              CongTyID = g.Key.CongTyID,
                              CongTyName=g.Key.CongTyName,
                              TuyenID=g.Key.TuyenID,
                              TuyenName=g.Key.TuyenName
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetTinhChat")]
        public async Task<ActionResult<IEnumerable<TinhChat>>> GetTinhChat()
        {
            return await _context.TinhChat.ToListAsync();
        }
        [HttpGet]
        [Route("GetDmdonVi")]
        public async Task<ActionResult<IEnumerable<DmdonVi>>> GetDmdonVi()
        {
            return await _context.DmdonVi.ToListAsync();
        }
        [HttpGet]
        [Route("GetCongTy")]
        public async Task<ActionResult<IEnumerable<CongTy>>> GetCongTy()
        {
            return await _context.CongTy.ToListAsync();
        }

        [HttpGet]
        [Route("GetDmdonViByDVQL")]
        public async Task<ActionResult<IEnumerable<DmdonVi>>> GetDmdonViByDVQL(string dvQL)
        {
            return await _context.DmdonVi.Where(x => x.Dvql == dvQL).ToListAsync();
        }

        [HttpGet]
        [Route("GetDonViDM")]
        public async Task<ActionResult<IEnumerable<DonViDM>>> GetDonViDM()
        {
            return await _context.DonViDM.ToListAsync();
        }
        [HttpGet]
        [Route("GetDonViKT")]
        public async Task<ActionResult<IEnumerable<DonViKT>>> GetDonViKT()
        {
            return await _context.DonViKT.ToListAsync();
        }

        [HttpGet]
        [Route("GetLoaiMay")]
        public async Task<ActionResult<IEnumerable<LoaiMay>>> GetLoaiMay()
        {
            return await _context.LoaiMay.ToListAsync();
        }

        [HttpGet]
        [Route("GetDauMay")]
        public async Task<ActionResult<IEnumerable<DMDauMay>>> GetDauMay()
        {
            return await _context.DMDauMay.ToListAsync();
        }

        [HttpGet]
        [Route("GetDMTaiXe")]
        public async Task<ActionResult<IEnumerable<DMTaiXe>>> GetDMTaiXe(string MaDV)
        {
            var query = (from nv in _context.CoBao                         
                         select new DMTaiXe
                         {
                             TaiXeID = nv.TaiXe1ID,
                             TaiXeName =nv.TaiXe1Name,
                             DonViID = nv.DvcbID
                         });
            if (MaDV != "TCT")
            {
                if (MaDV=="YV")
                    query = query.Where(x => x.DonViID == MaDV || x.DonViID=="HN");
                else if (MaDV == "DN")
                    query = query.Where(x => x.DonViID == MaDV || x.DonViID == "SG");
                else
                query = query.Where(x => x.DonViID == MaDV);
            }

            return await (from nv in query
                          join dv in _context.DmdonVi on nv.DonViID equals dv.MaDv
                          group nv by new { nv.TaiXeID, nv.TaiXeName, nv.DonViID, dv.TenDv } into g
                          select new DMTaiXe
                          {
                              TaiXeID = g.Key.TaiXeID,
                              TaiXeName = g.Key.TaiXeName,
                              DonViID = g.Key.DonViID,
                              DonViName=g.Key.TenDv
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetDMPhoXe")]
        public async Task<ActionResult<IEnumerable<DMPhoXe>>> GetDMPhoXe(string MaDV)
        {
            var query = (from nv in _context.CoBao                        
                         select new DMPhoXe
                        {
                            PhoXeID = nv.PhoXe1ID,
                            PhoXeName = nv.PhoXe1Name,
                            DonViID = nv.DvcbID
                        });
            if (MaDV != "TCT")
            {
                if (MaDV == "YV")
                    query = query.Where(x => x.DonViID == MaDV || x.DonViID == "HN");
                else if (MaDV == "DN")
                    query = query.Where(x => x.DonViID == MaDV || x.DonViID == "SG");
                else
                    query = query.Where(x => x.DonViID == MaDV);
            }

            return await (from nv in query
                          join dv in _context.DmdonVi on nv.DonViID equals dv.MaDv
                          group nv by new { nv.PhoXeID, nv.PhoXeName, nv.DonViID, dv.TenDv } into g
                          select new DMPhoXe
                          {
                              PhoXeID = g.Key.PhoXeID,
                              PhoXeName = g.Key.PhoXeName,
                              DonViID = g.Key.DonViID,
                              DonViName = g.Key.TenDv
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetTuyen")]
        public async Task<ActionResult<IEnumerable<Tuyen>>> GetTuyen()
        {
            return await _context.Tuyen.ToListAsync();
        }

        [HttpGet]
        [Route("GetTuyenMap")]
        public async Task<ActionResult<IEnumerable<TuyenMap>>> GetTuyenMap()
        {
            return await _context.TuyenMap.ToListAsync();
        }

        [HttpGet]
        [Route("GetGa")]
        public async Task<ActionResult<IEnumerable<DMGa>>> GetGa()
        {
            return await _context.DMGa.ToListAsync();
        }

        [HttpGet]
        [Route("GetDmtramNhienLieu")]
        public async Task<ActionResult<IEnumerable<DmtramNhienLieu>>> GetDmtramNhienLieu()
        {
            return await _context.DmtramNhienLieu.ToListAsync();
        }
        [HttpGet]
        [Route("GetDMLoaiDauMo")]
        public async Task<ActionResult<IEnumerable<DMLoaiDauMo>>> GetDMLoaiDauMo()
        {
            return await _context.DMLoaiDauMo.ToListAsync();
        }
        [HttpGet]
        [Route("GetNhanVien")]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetNhanVien()
        {
            return await _context.NhanVien.ToListAsync();
        }
        [HttpGet]
        [Route("GetBangNhatKy")]
        public async Task<ActionResult<IEnumerable<BangNhatKy>>> GetBangNhatKy()
        {
            return await (from x in _context.NhatKy
                          group x by new { x.TenBang } into g
                          select new BangNhatKy
                          {
                              TenBang = g.Key.TenBang
                          }).ToListAsync();
        }
       
        [HttpGet]
        [Route("GetNhatKy")]
        public async Task<ActionResult<IEnumerable<NhatKy>>> GetNhatKy(DateTime ngayBD,DateTime ngayKT,string tenBang,string tenNV)
        {
            var query = from nk in _context.NhatKy
                        where nk.Createddate >= ngayBD && nk.Createddate < ngayKT
                        select nk;
            if (tenBang != "ALL")
                query = query.Where(x => x.TenBang == tenBang);
            if (!string.IsNullOrWhiteSpace(tenNV))
                query = query.Where(x => x.CreatedName.Contains(tenNV));
            return await query.ToListAsync();
        }
        [HttpGet]
        [Route("GetPhienBan")]
        public async Task<ActionResult<IEnumerable<PhienBan>>> GetPhienBan()
        {
            return await _context.PhienBan.ToListAsync();
        }
    }
}
