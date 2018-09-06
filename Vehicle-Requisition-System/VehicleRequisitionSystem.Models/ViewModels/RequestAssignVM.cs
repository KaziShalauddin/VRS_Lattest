using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
   public class RequestAssignVM
    {
        public int RequestId { get; set; }
        [Display(Name = "Employee Id")]
        public string EmpIdNo { get; set; }

        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Designation")]
        public string Designation { get; set; }

        public string Description { get; set; }
        public string Location { get; set; }
        public int Persons { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Departure")]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime CheckInTime { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}
