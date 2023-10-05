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
    public class DuongSatsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;
        public DuongSatsController(CoBaoDTContext context)
        {
            _context = context;
        }

        #region NLDinhMuc      
      
        [HttpGet]
        [Route("DSGetNLDinhMucOBJ")]
        public async Task<ActionResult<DSNLDinhMuc>> DSGetNLDinhMucOBJ(DateTime NgayHL, string LoaiMay, short CongTac, string MaDV,string GhiChu)
        {
            var query = (from item in _context.DS_NLDinhMuc
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay && item.CongTacId == CongTac && item.MaDV==MaDV
                         orderby item.NgayHL descending
                         select item).Distinct();
            if (string.IsNullOrWhiteSpace(GhiChu))
                query = query.Where(x => x.GhiChu.Contains(GhiChu));
            var obj = await query.OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
            if (obj != null)
            {              
                return obj;
            }           
            return null;
        }

        [HttpGet]
        [Route("DSGetNLDinhMuc")]
        public async Task<ActionResult<IEnumerable<DSNLDinhMuc>>> DSGetNLDinhMuc(DateTime NgayHL, string LoaiMay, short CongTac, string MaDV)
        {
            var query = (from item in _context.DS_NLDinhMuc where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (CongTac>0)
                query = query.Where(x => x.CongTacId==CongTac);
            if (MaDV!="TCT")
                query = query.Where(x => x.MaDV==MaDV);
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("DSPutNLDinhMuc")]
        public async Task<ActionResult<DSNLDinhMuc>> DSPutNLDinhMuc(long id, DSNLDinhMuc dSNLDinhMuc)
        {
            dSNLDinhMuc.Modifydate = DateTime.Now;
            if (id != dSNLDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(dSNLDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return dSNLDinhMuc;
        }

        [HttpPost]
        [Route("DSPostNLDinhMuc")]
        public async Task<ActionResult<DSNLDinhMuc>> DSPostNLDinhMuc(DSNLDinhMuc dSNLDinhMuc)
        {
            dSNLDinhMuc.Createddate = DateTime.Now;
            dSNLDinhMuc.Modifydate = dSNLDinhMuc.Createddate;
            var query = _context.DS_NLDinhMuc.Where(x => x.LoaiMayID == dSNLDinhMuc.LoaiMayID && x.CongTacId == dSNLDinhMuc.CongTacId
            && x.MaDV == dSNLDinhMuc.MaDV && x.DinhMuc == dSNLDinhMuc.DinhMuc && x.NgayHL == dSNLDinhMuc.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.DS_NLDinhMuc.Add(dSNLDinhMuc);
            await _context.SaveChangesAsync();

            return dSNLDinhMuc;
        }

        [HttpDelete]
        [Route("DSDeleteNLDinhMuc")]
        public async Task<ActionResult<DSNLDinhMuc>> DSDeleteNLDinhMuc(long id)
        {
            var dSNLDinhMuc = _context.DS_NLDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (dSNLDinhMuc == null)
            {
                return NotFound();
            }
            _context.DS_NLDinhMuc.Remove(dSNLDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "DS_NLDinhMuc";
            nk.NoiDung = "Xóa NL định mức: " + dSNLDinhMuc.MaDV + ". Loại máy: " + dSNLDinhMuc.LoaiMayID+". Công tác: "+ dSNLDinhMuc.CongTacId;
            nk.Createddate = DateTime.Now;
            nk.Createdby = dSNLDinhMuc.Modifyby;
            nk.CreatedName = dSNLDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return dSNLDinhMuc;
        }
        #endregion
    }
}
