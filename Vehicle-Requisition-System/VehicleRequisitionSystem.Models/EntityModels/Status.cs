using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Status
    {
        public int Id { get; set; }

        [Display(Name = "Status")]
        public string Description { get; set; }
    }
}