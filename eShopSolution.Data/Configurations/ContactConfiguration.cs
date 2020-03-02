﻿using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired(true);
            builder.Property(x => x.Email).HasMaxLength(70).IsRequired(true);
            builder.Property(x => x.PhoneNumber).HasMaxLength(30).IsRequired(true);
            builder.Property(x => x.Message).IsRequired(true);
        }
    }
}