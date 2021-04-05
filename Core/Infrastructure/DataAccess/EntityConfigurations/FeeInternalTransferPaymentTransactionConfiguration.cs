using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EntityConfigurations
{
    public class FeeInternalTransferPaymentTransactionConfiguration : IEntityTypeConfiguration<FeeInternalTransferPaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<FeeInternalTransferPaymentTransaction> builder)
        {
            builder.HasBaseType<PaymentTransaction>();
        }
    }
}