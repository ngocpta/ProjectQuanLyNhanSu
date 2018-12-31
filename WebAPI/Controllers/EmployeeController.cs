using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.DepartmentException;
using WebAPI.ExceptionModel.EmployeeException;
using WebAPI.ExceptionModel.PostionException;
using WebAPI.ExceptionModel.SpecializeException;
using WebAPI.Model;
using WebAPI.Model.Constant;
using WebAPI.Model.Department;
using WebAPI.Model.Employee;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public EmployeeController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-employees")]
        public IActionResult GetAllEmployees()
        {
            Res res = new Res();
            try
            {
                var employees = _context.Employee;
                if (employees == null)
                    throw new EmployeeNotFoundException();
                var list = new List<EmployeeGetAll>();

                foreach (var em in employees)
                {
                    var employee = new EmployeeGetAll();
               
                    try
                    {
                        employee.Id = em.Id;
                        employee.Active = em.Active;
                        employee.Birthday = em.Birthday.ToString();
                        employee.CurrentAddress = em.CurrentAddress;
                        employee.DayInCompany = em.DayInCompany.ToString();
                        employee.DepartmentId = em.DepartmentId;
                        employee.DepartmentName =
                            _context.Department.FirstOrDefault(x => x.Id == em.DepartmentId) == null
                                ? string.Empty
                                : _context.Department.FirstOrDefault(x => x.Id == em.DepartmentId).Name;
                        employee.EducationLevel = em.EducationLevel;
                        employee.Email = em.Email;
                        employee.Fullname = em.Fullname;
                        employee.Gender = em.Gender;
                        employee.LanguageLevel = em.EducationLevel;
                        employee.PayrollDay = em.PayrollDay.ToString();
                        employee.Phone = em.Phone;
                        employee.PositionId = em.PositionId;
                        employee.PositionName = _context.Position.FirstOrDefault(x => x.Id == em.PositionId) == null
                            ? string.Empty
                            : _context.Position.FirstOrDefault(x => x.Id == em.PositionId).Name;
                        employee.SpecializeId = em.SpecializeId = em.SpecializeId;
                        employee.SpecializeName =
                            _context.Specialize.FirstOrDefault(x => x.Id == em.SpecializeId) == null
                                ? string.Empty
                                : _context.Specialize.FirstOrDefault(x => x.Id == em.SpecializeId).Name;
                        employee.WardId = em.WardId;
                        employee.WardName = _context.Ward.FirstOrDefault(x => x.Id == em.WardId) == null
                            ? string.Empty
                            : _context.Ward.FirstOrDefault(x => x.Id == em.WardId).Name;
                        var districtId = _context.Ward.FirstOrDefault(x => x.Id == em.WardId) == null
                            ? null
                            : _context.Ward.FirstOrDefault(x => x.Id == em.WardId).DistrictId;
                        employee.DistricName = _context.District.FirstOrDefault(x => x.Id == districtId).Name;
                        var provinceId = _context.District.FirstOrDefault(x => x.Id == districtId) == null
                            ? null
                            : _context.District.FirstOrDefault(x => x.Id == districtId).ProvinceId;
                        employee.ProvinceName = _context.Province.FirstOrDefault(x => x.Id == provinceId).Name;
                    }
                    catch (Exception e) { }
                    list.Add(employee);
                }

                return HandleSuccess(list);
            }
            catch (EmployeeNotFoundException e)
            {
                res.Status = EmployeeStatus.EmployeeNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-employees-by-department")]
        public IActionResult GetEmployeesByDepartment([FromBody] Req<DepartmentReq> req)
        {
            Res res = new Res();
            try
            {
                var employees = _context.Employee.Where(x => x.DepartmentId == req.value.Id);
                if (employees == null)
                    throw new EmployeeNotFoundException();
                var list = new List<EmployeeGetAll>();

                foreach (var em in employees)
                {
                    var employee = new EmployeeGetAll();

                    try
                    {
                        employee.Id = em.Id;
                        employee.Active = em.Active;
                        employee.Birthday = em.Birthday.ToString();
                        employee.CurrentAddress = em.CurrentAddress;
                        employee.DayInCompany = em.DayInCompany.ToString();
                        employee.DepartmentId = em.DepartmentId;
                        employee.DepartmentName =
                            _context.Department.FirstOrDefault(x => x.Id == em.DepartmentId) == null
                                ? string.Empty
                                : _context.Department.FirstOrDefault(x => x.Id == em.DepartmentId).Name;
                        employee.EducationLevel = em.EducationLevel;
                        employee.Email = em.Email;
                        employee.Fullname = em.Fullname;
                        employee.Gender = em.Gender;
                        employee.LanguageLevel = em.EducationLevel;
                        employee.PayrollDay = em.PayrollDay.ToString();
                        employee.Phone = em.Phone;
                        employee.PositionId = em.PositionId;
                        employee.PositionName = _context.Position.FirstOrDefault(x => x.Id == em.PositionId) == null
                            ? string.Empty
                            : _context.Position.FirstOrDefault(x => x.Id == em.PositionId).Name;
                        employee.SpecializeId = em.SpecializeId = em.SpecializeId;
                        employee.SpecializeName =
                            _context.Specialize.FirstOrDefault(x => x.Id == em.SpecializeId) == null
                                ? string.Empty
                                : _context.Specialize.FirstOrDefault(x => x.Id == em.SpecializeId).Name;
                        employee.WardId = em.WardId;
                        employee.WardName = _context.Ward.FirstOrDefault(x => x.Id == em.WardId) == null
                            ? string.Empty
                            : _context.Ward.FirstOrDefault(x => x.Id == em.WardId).Name;
                        var districtId = _context.Ward.FirstOrDefault(x => x.Id == em.WardId) == null
                            ? null
                            : _context.Ward.FirstOrDefault(x => x.Id == em.WardId).DistrictId;
                        employee.DistricName = _context.District.FirstOrDefault(x => x.Id == districtId).Name;
                        var provinceId = _context.District.FirstOrDefault(x => x.Id == districtId) == null
                            ? null
                            : _context.District.FirstOrDefault(x => x.Id == districtId).ProvinceId;
                        employee.ProvinceName = _context.Province.FirstOrDefault(x => x.Id == provinceId).Name;
                    }
                    catch (Exception e) { }
                    list.Add(employee);
                }

                return HandleSuccess(list);
            }
            catch (EmployeeNotFoundException e)
            {
                res.Status = EmployeeStatus.EmployeeNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-employee")]
        public IActionResult GetEmployee([FromBody] Req<EmployeeReq> req)
        {
            Res res = new Res();
            try
            {
                var em = _context.Employee.FirstOrDefault(p => p.Id == req.value.Id);
                if (em == null)
                    throw new EmployeeNotFoundException();
                var employee = new EmployeeRes();
                try
                {
                    employee.Id = em.Id;
                    employee.Active = em.Active;
                    employee.Birthday = em.Birthday.ToString();
                    employee.CurrentAddress = em.CurrentAddress;
                    employee.DayInCompany = em.DayInCompany.ToString();
                    employee.DepartmentId = em.DepartmentId;
                    employee.DepartmentName =
                        _context.Department.FirstOrDefault(x => x.Id == em.DepartmentId) == null
                            ? string.Empty
                            : _context.Department.FirstOrDefault(x => x.Id == em.DepartmentId).Name;
                    employee.EducationLevel = em.EducationLevel;
                    employee.Email = em.Email;
                    employee.Fullname = em.Fullname;
                    employee.Gender = em.Gender;
                    employee.LanguageLevel = em.EducationLevel;
                    employee.PayrollDay = em.PayrollDay.ToString();
                    employee.Phone = em.Phone;
                    employee.PositionId = em.PositionId;
                    employee.PositionName = _context.Position.FirstOrDefault(x => x.Id == em.PositionId) == null
                        ? string.Empty
                        : _context.Position.FirstOrDefault(x => x.Id == em.PositionId).Name;
                    employee.SpecializeId = em.SpecializeId = em.SpecializeId;
                    employee.SpecializeName =
                        _context.Specialize.FirstOrDefault(x => x.Id == em.SpecializeId) == null
                            ? string.Empty
                            : _context.Specialize.FirstOrDefault(x => x.Id == em.SpecializeId).Name;
                    employee.WardId = em.WardId;
                    employee.WardName = _context.Ward.FirstOrDefault(x => x.Id == em.WardId) == null
                        ? string.Empty
                        : _context.Ward.FirstOrDefault(x => x.Id == em.WardId).Name;
                    var districtId = _context.Ward.FirstOrDefault(x => x.Id == em.WardId) == null
                        ? null
                        : _context.Ward.FirstOrDefault(x => x.Id == em.WardId).DistrictId;
                    employee.DistricName = _context.District.FirstOrDefault(x => x.Id == districtId).Name;
                    var provinceId = _context.District.FirstOrDefault(x => x.Id == districtId) == null
                        ? null
                        : _context.District.FirstOrDefault(x => x.Id == districtId).ProvinceId;
                    employee.ProvinceName = _context.Province.FirstOrDefault(x => x.Id == provinceId).Name;

                    employee.EmployeeRalatives = _context.Relatives.Where(r => r.EmployeeId == em.Id).Select(re => new
                        EmployeeRalativesRes
                    {
                        Id = re.Id,
                        WardId = re.WardId,
                        Birthday = re.Birthday.ToString(),
                        EmployeeId = re.EmployeeId,
                        Fullname = re.Fullname,
                        Career = re.Career,
                        PhoneNumber = re.PhoneNumber,
                        Relationship = re.Relationship
                    }).ToList();
                    employee.EmployeeWorkProcesses = _context.WorkProcess.Where(r => r.EmployeeId == em.Id).Select(wo => new
                         EmployeeWorkProcessRes()
                    {
                        Id = wo.Id,
                        EmployeeId = wo.EmployeeId,
                        PhoneNumber = wo.PhoneNumber,
                        Address = wo.Address,
                        CompanyWorkedName = wo.CompanyWorkedName,
                        StartWork = wo.StartWork.ToString(),
                        EndWork = wo.EndWork.ToString()
                    }).ToList();
                }
                catch (Exception e) { }

                return HandleSuccess(employee);
            }
            catch (EmployeeNotFoundException e)
            {
                res.Status = EmployeeStatus.EmployeeNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateEmployee([FromBody] Req<EmployeeModelReq> req)
        {
            Res res = new Res();

            try
            {
                Employee employee = new Employee();
                try
                {
                    var checkCode = _context.Employee.Any(m => m.Id == req.value.Id);
                    if (checkCode)
                        throw new EmployeeAlreadyExistException();

                    employee.Id = "NV" + req.value.DepartmentId + req.value.Id;
                    employee.Active = req.value.Active;
                    if (_context.Department.FirstOrDefault(x => x.Id == req.value.DepartmentId) == null)
                        throw new DepartmentNotFoundException();
                    employee.DepartmentId = req.value.DepartmentId;
                    employee.PayrollDay = Convert.ToDateTime(req.value.PayrollDay);
                    employee.Phone = req.value.Phone;
                    if (_context.Position.FirstOrDefault(x => x.Id == req.value.PositionId) == null)
                        throw new PositionNotFoundException();
                    employee.PositionId = req.value.PositionId;
                    if (_context.Specialize.FirstOrDefault(x=>x.Id==req.value.SpecializeId)==null)
                        throw new SpecializeNotFoundException();
                    employee.SpecializeId = req.value.SpecializeId;
                    employee.WardId = req.value.WardId;
                    employee.Birthday = Convert.ToDateTime(req.value.Birthday);
                    employee.CurrentAddress = req.value.CurrentAddress;
                    employee.DayInCompany = Convert.ToDateTime(req.value.DayInCompany);
                    employee.Email = req.value.Email;
                    employee.EducationLevel = req.value.EducationLevel;
                    employee.Fullname = req.value.Fullname;
                    employee.Gender = req.value.Gender;
                    employee.LanguageLevel = req.value.LanguageLevel;

                    _context.Employee.Add(employee);
                    _context.SaveChanges();

                }

                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value = "SUCCESS CREATED !");
            }
            catch (EmployeeAlreadyExistException e)
            {
                res.Status = EmployeeStatus.EmployeeAlreadyExist;
                res.Value = e.Message;
            }
            catch (DepartmentNotFoundException e)
            {
                res.Status = DepartmentStatus.DepartmentNotFound;
                res.Value = e.Message;
            }
            catch (SpecializeNotFoundException e)
            {
                res.Status = SpecializeStatus.SpecializeNotFound;
                res.Value = e.Message;
            }
            catch (PositionNotFoundException e)
            {
                res.Status = PositionStatus.PositionNotFound;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateEmployee([FromBody] Req<EmployeeModelReq> req)
        {
            Res res = new Res();

            try
            {
                var employee = _context.Employee.FirstOrDefault(m => m.Id == req.value.Id);
                if (employee==null)
                    throw new EmployeeNotFoundException();
                try
                {
                    employee.Active = req.value.Active;
                    if (_context.Department.FirstOrDefault(x => x.Id == req.value.DepartmentId) == null)
                        throw new DepartmentNotFoundException();
                    employee.DepartmentId = req.value.DepartmentId;
                    employee.PayrollDay = Convert.ToDateTime(req.value.PayrollDay);
                    employee.Phone = req.value.Phone;
                    if (_context.Position.FirstOrDefault(x => x.Id == req.value.PositionId) == null)
                        throw new PositionNotFoundException();
                    employee.PositionId = req.value.PositionId;
                    if (_context.Specialize.FirstOrDefault(x => x.Id == req.value.SpecializeId) == null)
                        throw new SpecializeNotFoundException();
                    employee.SpecializeId = req.value.SpecializeId;
                    employee.WardId = req.value.WardId;
                    employee.Birthday = Convert.ToDateTime(req.value.Birthday);
                    employee.CurrentAddress = req.value.CurrentAddress;
                    employee.DayInCompany = Convert.ToDateTime(req.value.DayInCompany);
                    employee.Email = req.value.Email;
                    employee.EducationLevel = req.value.EducationLevel;
                    employee.Fullname = req.value.Fullname;
                    employee.Gender = req.value.Gender;
                    employee.LanguageLevel = req.value.LanguageLevel;

                    _context.SaveChanges();

                }

                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value = "SUCCESS UPDATED !");
            }
            catch (EmployeeAlreadyExistException e)
            {
                res.Status = EmployeeStatus.EmployeeAlreadyExist;
                res.Value = e.Message;
            }
            catch (DepartmentNotFoundException e)
            {
                res.Status = DepartmentStatus.DepartmentNotFound;
                res.Value = e.Message;
            }
            catch (SpecializeNotFoundException e)
            {
                res.Status = SpecializeStatus.SpecializeNotFound;
                res.Value = e.Message;
            }
            catch (PositionNotFoundException e)
            {
                res.Status = PositionStatus.PositionNotFound;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteEmployee([FromBody] Req<EmployeeReq> req)
        {
            Res res = new Res();
            var employee = _context.Employee.FirstOrDefault(m => m.Id == req.value.Id);
            if (employee == null)
                throw new EmployeeNotFoundException();
            try
            {
                _context.Remove(employee);

                _context.SaveChanges();

                return HandleSuccess(res.Value = "SUCCESS DELETED !");
            }

            catch (EmployeeNotFoundException e)
            {
                res.Status = EmployeeStatus.EmployeeNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }


        private IActionResult HandleSuccess(object data)
        {
            return Ok(new Res(data));
        }
    }
}