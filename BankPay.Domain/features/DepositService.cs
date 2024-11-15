﻿using BankPay.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankPay.Domain.features
{
    public class DepositService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public object CreateDeposit(string mobileNumber, decimal Balance)
        {
            var user = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNumber == mobileNumber && x.DeleteFlag == false);

            if (user != null)
            {
                if (Balance > 0)
                {
                    var deposit = new TblDeposit
                    {
                        MobileNumber = mobileNumber,
                        Balance = Balance
                    };

                    _db.TblDeposits.Add(deposit);
                    _db.SaveChanges();

                    return deposit;
                }
                else
                {
                    var error = new ErrorResponse
                    {
                        errorMessage = "Invalid Balance."
                    };
                    return error;
                }
            }
            else
            {
                var error = new ErrorResponse
                {
                    errorMessage = "Wrong Phone Number."
                };
                return error;
            }
        }
    }
}
