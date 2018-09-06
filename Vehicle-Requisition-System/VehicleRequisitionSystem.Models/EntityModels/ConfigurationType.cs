using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRequisitionSystem.Models.EntityModels
{
   public class ConfigurationType
    {
        public int Id { get; set; }
        
        public string Type { get; set; }

        public List<Configuration> Configurations { get; set; }
    }
}
