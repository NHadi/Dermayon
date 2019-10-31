using Dermayon.Common.Domain;
using Dermayon.Infrastructure.Data.MongoRepositories.Contracts;
using Dermayon.Infrastructure.EvenMessaging.Kafka.Contracts;
using Dermayon.Sample.SocialMedia.User.CqrsEventSourcing.Events;
using Dermayon.Sample.SocialMedia.User.Framework.DAL;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dermayon.Sample.SocialMedia.User.CqrsEventSourcing.Commands
{
    public class CreateUserCommand : ICommand
    {
        public Guid? Parent { get; set; }
        public string Username { get; set; }        
        public string Password { get; set; }        
        public string Name { get; set; }
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {        
        private readonly IKakfaProducer _kafkaProducer;        
        private readonly IMongoDbRepository<UserContextEvents, CreateUserCommand> _eventRepository;

        public CreateUserCommandHandler(IKakfaProducer kafkaProducer, IMongoDbRepository<UserContextEvents, CreateUserCommand> eventRepository)
        {
            _kafkaProducer = kafkaProducer;            
            _eventRepository = eventRepository;
        }

        public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {            
            await _eventRepository.InsertEvent(command, cancellationToken);
            await _kafkaProducer.Send(new UserCreatedEvent
            {
                Parent = command.Parent,
                Name = command.Name,
                Password = command.Password,
                Username = command.Username
            }, "UserServices");
        }
    }
}
