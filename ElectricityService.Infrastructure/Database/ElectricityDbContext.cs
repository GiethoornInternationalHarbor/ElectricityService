using Microsoft.EntityFrameworkCore;
using ElectricityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityService.Infrastructure.Database
{
    public class ElectricityDbContext : DbContext
    {
        public DbSet<Ship> Ships { get; set; }

        public ElectricityDbContext(DbContextOptions<ElectricityDbContext> options) : base(options)
        {
        }
    }
}
