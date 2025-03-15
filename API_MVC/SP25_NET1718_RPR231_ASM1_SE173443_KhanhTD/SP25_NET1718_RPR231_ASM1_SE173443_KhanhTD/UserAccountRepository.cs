using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories.Base;
using zSkinCareBookingRepositories.Models;

namespace zSkinCareBookingRepositories
{
	public class UserAccountRepository : GenericRepository<UserAccount>
	{
		public UserAccountRepository() { }

		public async Task<UserAccount> GetUserAccount(string userName, string password)
		{
			var userAccount = await _context.UserAccounts.FirstOrDefaultAsync(b => b.UserName == userName && b.Password == password && b.IsActive == true);
			return userAccount;
		}
	}
}
