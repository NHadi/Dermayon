using AutoMapper;
using Dermayon.Common.Securities;
using Dermayon.Sample.SocialMedia.User.Framework.DAL;
using Dermayon.Sample.SocialMedia.User.Framework.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dermayon.Sample.SocialMedia.User
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountUser, AccountUserRespond>();            

            CreateMap<AccountUserCreateRequest, AccountUser>()
                .ForMember(x => x.Id, o => o.MapFrom(s => Guid.NewGuid()))
                .ForMember(x => x.Password, o => o.MapFrom(s => Encryptor.Encrypt(s.Password, null)));

        }
    }
}
