using Core.ApplicationServices.ApplicationDTOs;

namespace Applications.WebClient.Models.ViewModel
{
    public class WithdrawalInternalTransferPaymentTransactionVM : InternalTransferPaymentTransactionVM
    {
        public WithdrawalInternalTransferPaymentTransactionVM(WithdrawalInternalTransferPaymentTransactionDTO paymentTransaction) : base(paymentTransaction)
        {
        }
    }
}