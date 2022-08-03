using System;
using CrimeReport.Models;
using Microsoft.EntityFrameworkCore;
#region
/* https://dotnetcoretutorials.com/2020/05/02/using-azure-cosmosdb-with-net-core-part-2-ef-core/
 * https://docs.microsoft.com/en-us/ef/core/providers/cosmos/?tabs=dotnet-core-cli
 * https://riptutorial.com/ef-core-providers/learn/1000021/azure-cosmos-db
 * https://blog.jeremylikness.com/blog/azure-cosmos-db-with-ef-core-on-blazor-server/
 * https://github.com/JeremyLikness/PlanetaryDocs#quickstart
*/
#endregion
namespace CrimeReport.Data
{
    public class CrimeDbContext: DbContext
    {
        public CrimeDbContext(DbContextOptions<CrimeDbContext> options) : base(options)
        {
        }
        public DbSet<Law> Laws { get; set; }
        public DbSet<Crime> Crimes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Law>( l =>
            {
                l.HasPartitionKey(l => l.PartionKey);
                l.ToContainer("Crime");
                
            });
        }
    }
}

