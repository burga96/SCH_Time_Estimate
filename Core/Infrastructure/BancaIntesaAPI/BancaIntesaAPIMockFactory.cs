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
                bank.CheckStatus("2108995781057", "0612") == Task.FromResult(true) &&
                bank.CheckStatus("2108004781057", "0612") == Task.FromResult(true) &&
                bank.CheckStatus("2008996781057", "0613") == Task.FromResult(true) &&
                bank.CheckStatus("1908996781057", "1213") == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 5) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 10) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 20) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 30) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 40) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 50) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 60) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 70) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 80) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 90) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 100) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 1000) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 10000) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 20000) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 50000) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 100000) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 200000) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 500000) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 5) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 10) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 20) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 30) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 40) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 50) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 60) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 70) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 80) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 90) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 100) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 1000) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 10000) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 20000) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 50000) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 100000) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 200000) == Task.FromResult(true) &&
                bank.Withdraw("2108996781057", "0612", 500000) == Task.FromResult(true) &&

                bank.Deposit("2108995781057", "0612", 5) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 10) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 20) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 30) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 40) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 50) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 60) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 70) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 80) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 90) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 100) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 1000) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 10000) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 20000) == Task.FromResult(true) &&
                bank.Deposit("2108995781057", "0612", 50000) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 5) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 10) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 20) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 30) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 40) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 50) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 60) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 70) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 80) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 90) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 100) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 1000) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 10000) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 20000) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 50000) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 100000) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 200000) == Task.FromResult(true) &&
                bank.Withdraw("2108995781057", "0612", 500000) == Task.FromResult(true) &&
                bank.Deposit("2108996781057", "0612", 15) == Task.FromResult(false) &&
                bank.Deposit("2108996781057", "0612", 25) == Task.FromResult(false) &&
                bank.Deposit("2108996781057", "0612", 35) == Task.FromResult(false) &&
                bank.Deposit("2108996781057", "0612", 45) == Task.FromResult(false) &&
                bank.Deposit("2108996781057", "0612", 55) == Task.FromResult(false) &&
                bank.Deposit("2108996781057", "0612", 65) == Task.FromResult(false) &&
                bank.Deposit("2108996781057", "0612", 75) == Task.FromResult(false) &&
                bank.Deposit("2108996781057", "0612", 85) == Task.FromResult(false) &&
                bank.Deposit("2108996781057", "0612", 95) == Task.FromResult(false) &&
                bank.Withdraw("2108996781057", "0612", 15) == Task.FromResult(false) &&
                bank.Withdraw("2108996781057", "0612", 25) == Task.FromResult(false) &&
                bank.Withdraw("2108996781057", "0612", 35) == Task.FromResult(false) &&
                bank.Withdraw("2108996781057", "0612", 45) == Task.FromResult(false) &&
                bank.Withdraw("2108996781057", "0612", 55) == Task.FromResult(false) &&
                bank.Withdraw("2108996781057", "0612", 65) == Task.FromResult(false) &&
                bank.Withdraw("2108996781057", "0612", 75) == Task.FromResult(false) &&
                bank.Withdraw("2108996781057", "0612", 85) == Task.FromResult(false) &&
                bank.Withdraw("2108996781057", "0612", 95) == Task.FromResult(false)
            );
            return bankAPI;
        }
    }
}