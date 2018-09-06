using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Designation
    {
        public int Id { get; set; }

        [Display(Name = "Designation")]
        public string   Name { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}