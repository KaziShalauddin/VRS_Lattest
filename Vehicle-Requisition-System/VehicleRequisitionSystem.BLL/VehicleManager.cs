using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;
using VehicleRequisitionSystem.Repositories;

namespace VehicleRequisitionSystem.BLL
{
    public class VehicleManager
    {
        VehicleRepository repository = new VehicleRepository();
        public bool Add(Vehicle vehicle)
        {
            if (string.IsNullOrEmpty(vehicle.BrandName))
            {
                throw new Exception("Vehicle Brand Name is not provided!");
            }

            if (string.IsNullOrEmpty(vehicle.RegistrationNo))
            {
                throw new Exception("Vehicle Registration No. is not provided!");
            }

            return repository.Add(vehicle);
        }

        public bool Update(Vehicle vehicle)
        {
            return repository.Update(vehicle);
        }

        public bool Remove(Vehicle vehicle)
        {
            return repository.Remove(vehicle);
        }

        public List<Vehicle> GetAll(bool withDeleted = false)
        {
            return repository.GetAll(withDeleted);
        }

        public Vehicle GetById(int id)
        {
            return repository.GetById(id);
        }

        public void Dispose()
        {
            repository.Dispose();
        }
    }
}
