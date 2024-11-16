using BankPay.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BankPay.Domain.features
{
    public class WithdrawService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public object CreateWithdraw(string mobileNumber, decimal amount, string pin)
        {
            var user = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNumber == mobileNumber);
            if (user == null)
                return new ErrorResponse { errorMessage = "User's mobile number not found." };

            if (user.Pin != pin)
                return new ErrorResponse { errorMessage = "Invalid PIN." };

            var depositAccount = _db.TblDeposits.FirstOrDefault(x => x.MobileNumber == mobileNumber && x.DeleteFlag == false);
            if (depositAccount == null)
                return new ErrorResponse { errorMessage = "Deposit account not found." };

            if (depositAccount.Balance < amount)
                return new ErrorResponse { errorMessage = "Insufficient balance." };

            depositAccount.Balance -= amount;
            _db.TblDeposits.Update(depositAccount);

            var transaction = new TblTransaction
            {
                TransactionNumber = Guid.NewGuid(),
                TransactionType = "Withdraw",
                SenderMobileNumber = mobileNumber,
                Amount = amount,
                Pin = pin,
                TransactionDate = DateTime.Now,
                Status = "Completed",
                DeleteFlag = false
            };

            _db.TblTransactions.Add(transaction);
            _db.SaveChanges();

            return new
            {
                Message = "Withdrawal successful",
                Transaction = transaction
            };
        }
    }
}
