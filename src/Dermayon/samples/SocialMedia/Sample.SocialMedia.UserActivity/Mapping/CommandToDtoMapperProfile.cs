using AutoMapper;
using Sample.SocialMedia.User.CqrsEventSourcing.Commands;
using Sample.SocialMedia.User.Framework.DTO;

namespace Sample.SocialMedia.User.Mapping
{
    public class CommandToDtoMapperProfile : Profile
    {
        public CommandToDtoMapperProfile()
        {
            CreateMap<CreateUserCommand, AccountUserCreateRequest>();
        }
    }
}
