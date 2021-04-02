using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EntityConfigurations
{
    public class WithdrawalPaymentTransactionConfiguration : IEntityTypeConfiguration<WithdrawalPaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<WithdrawalPaymentTransaction> builder)
        {
            builder.HasBaseType<PaymentTransaction>();
        }
    }
}