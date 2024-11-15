using BankPay.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPay.Domain.features
{
    public class TransactionHistoryService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public object GetTransactionHistory(string mobileNumber)
        {
            var user = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNumber == mobileNumber);
            if (user == null)
            {
                return new ErrorResponse { errorMessage = "User not found." };
            }

            var transactions = _db.TblTransactions
                .Where(x => x.SenderMobileNumber == mobileNumber || x.ReceiverMobileNumber == mobileNumber)
                .OrderByDescending(x => x.TransactionDate) 
                .Select(x => new
                {
                    x.TransactionNumber,
                    x.TransactionType,
                    x.SenderMobileNumber,
                    x.ReceiverMobileNumber,
                    x.Amount,
                    x.TransactionDate,
                    x.Status
                })
                .ToList();

            return transactions;
        }
    }
}
