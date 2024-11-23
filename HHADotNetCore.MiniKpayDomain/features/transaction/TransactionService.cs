using HHADotNetCore.MiniKpayDatabase.Models;
using HHADotNetCore.MiniKpayDomain.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHADotNetCore.MiniKpayDomain.features.transaction
{
    public class TransactionService
    {
        private readonly AppDbContext _db;

        public TransactionService(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<Result<ResultTransferResponseModel>> Transfer(int senderId, int receiverId, decimal amount, string pin)
        {
            Result<ResultTransferResponseModel> model = new Result<ResultTransferResponseModel>();

            var sender = await _db.TblWalletUsers.FirstOrDefaultAsync(x => x.UserId == senderId);
            var receiver = await _db.TblWalletUsers.FirstOrDefaultAsync(x => x.UserId == receiverId);

            if (sender == null || receiver == null)
            {
                model = Result<ResultTransferResponseModel>.ValidationError("Sender or Receiver not found.");
                goto Result;
            }

            if (sender.Balance < amount)
            {
                model = Result<ResultTransferResponseModel>.ValidationError("Insufficient balance.");
                goto Result;
            }

            if (sender.PinCode != pin)
            {
                model = Result<ResultTransferResponseModel>.ValidationError("Invalid PIN.");
                goto Result;
            }

            sender.Balance -= amount;
            receiver.Balance += amount;

            var Transaction = new TblTransaction
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Amount = amount,
                TransactionDate = DateTime.UtcNow
            };

            await _db.TblTransactions.AddAsync(Transaction);
            await _db.SaveChangesAsync();

            ResultTransferResponseModel item = new ResultTransferResponseModel()
            {
                Transaction = Transaction
            };
            model = Result<ResultTransferResponseModel>.Success(item, "Success.");
        Result:
            return model;
        }
    }
}
