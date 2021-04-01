using Core.Domain.ExternalInterfaces;
using Moq;
using System.Threading.Tasks;

namespace BancaIntesaAPI
{
    public static class BancaIntesaAPIMockFactory
    {
        public static IBankAPI Create()
        {
            var bankAPI = Mock.Of<IBankAPI>
            (
                bank => bank.CheckStatus("2108996781057", "0612") == Task.FromResult(true) &&
                bank.CheckStatus("2108004781057", "0612") == Task.FromResult(true) &&
                bank.CheckStatus("2008996781057", "0613") == Task.FromResult(true) &&
                bank.CheckStatus("1908996781057", "1213") == Task.FromResult(true)

            );

            return bankAPI;
        }
    }
}