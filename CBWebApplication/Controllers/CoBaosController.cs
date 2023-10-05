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
    public class CoBaosController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public CoBaosController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetCoBaoTT")]
        public async Task<ActionResult<CoBaoTT>> GetCoBaoTT(long id)
        {
            var coBaoTT = await _context.View_ThanhTich.FindAsync(id);

            if (coBaoTT == null)
            {
                return NotFound();
            }
            return coBaoTT;
        }

        [HttpGet]
        [Route("GetThanhTich")]
        public async Task<ActionResult<CoBaoTT>> GetThanhTich(long id)
        {
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        where cb.CoBaoID == id
                        select new CoBaoTT
                        {
                            CoBaoID = (long)cb.CoBaoID,
                            CoBaoGoc = (long)cb.CoBaoGoc,
                            SoCB = (string)cb.SoCB,
                            NgayCB = (DateTime)cb.NgayCB,
                            DauMayID = (string)cb.DauMayID,
                            LoaiMayID = (string)cb.LoaiMayID,
                            QuayVong = (int)(ct.QuayVong + ct.DungDM + ct.DungDN),
                            LuHanh = (int)ct.LuHanh,
                            DonThuan = (int)ct.DonThuan,
                            GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                            GioDung = (int)(ct.DungDM + ct.DungDN + ct.DungXP + ct.DungDD + ct.DungQD + ct.DungKT),
                            KM = (decimal)(ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay),
                            TKM = (decimal)(ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay),
                            NLTieuThu = (decimal)ct.TieuThu,
                            NLTieuChuan = (decimal)ct.DinhMuc
                        };
            CoBaoTT cbBaoTT = await (from x in query
                                     group x by new { x.CoBaoID, x.CoBaoGoc, x.SoCB, x.NgayCB, x.DauMayID, x.LoaiMayID } into g
                                     select new CoBaoTT
                                     {
                                         CoBaoID = g.Key.CoBaoID,
                                         CoBaoGoc = g.Key.CoBaoGoc,
                                         SoCB = g.Key.SoCB,
                                         NgayCB = g.Key.NgayCB,
                                         DauMayID = g.Key.DauMayID,
                                         LoaiMayID = g.Key.LoaiMayID,
                                         QuayVong = g.Sum(x => x.QuayVong),
                                         LuHanh = g.Sum(x => x.LuHanh),
                                         DonThuan = g.Sum(x => x.DonThuan),
                                         GioDon = g.Sum(x => x.GioDon),
                                         GioDung = g.Sum(x => x.GioDung),
                                         KM = g.Sum(x => x.KM),
                                         TKM = g.Sum(x => x.TKM),
                                         NLTieuThu = g.Sum(x => x.NLTieuThu),
                                         NLTieuChuan = g.Sum(x => x.NLTieuChuan),
                                         NLLoiLo = g.Sum(x => x.NLTieuChuan) - g.Sum(x => x.NLTieuThu)
                                     }).FirstOrDefaultAsync();
            return cbBaoTT;
        }

        [HttpGet]
        [Route("GetThanhTichByCoBaoGoc")]
        public async Task<ActionResult<IEnumerable<CoBaoTT>>> GetThanhTichByCoBaoGoc(long id)
        {
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        where cb.CoBaoGoc == id
                        select new CoBaoTT
                        {
                            CoBaoID = (long)cb.CoBaoID,
                            CoBaoGoc = (long)cb.CoBaoGoc,
                            SoCB = (string)cb.SoCB,
                            NgayCB = (DateTime)cb.NgayCB,
                            DauMayID = (string)cb.DauMayID,
                            LoaiMayID = (string)cb.LoaiMayID,
                            QuayVong = (int)(ct.QuayVong + ct.DungDM + ct.DungDN),
                            LuHanh = (int)ct.LuHanh,
                            DonThuan = (int)ct.DonThuan,
                            GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                            GioDung = (int)(ct.DungDM + ct.DungDN + ct.DungXP + ct.DungDD + ct.DungQD + ct.DungKT),
                            KM = (decimal)(ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay),
                            TKM = (decimal)(ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay),
                            NLTieuThu = (decimal)ct.TieuThu,
                            NLTieuChuan = (decimal)ct.DinhMuc
                        };

            return await (from x in query
                                     group x by new { x.CoBaoID, x.CoBaoGoc, x.SoCB, x.NgayCB, x.DauMayID, x.LoaiMayID } into g
                                     select new CoBaoTT
                                     {
                                         CoBaoID = g.Key.CoBaoID,
                                         CoBaoGoc = g.Key.CoBaoGoc,
                                         SoCB = g.Key.SoCB,
                                         NgayCB = g.Key.NgayCB,
                                         DauMayID = g.Key.DauMayID,
                                         LoaiMayID = g.Key.LoaiMayID,
                                         QuayVong = g.Sum(x => x.QuayVong),
                                         LuHanh = g.Sum(x => x.LuHanh),
                                         DonThuan = g.Sum(x => x.DonThuan),
                                         GioDon = g.Sum(x => x.GioDon),
                                         GioDung = g.Sum(x => x.GioDung),
                                         KM = g.Sum(x => x.KM),
                                         TKM = g.Sum(x => x.TKM),
                                         NLTieuThu = g.Sum(x => x.NLTieuThu),
                                         NLTieuChuan = g.Sum(x => x.NLTieuChuan),
                                         NLLoiLo = g.Sum(x => x.NLTieuChuan) - g.Sum(x => x.NLTieuThu)
                                     }).ToListAsync();            
        }

        [HttpGet]
        [Route("GetCoBaoTTByCoBaoGoc")]
        public async Task<ActionResult<IEnumerable<CoBaoTT>>> GetCoBaoTTByCoBaoGoc(long id)
        {
            return await _context.View_ThanhTich.Where(x => x.CoBaoGoc == id).ToListAsync();
        }
        // GET: api/CoBaos/Object 
        [HttpGet]
        [Route("GetCoBaoView")]
        public async Task<ActionResult<IEnumerable<CoBao>>> GetCoBaoView(DateTime NgayBD,DateTime NgayKT,string LoaiMay,string DauMay,string SoCB,string TaiXe)
        {            
            var query = from item in _context.CoBao
                        where item.NgayCB>=NgayBD && item.NgayCB<NgayKT
                        select item;
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if(!string.IsNullOrWhiteSpace(DauMay))
                query = query.Where(x => x.DauMayID .Contains(DauMay));
            if (!string.IsNullOrWhiteSpace(SoCB))
                query = query.Where(x => x.SoCB.Contains(SoCB));
            if (!string.IsNullOrWhiteSpace(TaiXe))
                query = query.Where(x => x.TaiXe1ID.Contains(TaiXe) || x.PhoXe1ID.Contains(TaiXe) || x.TaiXe2ID.Contains(TaiXe) || x.PhoXe2ID.Contains(TaiXe) || x.TaiXe3ID.Contains(TaiXe) || x.PhoXe3ID.Contains(TaiXe));
            return await query.ToListAsync();            
        }       

        [HttpGet]
        [Route("GetCoBaoCTView")]
        public async Task<ActionResult<IEnumerable<CoBaoCT>>> GetCoBaoCTView(long id)
        {
            return await _context.CoBaoCT.Where(x => x.CoBaoID == id).ToListAsync();
        }
        [HttpGet]
        [Route("GetCoBaoDM")]
        public async Task<ActionResult<IEnumerable<CoBaoDM>>> GetCoBaoDM(long id)
        {
            return await _context.CoBaoDM.Where(x => x.CoBaoID == id).ToListAsync();
        }
        
        [HttpGet]
        [Route("GetCoBaoCTByCoBaoGoc")]
        public async Task<ActionResult<IEnumerable<CoBaoCT>>> GetCoBaoCTByCoBaoGoc(long id)
        {
            var query = from ct in _context.CoBaoCT join cb in _context.CoBao on ct.CoBaoID equals cb.CoBaoID
                        where cb.CoBaoGoc==id
                        select ct;              
            return await query.OrderBy(x=>x.NgayXP).OrderBy(x=>x.GioDi).ToListAsync();
        }
        [HttpGet]
        [Route("GetCoBaoALL")]
        public async Task<ActionResult<CoBaoALL>> GetCoBaoIDALL(long id)
        {
            var coBao = await _context.CoBao.FindAsync(id);
            if (coBao == null)
            {
                return NotFound();
            }
            coBao.coBaoCTs = await _context.CoBaoCT.Where(x => x.CoBaoID == id).ToListAsync();
            coBao.coBaoDMs = await _context.CoBaoDM.Where(x => x.CoBaoID == id).ToListAsync();

            var doanThong = await _context.DoanThong.FindAsync(id);
            doanThong.doanThongCTs = await _context.DoanThongCT.Where(x => x.DoanThongID == id).ToListAsync();
            doanThong.doanThongDMs = await _context.DoanThongDM.Where(x => x.DoanThongID == id).ToListAsync();
            var coBaoAll = new CoBaoALL();
            coBaoAll.CoBaoID = coBao.CoBaoID;
            coBaoAll.coBaos = coBao;
            coBaoAll.doanThongs = doanThong;
            return coBaoAll;
        }
       
        [HttpGet]
        [Route("GetCoBaoID")]
        public async Task<ActionResult<CoBao>> GetCoBaoID(long id)
        {
            var coBao = await _context.CoBao.FindAsync(id);
            coBao.coBaoCTs = await _context.CoBaoCT.Where(x => x.CoBaoID == id).ToListAsync();
            coBao.coBaoDMs = await _context.CoBaoDM.Where(x => x.CoBaoID == id).ToListAsync();           
            if (coBao == null)
            {
                return NotFound();
            }
            return coBao;
        }

        [HttpGet]
        [Route("GetCoBaoGocID")]
        public async Task<ActionResult<CoBao>> GetCoBaoGocID(long id)
        {
            var coBao = await _context.CoBao.Where(x => x.CoBaoGoc == id).FirstOrDefaultAsync();
            coBao.coBaoCTs = await _context.CoBaoCT.Where(x => x.CoBaoID == coBao.CoBaoID).ToListAsync();
            coBao.coBaoDMs = await _context.CoBaoDM.Where(x => x.CoBaoID == coBao.CoBaoID).ToListAsync();
            if (coBao == null)
            {
                return NotFound();
            }
            return coBao;
        }

        [HttpGet]
        [Route("GetCoBaoCTID")]
        public async Task<ActionResult<CoBaoCT>> GetCoBaoCTID(long id)
        {
            var coBaoCT = await _context.CoBaoCT.FindAsync(id);
            if (coBaoCT == null)
            {
                return NotFound();
            }

            return coBaoCT;
        }

        [HttpGet]
        [Route("GetCoBaoDMID")]
        public async Task<ActionResult<CoBaoDM>> GetCoBaoDMID(long id)
        {
            var coBaoDM = await _context.CoBaoDM.FindAsync(id);
            if (coBaoDM == null)
            {
                return NotFound();
            }

            return coBaoDM;
        }

        [HttpGet]
        [Route("GetCoBaoMayGhep")]
        public async Task<ActionResult<CoBaoCT>> GetCoBaoMayGhep(DateTime NgayXP, string MacTauID, int GaID, string MayGhepID)
        {
            return await (from item in _context.CoBaoCT
                        where item.NgayXP== NgayXP && item.MacTauID == MacTauID && item.GaID==GaID && item.MayGhepID.Contains(MayGhepID)
                        select item).FirstOrDefaultAsync();           
        }

        [HttpGet]
        [Route("GetCoBaoPrev")]
        public async Task<ActionResult<CoBao>> GetCoBaoPrev(DateTime NgayGM, string LoaiMay, string DauMay,long CoBaoGoc)
        {
            return await (from item in _context.CoBao
                          where item.GiaoMay < NgayGM && item.LoaiMayID == LoaiMay && item.DauMayID == DauMay && item.CoBaoGoc!=CoBaoGoc select item)
                          .OrderByDescending(x=>x.GiaoMay).FirstOrDefaultAsync();
        }

        // PUT: api/CoBaos/       
        [HttpPut]
        [Route("PutCoBaoALL")]
        public async Task<ActionResult<CoBaoALL>> PutCoBaoALL(long id, CoBaoALL coBaoALL)
        {
            if (id != coBaoALL.CoBaoID)
            {
                return BadRequest();
            }
            coBaoALL.coBaos.Modifydate = DateTime.Now;
            _context.Entry(coBaoALL.coBaos).State = EntityState.Modified;

            var coBaoCTOld = coBaoALL.coBaos.coBaoCTs.ToList();
            //Xóa hết cơ báo chi tiết và thêm mới
            var coBaoCT = await _context.CoBaoCT.Where(x => x.CoBaoID == id).ToListAsync();
            if (coBaoCT != null)
            {
                _context.CoBaoCT.RemoveRange(coBaoCT);
            }
            //Thêm mới cơ báo chi tiết

            if (coBaoCTOld != null)
            {
                foreach (CoBaoCT ct in coBaoCTOld)
                {
                    ct.ID = 0;
                    ct.CoBaoID = coBaoALL.CoBaoID;
                    _context.CoBaoCT.Add(ct);
                }
            }
            var coBaoDMOld = coBaoALL.coBaos.coBaoDMs.ToList();
            //Xóa hết cơ báo dầu mỡ và thêm mới
            var coBaoDM = await _context.CoBaoDM.Where(x => x.CoBaoID == id).ToListAsync();
            if (coBaoDM != null)
            {
                _context.CoBaoDM.RemoveRange(coBaoDM);
            }            
            //Thêm mới cơ báo dầu mỡ
            if (coBaoDMOld != null)
            {
                foreach (CoBaoDM ct in coBaoDMOld)
                {
                    ct.ID = 0;
                    ct.CoBaoID = coBaoALL.CoBaoID;
                    _context.CoBaoDM.Add(ct);
                }
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
            coBaoALL.doanThongs.DoanThongID = coBaoALL.CoBaoID;
            coBaoALL.doanThongs.Createddate = coBaoALL.coBaos.Createddate;
            coBaoALL.doanThongs.Modifydate = coBaoALL.coBaos.Modifydate;
            if (coBaoALL.doanThongs.doanThongCTs != null)
            {
                foreach (DoanThongCT ct in coBaoALL.doanThongs.doanThongCTs)
                {
                    ct.ID = 0;
                    ct.DoanThongID = coBaoALL.CoBaoID;
                }
            }
            if (coBaoALL.doanThongs.doanThongDMs != null)
            {
                foreach (DoanThongDM ct in coBaoALL.doanThongs.doanThongDMs)
                {
                    ct.ID = 0;
                    ct.DoanThongID = coBaoALL.CoBaoID;
                }
            }
            _context.DoanThong.Add(coBaoALL.doanThongs);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoBaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return coBaoALL;
        }

        [HttpPut]
        [Route("PutCoBao")]
        public async Task<IActionResult> PutCoBao(long id, CoBao coBao)
        {
            if (id != coBao.CoBaoID)
            {
                return BadRequest();
            }
            coBao.Modifydate = DateTime.Now;
            _context.Entry(coBao).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoBaoExists(id))
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
        [Route("PutCoBaoCT")]
        public async Task<IActionResult> PutCoBaoCT(long id, CoBaoCT coBaoCT)
        {
            if (id != coBaoCT.ID)
            {
                return BadRequest();
            }
            _context.Entry(coBaoCT).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoBaoCTExists(id))
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
        [Route("PutCoBaoDM")]
        public async Task<IActionResult> PutCoBaoDM(long id, CoBaoDM coBaoDM)
        {
            if (id != coBaoDM.ID)
            {
                return BadRequest();
            }
            _context.Entry(coBaoDM).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoBaoDMExists(id))
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
        
        // [HttpPost]
        [Route("PostCoBaoALL")]
        public async Task<ActionResult<CoBaoALL>> PostCoBaoALL(CoBaoALL coBaoALL)
        {
            try
            {
                var cobaoG = _context.CoBao.Where(x=>x.CoBaoGoc== coBaoALL.coBaos.CoBaoGoc).FirstOrDefault();
                var cobaosl = _context.CoBao.OrderByDescending(x => x.CoBaoID).FirstOrDefault();
                if (cobaosl != null && cobaoG == null)
                {
                    coBaoALL.CoBaoID = cobaosl.CoBaoID + 1;
                    coBaoALL.coBaos.CoBaoID = coBaoALL.CoBaoID;
                    coBaoALL.coBaos.Createddate = DateTime.Now;
                    coBaoALL.coBaos.Modifydate = coBaoALL.coBaos.Createddate;
                    if (coBaoALL.coBaos.coBaoCTs != null)
                    {
                        foreach (CoBaoCT ct in coBaoALL.coBaos.coBaoCTs)
                        {
                            ct.ID = 0;
                            ct.CoBaoID = coBaoALL.CoBaoID;
                        }
                    }
                    if (coBaoALL.coBaos.coBaoDMs != null)
                    {
                        foreach (CoBaoDM ct in coBaoALL.coBaos.coBaoDMs)
                        {
                            ct.ID = 0;
                            ct.CoBaoID = coBaoALL.CoBaoID;
                        }
                    }

                    coBaoALL.doanThongs.DoanThongID = coBaoALL.CoBaoID;
                    coBaoALL.doanThongs.Createddate = coBaoALL.coBaos.Createddate;
                    coBaoALL.doanThongs.Modifydate = coBaoALL.coBaos.Modifydate;
                    if (coBaoALL.doanThongs.doanThongCTs != null)
                    {
                        foreach (DoanThongCT ct in coBaoALL.doanThongs.doanThongCTs)
                        {
                            ct.ID = 0;
                            ct.DoanThongID = coBaoALL.CoBaoID;
                        }
                    }
                    if (coBaoALL.doanThongs.doanThongDMs != null)
                    {
                        foreach (DoanThongDM ct in coBaoALL.doanThongs.doanThongDMs)
                        {
                            ct.ID = 0;
                            ct.DoanThongID = coBaoALL.CoBaoID;
                        }
                    }
                }
                _context.CoBao.Add(coBaoALL.coBaos);
                _context.DoanThong.Add(coBaoALL.doanThongs);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return coBaoALL;
        }

        [HttpPost]
        [Route("PostCoBao")]
        public async Task<ActionResult<CoBao>> PostCoBao(CoBao coBao)
        {
            try
            {
                var cobaosl = _context.CoBao.OrderByDescending(x => x.CoBaoID).FirstOrDefault();
                if (cobaosl != null)
                    coBao.CoBaoID = cobaosl.CoBaoID + 1;
                coBao.Createddate = DateTime.Now;
                coBao.Modifydate = coBao.Createddate;
                _context.CoBao.Add(coBao);               
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new Exception(e.Message);
            }  
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return CreatedAtAction("GetCoBaoID", new { id = coBao.CoBaoID }, coBao);
        }

        [HttpPost]
        [Route("PostCoBaoCT")]
        public async Task<ActionResult<CoBaoCT>> PostCoBaoCT(CoBaoCT coBaoCT)
        {
            _context.CoBaoCT.Add(coBaoCT);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CoBaoCTExists(coBaoCT.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetCoBaoCTID", new { id = coBaoCT.ID }, coBaoCT);
        }

        [HttpPost]
        [Route("PostCoBaoDM")]
        public async Task<ActionResult<CoBaoDM>> PostCoBaoDM(CoBaoDM coBaoDM)
        {
            _context.CoBaoDM.Add(coBaoDM);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CoBaoDMExists(coBaoDM.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCoBaoDMID", new { id = coBaoDM.ID }, coBaoDM);
        }

        // DELETE: api/CoBaos/5
        [HttpDelete]
        [Route("DeleteCoBaoALL")]
        public async Task<ActionResult<CoBao>> DeleteCoBaoALL(long id)
        {
            var coBao = await _context.CoBao.FindAsync(id);
            if (coBao == null)
            {
                return NotFound();
            }
            _context.CoBao.Remove(coBao);
            var coBaoCT = await _context.CoBaoCT.Where(x => x.CoBaoID == id).ToListAsync();
            if(coBaoCT!=null)
            {
                _context.CoBaoCT.RemoveRange(coBaoCT);
            }
            var coBaoDM = await _context.CoBaoDM.Where(x => x.CoBaoID == id).ToListAsync();
            if (coBaoDM != null)
            {
                _context.CoBaoDM.RemoveRange(coBaoDM);
            }

            var doanThong = await _context.DoanThong.FindAsync(id);
            if (doanThong!= null)
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
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);                
            }
            //Ghi nhật ký xóa cơ báo
            return coBao;
        }

        [HttpDelete]
        [Route("DeleteCoBaoGocALL")]
        public async Task<ActionResult<CoBao>> DeleteCoBaoGocALL(long id, string manv, string tennv)
        {
            var coBao = await _context.CoBao.Where(x => x.CoBaoGoc == id).FirstOrDefaultAsync();
            if (coBao == null)
            {                
                return NotFound();
            }
            _context.CoBao.Remove(coBao);
            var coBaoCT = await _context.CoBaoCT.Where(x => x.CoBaoID == coBao.CoBaoID).ToListAsync();
            if (coBaoCT != null)
            {
                _context.CoBaoCT.RemoveRange(coBaoCT);
            }
            var coBaoDM = await _context.CoBaoDM.Where(x => x.CoBaoID == coBao.CoBaoID).ToListAsync();
            if (coBaoDM != null)
            {
                _context.CoBaoDM.RemoveRange(coBaoDM);
            }

            var doanThong = await _context.DoanThong.FindAsync(coBao.CoBaoID);
            if (doanThong != null)
            {
                _context.DoanThong.Remove(doanThong);
            }
            var doanThongCT = await _context.DoanThongCT.Where(x => x.DoanThongID == coBao.CoBaoID).ToListAsync();
            if (doanThongCT != null)
            {
                _context.DoanThongCT.RemoveRange(doanThongCT);
            }
            var doanThongDM = await _context.DoanThongDM.Where(x => x.DoanThongID == coBao.CoBaoID).ToListAsync();
            if (doanThongDM != null)
            {
                _context.DoanThongDM.RemoveRange(doanThongDM);
            }
            //Ghi nhật ký xóa cơ báo            
            NhatKy nk = new NhatKy();
            nk.TenBang = "CoBao";
            nk.NoiDung = "Xóa cơ báo điện tử: " + coBao.CoBaoID + "-" + coBao.CoBaoGoc + "-" + coBao.SoCB + "-" + coBao.NgayCB;
            nk.Createddate = DateTime.Now;
            nk.Createdby = manv;
            nk.CreatedName = tennv;
            _context.NhatKy.Add(nk);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
            //Ghi nhật ký xóa cơ báo
            return coBao;
        }

        [HttpDelete]
        [Route("DeleteCoBao")]
        public async Task<ActionResult<CoBao>> DeleteCoBao(long id)
        {
            var coBao = await _context.CoBao.FindAsync(id);
            if (coBao == null)
            {
                return NotFound();
            }

            _context.CoBao.Remove(coBao);
            await _context.SaveChangesAsync();

            return coBao;
        }

        [HttpDelete]
        [Route("DeleteCoBaoCT")]
        public async Task<ActionResult<CoBaoCT>> DeleteCoBaoCT(long id)
        {
            var coBaoCT = await _context.CoBaoCT.FindAsync(id);
            if (coBaoCT == null)
            {
                return NotFound();
            }

            _context.CoBaoCT.Remove(coBaoCT);
            await _context.SaveChangesAsync();

            return coBaoCT;
        }
        [HttpDelete]
        [Route("DeleteCoBaoDM")]
        public async Task<ActionResult<CoBaoDM>> DeleteCoBaoDM(long id)
        {
            var coBaoDM = await _context.CoBaoDM.FindAsync(id);
            if (coBaoDM == null)
            {
                return NotFound();
            }

            _context.CoBaoDM.Remove(coBaoDM);
            await _context.SaveChangesAsync();

            return coBaoDM;
        }

        [HttpDelete]
        [Route("DeleteCoBaoDMAll")]
        public async Task<IActionResult> DeleteCoBaoDMAll(long id)
        {
            var existing = _context.CoBaoDM.Where(x => x.CoBaoID == id).ToList();
            if (existing.Any())
            {
                _context.CoBaoDM.RemoveRange(existing);
                await _context.SaveChangesAsync();
            }            
            return NoContent();
        }

        private bool CoBaoExists(long id)
        {
            return _context.CoBao.Any(e => e.CoBaoID == id);
        }
        private bool CoBaoCTExists(long id)
        {
            return _context.CoBaoCT.Any(e => e.ID == id);
        }
        private bool CoBaoDMExists(long id)
        {
            return _context.CoBaoDM.Any(e => e.ID == id);
        }
    }
}
