﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.DBContext;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Repositories
{
    public class VehicleRepository
    {
        VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();
        public bool Add(Vehicle vehicle)
        {
            db.Vehicles.Add(vehicle);
            return db.SaveChanges() > 0;
        }

        public bool Update(Vehicle vehicle)
        {
            db.Vehicles.Attach(vehicle);
            db.Entry(vehicle).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool Remove(Vehicle vehicle)
        {
            vehicle.IsDeleted = true;
            return Update(vehicle);

        }

        public List<Vehicle> GetAll(bool withDeleted = false)
        {
            return db.Vehicles.Where(c => c.IsDeleted == withDeleted).ToList();
        }

        public Vehicle GetById(int id)
        {
            return db.Vehicles.FirstOrDefault(c => c.Id == id);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }


}
