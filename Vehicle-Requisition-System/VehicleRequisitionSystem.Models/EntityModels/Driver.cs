using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime CheckInTime { get; set; }
       
        public string Status { get; set; }

        public bool IsDeleted { get; set; }

        public List<AssignedRequisition> AssignedRequisitions { get; set; }
    }
}