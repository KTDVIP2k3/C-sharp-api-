using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories.Base;
using zSkinCareBookingRepositories_.DTO;
using zSkinCareBookingRepositories_.Models;

namespace zSkinCareBookingRepositories_
{
    public class UserAccountRepository : GenericRepository<UserAccount>
    {
        public UserAccountRepository() { }

        public async Task<int> Authenticate(LoginDTO userAccount)
        {
            var userInfo = await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserName == userAccount.UserName);
            if (userInfo == null)
            {
                return 0;
            }
            else if (userInfo.Password != userAccount.Password)
            {
                return 1;
            }
            return 2;
        }

        public async Task<UserAccount> GetUserAccount(LoginDTO userAccount)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserName == userAccount.UserName && u.Password == userAccount.Password);
        }
    }
}
