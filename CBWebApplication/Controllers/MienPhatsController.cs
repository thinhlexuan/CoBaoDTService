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
    public class MienPhatsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public MienPhatsController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetMienPhat")]
        public async Task<ActionResult<IEnumerable<MienPhat>>> GetMienPhat(string MaDV, int TuThang, int DenThang, int TuNam, int DenNam)
        {
            var query = from item in _context.MienPhat
                        where item.ThangDT >= TuThang && item.ThangDT <= DenThang && item.NamDT >= TuNam && item.NamDT <= DenNam
                        select item;
            if (MaDV != "TCT")
                query = query.Where(x => x.MaDV == MaDV);
            return await query.ToListAsync();
        }       

        [HttpPut]
        [Route("PutMienPhat")]
        public async Task<ActionResult<MienPhat>> PutMienPhat(MienPhat mienPhat)
        {
            mienPhat.ModifyDate = DateTime.Now;
            _context.Entry(mienPhat).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return mienPhat;
        }

        [HttpPost]
        [Route("PostMienPhat")]
        public async Task<ActionResult<MienPhat>> PostMienPhat(MienPhat mienPhat)
        {
            mienPhat.CreatedDate = DateTime.Now;
            mienPhat.ModifyDate = mienPhat.CreatedDate;

            var queryCB = (from dt in _context.DoanThong join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                           where dt.ThangDT == mienPhat.ThangDT && dt.NamDT == mienPhat.NamDT && cb.SoCB == mienPhat.SoCB && cb.DvcbID==mienPhat.MaDV select cb).FirstOrDefault();
            if (queryCB == null)
                return NotFound();
            mienPhat.CoBaoID = queryCB.CoBaoID;
            var query = _context.MienPhat.Where(x => x.CoBaoID == mienPhat.CoBaoID && x.ThangDT== mienPhat.ThangDT && x.NamDT==mienPhat.NamDT).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.MienPhat.Add(mienPhat);
            await _context.SaveChangesAsync();
            return mienPhat;
        }

        [HttpDelete]
        [Route("DeleteMienPhat")]
        public async Task<ActionResult<MienPhat>> DeleteMienPhat(long id,string manv,string tennv)
        {
            var mienPhat = _context.MienPhat.Where(x => x.CoBaoID == id).FirstOrDefault();
            if (mienPhat == null)
            {
                return NotFound();
            }
            _context.MienPhat.Remove(mienPhat);

            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "MienPhat";
            nk.NoiDung = "Xóa miễn phạt cơ báo: " + mienPhat.SoCB + "-" + mienPhat.ThangDT + "-" + mienPhat.NamDT + "-" + mienPhat.MaDV;
            nk.Createddate = DateTime.Now;
            nk.Createdby = manv;
            nk.CreatedName = tennv;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return mienPhat;
        }
    }
}
