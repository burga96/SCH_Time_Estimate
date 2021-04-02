using Core.ApplicationServices.ApplicationExceptions;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.Domain.Entities;
using Core.Domain.ExternalInterfaces;
using Core.Domain.RepositoryInterfaces;
using System;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ApplicationServices
{
    public class PaymentTransactionService : IPaymentTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAPIDeterminator _bankAPIDeterminator;

        public PaymentTransactionService(IUnitOfWork unitOfWork,
            IBankAPIDeterminator bankAPIDeterminator)
        {
            _bankAPIDeterminator = bankAPIDeterminator;
            _unitOfWork = unitOfWork;
        }

        public async Task MakeDepositPaymentTransaction(string uniqueMasterCitizenNumberValue,
            string postalIndexNumber,
            string password,
            decimal amount)
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
            if (wallet.Password != password)
            {
                throw new WrongPasswordException("Wrong password");
            }
            IBankAPI bankAPI = _bankAPIDeterminator.DeterminateBankAPI(wallet.SupportedBank);
            bool successWithdrawal = await bankAPI.Withdraw(uniqueMasterCitizenNumberValue, postalIndexNumber, amount);
            if (!successWithdrawal)
            {
                throw new BankAPIException("Bank api - failed to withdrawal ");
            }
            DepositPaymentTransaction depositPaymentTransaction = wallet.MakeDepositTransaction(amount);
            await _unitOfWork.PaymentTransactionRepository.Insert(depositPaymentTransaction);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task MakeWithdrawalPaymentTransaction(string uniqueMasterCitizenNumberValue,
           string postalIndexNumber,
           string password,
           decimal amount)
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
            if (wallet.Password != password)
            {
                throw new WrongPasswordException("Wrong password");
            }
            IBankAPI bankAPI = _bankAPIDeterminator.DeterminateBankAPI(wallet.SupportedBank);
            bool successDeposit = await bankAPI.Deposit(uniqueMasterCitizenNumberValue, postalIndexNumber, amount);
            if (!successDeposit)
            {
                throw new BankAPIException("Bank api - failed to deposit");
            }
            WithdrawalPaymentTransaction withdrawalPaymentTransaction = wallet.MakeWithdrawalTransaction(amount);
            await _unitOfWork.PaymentTransactionRepository.Insert(withdrawalPaymentTransaction);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}