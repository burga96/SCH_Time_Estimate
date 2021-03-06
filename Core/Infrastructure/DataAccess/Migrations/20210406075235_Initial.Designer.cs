// <auto-generated />
using System;
using Core.Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(TimeEstimateDBContext))]
    [Migration("20210406075235_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Domain.Entities.PaymentTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentTransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WalletId");

                    b.ToTable("PaymentTransactions");

                    b.HasDiscriminator<string>("PaymentTransactionType").HasValue("PaymentTransaction");
                });

            modelBuilder.Entity("Core.Domain.Entities.SupportedBank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SupportedBanks");
                });

            modelBuilder.Entity("Core.Domain.Entities.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CurrentAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalIndexNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SupportedBankId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupportedBankId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("Core.Domain.Entities.DepositInternalTransferPaymentTransaction", b =>
                {
                    b.HasBaseType("Core.Domain.Entities.PaymentTransaction");

                    b.Property<string>("InternalTransferId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SecondWalletId")
                        .HasColumnType("int");

                    b.HasIndex("SecondWalletId");

                    b.HasDiscriminator().HasValue("DepositInternalTransferPaymentTransaction");
                });

            modelBuilder.Entity("Core.Domain.Entities.DepositPaymentTransaction", b =>
                {
                    b.HasBaseType("Core.Domain.Entities.PaymentTransaction");

                    b.HasDiscriminator().HasValue("DepositPaymentTransaction");
                });

            modelBuilder.Entity("Core.Domain.Entities.FeeInternalTransferPaymentTransaction", b =>
                {
                    b.HasBaseType("Core.Domain.Entities.PaymentTransaction");

                    b.Property<string>("InternalTransferId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SecondWalletId")
                        .HasColumnType("int");

                    b.HasIndex("SecondWalletId");

                    b.HasDiscriminator().HasValue("FeeInternalTransferPaymentTransaction");
                });

            modelBuilder.Entity("Core.Domain.Entities.WithdrawalInternalTransferPaymentTransaction", b =>
                {
                    b.HasBaseType("Core.Domain.Entities.PaymentTransaction");

                    b.Property<string>("InternalTransferId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SecondWalletId")
                        .HasColumnType("int");

                    b.HasIndex("SecondWalletId");

                    b.HasDiscriminator().HasValue("WithdrawalInternalTransferPaymentTransaction");
                });

            modelBuilder.Entity("Core.Domain.Entities.WithdrawalPaymentTransaction", b =>
                {
                    b.HasBaseType("Core.Domain.Entities.PaymentTransaction");

                    b.HasDiscriminator().HasValue("WithdrawalPaymentTransaction");
                });

            modelBuilder.Entity("Core.Domain.Entities.PaymentTransaction", b =>
                {
                    b.HasOne("Core.Domain.Entities.Wallet", "Wallet")
                        .WithMany("PaymentTransactions")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.Entities.Wallet", b =>
                {
                    b.HasOne("Core.Domain.Entities.SupportedBank", "SupportedBank")
                        .WithMany()
                        .HasForeignKey("SupportedBankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Core.Domain.ValueObjects.PersonalData", "PersonalData", b1 =>
                        {
                            b1.Property<int>("WalletId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LastName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("WalletId");

                            b1.ToTable("Wallets");

                            b1.WithOwner()
                                .HasForeignKey("WalletId");
                        });

                    b.OwnsOne("Core.Domain.ValueObjects.UniqueMasterCitizenNumber", "UniqueMasterCitizenNumber", b1 =>
                        {
                            b1.Property<int>("WalletId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(450)");

                            b1.HasKey("WalletId");

                            b1.HasIndex("Value")
                                .IsUnique()
                                .HasFilter("[UniqueMasterCitizenNumber_Value] IS NOT NULL");

                            b1.ToTable("Wallets");

                            b1.WithOwner()
                                .HasForeignKey("WalletId");
                        });
                });

            modelBuilder.Entity("Core.Domain.Entities.DepositInternalTransferPaymentTransaction", b =>
                {
                    b.HasOne("Core.Domain.Entities.Wallet", "SecondWallet")
                        .WithMany()
                        .HasForeignKey("SecondWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.Entities.FeeInternalTransferPaymentTransaction", b =>
                {
                    b.HasOne("Core.Domain.Entities.Wallet", "SecondWallet")
                        .WithMany()
                        .HasForeignKey("SecondWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.Entities.WithdrawalInternalTransferPaymentTransaction", b =>
                {
                    b.HasOne("Core.Domain.Entities.Wallet", "SecondWallet")
                        .WithMany()
                        .HasForeignKey("SecondWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
