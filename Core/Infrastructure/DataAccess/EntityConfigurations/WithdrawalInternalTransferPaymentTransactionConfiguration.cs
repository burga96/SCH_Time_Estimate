using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EntityConfigurations
{
    public class WithdrawalInternalTransferPaymentTransactionConfiguration : IEntityTypeConfiguration<WithdrawalInternalTransferPaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<WithdrawalInternalTransferPaymentTransaction> builder)
        {
            builder.HasBaseType<PaymentTransaction>();
        }
    }
}