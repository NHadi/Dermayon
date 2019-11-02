using AutoMapper;
using Dermayon.Infrastructure.Data.MongoRepositories.Contracts;
using Sample.SocialMedia.User.Framework.BLL.Contracts;
using Sample.SocialMedia.User.Framework.DAL;
using Sample.SocialMedia.User.Framework.DAL.Contracts;
using Sample.SocialMedia.User.Framework.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.SocialMedia.User.Framework.BLL
{
    public class AccountUserBLL : IAccountUserBLL
    {
        private readonly IUnitOfWorkMongo<UserContext> _uow;
        private readonly IMapper _mapper;
        private readonly IAccountUserDAL _accountUserDAL;
        public AccountUserBLL(IMapper mapper, 
            IUnitOfWorkMongo<UserContext> uow,
            IAccountUserDAL accountUserDAL)
        {
            _mapper = mapper;
            _uow = uow;
            _accountUserDAL = accountUserDAL;
        }
        public async Task AddAsync(AccountUserCreateRequest accountUser)
        {
            try
            {
                var dataInserted = _mapper.Map<AccountUser>(accountUser);                
                _accountUserDAL.Insert(dataInserted);
                await _uow.Commit();
            }
            catch (Exception ex)
            {
                // Log here
                throw ex;
            }
            
        }

        public async Task DeleteAsync(Guid Id)
        {
            try
            {
                var data = _accountUserDAL.GetById(Id);
                _accountUserDAL.Delete(data);
                await _uow.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task DeleteAll()
        {
            try
            {
                var data = _accountUserDAL.Get().ToList();
                _accountUserDAL.DeleteRange(data);
                await _uow.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AccountUserRespond Detail(Guid Id)
        {
            try
            {
                var data = _accountUserDAL.GetById(Id);
                var result = _mapper.Map<AccountUserRespond>(data);
                return result;
            }
            catch (Exception ex)
            {
                // Log Here
                throw ex;
            }
        }

        public List<AccountUserRespond> ListUser()
        {
            try
            {
                var data = _accountUserDAL.Get().ToList();
                var result = _mapper.Map<List<AccountUserRespond>>(data);
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
