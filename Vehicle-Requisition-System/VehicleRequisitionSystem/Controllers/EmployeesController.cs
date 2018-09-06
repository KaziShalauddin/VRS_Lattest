using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using VehicleRequisitionSystem.Models;
using VehicleRequisitionSystem.Models.DBContext;
using VehicleRequisitionSystem.Models.EntityModels;
using VehicleRequisitionSystem.Models.ViewModel;
using VehicleRequisitionSystem.Models.ViewModels;

namespace VehicleRequisitionSystem.Controllers
{
    public class EmployeesController : Controller
    {
        public VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();
        // GET: Employees
        public ActionResult Index()
        {

            var employees = db.Employees.Include(e => e.Department).Include(e => e.Designation);
            return View(employees.ToList());
        }
        private string SetEmpIdNo(string departmentName)
        {
            if (departmentName == null) throw new ArgumentNullException(nameof(departmentName));
            var deptName = departmentName.Substring(0, 3);
            var countId = db.Employees.Count();
            countId++;
            if (countId <= 9)
            {

                string empIdNo = Convert.ToString(deptName + "-000" + countId);
                return empIdNo;
            }
            if (countId <= 99)
            {
                string empIdNo = Convert.ToString(deptName + "-00" + countId);
                return empIdNo;
            }
            if (countId <= 999)
            {
                string empIdNo = Convert.ToString(deptName + "-0" + countId);
                return empIdNo;
            }
            else
            {
                string empIdNo = Convert.ToString(deptName + countId);
                return empIdNo;
            }
        }
        public ActionResult MyProfile()
        {

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var employeeId = db.Employees.Where(e => e.UserId == user.Id).Select(e=>e.EmpIdNo).FirstOrDefault();
            var employees = db.Employees.Where(e => e.EmpIdNo == employeeId).Include(e => e.Department).Include(e => e.Designation).FirstOrDefault();
            
           
            return View(employees);

        }
        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.Employees.Include(e => e.Department).Include(e => e.Designation).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);

        }

        // GET: Employees/Create
        public ActionResult EmployeeCreate()
        {
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "Id", "Name");
            ViewBag.Designations = new SelectList(db.Designations.ToList(), "Id", "Name");
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "Id", "Name");
            ViewBag.Designations = new SelectList(db.Designations.ToList(), "Id", "Name");
            return View();
        }


        public JsonResult EmployeeJsonCreate(EmployeeEntryVM model)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var departmentName =
                db.Departments.Where(e => e.Id == model.DepartmentId).Select(e => e.Name).FirstOrDefault();
            var empId = SetEmpIdNo(departmentName);

            var file = model.ImageFile;

            byte[] imagebyte = null;


            if (file != null)
            {
                //File Upload & Retrive
                //var fileName = Path.GetFileName(file.FileName);
                //var extention = Path.GetExtension(file.FileName);
                //var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);


                //File Upload & Retrive(both SQL and non-SQL)
                file.SaveAs(Server.MapPath("~/Images/" + file.FileName));

                //File SQL Upload & Retrive
                BinaryReader reader = new BinaryReader(file.InputStream);

                imagebyte = reader.ReadBytes(file.ContentLength);

                Employee img = new Employee();
                img.DepartmentId = model.DepartmentId;
                img.DesignationId = model.DesignationId;
                img.EmpIdNo = empId;
                img.IsDriver = model.IsDriver;
                img.DrivingLicenseNo = model.DrivingLicenseNo;
                img.Address = model.Address;
                img.Email = model.Email;
                img.Phone = model.Phone;
                img.UserId = user.Id;
                img.Name = model.Name;
                img.Image = imagebyte;
                img.ImagePath = "/Images/" + file.FileName;
                img.IsDeleted = false;
                db.Employees.Add(img);
                db.SaveChanges();



            }

            //File Upload & Retrive
            //return Json(file.FileName, JsonRequestBehavior.AllowGet);

            //File SQL Upload & Retrive
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeCreate_V2()
        {
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "Id", "Name");
            ViewBag.Designations = new SelectList(db.Designations.ToList(), "Id", "Name");
            return View();
        }

        public JsonResult EmployeeJsonCreate_V2(EmployeeEntryVm model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user =
                    System.Web.HttpContext.Current.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
                        .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());


                var file = model.ImageFile;

                byte[] imagebyte = null;


                if (file != null)
                {

                    file.SaveAs(Server.MapPath("~/Images/" + file.FileName));

                    BinaryReader reader = new BinaryReader(file.InputStream);

                    imagebyte = reader.ReadBytes(file.ContentLength);

                    Employee img = new Employee();
                    img.DepartmentId = model.DepartmentId;
                    img.DesignationId = model.DesignationId;
                    img.IsDriver = model.IsDriver;
                    img.DrivingLicenseNo = model.DrivingLicenseNo;
                    img.Address = model.Address;
                    img.Email = model.Email;
                    img.Phone = model.Phone;
                    img.UserId = user.Id;
                    img.Name = model.Name;
                    img.Image = imagebyte;
                    img.ImagePath = "/Images/" + file.FileName;
                    img.IsDeleted = false;
                    db.Employees.Add(img);
                    db.SaveChanges();
                }

                return Json(JsonRequestBehavior.AllowGet);
            }
            return Json(model, JsonRequestBehavior.DenyGet);
        }


        public ActionResult DriverList()
        {

            var employees = db.Employees.Include(e => e.Department).Include(e => e.Designation).Where(e => e.IsDriver == true);
            return View(employees.ToList());
        }
        // GET: Employees/Details/5
        public ActionResult DriverDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.Employees.Include(e => e.Department).Include(e => e.Designation).Where(e => e.IsDriver == true).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);

        }
        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
