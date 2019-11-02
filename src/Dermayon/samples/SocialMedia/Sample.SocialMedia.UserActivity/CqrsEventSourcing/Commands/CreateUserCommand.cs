using Dermayon.Common.Domain;
using Dermayon.Common.Infrastructure.Data.Contracts;
using Dermayon.Infrastructure.EvenMessaging.Kafka.Contracts;
using Sample.SocialMedia.User.CqrsEventSourcing.Events;
using Sample.SocialMedia.User.Framework.DAL;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.SocialMedia.User.CqrsEventSourcing.Commands
{
    public class CreateUserCommand : ICommand
    {
        public string Username { get; set; }        
        public string Password { get; set; }        
        public string Name { get; set; }
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {        
        private readonly IKakfaProducer _kafkaProducer;        
        private readonly IEventRepository<UserContextEvents, UserCreatedEvent> _eventUserRepository;

        public CreateUserCommandHandler(IKakfaProducer kafkaProducer, IEventRepository<UserContextEvents, UserCreatedEvent> eventUserRepository)
        {
            _kafkaProducer = kafkaProducer;
            _eventUserRepository = eventUserRepository;
        }

        public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var userCreatedEvent = new UserCreatedEvent
            {
                Name = command.Name,
                Password = command.Password,
                Username = command.Username,      
            };

            await _eventUserRepository.InserEvent(userCreatedEvent, cancellationToken);
            await _kafkaProducer.Send(userCreatedEvent, "SocialMediaServices");
        }
    }
}
