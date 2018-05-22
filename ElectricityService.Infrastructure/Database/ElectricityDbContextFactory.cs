using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ElectricityService.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

#if DEBUG
namespace ElectricityService.Infrastructure.Database
{
    public class ElectricityDbContextFactory : IDesignTimeDbContextFactory<ElectricityDbContext>
    {
        public ElectricityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ElectricityDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ElectricityService;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ElectricityDbContext(optionsBuilder.Options);
        }
    }

}
#endif