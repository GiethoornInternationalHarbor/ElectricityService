using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElectricityService.Core.Models
{
	public class ServiceRequest
	{
		[Required]
		public Guid ServiceId { get; set; }
		[Required]
		public Guid ShipId { get; set; }

		/// <summary>
		/// Gets or sets the customer identifier.
		/// </summary>
		public Guid CustomerId { get; set; }
	}
}
