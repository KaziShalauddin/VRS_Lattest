using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
    public class EmployeeRequestVm
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

        //public int EmployeeId { get; set; }
        //public Employee Employee { get; set; }

        [Display(Name = "Is Canceled")]
        public string IsCanceledText => IsCanceled ? "Yes" : "No";
    }
}