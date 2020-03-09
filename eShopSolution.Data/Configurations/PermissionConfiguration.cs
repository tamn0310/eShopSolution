using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.AppRole).WithMany(x => x.Permission).HasForeignKey(x => x.RodeId);
            builder.HasOne(x => x.Function).WithMany(x => x.Permission).HasForeignKey(x => x.FunctionId);
            builder.HasOne(x => x.Action).WithMany(x => x.Permission).HasForeignKey(x => x.ActionId);
        }
    }
}