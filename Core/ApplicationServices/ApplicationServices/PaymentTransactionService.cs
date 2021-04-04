﻿using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationExceptions;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.Domain.Entities;
using Core.Domain.ExternalInterfaces;
using Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ApplicationServices
{
    public class PaymentTransactionService : IPaymentTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAPIDeterminator _bankAPIDeterminator;
        private readonly string _adminPassword;

        public PaymentTransactionService(IUnitOfWork unitOfWork,
            IBankAPIDeterminator bankAPIDeterminator)
        {
            _bankAPIDeterminator = bankAPIDeterminator;
            _unitOfWork = unitOfWork;
        }

        public async Task<DepositPaymentTransactionDTO> MakeDepositPaymentTransaction(string uniqueMasterCitizenNumberValue,
            string password,
            decimal amount)
        {
            Wallet wallet = await CheckForWallet(uniqueMasterCitizenNumberValue, password);
            IBankAPI bankAPI = _bankAPIDeterminator.DeterminateBankAPI(wallet.SupportedBank);
            bool successWithdrawal = await bankAPI.Withdraw(uniqueMasterCitizenNumberValue, wallet.PostalIndexNumber, amount);
            if (!successWithdrawal)
            {
                throw new BankAPIException("Bank api - failed to withdrawal");
            }
            DepositPaymentTransaction depositPaymentTransaction = wallet.MakeDepositTransaction(amount);
            await _unitOfWork.PaymentTransactionRepository.Insert(depositPaymentTransaction);
            await _unitOfWork.SaveChangesAsync();
            return new DepositPaymentTransactionDTO(depositPaymentTransaction);
        }

        public async Task<WithdrawalPaymentTransactionDTO> MakeWithdrawalPaymentTransaction(string uniqueMasterCitizenNumberValue,
           string password,
           decimal amount)
        {
            Wallet wallet = await CheckForWallet(uniqueMasterCitizenNumberValue, password);
            IBankAPI bankAPI = _bankAPIDeterminator.DeterminateBankAPI(wallet.SupportedBank);
            bool successDeposit = await bankAPI.Deposit(uniqueMasterCitizenNumberValue, wallet.PostalIndexNumber, amount);
            if (!successDeposit)
            {
                throw new BankAPIException("Bank api - failed to deposit");
            }
            WithdrawalPaymentTransaction withdrawalPaymentTransaction = wallet.MakeWithdrawalTransaction(amount);
            await _unitOfWork.PaymentTransactionRepository.Insert(withdrawalPaymentTransaction);
            await _unitOfWork.SaveChangesAsync();
            return new WithdrawalPaymentTransactionDTO(withdrawalPaymentTransaction);
        }

        public async Task<WalletDTO> GetWalletWithFiltertedPaymentTransactionsForUser(string uniqueMasterCitizenNumberValue,
            string password,
            DateTime? from,
            DateTime? to)
        {
            Wallet wallet = await CheckForWallet(uniqueMasterCitizenNumberValue, password);
            List<PaymentTransaction> filteredTransactions = new List<PaymentTransaction>();
            foreach (PaymentTransaction paymentTransaction in wallet.PaymentTransactions)
            {
                if (((from != null && paymentTransaction.DateCreated >= from) || (from == null)) &&
                    ((to != null && paymentTransaction.DateCreated <= to) || (to == null)))
                {
                    filteredTransactions.Add(paymentTransaction);
                }
            }
            WalletDTO walletDTO = new WalletDTO(wallet, filteredTransactions);
            return walletDTO;
        }

        public async Task<IEnumerable<PaymentTransactionDTO>> GetAllPaymentTransactions(DateTime? from, DateTime? to)
        {
            IEnumerable<PaymentTransaction> paymentTransactions = await _unitOfWork.PaymentTransactionRepository
                .GetFilteredList(
                 paymentTransaction => (((from != null && paymentTransaction.DateCreated >= from) || (from == null)) &&
                    ((to != null && paymentTransaction.DateCreated <= to) || (to == null)))
                );
            return paymentTransactions.ToPaymentTransactionDTOs();
        }

        private async Task<Wallet> CheckForWallet(string uniqueMasterCitizenNumberValue, string password)
        {
            Wallet wallet = await _unitOfWork.WalletRepository.GetFirstWithIncludes(Wallet =>
                  Wallet.UniqueMasterCitizenNumber.Value == uniqueMasterCitizenNumberValue,
                  Wallet => Wallet.SupportedBank,
                  Wallet => Wallet.PaymentTransactions
            );
            if (wallet == null)
            {
                throw new ArgumentException($"Wallet with UMCN {uniqueMasterCitizenNumberValue} does not exist");
            }
            bool validPassword = wallet.VerifyPassword(password);
            if (!validPassword)
            {
                throw new WrongPasswordException();
            }

            return wallet;
        }
    }
}