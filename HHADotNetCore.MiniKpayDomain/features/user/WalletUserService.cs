using HHADotNetCore.MiniKpayDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHADotNetCore.MiniKpayDomain.features.user
{
    public class WalletUserService
    {
        private readonly AppDbContext _db;

        public WalletUserService(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<TblWalletUser> CreateUserAsync(TblWalletUser User)
        {
            _db.TblWalletUsers.Add(User);
            await _db.SaveChangesAsync();
            return User;
        }

        public async Task<TblWalletUser?> GetUserAsync(int userId)
        {
            var user = await _db.TblWalletUsers.FirstOrDefaultAsync(x => x.UserId == userId);
            return user;
        }

        public async Task<TblWalletUser?> ChangePinAsync(int id, TblWalletUser newPin)
        {
            var item = await _db.TblWalletUsers.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id)!;
            if (item is null)
            {
                return null;
            }

            item.PinCode = item.PinCode;
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }

        public async Task<TblWalletUser?> UpdateUserAsync(int id, TblWalletUser UpdatedUser)
        {
            var user = await _db.TblWalletUsers.FirstOrDefaultAsync(x => x.UserId == id)!;

            if (user is null)
            {
                return null;
            }

            user.UserName = UpdatedUser.UserName;
            user.MobileNumber = UpdatedUser.MobileNumber;

            _db.SaveChanges();
            return user;
        }
    }
}
