using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EntityConfigurations
{
    public class DepositPaymentTransactionConfiguration : IEntityTypeConfiguration<DepositPaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<DepositPaymentTransaction> builder)
        {
            builder.HasBaseType<PaymentTransaction>();
        }
    }
}