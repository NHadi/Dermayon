using AutoMapper;
using Dermayon.Common.Securities;
using Sample.SocialMedia.User.Framework.DAL;
using Sample.SocialMedia.User.Framework.DTO;
using System;

namespace Dermayon.Sample.SocialMedia.User.Mapping
{
    public class DtoToDomainMapperProfile : Profile
    {
        public DtoToDomainMapperProfile()
        {
            CreateMap<AccountUser, AccountUserRespond>();

            CreateMap<AccountUserCreateRequest, AccountUser>()
                .ForMember(x => x.Id, o => o.MapFrom(s => Guid.NewGuid()))
                .ForMember(x => x.Password, o => o.MapFrom(s => Encryptor.Encrypt(s.Password, null)));
        }
    }
}
