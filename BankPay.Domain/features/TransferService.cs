using BankPay.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPay.Domain.features
{
    public class TransferService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public object CreateTransfer(string senderMobileNumber, string receiverMobileNumber, decimal amount, string pin)
        {
            var sender = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNumber == senderMobileNumber);
            if (sender == null)
            {
                return new ErrorResponse { errorMessage = "Sender's mobile number not found." };
            }

            if (sender.Pin != pin)
            {
                return new ErrorResponse { errorMessage = "Invalid PIN." };
            }

            var senderAccount = _db.TblDeposits.FirstOrDefault(x => x.MobileNumber == senderMobileNumber && x.DeleteFlag == false);
            if (senderAccount == null || senderAccount.Balance < amount)
            {
                return new ErrorResponse { errorMessage = "Insufficient balance." };
            }

            var receiver = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNumber == receiverMobileNumber);
            if (receiver == null)
            {
                return new ErrorResponse { errorMessage = "Receiver's mobile number not found." };
            }

            senderAccount.Balance -= amount;
            _db.TblDeposits.Update(senderAccount);

            var receiverAccount = _db.TblDeposits.FirstOrDefault(x => x.MobileNumber == receiverMobileNumber && x.DeleteFlag == false);
            if (receiverAccount == null)
            {
                receiverAccount = new TblDeposit
                {
                    MobileNumber = receiverMobileNumber,
                    Balance = amount,
                    DeleteFlag = false
                };
                _db.TblDeposits.Add(receiverAccount);
            }
            else
            {
                receiverAccount.Balance += amount;
                _db.TblDeposits.Update(receiverAccount);
            }

            var transaction = new TblTransaction
            {
                TransactionNumber = Guid.NewGuid(),
                TransactionType = "Transfer",
                SenderMobileNumber = senderMobileNumber,
                ReceiverMobileNumber = receiverMobileNumber,
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
                Message = "Transfer successful",
                Transaction = transaction
            };
        }
    }
}
