using AutoMapper;
using Dermayon.Common.Domain;
using Dermayon.Infrastructure.EvenMessaging.Kafka;
using Dermayon.Infrastructure.EvenMessaging.Kafka.Contracts;
using Dermayon.Sample.SocialMedia.User.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sample.SocialMedia.User.CqrsEventSourcing.Commands;
using Sample.SocialMedia.User.CqrsEventSourcing.EventHanders;
using Sample.SocialMedia.User.CqrsEventSourcing.Events;
using Sample.SocialMedia.User.Framework.BLL;
using Sample.SocialMedia.User.Framework.BLL.Contracts;
using Sample.SocialMedia.User.Framework.DAL;
using Sample.SocialMedia.User.Framework.DAL.Contracts;
using Sample.SocialMedia.User.Mapping;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Sample.SocialMedia.UserActivity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InitDermayonBootsraper()
                    // Set Kafka Configuration
                   .InitKafka()
                       .Configure<KafkaEventConsumerConfiguration>(Configuration.GetSection("KafkaConsumer"))
                       .Configure<KafkaEventProducerConfiguration>(Configuration.GetSection("KafkaProducer"))
                   .RegisterKafkaConsumer<UserCreatedEvent, UserCreatedEventHandler>()
                   // Implement CQRS Event Sourcing => UserContextEvents [Commands]
                   .RegisterEventSources()
                       .RegisterMongoContext<UserContextEvents, UserContextEventSetting>
                            (Configuration.GetSection("ConnectionStrings:AccountUserEvents")
                           .Get<UserContextEventSetting>())
                   // Implement CQRS Event Sourcing => UserContext [Query] & 
                   .RegisterMongo()
                       .RegisterMongoContext<UserContext, UserContextSetting>
                           (Configuration.GetSection("ConnectionStrings:AccountUser")
                           .Get<UserContextSetting>());

            ConfigureCommandHandlers(services);
            ConfigureEventHandlers(services);
            ConfigureApplicationServices(services);
            ConfigureAutoMapper(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddOpenApiDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseMvc();
        }
        
        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CommandToDtoMapperProfile());
                cfg.AddProfile(new CommandToEventMapperProfile());
                cfg.AddProfile(new DtoToCommandMapperProfile());
                cfg.AddProfile(new DtoToDomainMapperProfile());

            });
            mapperConfig.AssertConfigurationIsValid();
            services.AddSingleton(provider => mapperConfig.CreateMapper());
        }
        private static void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IAccountUserBLL, AccountUserBLL>();
            services.AddScoped<IAccountUserDAL, AccountUserDAL>();

        }
        private static void ConfigureEventHandlers(IServiceCollection services)
        {
            services.AddTransient<UserCreatedEventHandler>();
        }
        private static void ConfigureCommandHandlers(IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
        }
    }
}
