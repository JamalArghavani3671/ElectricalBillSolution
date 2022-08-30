using DataAccess.EfCore;
using DataAccess.EfCore.Repositories;
using Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Services
{
    public static class ElectricalBillApiServiceRegistration
    {
        public static IServiceCollection RegisterPhoneBookApiServices(this IServiceCollection services)
        {
            var connectionString = "Data Source=ARGHAVAN;Initial Catalog=ElectricalBill;" +
                   "Persist Security Info=True;User ID=Arghavani;Password=0830055762";

            services.AddDbContext<ElectricalBillAPIContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IElectricBillRepository, ElectricBillRepository>();

            return services;
        }
    }
}
