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
    public class DMNhanViensController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public DMNhanViensController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetViewDMNhanVien")]
        public async Task<ActionResult<IEnumerable<ViewDMNhanVien>>> GetViewDMNhanVien(string MaDV,string MaNV)
        {
           var query= from nv in _context.DMNhanVien
                          join dv in _context.DmdonVi on nv.MaDV equals dv.MaDv                          
                          select new ViewDMNhanVien
                          {
                              NhanVienID=nv.NhanVienID,
                              Username = nv.Username,
                              FullName = nv.FullName,
                              MaSo = nv.MaSo,
                              ChucVu = nv.ChucVu,
                              PhoneNumber = nv.PhoneNumber,
                              Email = nv.Email,
                              MaDV = nv.MaDV,
                              TenDV = dv.TenDv,
                              MaCT = dv.MaCt,
                              DVQL = dv.Dvql,
                              CodeQL = dv.CodeQl,
                              CapQL = dv.CapQl,
                              CreatedDate=nv.CreatedDate,
                              CreatedBy=nv.CreatedBy,
                              ModifyDate=nv.ModifyDate,
                              ModifyBy=nv.ModifyBy
                          };
            if (MaDV != "TCT")
                query = query.Where(x => x.MaCT == MaDV);
            if (!string.IsNullOrWhiteSpace(MaNV))
                query = query.Where(x=>x.MaSo.Contains(MaNV));
            return await query.ToListAsync();
        }
       
        [HttpGet]
        [Route("GetByID")]
        public async Task<ActionResult<DMNhanVien>> GetByID(int id)
        {
            var dmNhanVien = await _context.DMNhanVien.Where(x => x.NhanVienID == id).FirstOrDefaultAsync();
            if (dmNhanVien == null)
            {
                return NotFound();
            }
            return dmNhanVien;
        }


        [HttpGet]
        [Route("GetDMTaiXe")]
        public async Task<ActionResult<IEnumerable<DMTaiXe>>> GetDMTaiXe(string MaDV)
        {
            var query = (from nv in _context.DMNhanVien
                         join dv in _context.DmdonVi on nv.MaDV equals dv.MaDv
                         where nv.ChucVu == "LTAU"
                         select new DMTaiXe
                         {
                             TaiXeID = nv.MaSo,
                             TaiXeName = nv.FullName,
                             DonViID = dv.MaCt
                         });
            if (MaDV != "TCT")
            {
                if (MaDV == "YV"|| MaDV == "HN")
                    query = query.Where(x => x.DonViID == MaDV || x.DonViID == "HN");
                else if (MaDV == "DN"|| MaDV == "SG")
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
                              DonViName = g.Key.TenDv
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetDMPhoXe")]
        public async Task<ActionResult<IEnumerable<DMPhoXe>>> GetDMPhoXe(string MaDV)
        {
            var query = (from nv in _context.DMNhanVien
                         join dv in _context.DmdonVi on nv.MaDV equals dv.MaDv                         
                         select new DMPhoXe
                         {
                             PhoXeID = nv.MaSo,
                             PhoXeName = nv.FullName,
                             DonViID = dv.MaCt
                         });
            if (MaDV != "TCT")
            {
                if (MaDV == "YV" || MaDV == "HN")
                    query = query.Where(x => x.DonViID == MaDV || x.DonViID == "HN");
                else if (MaDV == "DN" || MaDV == "SG")
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

        [HttpPut]
        [Route("PutByID")]
        public async Task<ActionResult<DMNhanVien>> PutByID(int id, DMNhanVien dMNhanVien)
        {
            dMNhanVien.ModifyDate = DateTime.Now;
           
            if (id != dMNhanVien.NhanVienID)
            {
                return BadRequest();
            }
            _context.Entry(dMNhanVien).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return dMNhanVien;
        }

        [HttpPost]
        [Route("PostByID")]
        public async Task<ActionResult<DMNhanVien>> PostByID(DMNhanVien dMNhanVien)
        {
            dMNhanVien.CreatedDate = DateTime.Now;
            dMNhanVien.ModifyDate = dMNhanVien.CreatedDate;            
            var query = _context.DMNhanVien.Where(x => x.MaSo==dMNhanVien.MaSo && x.MaDV == dMNhanVien.MaDV).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.DMNhanVien.Add(dMNhanVien);
            await _context.SaveChangesAsync();

            return dMNhanVien;
        }

        [HttpDelete]
        [Route("DeleteByID")]
        public async Task<ActionResult<DMNhanVien>> DeleteByID(int id)
        {
            var dmNhanVien = _context.DMNhanVien.Where(x => x.NhanVienID == id).FirstOrDefault();
            if (dmNhanVien == null)
            {
                return NotFound();
            }
            _context.DMNhanVien.Remove(dmNhanVien);

            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "DMNhanVien";
            nk.NoiDung = "Xóa nhân viên: " + dmNhanVien.Username + "-" + dmNhanVien.FullName +". Mã số: " +dmNhanVien.MaSo+". Đội máy: "+dmNhanVien.MaDV;
            nk.Createddate = DateTime.Now;
            nk.Createdby = dmNhanVien.ModifyBy;
            nk.CreatedName = dmNhanVien.ModifyBy;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return dmNhanVien;
        }
    }
}
