using Core.Domain.Entities.Enums;
using Core.Domain.Exceptions;
using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Entities
{
    public class Wallet
    {
        public Wallet()
        {
        }

        public Wallet(string uniqueMasterCitizenNumberValue,
            SupportedBank supportedBank,
            string firstName,
            string lastName,
            string password,
            string postalIndexNumber)
        {
            UniqueMasterCitizenNumber = new UniqueMasterCitizenNumber(uniqueMasterCitizenNumberValue);
            PersonalData = new PersonalData(firstName, lastName);
            SupportedBank = supportedBank;
            Password = password;
            PostalIndexNumber = postalIndexNumber;
            Status = WalletStatus.ACTIVE;
            PaymentTransactions = new List<PaymentTransaction>();
        }

        public int Id { get; private set; }
        public decimal CurrentAmount { get; private set; }
        public string PostalIndexNumber { get; set; }
        public UniqueMasterCitizenNumber UniqueMasterCitizenNumber { get; private set; }
        public PersonalData PersonalData { get; private set; }
        public int SupportedBankId { get; private set; }
        public SupportedBank SupportedBank { get; private set; }
        public string Password { get; private set; }
        public ICollection<PaymentTransaction> PaymentTransactions { get; private set; }
        public WalletStatus Status { get; private set; }

        public bool VerifyPassword(string password)
        {
            return Password.Equals(password);
        }

        public DepositPaymentTransaction MakeDepositTransaction(decimal depositAmount)
        {
            var depositPaymentTransaction = new DepositPaymentTransaction(this, depositAmount);
            PaymentTransactions.Add(depositPaymentTransaction);
            CurrentAmount += depositAmount;
            return depositPaymentTransaction;
        }

        public WithdrawalPaymentTransaction MakeWithdrawalTransaction(decimal withdrawalAmount)
        {
            if (CurrentAmount < withdrawalAmount)
            {
                throw new NotEnoughAmountException();
            }
            CurrentAmount -= withdrawalAmount;
            var withdrawalPaymentTransaction = new WithdrawalPaymentTransaction(this, withdrawalAmount);
            PaymentTransactions.Add(withdrawalPaymentTransaction);
            return withdrawalPaymentTransaction;
        }

        public void ChangePassword(string newPassword)
        {
            bool onlyDigits = newPassword.All(c => char.IsDigit(c));
            if (newPassword.Length != 6 || !onlyDigits)
            {
                throw new InvalidNewPasswordException();
            }
            Password = newPassword;
        }

        public void Activate()
        {
            if (Status != WalletStatus.BLOCKED)
            {
                throw new WalletStatusException("Cannot activate wallet because it is not blocked");
            }
            Status = WalletStatus.ACTIVE;
        }

        public void Block()
        {
            if (Status != WalletStatus.ACTIVE)
            {
                throw new WalletStatusException("Cannot block wallet because it is not active");
            }
            Status = WalletStatus.BLOCKED;
        }
    }
}