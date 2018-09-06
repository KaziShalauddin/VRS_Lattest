using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Employee
    {
        public int Id { get; set; }
        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public int DesignationId { get; set; }
        public Designation Designation { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public byte[] Image { get; set; }
        public string ImagePath { get; set; }

        public bool IsDriver { get; set; }
        [Display(Name = "Driving License No.")]
        public string DrivingLicenseNo { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Employee Id No.")]
        public string EmpIdNo { get; set; }
        public bool IsDeleted { get; set; }
       

        public List<RequisitionRequest> RequisitionRequests { get; set; } 
    }
}