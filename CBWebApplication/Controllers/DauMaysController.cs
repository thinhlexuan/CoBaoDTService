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
    public class DauMaysController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public DauMaysController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetViewDauMay")]
        public async Task<ActionResult<IEnumerable<ViewDauMay>>> GetViewDauMay(string maDVSH,string maDVQL,string loaiMay, string dauMay)
        {
            var query = from dm in _context.DauMay
                        join dvsh in _context.DonViDM on dm.MaCTSoHuu equals dvsh.MaDV
                        join dvql in _context.DonViDM on dm.MaCTQuanLy equals dvql.MaDV
                        select new ViewDauMay
                        {
                            ID=dm.ID,
                            DauMayID=dm.DauMayID,
                            LoaiMayID=dm.LoaiMayID,
                            MaCTSoHuu=dm.MaCTSoHuu,
                            TenCTSoHuu=dvsh.TenDV,
                            MaCTQuanLy=dm.MaCTQuanLy,
                            TenCTQuanLy=dvql.TenDV,
                            NgayHL=dm.NgayHL,
                            Active=dm.Active,
                            CreatedDate=dm.CreatedDate,
                            CreatedBy=dm.CreatedBy,
                            CreatedName=dm.CreatedName,
                            ModifyDate=dm.ModifyDate,
                            ModifyBy=dm.ModifyBy,
                            ModifyName=dm.ModifyName
                        };
            if (maDVSH != "TCT")
                query = query.Where(x => x.MaCTSoHuu == maDVSH);
            if (maDVQL != "TCT")
                query = query.Where(x => x.MaCTQuanLy == maDVQL);
            if (!string.IsNullOrWhiteSpace(loaiMay))
                query = query.Where(x => x.LoaiMayID.Contains(loaiMay));
            if (!string.IsNullOrWhiteSpace(dauMay))
                query = query.Where(x => x.DauMayID.Contains(dauMay));
            return await query.ToListAsync();
        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<ActionResult<DauMay>> GetByID(int id)
        {
            var dauMay = await _context.DauMay.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (dauMay == null)
            {
                return NotFound();
            }
            return dauMay;
        }

        [HttpPut]
        [Route("PutByID")]
        public async Task<ActionResult<DauMay>> PutByID(int id, DauMay dauMay)
        {
            dauMay.ModifyDate = DateTime.Now;

            if (id != dauMay.ID)
            {
                return BadRequest();
            }
            _context.Entry(dauMay).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return dauMay;
        }

        [HttpPost]
        [Route("PostByID")]
        public async Task<ActionResult<DauMay>> PostByID(DauMay dauMay)
        {
            dauMay.CreatedDate = DateTime.Now;
            dauMay.ModifyDate = dauMay.CreatedDate;
            var query = _context.DauMay.Where(x => x.LoaiMayID == dauMay.LoaiMayID && x.DauMayID == dauMay.DauMayID && x.MaCTSoHuu==dauMay.MaCTSoHuu && x.MaCTQuanLy==dauMay.MaCTQuanLy && x.NgayHL==dauMay.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.DauMay.Add(dauMay);
            await _context.SaveChangesAsync();
            return dauMay;
        }

        [HttpDelete]
        [Route("DeleteByID")]
        public async Task<ActionResult<DauMay>> DeleteByID(int id)
        {
            var dauMay = _context.DauMay.Where(x => x.ID == id).FirstOrDefault();
            if (dauMay == null)
            {
                return NotFound();
            }
            _context.DauMay.Remove(dauMay);

            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "DauMay";
            nk.NoiDung = "Xóa Đầu Máy: " + dauMay.LoaiMayID + "-" + dauMay.DauMayID + ". MaCTSH: " + dauMay.MaCTSoHuu + ". MaCTQL: " + dauMay.MaCTQuanLy + ". NgayHL: " + dauMay.NgayHL;
            nk.Createddate = DateTime.Now;
            nk.Createdby = dauMay.ModifyBy;
            nk.CreatedName = dauMay.ModifyBy;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return dauMay;
        }
    }
}
