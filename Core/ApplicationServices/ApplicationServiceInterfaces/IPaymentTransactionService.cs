using Core.ApplicationServices.ApplicationDTOs;
using Core.Domain.Entities;
using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ApplicationServiceInterfaces
{
    public interface IPaymentTransactionService
    {
        Task<DepositPaymentTransactionDTO> MakeDepositPaymentTransaction(string uniqueMasterCitizenNumber,
            string password,
            decimal amount);

        Task<WithdrawalPaymentTransactionDTO> MakeWithdrawalPaymentTransaction(string uniqueMasterCitizenNumber,
            string password,
            decimal amount);

        Task MakeInternalTransferPaymentTransaction(string fromUniqueMasterCitizenNumber,
            string password,
            string secondUniqueMasterCitizenNumber,
            decimal amount);

        Task<WalletDTO> GetWalletWithFiltertedPaymentTransactionsForUser(string uniqueMasterCitizenNumberValue,
            string password,
            DateTime? from,
            DateTime? to);

        Task<IEnumerable<PaymentTransactionDTO>> GetAllPaymentTransactions(DateTime? from,
            DateTime? to);
    }
}