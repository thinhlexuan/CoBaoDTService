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
    public class KeHoachsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public KeHoachsController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetKeHoachView")]
        public async Task<ActionResult<IEnumerable<KeHoachView>>> GetKeHoachView(string nhomKH,short kyKH,short namKH)
        {
            var query = (from kh in _context.KeHoach select kh);
            if (nhomKH!="ALL")
                query = query.Where(x => x.NhomKH==nhomKH && x.KyKH==kyKH && x.NamKH==namKH);
            await query.ToListAsync();

            return await (from kh in query
                          join la in _context.LoaiKeHoach on kh.MaLoai equals la.MaLoai                          
                          select new KeHoachView
                          {
                             ID=kh.ID,
                             MaLoai=kh.MaLoai,
                             SoTT=la.SoTT,
                             TenLoai=la.TenLoai,
                             DonVi=la.DonVi,
                             NhomKH=kh.NhomKH,
                             KyKH=kh.KyKH,
                             NamKH=kh.NamKH,
                             YV=kh.YV,
                             HN=kh.HN,
                             VIN=kh.VIN,
                             DN=kh.DN,
                             SG=kh.SG,
                             CreatedDate=kh.CreatedDate,
                             CreatedBy=kh.CreatedBy,
                             CreatedName=kh.CreatedName,
                             ModifyDate=kh.ModifyDate,
                             ModifyBy=kh.ModifyBy,
                             ModifyName=kh.ModifyName

                          }).ToListAsync();
        }
       

        [HttpGet]
        [Route("GetLoaiKeHoach")]
        public async Task<ActionResult<IEnumerable<LoaiKeHoach>>> GetLoaiKeHoach()
        {
            return await _context.LoaiKeHoach.ToListAsync();            
        }

        [HttpPut]
        [Route("PutKeHoach")]
        public async Task<ActionResult<KeHoach>> PutKeHoach(long id, KeHoach keHoach)
        {
            keHoach.ModifyDate = DateTime.Now;
            if (id != keHoach.ID)
            {
                return BadRequest();
            }
            _context.Entry(keHoach).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return keHoach;
        }

        [HttpPost]
        [Route("PostKeHoach")]
        public async Task<ActionResult<KeHoach>> PostKeHoach(KeHoach keHoach)
        {
            keHoach.CreatedDate = DateTime.Now;
            keHoach.ModifyDate = keHoach.CreatedDate;
            var query = await _context.KeHoach.FindAsync(keHoach.ID);
            if (query != null)
            {
                return BadRequest();
            }           
            _context.KeHoach.Add(keHoach);
            await _context.SaveChangesAsync();

            return keHoach;
        }

        [HttpDelete]
        [Route("DeleteKeHoach")]
        public async Task<ActionResult<KeHoach>> DeleteKeHoach(long id,string maNV,string tenNV)
        {
            var query = await _context.KeHoach.FindAsync(id);
            if (query == null)
            {
                return NotFound();
            }
            _context.KeHoach.Remove(query);

            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "KeHoach";
            nk.NoiDung = "Xóa Kế Hoạch: " + query.MaLoai + "-Nhóm: " + query.NhomKH + "-Kỳ" + query.KyKH + "-Năm: " + query.NamKH;
            nk.Createddate = DateTime.Now;
            nk.Createdby = maNV;
            nk.CreatedName = tenNV;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return query;
        }
    }
}
