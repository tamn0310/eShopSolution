using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.Data.Configurations
{
    public class ActionConfiguration : IEntityTypeConfiguration<Entities.Action>
    {
        public void Configure(EntityTypeBuilder<Entities.Action> builder)
        {
            builder.ToTable("Actions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired(true);
        }
    }
}