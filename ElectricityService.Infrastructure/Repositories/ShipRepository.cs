using Microsoft.EntityFrameworkCore;
using ElectricityService.Infrastructure.Database;
using ElectricityService.Core;
using ElectricityService.Core.Models;
using ElectricityService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityService.Infrastructure.Repositories
{
    public class ShipRepository : IShipRepository
    {
        private readonly ElectricityDbContext _database;
        public ShipRepository(ElectricityDbContext database)
        {
            _database = database;
        }

        public async Task DeleteShip(Guid shipId)
        {
            Ship shipToDelete = new Ship() { Id = shipId };
            _database.Entry(shipToDelete).State = EntityState.Deleted;
            await _database.SaveChangesAsync();
        }

        public async Task UpdateShip(Ship value)
        {
            _database.Ships.Update(value);
            await _database.SaveChangesAsync();
        }

        public async Task<Ship> CreateShip(Ship value)
        {
            var newShip = (await _database.Ships.AddAsync(value)).Entity;
            await _database.SaveChangesAsync();

            return newShip;
        }

        public Task<Ship> GetShip(Guid shipId)
        {
            return _database.Ships.LastOrDefaultAsync(r => r.Id == shipId);
        }


    }
}
