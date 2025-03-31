using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories_.DTO;
using zSkinCareBookingRepositories_.Models;

namespace zSkinCareBookingServices_.InterfaceService
{
    public interface UserAccountServiceInterface
    {
        Task<int> Authenticate(LoginDTO userAccount);

        Task<UserAccount> GetUserAccount(LoginDTO userAccount);
    }
}
