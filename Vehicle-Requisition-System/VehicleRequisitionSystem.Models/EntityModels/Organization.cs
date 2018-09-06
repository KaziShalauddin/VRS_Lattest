using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Organization
    {
        public int Id { get; set; }

        [Display(Name = "Organization")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public List<Department> Departments { get; set; }
    }
}
