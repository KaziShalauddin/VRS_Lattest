using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class AssignedRequisition
    {
        public int Id { get; set; }

       [ Display(Name = "Assigned Requisition Status")]
        public string Status { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int RequisitionRequestId { get; set; }
        public RequisitionRequest RequisitionRequest { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public bool IsDeleted { get; set; }

        public List<RequisitionRequest> RequisitionRequests { get; set; }

    }
}