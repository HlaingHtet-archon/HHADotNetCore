using BankPay.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankPay.Domain.features
{
    public class DepositService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public object CreateDeposit(string mobileNumber, decimal amount)
        {
            var userAccount = _db.TblDeposits.FirstOrDefault(x => x.MobileNumber == mobileNumber && x.DeleteFlag == false);

            if (userAccount == null)
            {
                return new ErrorResponse { errorMessage = "User's account not found." };
            }

            if (amount <= 0)
            {
                return new ErrorResponse { errorMessage = "Invalid deposit amount." };
            }

            userAccount.Balance += amount;

            _db.TblDeposits.Update(userAccount);
            _db.SaveChanges();

            var transaction = new TblTransaction
            {
                TransactionNumber = Guid.NewGuid(),
                TransactionType = "Deposit",
                SenderMobileNumber = mobileNumber, 
                Amount = amount,
                TransactionDate = DateTime.Now,
                Status = "Completed",
                DeleteFlag = false
            };

            _db.TblTransactions.Add(transaction);
            _db.SaveChanges();

            return new
            {
                Message = "Deposit successful",
                AccountBalance = userAccount.Balance,
                Transaction = transaction
            };
        }
    }
}



