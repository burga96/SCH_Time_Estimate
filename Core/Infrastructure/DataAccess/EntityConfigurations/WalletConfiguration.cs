using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EntityConfigurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasOne(Wallet => Wallet.SupportedBank);
            builder.OwnsOne(Wallet => Wallet.PersonalData);
            builder.OwnsOne(Wallet => Wallet.UniqueMasterCitizenNumber);

            builder.Property(Wallet => Wallet.Password).IsRequired();
        }
    }
}