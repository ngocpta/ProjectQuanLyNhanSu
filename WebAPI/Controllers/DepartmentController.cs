using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.ExceptionModel.DepartmentException;
using WebAPI.ExceptionModel.LocationException;
using WebAPI.Model;
using WebAPI.Model.Constant;
using WebAPI.Model.Department;
using WebAPI.Model.Location.Department;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public DepartmentController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-departments")]
        public IActionResult GetAllDepartments()
        {
            Res res = new Res();
            try
            {
                var departments = _context.Department;
                if (departments==null)
                    throw new DepartmentNotFoundException();
                var list=new List<DepartmentRes>();

                foreach (var d in departments)
                {
                    var de = new DepartmentRes();
                    try
                    {
                        de.Id = d.Id;
                        de.Active = d.Active;
                        de.Name = d.Name;
                        de.PhoneNumber = d.PhoneNumber;
                    }
                    catch (Exception e) { }
                    list.Add(de);
                }

                return HandleSuccess(list);
            }
            catch (DepartmentNotFoundException e)
            {
                res.Status = DepartmentStatus.DepartmentNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-department")]
        public IActionResult GetDepartment([FromBody] Req<DepartmentReq> req)
        {
            Res res = new Res();
            try
            {
                var department = _context.Department.FirstOrDefault(p => p.Id == req.value.Id);
                if (department == null)
                    throw new DepartmentNotFoundException();
                var de=new DepartmentRes();
                try
                {
                    de.Id = department.Id;
                    de.Name = department.Name;
                    de.PhoneNumber = department.PhoneNumber;
                    de.Active = department.Active;
                }
                catch (Exception e) { }

                return HandleSuccess(de);
            }
            catch (DepartmentNotFoundException e)
            {
                res.Status = DepartmentStatus.DepartmentNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateDepartment([FromBody] Req<DepartmentModelReq> req)
        {
            Res res = new Res();

            try
            {
                Department department = new Department();
                try
                {
                    var checkCode = _context.Department.Any(m => m.Id == req.value.Id);
                    if (checkCode)
                        throw new DepartmentAlreadyExistException();

                    department.Id = "P" + Convert.ToString(DateTime.Today.Day) +
                                    Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) +
                                    req.value.Id;
                    department.Active = req.value.Active;
                    department.Name = req.value.Name;
                    department.PhoneNumber = req.value.PhoneNumber;

                    _context.Department.Add(department);
                    _context.SaveChanges();

                }

                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value = "SUCCESS CREATED DEPARTMENT!");
            }
            catch (DepartmentAlreadyExistException e)
            {
                res.Status = DepartmentStatus.DepartmentAlreadyExist;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateDepartment([FromBody] Req<DepartmentModelReq> req)
        {
            Res res = new Res();
            var department = _context.Department.FirstOrDefault(m => m.Id == req.value.Id);
            if (department == null)
                throw new DepartmentNotFoundException();
            try
            {
                department.Active = req.value.Active;
                department.Name = req.value.Name;
                department.PhoneNumber = req.value.PhoneNumber;

                _context.SaveChanges();
                
                return HandleSuccess(res.Value = "SUCCESS UPDATED DEPARTMENT!");
            }

            catch (DepartmentNotFoundException e)
            {
                res.Status = DepartmentStatus.DepartmentNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteDepartment([FromBody] Req<DepartmentReq> req)
        {
            Res res = new Res();
            var department = _context.Department.FirstOrDefault(m => m.Id == req.value.Id);
            if (department == null)
                throw new DepartmentNotFoundException();
            try
            {
                _context.Remove(department);

                _context.SaveChanges();

                return HandleSuccess(res.Value = "SUCCESS DELETED DEPARTMENT!");
            }

            catch (DepartmentNotFoundException e)
            {
                res.Status = DepartmentStatus.DepartmentNotFound;
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