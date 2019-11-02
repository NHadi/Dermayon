using AutoMapper;
using Sample.SocialMedia.User.CqrsEventSourcing.Commands;
using Sample.SocialMedia.User.Framework.DTO;

namespace Dermayon.Sample.SocialMedia.User.Mapping
{
    public class DtoToCommandMapperProfile : Profile
    {
        public DtoToCommandMapperProfile()
        {
            CreateMap<AccountUserCreateRequest, CreateUserCommand>();
        }
    }
}
