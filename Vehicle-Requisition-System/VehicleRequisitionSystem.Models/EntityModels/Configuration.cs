using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Configuration
    {
        public int Id { get; set; }
        [Display(Name = "Configuration Status")]
        public string Name { get; set; }

        public int ConfigurationTypeId { get; set; }
        public ConfigurationType ConfigurationType { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
