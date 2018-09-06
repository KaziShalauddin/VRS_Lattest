using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using VehicleRequisitionSystem.Models;
using VehicleRequisitionSystem.Models.DBContext;
using VehicleRequisitionSystem.Models.EntityModels;
using VehicleRequisitionSystem.Models.ViewModels;

namespace VehicleRequisitionSystem.Controllers
{
    public class AssignedRequestsController : Controller
    {
        private VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();

        // GET: AssignedRequests
        public ActionResult Index()
        {
            var assignedRequests = db.AssignedRequests.Include(a => a.Employee).Include(a => a.Request).Include(a => a.Vehicle);
            return View(assignedRequests.ToList());
        }

        // GET: AssignedRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedRequest assignedRequest = db.AssignedRequests.Find(id);
            if (assignedRequest == null)
            {
                return HttpNotFound();
            }
            return View(assignedRequest);
        }
        private RequestAssignVM SetRequestDetails(int? id)
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
          
            RequestAssignVM rsVM = new RequestAssignVM
            {
                RequestId = rs.Id,
                Description = rs.Description,
                Location = rs.Location,
                Persons = rs.Persons,
                DepartureTime = rs.DepartureTime,
                CheckInTime = rs.CheckInTime,
                EmpIdNo = rs.EmpIdNo,
                Name = employeeName,
                Designation = designation,
                Department = department
               
            };
            return rsVM;
        }
        // GET: AssignedRequests/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.RequestId = id;
            var model = SetRequestDetails(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            //request.EmpIdNo= requestEntry.Description;
            //request.Location = requestEntry.Location;
            //request.Persons = requestEntry.Persons;
            //request.DepartureTime = requestEntry.DepartureTime;
            //request.CheckInTime = requestEntry.CheckInTime;
            //if (requestEntry.RequestFor)
            //{
            //    request.EmpIdNo = requestEntry.EmployeeId;
            //}
            //else
            //{
            //    request.EmpIdNo = GetPresentUserEmpIdNo();
            //}
            //request.ConfigurationId = 3;
            //db.Requests.Add(request);
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(e => e.IsDriver == true), "Id", "Name");
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "RegistrationNo");
            return View(model);
        }

        // POST: AssignedRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,EmpIdNo,RequestId,EmployeeId,VehicleId")] AssignedRequest assignedRequest)
        public ActionResult Create(AssignedRequest assignedRequest)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            assignedRequest.UserId = user.Id;
            
            if (ModelState.IsValid)
            {
                Request request = db.Requests.Find(assignedRequest.Id);
                if (request != null)
                {
                    request.ConfigurationId = 2;
                    db.Entry(request).State = EntityState.Modified;
                    //db.SaveChanges();
                    assignedRequest.RequestId = request.Id;
                    assignedRequest.EmpIdNo = request.EmpIdNo;
                    db.AssignedRequests.Add(assignedRequest);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees.Where(e => e.IsDriver == true), "Id", "Name", assignedRequest.EmployeeId);

            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "BrandName", assignedRequest.VehicleId);
            return View();
        }

        // GET: AssignedRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedRequest assignedRequest = db.AssignedRequests.Find(id);
            if (assignedRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", assignedRequest.EmployeeId);
            ViewBag.RequestId = new SelectList(db.Requests, "Id", "UserId", assignedRequest.RequestId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "BrandName", assignedRequest.VehicleId);
            return View(assignedRequest);
        }

        // POST: AssignedRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,EmpIdNo,RequestId,EmployeeId,VehicleId,IsDeleted")] AssignedRequest assignedRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignedRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", assignedRequest.EmployeeId);
            ViewBag.RequestId = new SelectList(db.Requests, "Id", "UserId", assignedRequest.RequestId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "BrandName", assignedRequest.VehicleId);
            return View(assignedRequest);
        }

        // GET: AssignedRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedRequest assignedRequest = db.AssignedRequests.Find(id);
            if (assignedRequest == null)
            {
                return HttpNotFound();
            }
            return View(assignedRequest);
        }

        // POST: AssignedRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignedRequest assignedRequest = db.AssignedRequests.Find(id);
            db.AssignedRequests.Remove(assignedRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
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
