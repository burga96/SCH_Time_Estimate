using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EntityConfigurations
{
    public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.HasOne(PaymentTransaction => PaymentTransaction.Wallet)
                .WithMany(Wallet => Wallet.PaymentTransactions)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("PaymentTransactions")
                 .HasDiscriminator<string>("PaymentTransactionType")
                 .HasValue<DepositPaymentTransaction>(nameof(DepositPaymentTransaction))
                 .HasValue<WithdrawalPaymentTransaction>(nameof(WithdrawalPaymentTransaction))
                 .HasValue<DepositInternalTransferPaymentTransaction>(nameof(DepositInternalTransferPaymentTransaction))
                 .HasValue<WithdrawalInternalTransferPaymentTransaction>(nameof(WithdrawalInternalTransferPaymentTransaction));
        }
    }
}