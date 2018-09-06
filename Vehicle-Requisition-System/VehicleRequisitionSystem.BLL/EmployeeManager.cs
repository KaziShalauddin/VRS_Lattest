using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;
using VehicleRequisitionSystem.Repositories;

namespace VehicleRequisitionSystem.BLL
{
    public class EmployeeManager
    {
        EmployeeRepository repository = new EmployeeRepository();
        public bool Add(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name))
            {
                throw new Exception("Employee Name is not provided!");
            }

            if (string.IsNullOrEmpty(employee.Email))
            {
                throw new Exception("Employee Email is not provided!");
            }

            return repository.Add(employee);
        }

        public bool Update(Employee employee)
        {
            return repository.Update(employee);
        }

        public bool Remove(Employee employee)
        {
            return repository.Remove(employee);
        }

        public List<Employee> GetAll(bool withDeleted = false)
        {
            return repository.GetAll(withDeleted);
        }

        public Employee GetById(int id)
        {
            return repository.GetById(id);
        }

        public void Dispose()
        {
            repository.Dispose();
        }
    }
}
