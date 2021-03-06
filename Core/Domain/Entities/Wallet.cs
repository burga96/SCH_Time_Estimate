using Core.Domain.Entities.Enums;
using Core.Domain.Exceptions;
using Core.Domain.Helpers;
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
            CreatedAt = DateTime.Now;
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

        public void DeletePaymentTransaction(PaymentTransaction paymentTransaction)
        {
            if (!PaymentTransactions.Contains(paymentTransaction))
            {
                return;
            }
            PaymentTransactions.Remove(paymentTransaction);
        }

        public WalletStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public IEnumerable<InternalTransferPaymentTransaction> InternalTransferPaymentTransactions()
        {
            return PaymentTransactions.Where(PaymentTransaction => PaymentTransaction is InternalTransferPaymentTransaction)
                .Select(PaymentTransaction => (InternalTransferPaymentTransaction)PaymentTransaction)
                .ToList();
        }

        public IEnumerable<PaymentTransaction> AllDepositPaymentTransactions()
        {
            return PaymentTransactions.Where(PaymentTransaction =>
                PaymentTransaction.Type == PaymentTransactionType.DEPOSIT ||
                PaymentTransaction.Type == PaymentTransactionType.INTERNAL_TRANSFER_DEPOSIT
            ).ToList();
        }

        public IEnumerable<PaymentTransaction> AllWithdrawalPaymentTransactions()
        {
            return PaymentTransactions.Where(PaymentTransaction =>
                PaymentTransaction.Type == PaymentTransactionType.WITHDRAWAL ||
                PaymentTransaction.Type == PaymentTransactionType.INTERNAL_TRANSFER_WITHDRAWAL
            ).ToList();
        }

        public void AddAmount(decimal amount)
        {
            CurrentAmount += amount;
        }

        public void ChangeCreatedAtDate(DateTime date)
        {
            CreatedAt = date;
        }

        public decimal DepositPaymentTransactionsSum(DateTime date)
        {
            decimal sum = AllDepositPaymentTransactions()
                .Where(PaymentTransaction => PaymentTransaction.IsInSpecificMonth(date))
                .Select(PaymentTransaction => PaymentTransaction.Amount)
                .Sum();
            return sum;
        }

        public decimal WithdrawalPaymentTransactionsSum(DateTime date)
        {
            decimal sum = AllWithdrawalPaymentTransactions()
                .Where(PaymentTransaction => PaymentTransaction.IsInSpecificMonth(date))
                .Select(PaymentTransaction => PaymentTransaction.Amount)
                .Sum();
            return sum;
        }

        public int NumberOfPaymentTransactionOnSpecificMonth(DateTime date)
        {
            int numberOfPaymentTransactionOnSpecificMonth = InternalTransferPaymentTransactions()
                .Where(PaymentTransaction =>
                   PaymentTransaction.DateCreated.Year == date.Year
                   && PaymentTransaction.DateCreated.Month == date.Month
                )
                .Count();
            return numberOfPaymentTransactionOnSpecificMonth;
        }

        public void CheckIfWalletIsBlocked()
        {
            if (Status == WalletStatus.BLOCKED)
            {
                throw new WalletStatusException("Wallet is blocked");
            }
        }

        public bool VerifyPassword(string password)
        {
            return Password.Equals(password);
        }

        public DepositPaymentTransaction MakeDepositTransaction(decimal depositAmount)
        {
            CheckIfWalletIsBlocked();
            bool isLimitExceeded = LimitPaymentTransactionCalculator.IsDepositLimitExceed(this, depositAmount);
            if (isLimitExceeded)
            {
                throw new LimitExceededException();
            }
            var depositPaymentTransaction = new DepositPaymentTransaction(this, depositAmount);
            PaymentTransactions.Add(depositPaymentTransaction);
            CurrentAmount += depositAmount;
            return depositPaymentTransaction;
        }

        public WithdrawalPaymentTransaction MakeWithdrawalTransaction(decimal withdrawalAmount)
        {
            CheckIfWalletIsBlocked();
            bool isLimitExceeded = LimitPaymentTransactionCalculator.IsWithdrawalLimitExceed(this, withdrawalAmount);
            if (isLimitExceeded)
            {
                throw new LimitExceededException();
            }
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
            CheckIfWalletIsBlocked();
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

        public InternalTransferPaymentTransactions MakeInternalTransfer(Wallet toWallet, decimal amount)
        {
            CheckIfWalletIsBlocked();
            toWallet.CheckIfWalletIsBlocked();
            decimal feeAmount = FeeCalculator.CalculateFee(this, amount);
            bool isWithdrawalLimitExceeded = LimitPaymentTransactionCalculator.IsWithdrawalLimitExceed(this, amount);
            if (isWithdrawalLimitExceeded)
            {
                throw new LimitExceededException();
            }
            bool isDepositLimitExceeded = LimitPaymentTransactionCalculator.IsDepositLimitExceed(toWallet, amount);
            if (isDepositLimitExceeded)
            {
                throw new LimitExceededException();
            }
            if (CurrentAmount < amount + feeAmount)
            {
                throw new NotEnoughAmountException();
            }
            string internalTransferId = Guid.NewGuid().ToString();
            var deposit = new DepositInternalTransferPaymentTransaction(toWallet, this, amount, internalTransferId);
            var withdrawal = new WithdrawalInternalTransferPaymentTransaction(this, toWallet, amount, internalTransferId);
            FeeInternalTransferPaymentTransaction feePaymentTransaction = new FeeInternalTransferPaymentTransaction(this, toWallet, feeAmount, internalTransferId);
            this.CurrentAmount -= amount;
            toWallet.CurrentAmount += amount;
            PaymentTransactions.Add(withdrawal);
            toWallet.PaymentTransactions.Add(deposit);
            var internalTransferPaymentTransactions = new InternalTransferPaymentTransactions(deposit, withdrawal, feePaymentTransaction);
            if (internalTransferPaymentTransactions.HasFee())
            {
                this.CurrentAmount -= feeAmount;
                PaymentTransactions.Add(feePaymentTransaction);
            }
            return internalTransferPaymentTransactions;
        }
    }
}