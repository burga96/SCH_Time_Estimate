using Core.Domain.Entities.Enums;
using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
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
            string password)
        {
            UniqueMasterCitizenNumber = new UniqueMasterCitizenNumber(uniqueMasterCitizenNumberValue);
            PersonalData = new PersonalData(firstName, lastName);
            SupportedBank = supportedBank;
            Password = password;
            PaymentTransactions = new List<PaymentTransaction>();
        }

        public int Id { get; private set; }
        public decimal CurrentAmount { get; private set; }
        public UniqueMasterCitizenNumber UniqueMasterCitizenNumber { get; private set; }
        public PersonalData PersonalData { get; private set; }
        public int SupportedBankId { get; private set; }
        public SupportedBank SupportedBank { get; private set; }
        public string Password { get; private set; }
        public ICollection<PaymentTransaction> PaymentTransactions { get; private set; }

        public DepositPaymentTransaction MakeDepositTransaction(decimal depositAmount)
        {
            var depositPaymentTransaction = new DepositPaymentTransaction(this, depositAmount);
            PaymentTransactions.Add(depositPaymentTransaction);
            return depositPaymentTransaction;
        }

        public WithdrawalPaymentTransaction MakeWithdrawalTransaction(decimal withdrawalAmount)
        {
            if (CurrentAmount <= withdrawalAmount)
            {
                //todo throw
            }
            var withdrawalPaymentTransaction = new WithdrawalPaymentTransaction(this, withdrawalAmount);
            PaymentTransactions.Add(withdrawalPaymentTransaction);
            return withdrawalPaymentTransaction;
        }
    }
}