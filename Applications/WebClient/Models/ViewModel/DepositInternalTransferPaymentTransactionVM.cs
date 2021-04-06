using Core.ApplicationServices.ApplicationDTOs;

namespace Applications.WebClient.Models.ViewModel
{
    public class DepositInternalTransferPaymentTransactionVM : InternalTransferPaymentTransactionVM
    {
        public DepositInternalTransferPaymentTransactionVM(DepositInternalTransferPaymentTransactionDTO paymentTransaction) : base(paymentTransaction)
        {
        }
    }
}