using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.LocationException;
using WebAPI.ExceptionModel.MemberException;
using WebAPI.Model;
using WebAPI.Model.Constant;
using WebAPI.Model.Location.District;
using WebAPI.Model.Location.Province;
using WebAPI.Model.Location.Ward;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public LocationController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-provinces")]
        public IActionResult GetAllProvinces()
        {
            Res res = new Res();
            try
            {
                var provinces = _context.Province;
                if (provinces==null)
                    throw new ProvinceNotFoundException();
                var list=new List<ProvinceRes>();
                foreach (var p in provinces)
                {
                    var province=new ProvinceRes();
                    try
                    {
                        province.Id = p.Id;
                        province.Code = p.Code;
                        province.Name = p.Name;
                    }
                    catch (Exception e) { }
                    list.Add(province);
                }

                return HandleSuccess(list);
            }
            catch (ProvinceNotFoundException e)
            {
                res.Status = LocationStatus.ProvinceNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-province")]
        public IActionResult GetProvince([FromBody] Req<ProvinceReq> req)
        {
            Res res = new Res();
            try
            {
                var province = _context.Province.FirstOrDefault(p => p.Code == req.value.Code);
                if (province == null)
                    throw new ProvinceNotFoundException();
                var pro = new ProvinceRes();
                try
                {
                    pro.Id = province.Id;
                    pro.Code = province.Code;
                    pro.Name = province.Name;
                }
                catch (Exception e)
                {
                }

                return HandleSuccess(pro);
            }
            catch (ProvinceNotFoundException e)
            {
                res.Status = LocationStatus.ProvinceNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-district-by-province")]
        public IActionResult GetDisstrictByProvince([FromBody]Req<ProvinceReq> req)
        {
            Res res = new Res();
            try
            {
                var province = _context.Province.FirstOrDefault(p => p.Code == req.value.Code);
                if (province == null)
                    throw new ProvinceNotFoundException();

                var districts = _context.District.Where(d => d.ProvinceId == province.Id);

                var list = new List<DistrictRes>();
                foreach (var d in districts)
                {
                    var district = new DistrictRes();
                    try
                    {
                        district.Id = d.Id;
                        district.Code = d.Code;
                        district.Name = d.Name;
                        district.ProvinceName = _context.Province.FirstOrDefault(p => p.Id == d.ProvinceId) != null
                            ? _context.Province.FirstOrDefault(p => p.Id == d.ProvinceId).Name
                            : string.Empty;
                    }
                    catch (Exception e) { }
                    list.Add(district);
                }

                return HandleSuccess(list);
            }
            catch (ProvinceNotFoundException e)
            {
                res.Status = LocationStatus.ProvinceNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-district")]
        public IActionResult GetDistrict([FromBody] Req<DistrictReq> req)
        {
            Res res = new Res();
            try
            {
                var district = _context.District.FirstOrDefault(p => p.Code == req.value.Code);
                if (district == null)
                    throw new DistrictNotFoundException();
                var di = new DistrictRes();
                try
                {
                    di.Id = district.Id;
                    di.Code = district.Code;
                    di.Name = district.Name;
                    di.ProvinceName = _context.Province.FirstOrDefault(p => p.Id == district.ProvinceId) != null
                        ? _context.Province.FirstOrDefault(p => p.Id == district.ProvinceId).Name
                        : string.Empty;
                }
                catch (Exception e)
                {
                }

                return HandleSuccess(di);
            }
            catch (DistrictNotFoundException e)
            {
                res.Status = LocationStatus.DistrictNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-ward-by-district")]
        public IActionResult GetWardByDistrict([FromBody]Req<DistrictReq> req)
        {
            Res res = new Res();
            try
            {
                var district = _context.District.FirstOrDefault(p => p.Code == req.value.Code);
                if (district == null)
                    throw new DistrictNotFoundException();

                var wards = _context.Ward.Where(d => d.DistrictId == district.Id);
                if (wards == null)
                    throw new WardNotFoundException();

                var list = new List<WardRes>();
                foreach (var w in wards)
                {
                    var ward = new WardRes();
                    try
                    {
                        ward.Id = w.Id;
                        ward.Code = w.Code;
                        ward.Name = w.Name;
                        ward.DistrictName = _context.District.FirstOrDefault(d => d.Id ==w.DistrictId) != null
                            ? _context.District.FirstOrDefault(d => d.Id == w.DistrictId).Name
                            : string.Empty;
                        var provinceID = _context.District.FirstOrDefault(p => p.Id == w.DistrictId) == null
                            ? null
                            : _context.District.FirstOrDefault(p => p.Id == w.DistrictId).ProvinceId;
                        ward.ProvinceName = _context.Province.FirstOrDefault(p => p.Id == provinceID) == null
                            ? string.Empty
                            : _context.Province.FirstOrDefault(p => p.Id == provinceID).Name;
                    }
                    catch (Exception e) { }
                    list.Add(ward);
                }

                return HandleSuccess(list);
            }
            catch (DistrictNotFoundException e)
            {
                res.Status = LocationStatus.DistrictNotFound;
                res.Value = e.Message;
            }
            catch (WardNotFoundException e)
            {
                res.Status = LocationStatus.WardNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-ward")]
        public IActionResult GetWard([FromBody] Req<WardReq> req)
        {
            Res res = new Res();
            try
            {
                var ward = _context.Ward.FirstOrDefault(p => p.Code == req.value.Code);
                if (ward == null)
                    throw new WardNotFoundException();
                var wa = new WardRes();
                try
                {
                    wa.Id = ward.Id;
                    wa.Code = ward.Code;
                    wa.Name = ward.Name;
                    wa.DistrictName = _context.District.FirstOrDefault(d => d.Id == ward.DistrictId) != null
                        ? _context.District.FirstOrDefault(d => d.Id == ward.DistrictId).Name
                        : string.Empty;
                    var provinceID = _context.District.FirstOrDefault(p => p.Id == ward.DistrictId) == null
                        ? null
                        : _context.District.FirstOrDefault(p => p.Id == ward.DistrictId).ProvinceId;
                    wa.ProvinceName = _context.Province.FirstOrDefault(p => p.Id == provinceID) == null
                        ? string.Empty
                        : _context.Province.FirstOrDefault(p => p.Id == provinceID).Name;
                }
                catch (Exception e)
                {
                }

                return HandleSuccess(wa);
            }
            catch (WardNotFoundException e)
            {
                res.Status = LocationStatus.WardNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        private IActionResult HandleSuccess(object data)
        {
            return Ok(new Res(data));
        }
        private IActionResult HandleError(string status, string err)
        {
            return Ok(new Res
            {
                Status = status,
                Value = err
            });
        }
    }
}