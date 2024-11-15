using BankPay.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPay.Domain.features
{
    public class DepositService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public object CreateDeposit(TblDeposit deposit)
        {
            var user = _db.TblUsers.FirstOrDefault(x => x.MobileNumber == deposit.MobileNumber);

            if (user == null)
            {
                return new ErrorResponse { errorMessage = "User not found." }; 
            }

            if (deposit.Balance <= 0)
            {
                return new ErrorResponse { errorMessage = "Deposit amount must be greater than zero." };  
            }

            deposit.DeleteFlag = false; 
            _db.TblDeposits.Add(deposit);
            _db.SaveChanges();

            user.Balance += deposit.Balance;
            _db.SaveChanges();

            return deposit; 
        }
    }
}
