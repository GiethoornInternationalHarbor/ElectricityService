using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectricityService.Core.Models
{
    /* requested by Kevin */
    public class ShipService
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Price { get; set; }
    }
}
