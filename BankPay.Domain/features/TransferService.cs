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
            var Sender = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNumber == senderMobileNumber && x.DeleteFlag == false);
            var Receiver = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNumber == receiverMobileNumber && x.DeleteFlag == false);

            if (Sender == null || Receiver == null)
                return new ErrorResponse { errorMessage = "Mobile phone number doesn't exist." };

            if (Sender.MobileNumber == Receiver.MobileNumber)
                return new ErrorResponse { errorMessage = "Mobile phone numbers cannot be the same!" };

            if (Sender.Pin != pin)
                return new ErrorResponse { errorMessage = "Incorrect PIN!" };

            if (amount > Sender.Balance || amount < 100)
                    return new ErrorResponse { errorMessage = "Invalid amount!" };

                Sender.Balance -= amount;
                Receiver.Balance += amount;

                _db.TblUsers.Update(Sender);
                _db.TblUsers.Update(Receiver);
                _db.SaveChanges();

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

            return "Transaction successfully completed";
        }
    }
}
