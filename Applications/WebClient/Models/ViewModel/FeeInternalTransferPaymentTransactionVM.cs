using Core.ApplicationServices.ApplicationDTOs;

namespace Applications.WebClient.Models.ViewModel
{
    public class FeeInternalTransferPaymentTransactionVM : InternalTransferPaymentTransactionVM
    {
        public FeeInternalTransferPaymentTransactionVM(FeeInternalTransferPaymentTransactionDTO paymentTransaction) : base(paymentTransaction)
        {
        }
    }
}