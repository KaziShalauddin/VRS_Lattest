using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.ViewModels
{
    public class EmployeeRequisitionRequestVM
    {
       
        public string Cause { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Departure")]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime CheckInTime { get; set; }
        public string Status { get; set; }

       
    }
}