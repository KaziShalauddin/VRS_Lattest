using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleRequisitionSystem.Models.DBContext;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Controllers
{
    public class AssignedRequisitionsController : Controller
    {
        private VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();


        // GET: AssignedRequisitions
        public ActionResult Index()
        {
            var assignedRequisitions = db.AssignedRequisitions.Include(a => a.Driver).Include(a => a.Employee).Include(a => a.RequisitionRequest).Include(a => a.Vehicle);
            return View(assignedRequisitions.ToList());
        }
        
        // GET: AssignedRequisitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedRequisition assignedRequisition = db.AssignedRequisitions.Find(id);
            if (assignedRequisition == null)
            {
                return HttpNotFound();
            }
            return View(assignedRequisition);
        }

        // GET: AssignedRequisitions/Create
        public ActionResult Create()
        {
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            ViewBag.RequisitionRequestId = new SelectList(db.Requisitions, "Id", "Cause");
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Status");
            return View();
        }

        // POST: AssignedRequisitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status,EmployeeId,RequisitionRequestId,DriverId,VehicleId,IsDeleted")] AssignedRequisition assignedRequisition)
        {
            if (ModelState.IsValid)
            {
                db.AssignedRequisitions.Add(assignedRequisition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "Name", assignedRequisition.DriverId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", assignedRequisition.EmployeeId);
            ViewBag.RequisitionRequestId = new SelectList(db.Requisitions, "Id", "Cause", assignedRequisition.RequisitionRequestId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Status", assignedRequisition.VehicleId);
            return View(assignedRequisition);
        }

        // GET: AssignedRequisitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedRequisition assignedRequisition = db.AssignedRequisitions.Find(id);
            if (assignedRequisition == null)
            {
                return HttpNotFound();
            }
            List<Status> sList = new List<Status>();
            //Status status=new Status();
            //status.Id = 1;
            //status.Description = "Waiting";
            //sList.Add(status);
            sList.AddRange(
                new[] {
                new Status { Description = "Waiting", Id = 1 },
                new Status { Description = "Assigned", Id = 2 },
                new Status { Description = "Not Assigned", Id = 3 }
                });
            ViewBag.Status = new SelectList(sList, "Id", "Description", assignedRequisition.Status); 
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "Name", assignedRequisition.DriverId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", assignedRequisition.EmployeeId);
            ViewBag.RequisitionRequestId = new SelectList(db.Requisitions, "Id", "Cause", assignedRequisition.RequisitionRequestId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Status", assignedRequisition.VehicleId);
            return View(assignedRequisition);
        }

        // POST: AssignedRequisitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status,EmployeeId,RequisitionRequestId,DriverId,VehicleId,IsDeleted")] AssignedRequisition assignedRequisition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignedRequisition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<Status> sList = new List<Status>();
            //Status status=new Status();
            //status.Id = 1;
            //status.Description = "Waiting";
            //sList.Add(status);
            sList.AddRange(
                new[] {
                new Status { Description = "Waiting", Id = 1 },
                new Status { Description = "Assigned", Id = 2 },
                new Status { Description = "Not Assigned", Id = 3 }
                });
            ViewBag.Status = new SelectList(sList, "Id", "Description", assignedRequisition.Status);
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "Name", assignedRequisition.DriverId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", assignedRequisition.EmployeeId);
            ViewBag.RequisitionRequestId = new SelectList(db.Requisitions, "Id", "Cause", assignedRequisition.RequisitionRequestId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Status", assignedRequisition.VehicleId);
            return View(assignedRequisition);
        }

        // GET: AssignedRequisitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedRequisition assignedRequisition = db.AssignedRequisitions.Find(id);
            if (assignedRequisition == null)
            {
                return HttpNotFound();
            }
            return View(assignedRequisition);
        }

        // POST: AssignedRequisitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignedRequisition assignedRequisition = db.AssignedRequisitions.Find(id);
            db.AssignedRequisitions.Remove(assignedRequisition);
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

    public class Status
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public void Add(int i)
        {
            throw new NotImplementedException();
        }
    }

}
