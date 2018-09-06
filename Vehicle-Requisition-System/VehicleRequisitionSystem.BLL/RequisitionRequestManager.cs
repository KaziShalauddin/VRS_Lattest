using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;
using VehicleRequisitionSystem.Repositories;

namespace VehicleRequisitionSystem.BLL
{
    public class RequisitionRequestManager
    {
        RequisitionRequestRepositiory repository = new RequisitionRequestRepositiory();
        public bool Add(RequisitionRequest request)
        {
            if (string.IsNullOrEmpty(request.Description))
            {
                throw new Exception("Description is not provided!");
            }

            if (string.IsNullOrEmpty(request.Location))
            {
                throw new Exception("Location is not provided!");
            }

            return repository.Add(request);
        }

        public bool Update(RequisitionRequest request)
        {
            return repository.Update(request);
        }

        public bool Remove(RequisitionRequest request)
        {
            return repository.Remove(request);
        }

        public List<RequisitionRequest> GetAll(bool withDeleted = false)
        {
            return repository.GetAll(withDeleted);
        }

        public RequisitionRequest GetById(int id)
        {
            return repository.GetById(id);
        }

        public void Dispose()
        {
            repository.Dispose();
        }
    }
}
