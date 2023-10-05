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
    public class XuatCoBaosController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public XuatCoBaosController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetXCoBao")]
        public async Task<ActionResult<IEnumerable<XCoBao>>> GetXCoBao(int thangDT, int namDT, string DonVi, string LoaiMay)
        {
            var query = from cb in _context.CoBao
                        join dt in _context.DoanThong on cb.CoBaoID equals dt.DoanThongID
                        where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == DonVi
                        select cb;
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay).OrderBy(x => x.NgayCB);
            return await query.Select(s => new XCoBao
            {
                CoBaoID = s.CoBaoID,
                CoBaoGoc = s.CoBaoGoc,
                SoCB=s.SoCB,
                DauMayID=s.DauMayID,
                LoaiMayID=s.LoaiMayID,
                DvdmID=s.DvdmID,
                DvdmName=s.DvdmName,
                DvcbID=s.DvcbID,
                DvcbName=s.DvcbName,
                NgayCB=s.NgayCB,
                RutGio=s.RutGio,
                ChatLuong=s.ChatLuong,
                SoLanRaKho=s.SoLanRaKho,
                TaiXe1ID=s.TaiXe1ID,
                TaiXe1Name=s.TaiXe1Name,
                TaiXe1GioLT=s.TaiXe1GioLT,
                PhoXe1ID=s.PhoXe1ID,
                PhoXe1Name=s.PhoXe1Name,
                PhoXe1GioLT=s.PhoXe1GioLT,
                TaiXe2ID=s.TaiXe2ID,
                TaiXe2Name=s.TaiXe2Name,
                TaiXe2GioLT=s.TaiXe2GioLT,
                PhoXe2ID=s.PhoXe2ID,
                PhoXe2Name=s.PhoXe2Name,
                PhoXe2GioLT=s.PhoXe2GioLT,
                TaiXe3ID=s.TaiXe3ID,
                TaiXe3Name=s.TaiXe3Name,
                TaiXe3GioLT=s.TaiXe3GioLT,
                PhoXe3ID=s.PhoXe3ID,
                PhoXe3Name=s.PhoXe3Name,
                PhoXe3GioLT=s.PhoXe3GioLT,
                LenBan=s.LenBan,
                NhanMay=s.NhanMay,
                RaKho=s.RaKho,
                VaoKho=s.VaoKho,
                GiaoMay=s.GiaoMay,
                XuongBan=s.XuongBan,
                NLBanTruoc=s.NLBanTruoc,
                NLThucNhan=s.NLThucNhan,
                NLLinh=s.NLLinh,
                TramNLID=s.TramNLID,
                NLTrongDoan=s.NLTrongDoan,
                NLBanSau=s.NLBanSau,
                SHDT=s.SHDT,
                MaCB=s.MaCB,
                DonDocDuong=s.DonDocDuong,
                DungDocDuong=s.DungDocDuong,
                DungNoMay=s.DungNoMay,
                GhiChu=s.GhiChu

            }).ToListAsync();           
        }
        [HttpGet]
        [Route("GetXCoBaoCT")]
        public async Task<ActionResult<IEnumerable<XCoBaoCT>>> GetXCoBaoCT(int thangDT, int namDT, string DonVi, string LoaiMay)
        {
            var queryCBID = from cb in _context.CoBao
                            join dt in _context.DoanThong on cb.CoBaoID equals dt.DoanThongID
                            where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == DonVi
                            select cb;
            if (LoaiMay != "ALL")
            {
                queryCBID = queryCBID.Where(x => x.LoaiMayID == LoaiMay);
            }
                await queryCBID.ToListAsync();
                var query = from ct in _context.CoBaoCT
                            join cb in queryCBID on ct.CoBaoID equals cb.CoBaoID
                            select ct;            
                
            return await query.Select(s => new XCoBaoCT
            {
                ID=s.ID,
                CoBaoID = s.CoBaoID,
                NgayXP=s.NgayXP,
                GioDen=s.GioDen,
                GioDi=s.GioDi,
                GioDon=s.GioDon,
                MacTauID=s.MacTauID,
                CongTyID=s.CongTyID,
                CongTyName=s.CongTyName,
                CongTacID=s.CongTacID,
                CongTacName=s.CongTacName,
                GaID=s.GaID,
                GaName=s.GaName,
                TuyenID=s.TuyenID,
                TuyenName=s.TuyenName,
                Tan=s.Tan,
                XeTotal=s.XeTotal,
                TanXeRong=s.TanXeRong,
                XeRongTotal=s.XeRongTotal,
                TinhChatID=s.TinhChatID,
                TinhChatName=s.TinhChatName,
                MayGhepID=s.MayGhepID,
                KmAdd=s.KmAdd
            }).ToListAsync();
        }
        [HttpGet]
        [Route("GetXCoBaoDM")]
        public async Task<ActionResult<IEnumerable<XCoBaoDM>>> GetXCoBaoDM(int thangDT, int namDT, string DonVi, string LoaiMay)
        {
            var queryCBID= from cb in _context.CoBao
                           join dt in _context.DoanThong on cb.CoBaoID equals dt.DoanThongID
                           where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == DonVi
                           select cb;
            if (LoaiMay != "ALL")
            {
                queryCBID = queryCBID.Where(x => x.LoaiMayID == LoaiMay);
            }
            await queryCBID.ToListAsync();
            var query = from ct in _context.CoBaoDM
                        join cb in queryCBID on ct.CoBaoID equals cb.CoBaoID
                        select ct;           

            return await query.Select(s => new XCoBaoDM
            {
                ID=s.ID,
                CoBaoID = s.CoBaoID,
                LoaiDauMoID=s.LoaiDauMoID,
                LoaiDauMoName=s.LoaiDauMoName,
                DonViTinh=s.LoaiDauMoName,
                Nhan=s.Nhan,
                Linh=s.Linh,
                MaTram=s.MaTram,
                TenTram=s.TenTram,
                Giao=s.Giao
            }).ToListAsync();
        }
    }
}
