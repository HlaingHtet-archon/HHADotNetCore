using HHADotNetCore.MiniKpayDatabase.Models;
using HHADotNetCore.MiniKpayDomain.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static HHADotNetCore.MiniKpayDomain.model.TransactionResponseModel;

namespace HHADotNetCore.MiniKpayDomain.features.transaction
{
    public class TransactionService
    {
        private readonly AppDbContext _db;

        public TransactionService(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<Result<ResultTransferResponseModel>> TransferAsync(int senderId, int receiverId, decimal amount, string pin)
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

            _db.TblTransactions.Add(Transaction);
            await _db.SaveChangesAsync();

            ResultTransferResponseModel item = new ResultTransferResponseModel()
            {
                Transaction = Transaction
            };
            model = Result<ResultTransferResponseModel>.Success(item, "Success.");
        Result:
            return model;
        }

        public async Task<Result<ResultTransactionResponseModel>> WithdrawAsync(int userId, decimal amount, string pin)
        {
            Result<ResultTransactionResponseModel> model = new Result<ResultTransactionResponseModel>();

            var user = await _db.TblWalletUsers.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
            {
                model = Result<ResultTransactionResponseModel>.ValidationError("User not found");
                goto Result;
            }

            if (user.Balance < amount)
            {
                model = Result<ResultTransactionResponseModel>.ValidationError("Insufficient balance.");
                goto Result;
            }

            if (user.PinCode != pin)
            {
                model = Result<ResultTransactionResponseModel>.ValidationError("Invalid PIN.");
                goto Result;
            }

            user.Balance -= amount;

            var Transaction = new TblTransaction
            {
                SenderId = userId,
                Amount = amount,
                TransactionDate = DateTime.Now,
                TransactionType = "Withdraw"
            };

            _db.TblTransactions.Add(Transaction);
            await _db.SaveChangesAsync();

            ResultTransactionResponseModel item = new ResultTransactionResponseModel()
            {
                Transaction = Transaction
            };
            model = Result<ResultTransactionResponseModel>.Success(item, "Success.");
        Result:
            return model;
        }

        public async Task<Result<ResultTransactionResponseModel>> DepositAsync(int userId, decimal amount)
        {

            Result<ResultTransactionResponseModel> model = new Result<ResultTransactionResponseModel>();

            var user = await _db.TblWalletUsers.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
            {
                model = Result<ResultTransactionResponseModel>.ValidationError("User not found");
                goto Result;
            }


            if (user.Balance < amount)
            {
                model = Result<ResultTransactionResponseModel>.ValidationError("Insufficient balance.");
                goto Result;
            }
            user.Balance += amount;

            var Transaction = new TblTransaction
            {
                SenderId = userId,
                Amount = amount,
                TransactionDate = DateTime.Now,
                TransactionType = "Withdraw"
            };
            _db.TblTransactions.Add(Transaction);
            await _db.SaveChangesAsync();

            ResultTransactionResponseModel item = new ResultTransactionResponseModel()
            {
                Transaction = Transaction
            };
            model = Result<ResultTransactionResponseModel>.Success(item, "Success.");
        Result:
            return model;
        }
    }
}
