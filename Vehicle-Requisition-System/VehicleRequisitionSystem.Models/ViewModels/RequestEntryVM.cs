using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
    public class RequestEntryVM
    {
       
        public string Description { get; set; }
        public string Location { get; set; }
        public int Persons { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Departure")]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime CheckInTime { get; set; }
        
        
        [Display(Name = "Request For Other")]
        public bool RequestFor { get; set; }


        public string EmployeeId { get; set; }
        

    }
}
