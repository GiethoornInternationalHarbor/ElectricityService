using ElectricityService.Core.Messaging;
using ElectricityService.Core.Models;
using ElectricityService.Core.Services;
using System.Threading.Tasks;
using Utf8Json;

namespace ElectricityService.App.Messaging
{
	public class ElectricityEventHandler : IEventHandlerCallback
	{
		private readonly IElectricityService _electricityService;

		public ElectricityEventHandler(IElectricityService electricityService)
		{
			_electricityService = electricityService;
		}

		public async Task<bool> HandleEventAsync(EventTypes eventType, string message)
		{
			switch (eventType)
			{
				case EventTypes.ShipDocked:
					{
						return await HandleShipDocked(message);
					}
				case EventTypes.ShipUndocked:
					{

						return await HandleShipUndocked(message);
					}
				case EventTypes.ServiceRequested:
					{
						return await HandleServiceRequested(message);
					}
				case EventTypes.Unknown:
					{
						return true;
					}
			}

			return true;
		}

		private async Task<bool> HandleShipUndocked(string message)
		{

			ShipDockedMessageEvent receivedShip = JsonSerializer.Deserialize<ShipDockedMessageEvent>(message);
			await _electricityService.DeleteShipAsync(receivedShip.ShipId);
			return true;
		}

		private async Task<bool> HandleShipDocked(string message)
		{
			//1. Deserialize ship
			ShipDockedMessageEvent receivedShip = JsonSerializer.Deserialize<ShipDockedMessageEvent>(message);
			//2. Dump ship in db
			Ship createdShip = await _electricityService.CreateShipAsync(new Ship() { Id = receivedShip.ShipId });
			return true;
		}

		private async Task<bool> HandleServiceRequested(string message)
		{

			//musing servicerequest model now
			var receivedShipService = JsonSerializer.Deserialize<ServiceRequest>(message);

			//check if the service that is requested actually is electricityling.
			if (receivedShipService.ServiceId == ShipServiceConstants.ElectricityId)
			{
				Ship existingShip = await _electricityService.GetShipAsync(receivedShipService.ShipId);

				//check if ship is in our DB and thus is docked
				if (existingShip != null)
				{

					await _electricityService.Electricity(existingShip);

					//call the overload method
					Task.Run(() => _electricityService.SendServiceCompletedAsync(receivedShipService));

				}
			}
			return true;
		}

	}
}