using Core.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.IntegrationTests.Common
{
    [TestClass]
    public class IntegrationSetUp
    {
        [AssemblyInitialize()]
        public static async Task AssemblyInit(TestContext context)
        {
            var dbContextFactory = new SampleDbContextFactory();
            using (var dbContext = dbContextFactory.CreateDbContext(new string[] { }))
            {
                await dbContext.Database.EnsureCreatedAsync();
                dbContext.Database.BeginTransaction();
                dbContext.SaveChanges();
                SupportedBank supportedBankAPI = new SupportedBank("Banca Intesa");
                dbContext.SupportedBanks.Add(supportedBankAPI);
                SupportedBank bankWithNoAPI = new SupportedBank("Random");
                dbContext.SupportedBanks.Add(bankWithNoAPI);
                dbContext.SaveChanges();
                dbContext.Database.CommitTransaction();
            }
        }

        [AssemblyCleanup()]
        public static async Task AssemblyCleanup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            using (var dbContext = dbContextFactory.CreateDbContext(new string[] { }))
            {
                await dbContext.Database.EnsureDeletedAsync();
            }
        }
    }
}