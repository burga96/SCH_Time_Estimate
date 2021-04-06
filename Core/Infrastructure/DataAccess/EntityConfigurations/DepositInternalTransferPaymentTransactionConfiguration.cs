using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EntityConfigurations
{
    public class DepositInternalTransferPaymentTransactionConfiguration : IEntityTypeConfiguration<DepositInternalTransferPaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<DepositInternalTransferPaymentTransaction> builder)
        {
            builder.HasBaseType<PaymentTransaction>();
        }
    }
}