using Dermayon.Sample.SocialMedia.User.Framework.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dermayon.Sample.SocialMedia.User.Framework.BLL.Contracts
{
    public interface IAccountUserBLL
    {
        Task AddAsync(AccountUserCreateRequest accountUser);        
        Task DeleteAsync(Guid Id);
        Task DeleteAll();        
        AccountUserRespond Detail(Guid Id);        
        List<AccountUserRespond> ListUser();        
    }
}
