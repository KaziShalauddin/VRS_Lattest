using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Display(Name = "Vehicle Brand")]
        public string BrandName { get; set; }
        public string Model { get; set; }
        [Display(Name = "Vehicle License No.")]
        public string RegistrationNo { get; set; }
        public string ChesisNo { get; set; }
        public int Capacity { get; set; }

        [Display(Name = "Status")]
        public int Status { get; set; }

        public bool IsDeleted { get; set; }

        public List<AssignedRequisition> AssignedRequisitions { get; set; }
    }
}