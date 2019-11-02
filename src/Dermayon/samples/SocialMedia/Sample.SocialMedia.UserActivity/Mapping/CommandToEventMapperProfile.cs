using AutoMapper;
using Sample.SocialMedia.User.CqrsEventSourcing.Commands;
using Sample.SocialMedia.User.CqrsEventSourcing.Events;

namespace Sample.SocialMedia.User.Mapping
{
    public class CommandToEventMapperProfile : Profile
    {
        public CommandToEventMapperProfile()
        {
            CreateMap<CreateUserCommand, UserCreatedEvent>();
        }
    }
}
