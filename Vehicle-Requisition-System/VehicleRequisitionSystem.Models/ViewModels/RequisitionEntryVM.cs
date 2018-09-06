using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
    public class RequisitionEntryVM
    {
        public int Id { get; set; }

        [Display(Name = "Request By")]
        public string UserId { get; set; }

        public string Description { get; set; }
        public string Location { get; set; }
        public int Persons { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Departure")]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime CheckInTime { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = "Request For")]
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        //public int? VehicleId { get; set; }
        //public Vehicle Vehicle { get; set; }

        //public int? DriverId { get; set; }
        //public Driver Driver { get; set; }

        public int? StatusId { get; set; }
        public Status Status { get; set; }

        public int? AssignedRequisitionId { get; set; }
        public AssignedRequisition AssignedRequisition { get; set; }
    }
}