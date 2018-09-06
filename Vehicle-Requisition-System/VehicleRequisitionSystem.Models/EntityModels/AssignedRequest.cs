using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRequisitionSystem.Models.EntityModels
{
   public class AssignedRequest
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string EmpIdNo { get; set; }
      
        public int RequestId { get; set; }
        public Request Request { get; set; }
        [Display(Name = "Driver")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public bool IsDeleted { get; set; }

    }
}
