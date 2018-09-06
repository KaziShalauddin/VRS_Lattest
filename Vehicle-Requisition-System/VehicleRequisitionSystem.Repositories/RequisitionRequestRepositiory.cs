using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.DBContext;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Repositories
{
    public class RequisitionRequestRepositiory
    {
        VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();
        public bool Add(RequisitionRequest request)
        {
            db.Requisitions.Add(request);
            return db.SaveChanges() > 0;
        }

        public bool Update(RequisitionRequest request)
        {
            db.Requisitions.Attach(request);
            db.Entry(request).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool Remove(RequisitionRequest request)
        {
            request.IsDeleted = true;
            return Update(request);

        }

        public List<RequisitionRequest> GetAll(bool withDeleted = false)
        {
            return db.Requisitions.Where(c => c.IsDeleted == withDeleted).ToList();
        }

        public RequisitionRequest GetById(int id)
        {
            return db.Requisitions.FirstOrDefault(c => c.Id == id);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
