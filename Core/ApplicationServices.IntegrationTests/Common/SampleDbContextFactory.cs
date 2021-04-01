using Core.Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.IntegrationTests.Common
{
    public class SampleDbContextFactory : IDesignTimeDbContextFactory<TimeEstimateDBContext>
    {
        public TimeEstimateDBContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<TimeEstimateDBContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=testDB;Trusted_Connection=True;MultipleActiveResultSets=true")
            .Options;

            return new TimeEstimateDBContext(options);
        }
    }
}