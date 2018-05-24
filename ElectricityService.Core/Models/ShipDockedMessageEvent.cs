using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityService.Core.Models
{
	public struct ShipDockedMessageEvent
	{
		public Guid ShipId { get; set; }
	}
}
