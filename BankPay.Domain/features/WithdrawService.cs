using BankPay.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPay.Domain.features
{
    public class WithdrawService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public object CreateWithdraw(string mobileNumber, decimal Balance)
        {
            var user = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNumber == mobileNumber && x.DeleteFlag == false);

            if (user != null)
            {
                if (Balance > 0 && user.Balance >= Balance)
                {
                    user.Balance -= Balance;

                    var withdraw = new TblWithdraw
                    {
                        MobileNumber = mobileNumber,
                        Balance = Balance
                    };

                    _db.TblWithdraws.Add(withdraw);
                    _db.TblUsers.Update(user); 
                    _db.SaveChanges();
                }
                else
                {
                    var error = new ErrorResponse
                    {
                        errorMessage = "Insufficient balance or invalid amount."
                    };
                    return error;
                }
            }
            else
            {
                var error = new ErrorResponse
                {
                    errorMessage = "Invalid phone number."
                };
                return error;
            }

            return new { message = "Withdrawal successful", balance = user.Balance };
        }

    }
}
