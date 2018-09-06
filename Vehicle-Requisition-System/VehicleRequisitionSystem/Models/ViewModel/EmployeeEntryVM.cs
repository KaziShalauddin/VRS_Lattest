using System.ComponentModel.DataAnnotations;
using System.Web;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModel
{
    public class EmployeeEntryVm
    {

        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public int DesignationId { get; set; }
        public Designation Designation { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public HttpPostedFileWrapper ImageFile { get; set; }
        public string ImagePath { get; set; }

        public bool IsDriver { get; set; }
        [RequiredIf("IsDriver", true, ErrorMessage = "This field is required!")]
        [Display(Name = "Driving License No.")]
        public string DrivingLicenseNo { get; set; }

      

        

    }
}