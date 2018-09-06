using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleRequisitionSystem.BLL;
using VehicleRequisitionSystem.Models.DBContext;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Controllers
{
    public class VehiclesController : Controller
    {
        // private VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();
        VehicleManager _vehicleManager = new VehicleManager();
        // GET: Vehicles
        public ActionResult Index()
        {
            return View(_vehicleManager.GetAll());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Vehicle vehicle = db.Vehicles.Find(id);
            Vehicle vehicle = _vehicleManager.GetById((int)id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrandName,Model,RegistrationNo,ChesisNo,Capacity")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                //db.Vehicles.Add(vehicle);
                //db.SaveChanges();

                _vehicleManager.Add(vehicle);
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // Vehicle vehicle = db.Vehicles.Find(id);

            Vehicle vehicle = _vehicleManager.GetById((int)id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrandName,Model,RegistrationNo,ChesisNo,Capacity,Status,IsDeleted")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(vehicle).State = EntityState.Modified;
                //db.SaveChanges();

                _vehicleManager.Update(vehicle);
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Vehicle vehicle = db.Vehicles.Find(id);

            Vehicle vehicle = _vehicleManager.GetById((int)id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Vehicle vehicle = db.Vehicles.Find(id);
            //if (vehicle == null)
            //{
            //    return HttpNotFound();
            //}
            //vehicle.IsDeleted = true;
            //db.Entry(vehicle).State = EntityState.Modified;
            //db.SaveChanges();

            Vehicle vehicle = _vehicleManager.GetById(id);
            _vehicleManager.Remove(vehicle);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vehicleManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
