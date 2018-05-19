using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ElectricityService.Core.Models
{
   public class Ship
    {

        [Key]
        public string Serial { get; set; }
        public string Name { get; set; }

        [IgnoreDataMember]
        public ShipService ShipService { get; set; }
    }
}
