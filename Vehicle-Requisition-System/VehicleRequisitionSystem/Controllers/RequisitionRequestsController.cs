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
    public class RequisitionRequestsController : Controller
    {
        private VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();

        // GET: RequisitionRequests
        public ActionResult Index()
        {
            var requisitions = db.Requisitions.Include(r => r.AssignedRequisition).Include(r => r.Employee).Include(r => r.Status);
            return View(requisitions.ToList());
        }

        // GET: RequisitionRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionRequest requisitionRequest = db.Requisitions.Include(r => r.Employee).FirstOrDefault(e => e.Id == id);
            if (requisitionRequest == null)
            {
                return HttpNotFound();
            }
            return View(requisitionRequest);
        }

        // GET: RequisitionRequests/Create
        public ActionResult Create()
        {
            ViewBag.AssignedRequisitionId = new SelectList(db.AssignedRequisitions, "Id", "Status");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Description");
            return View();
        }

        // POST: RequisitionRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Location,Persons,DepartureTime,CheckInTime,IsCanceled,EmployeeId,StatusId")] RequisitionRequest requisitionRequest)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            requisitionRequest.UserId = user.Id;
            if (ModelState.IsValid)
            {
                db.Requisitions.Add(requisitionRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedRequisitionId = new SelectList(db.AssignedRequisitions, "Id", "Status", requisitionRequest.AssignedRequisitionId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", requisitionRequest.EmployeeId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Description", requisitionRequest.StatusId);
            return View();
        }

        // GET: RequisitionRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionRequest requisitionRequest = db.Requisitions.Find(id);
            if (requisitionRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedRequisitionId = new SelectList(db.AssignedRequisitions, "Id", "Status", requisitionRequest.AssignedRequisitionId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", requisitionRequest.EmployeeId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Description", requisitionRequest.StatusId);
            return View(requisitionRequest);
        }

        // POST: RequisitionRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Description,Location,Persons,DepartureTime,CheckInTime,IsCanceled,IsDeleted,EmployeeId,StatusId,AssignedRequisitionId")] RequisitionRequest requisitionRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisitionRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedRequisitionId = new SelectList(db.AssignedRequisitions, "Id", "Status", requisitionRequest.AssignedRequisitionId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", requisitionRequest.EmployeeId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Description", requisitionRequest.StatusId);
            return View(requisitionRequest);
        }

        // GET: RequisitionRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionRequest requisitionRequest = db.Requisitions.Find(id);
            if (requisitionRequest == null)
            {
                return HttpNotFound();
            }
            return View(requisitionRequest);
        }

        // POST: RequisitionRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequisitionRequest requisitionRequest = db.Requisitions.Find(id);
            db.Requisitions.Remove(requisitionRequest);
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
