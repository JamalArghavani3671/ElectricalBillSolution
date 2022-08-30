using DataAccess.EfCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.EfCore
{
    public class ElectricalBillAPIContext : DbContext
    {
        public ElectricalBillAPIContext([NotNull] DbContextOptions<ElectricalBillAPIContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ElectricBillConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
