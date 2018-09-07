using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using VehicleRequisitionSystem.Models;
using VehicleRequisitionSystem.Models.DBContext;
using VehicleRequisitionSystem.Models.EntityModels;
using VehicleRequisitionSystem.Models.ViewModels;

namespace VehicleRequisitionSystem.Controllers
{
    public class RequestsController : Controller
    {
        private VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();

        // GET: Requests
        public ActionResult Index()
        {
            GetCompletedRequisitionList();
            List<RequestListVM> rsList = new List<RequestListVM>();
            var requestList = db.Requests.Include(e => e.Configuration).ToList();
            foreach (var item in requestList)
            {
                var employee = db.Employees.Include(e => e.Department).Include(e => e.Designation).Where(e => e.EmpIdNo == item.EmpIdNo).ToList();
                var employeeName = employee.Select(e => e.Name).FirstOrDefault();
                var department = employee.Select(e => e.Department.Name).FirstOrDefault();
                var designation = employee.Select(e => e.Designation.Name).FirstOrDefault();

                RequestListVM rs = new RequestListVM();
                rs.Id = item.Id;
                rs.Description = item.Description;
                rs.Location = item.Location;
                rs.Persons = item.Persons;
                rs.DepartureTime = item.DepartureTime;
                rs.CheckInTime = item.CheckInTime;
                rs.EmpIdNo = item.EmpIdNo;
                rs.Name = employeeName;
                rs.Designation = designation;
                rs.Configuration = item.Configuration.Name;
                rs.Department = department;
                rs.IsCanceled = item.IsCanceled;
                rsList.Add(rs);
            }
            //var requests = db.Requests.Include(r => r.Configuration).Include(r => r.Employee).Include(r => r.Employee.Department).Include(r => r.Employee.Designation);
            return View(rsList);
        }

        private void GetCompletedRequisitionList()
        {
            var requestList = db.Requests.Include(e => e.Configuration).ToList();
            foreach (var item in requestList)
            {
                var today = DateTime.Today;
                if (item.CheckInTime<today){

                    item.ConfigurationId = 3;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public ActionResult MyRequestList()
        {
            var empIdNo = GetPresentUserEmpIdNo();
            var requests = db.Requests.Include(r => r.Configuration).Where(e => e.EmpIdNo == empIdNo);
            return View(requests.ToList());
        }

        private string GetPresentUserEmpIdNo()
        {
            ApplicationUser user =
                System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var empIdNo = db.Employees.Where(e => e.UserId == user.Id).Select(e => e.EmpIdNo).FirstOrDefault();
            return empIdNo;
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = SetRequestDetails(id);


            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        public ActionResult AssignedDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedRequest assignedRequest = db.AssignedRequests.Include(e=>e.Employee).Include(e=>e.Vehicle).FirstOrDefault(e => e.RequestId == id);
            if (assignedRequest == null)
            {
                return HttpNotFound();
            }
            return View(assignedRequest);
        }
        private RequestListVM SetRequestDetails(int? id)
        {
            Request rs = db.Requests.Find(id);
            var employee =
                db.Employees.Include(e => e.Department)
                    .Include(e => e.Designation)
                    .Where(e => e.EmpIdNo == rs.EmpIdNo)
                    .ToList();
            var employeeName = employee.Select(e => e.Name).FirstOrDefault();
            var department = employee.Select(e => e.Department.Name).FirstOrDefault();
            var designation = employee.Select(e => e.Designation.Name).FirstOrDefault();
            var status = db.Configurations.Where(e => e.Id == rs.ConfigurationId).Select(e => e.Name).FirstOrDefault();
            RequestListVM rsVM = new RequestListVM
            {
                Id = rs.Id,
                Description = rs.Description,
                Location = rs.Location,
                Persons = rs.Persons,
                DepartureTime = rs.DepartureTime,
                CheckInTime = rs.CheckInTime,
                EmpIdNo = rs.EmpIdNo,
                Name = employeeName,
                Designation = designation,
                Configuration = status,
                Department = department,
                IsCanceled = rs.IsCanceled
            };
            return rsVM;
        }
        // GET: Requests/Create
        public ActionResult Create()
        {
            //ViewBag.ConfigurationId = new SelectList(db.Configurations, "Id", "Name");
            //ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestEntryVM requestEntry)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                Request request = new Request();
                request.UserId = user.Id;
                request.Description = requestEntry.Description;
                request.Location = requestEntry.Location;
                request.Persons = requestEntry.Persons;
                request.DepartureTime = requestEntry.DepartureTime;
                request.CheckInTime = requestEntry.CheckInTime;
                if (requestEntry.RequestFor)
                {
                    request.EmpIdNo = requestEntry.EmployeeId;
                }
                else
                {
                    request.EmpIdNo = GetPresentUserEmpIdNo();
                }
                request.ConfigurationId = 1;
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View();
        }

        // GET: Requests/Edit/5
        //Request Edit by Controller
        public ActionResult ControllerEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ControllerEdit([Bind(Include = "Id,Description,Location,Persons,DepartureTime,CheckInTime,IsCanceled,EmpIdNo")] Request request)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                request.UserId = user.Id;
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(request);
        }
        //Request Edit by Controller
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Location,Persons,DepartureTime,CheckInTime,IsCanceled")] Request request)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                request.UserId = user.Id;
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyRequestList");
            }
            return View(request);
        }
        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            if (request != null)
            {
                request.IsDeleted = true;
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }


}
