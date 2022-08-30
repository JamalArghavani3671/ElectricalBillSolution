using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.EfCore
{
    public class ElectricalBillAPIContextFactory : IDesignTimeDbContextFactory<ElectricalBillAPIContext>
    {
        public ElectricalBillAPIContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=ARGHAVAN;Initial Catalog=ElectricalBill;" +
                   "Persist Security Info=True;User ID=Arghavani;Password=11111";

            var optionsBuilder = new DbContextOptionsBuilder<ElectricalBillAPIContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ElectricalBillAPIContext(optionsBuilder.Options);
        }
    }
}
