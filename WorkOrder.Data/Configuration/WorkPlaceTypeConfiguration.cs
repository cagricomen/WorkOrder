using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOrder.Core.Entities;

namespace WorkOrder.Data.Configuration
{
    public class WorkPlaceTypeConfiguration : IEntityTypeConfiguration<WorkPLaceType>
    {
        public void Configure(EntityTypeBuilder<WorkPLaceType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
