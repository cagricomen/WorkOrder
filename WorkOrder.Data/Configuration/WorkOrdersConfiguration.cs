using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOrder.Core.Entities;

namespace WorkOrder.Data.Configuration
{
    public class WorkOrdersConfiguration : IEntityTypeConfiguration<WorkOrders>
    {
        public void Configure(EntityTypeBuilder<WorkOrders> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Job).IsRequired();
        }
    }
}
