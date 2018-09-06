using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
    public class AdminRequestAssignVm
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Cause { get; set; }
        public string Place { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Departure")]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime CheckInTime { get; set; }

        public bool IsCanceled { get; set; }
        public bool IsAssigned { get; set; }

        public bool IsDeleted { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}