using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Request
    {
        public int Id { get; set; }


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
        public string EmpIdNo { get; set; }
        //public Employee Employee { get; set; }

        public int? ConfigurationId { get; set; }
        public Configuration Configuration { get; set; }

        //public int? EmployeeId { get; set; }
        //public Employee Employee { get; set; }
        //public int? ConfigurationId { get; set; }
        //public Configuration Configuration { get; set; }

        //public int? AssignedRequisitionId { get; set; }
        //public AssignedRequisition AssignedRequisition { get; set; }
    }
}
