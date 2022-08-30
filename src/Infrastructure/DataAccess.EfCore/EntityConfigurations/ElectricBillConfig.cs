using DataAccess.EfCore.EntityConfigurations.Common;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EfCore.EntityConfigurations
{
    public class ElectricBillConfig : CommonEntityTypeConfig<ElectricBill>
    {
        public override void Configure(EntityTypeBuilder<ElectricBill> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(ElectricBill));
        }
    }
}