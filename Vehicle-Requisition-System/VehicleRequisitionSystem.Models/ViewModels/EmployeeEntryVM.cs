using System.ComponentModel.DataAnnotations;
using System.Web;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
    public class EmployeeEntryVM
    {

        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public HttpPostedFileWrapper ImageFile { get; set; }
        public string ImagePath { get; set; }

        public bool IsDriver { get; set; }
        [Display(Name = "Driving License No.")]
        public string DrivingLicenseNo { get; set; }

    }
}