using BankPay.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BankPay.Domain.features
{
    public class UserService
    {
        private readonly AppDbContext _db = new AppDbContext();
        public List<TblUser> GetUsers()
        {
            var result = _db.TblUsers.AsNoTracking().ToList();
            return result;
        }

        public TblUser GetUser(int id)
        {
            var item = _db.TblUsers
                .AsNoTracking()
                .FirstOrDefault(x => x.UserId == id);
            return item;
        }

        public object CreateUser(TblUser user)
        {
            var existingAccount = _db.TblUsers.FirstOrDefault(x => x.MobileNumber == user.MobileNumber);

            if (existingAccount != null)
            {
                var errorResponse = new ErrorResponse
                {
                    errorMessage = "Mobile number is already taken." 
                };
                return errorResponse;
            }

            _db.TblUsers.Add(user);
            _db.SaveChanges();

            return user;
        }

        public TblUser UpdateUser(int userId, TblUser user)
        { 
            var existingUser = _db.TblUsers.FirstOrDefault(x => x.UserId == userId);

            if (existingUser == null)
            {
                return null;
            }

            if (user.Balance < 0)
            {
                return null;
            }

            existingUser.FullName = user.FullName;
            existingUser.MobileNumber = user.MobileNumber;
            existingUser.Pin = user.Pin;
            existingUser.Balance = user.Balance;

            _db.Entry(existingUser).State = EntityState.Modified;
            _db.SaveChanges();

            return existingUser;
        }

        public TblUser PatchUser(int userId, TblUser user)
        {
            var existingUser = _db.TblUsers.FirstOrDefault(x => x.UserId == userId);

            if (existingUser == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(user.FullName))
            {
                existingUser.FullName = user.FullName;
            }
            if (!string.IsNullOrEmpty(user.MobileNumber))
            {
                existingUser.MobileNumber = user.MobileNumber;
            }
            if (!string.IsNullOrEmpty(user.Pin))
            {
                existingUser.Pin = user.Pin;
            }    
            if(user.Balance.HasValue)
            {
                existingUser.Balance = user.Balance;
            }
           
            _db.Entry(existingUser).State = EntityState.Modified;
            _db.SaveChanges();

            return existingUser;
        }

        public bool? DeleteUser(int id)
        {
            var item = _db.TblUsers
                .AsNoTracking()
                .FirstOrDefault(x => x.UserId == id);
            if (item == null)
            {
                return null;
            }

            item.DeleteFlag = true;

            _db.Entry(item).State = EntityState.Modified;
            int result = _db.SaveChanges();
            return result > 0;
        }
    }
}

