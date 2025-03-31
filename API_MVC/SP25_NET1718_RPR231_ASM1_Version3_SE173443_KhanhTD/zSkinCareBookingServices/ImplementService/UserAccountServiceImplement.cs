using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories_;
using zSkinCareBookingRepositories_.DTO;
using zSkinCareBookingRepositories_.Models;
using zSkinCareBookingServices_.InterfaceService;

namespace zSkinCareBookingServices_.ImplementService
{
    public class UserAccountServiceImplement : UserAccountServiceInterface
    {
        private readonly UserAccountRepository _userAccountRepository;

        public UserAccountServiceImplement(UserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
        public async Task<int> Authenticate(LoginDTO userAccount)
        {
            return await _userAccountRepository.Authenticate(userAccount);
        }

        public async Task<UserAccount> GetUserAccount(LoginDTO userAccount)
        {
            return await _userAccountRepository.GetUserAccount(userAccount);
        }
    }
}
