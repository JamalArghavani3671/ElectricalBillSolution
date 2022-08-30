using DataAccess.EfCore.Repositories.Common;
using Entity.Entities;
using Interface.Repositories;

namespace DataAccess.EfCore.Repositories
{
    public class ElectricBillRepository : BaseRepository<ElectricBill>, IElectricBillRepository
    {
        public ElectricBillRepository(ElectricalBillAPIContext context) : base(context)
        {
        }
    }
}
