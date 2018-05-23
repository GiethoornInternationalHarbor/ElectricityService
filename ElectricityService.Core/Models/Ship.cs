using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectricityService.Core.Models
{
    public class Ship
    {
        /// <summary>
        /// Gets or sets the guid
        /// </summary>
        [Required]
        [Key]
        public Guid Id { get; set; }
    }
}
