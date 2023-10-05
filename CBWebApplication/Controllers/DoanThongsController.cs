using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBWebApplication.Context;
using CBWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CBWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoanThongsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public DoanThongsController(CoBaoDTContext context)
        {
            _context = context;
        }        
        // GET: api/DoanThongs/Object
        [HttpGet]
        [Route("GetDoanThongView")]
        public async Task<ActionResult<IEnumerable<DoanThongView>>> GetDoanThongView(int ThangDT, int NamDT, string LoaiMay,string DonVi, String DauMay, string SoCB, string TaiXe,string MacTau)
        {
            var query = from dt in _context.View_DoanThong
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        where dt.ThangDT == ThangDT && dt.NamDT == NamDT
                        select dt;
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if(DonVi != "TCT")
                query = query.Where(x => x.DvcbID == DonVi);
            if (!string.IsNullOrWhiteSpace(DauMay))
                query = query.Where(x => x.DauMayID.Contains(DauMay));
            if (!string.IsNullOrWhiteSpace(SoCB))
                query = query.Where(x => x.SoCB.Contains(SoCB));
            if (!string.IsNullOrWhiteSpace(TaiXe))
                query = query.Where(x => x.TaiXe1ID.Contains(TaiXe) || x.PhoXe1ID.Contains(TaiXe) || x.TaiXe2ID.Contains(TaiXe) || x.PhoXe2ID.Contains(TaiXe) || x.TaiXe3ID.Contains(TaiXe) || x.PhoXe3ID.Contains(TaiXe));
            //await query.ToListAsync();
            if (!string.IsNullOrWhiteSpace(MacTau))
            {
                return await (from dt in query
                              join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                              where ct.MacTauID.Contains(MacTau)
                              select dt).ToListAsync();
            }
            else
                return await query.ToListAsync();
        }

        [HttpGet]
        [Route("GetTTDoanThong")]
        public async Task<ActionResult<IEnumerable<DoanThongView>>> GetTTDoanThong(DateTime NgayBD, DateTime NgayKT, string LoaiMay, string DonVi, String DauMay, string SoCB, string TaiXe, string MacTau)
        {
            DateTime _ngayKT = NgayKT.AddDays(1);
            var query = from dt in _context.View_DoanThong
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        where dt.NgayCB >= NgayBD && dt.NgayCB < _ngayKT
                        select dt;
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (DonVi != "TCT")
                query = query.Where(x => x.DvcbID == DonVi);
            if (!string.IsNullOrWhiteSpace(DauMay))
                query = query.Where(x => x.DauMayID.Contains(DauMay));
            if (!string.IsNullOrWhiteSpace(SoCB))
                query = query.Where(x => x.SoCB.Contains(SoCB));
            if (!string.IsNullOrWhiteSpace(TaiXe))
                query = query.Where(x => x.TaiXe1ID.Contains(TaiXe) || x.PhoXe1ID.Contains(TaiXe) || x.TaiXe2ID.Contains(TaiXe) || x.PhoXe2ID.Contains(TaiXe) || x.TaiXe3ID.Contains(TaiXe) || x.PhoXe3ID.Contains(TaiXe));
            //await query.ToListAsync();
            if (!string.IsNullOrWhiteSpace(MacTau))
            {
                return await (from dt in query
                              join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                              where ct.MacTauID.Contains(MacTau)
                              select dt).ToListAsync();
            }
            else
                return await query.ToListAsync();
        }

        [HttpGet]
        [Route("GetDoanThongViewID")]
        public async Task<ActionResult<DoanThongView>> GetDoanThongViewID(long id)
        {
            var doanThong = await _context.View_DoanThong.FindAsync(id);

            if (doanThong == null)
            {
                return NotFound();
            }            
            return doanThong;
        }

        [HttpGet]
        [Route("GetDoanThongID")]
        public async Task<ActionResult<DoanThong>> GetByDoanThongID(long id)
        {
            var doanThong = await _context.DoanThong.FindAsync(id);
            if (doanThong == null)
            {
                return NotFound();
            }           
            return doanThong;
        }

        [HttpGet]
        [Route("GetDoanThongCT")]
        public async Task<ActionResult<IEnumerable<DoanThongCT>>> GetDoanThongCT(long id)
        {
            var listdoanthongct = await _context.DoanThongCT.Where(x => x.DoanThongID == id).OrderBy(x=>x.NgayXP).ToListAsync();
            if (listdoanthongct == null)
            {
                return NotFound();
            }
            return listdoanthongct;
        }

        [HttpGet]
        [Route("GetDoanThongDM")]
        public async Task<ActionResult<IEnumerable<DoanThongDM>>> GetDoanThongDM(long id)
        {
            var listdoanthongdm = await _context.DoanThongDM.Where(x => x.DoanThongID == id).ToListAsync();
            if (listdoanthongdm == null)
            {
                return NotFound();
            }
            return listdoanthongdm;
        }

        [HttpGet]
        [Route("GetDoanThongCTID")]
        public async Task<ActionResult<DoanThongCT>> GetDoanThongCTID(long id)
        {
            var doanThongct = await _context.DoanThongCT.FindAsync(id);
            if (doanThongct == null)
            {
                return NotFound();
            }
            return doanThongct;
        }
        [HttpGet]
        [Route("GetDoanThongDMID")]
        public async Task<ActionResult<DoanThongDM>> GetDoanThongDMID(long id)
        {
            var doanThongdm = await _context.DoanThongDM.FindAsync(id);
            if (doanThongdm == null)
            {
                return NotFound();
            }
            return doanThongdm;
        }

        [HttpPost]
        [Route("PostDoanThong")]
        public async Task<ActionResult<DoanThong>> PostDoanThong(DoanThong doanThong)
        {
            _context.DoanThong.Add(doanThong);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoanThongExists(doanThong.DoanThongID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return doanThong;
        }

        [HttpPost]
        [Route("PostDoanThongCT")]
        public async Task<ActionResult<DoanThongCT>> PostDoanThongCT(DoanThongCT chitiet)
        {
            _context.DoanThongCT.Add(chitiet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoanThongCTExists(chitiet.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetDoanThongCTID", new { id = chitiet.ID }, chitiet);
        }

        [HttpPost]
        [Route("PostDoanThongDM")]
        public async Task<ActionResult<DoanThongDM>> PostDoanThongDM(DoanThongDM chitiet)
        {
            _context.DoanThongDM.Add(chitiet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoanThongDMExists(chitiet.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetDoanThongDMID", new { id = chitiet.ID }, chitiet);
        }

      
        [HttpPut]
        [Route("PutDoanThongALL")]
        public async Task<ActionResult<DoanThong>> PutDoanThongALL(long id, DoanThong doanthongALL)
        {
            if (id != doanthongALL.DoanThongID)
            {
                return BadRequest();
            }          
            //Đối với đoạn thống thì phần chi tiết xóa hết cũ và thêm mới.
            //Xóa
            var doanThong = await _context.DoanThong.FindAsync(id);
            if (doanThong != null)
            {
                _context.DoanThong.Remove(doanThong);
            }
            var doanThongCT = await _context.DoanThongCT.Where(x => x.DoanThongID == id).ToListAsync();
            if (doanThongCT != null)
            {
                _context.DoanThongCT.RemoveRange(doanThongCT);
            }
            var doanThongDM = await _context.DoanThongDM.Where(x => x.DoanThongID == id).ToListAsync();
            if (doanThongDM != null)
            {
                _context.DoanThongDM.RemoveRange(doanThongDM);
            }
            //Thêm mới
            doanthongALL.DoanThongID = id;
            //doanthongALL.Createddate = DateTime.Now;
            doanthongALL.Modifydate = DateTime.Now;
            if (doanthongALL.doanThongCTs != null)
            {
                foreach (DoanThongCT ct in doanthongALL.doanThongCTs)
                {
                    ct.ID = 0;
                    ct.DoanThongID = id;
                }
            }
            if (doanthongALL.doanThongDMs != null)
            {
                foreach (DoanThongDM ct in doanthongALL.doanThongDMs)
                {
                    ct.ID = 0;
                    ct.DoanThongID = id;
                }
            }
            _context.DoanThong.Add(doanthongALL);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoanThongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return doanthongALL;
        }


        [HttpPut]
        [Route("PutDoanThong")]
        public async Task<IActionResult> PutDoanThong(long id, DoanThong doanthong)
        {
            if (id != doanthong.DoanThongID)
            {
                return BadRequest();
            }
            doanthong.Modifydate = DateTime.Now;
            _context.Entry(doanthong).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoanThongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPut]
        [Route("PutDoanThongCT")]
        public async Task<IActionResult> PutDoanThongCT(long id, DoanThongCT chitiet)
        {
            if (id != chitiet.ID)
            {
                return BadRequest();
            }
            _context.Entry(chitiet).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoanThongCTExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPut]
        [Route("PutDoanThongDM")]
        public async Task<IActionResult> PutDoanThongDM(long id, DoanThongDM chitiet)
        {
            if (id != chitiet.ID)
            {
                return BadRequest();
            }
            _context.Entry(chitiet).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoanThongDMExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteDoanThong")]
        public async Task<ActionResult<DoanThong>> DeleteDoanThong(long id)
        {
            var doanthong = await _context.DoanThong.FindAsync(id);
            if (doanthong == null)
            {
                return NotFound();
            }
            _context.DoanThong.Remove(doanthong);
            await _context.SaveChangesAsync();
            return doanthong;
        }

        [HttpDelete]
        [Route("DeleteDoanThongCT")]
        public async Task<ActionResult<DoanThongCT>> DeleteDoanThongCT(long id)
        {
            var chitiet = await _context.DoanThongCT.FindAsync(id);
            if (chitiet == null)
            {
                return NotFound();
            }
            _context.DoanThongCT.Remove(chitiet);
            await _context.SaveChangesAsync();
            return chitiet;
        }

        [HttpDelete]
        [Route("DeleteDoanThongDM")]
        public async Task<ActionResult<DoanThongDM>> DeleteDoanThongDM(long id)
        {
            var chitiet = await _context.DoanThongDM.FindAsync(id);
            if (chitiet == null)
            {
                return NotFound();
            }
            _context.DoanThongDM.Remove(chitiet);
            await _context.SaveChangesAsync();
            return chitiet;
        }

        [HttpDelete]
        [Route("DeleteDoanThongCTAll")]
        public async Task<IActionResult> DeleteDoanThongCTAll(long id)
        {
            var existing = _context.DoanThongCT.Where(x => x.ID==id).ToList();
            if (existing.Any())
            {
                _context.DoanThongCT.RemoveRange(existing);
                await _context.SaveChangesAsync();
            }           
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteDoanThongDMAll")]
        public async Task<IActionResult> DeleteDoanThongDMAll(long id)
        {
            var existing = _context.DoanThongDM.Where(x => x.ID == id).ToList();
            if (existing.Any())
            {
                _context.DoanThongDM.RemoveRange(existing);
                await _context.SaveChangesAsync();
            }           
            return NoContent();
        }

        private bool DoanThongExists(long id)
        {
            return _context.DoanThong.Any(e => e.DoanThongID == id);
        }

        private bool DoanThongCTExists(long id)
        {
            return _context.DoanThongCT.Any(e => e.ID == id);
        }
        private bool DoanThongDMExists(long id)
        {
            return _context.DoanThongDM.Any(e => e.ID == id);
        }
    }
}
