using AutoMapper;
using Dermayon.Sample.SocialMedia.User.Framework.BLL;
using Dermayon.Sample.SocialMedia.User.Framework.BLL.Contracts;
using Dermayon.Sample.SocialMedia.User.Framework.DAL;
using Dermayon.Sample.SocialMedia.User.Framework.DAL.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dermayon.Sample.SocialMedia.User
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
                    .RegisterMongo()
                        // Implement CQRS Event Sourcing => UserContext [Query] & UserContextEvents [Commands]
                        .RegisterMongoContext<UserContext, UserContextSetting>
                            (Configuration.GetSection("ConnectionStrings:AccountUser")
                                          .Get<UserContextSetting>())
                        .RegisterMongoContext<UserContextEvents, UserContextEventSetting>
                            (Configuration.GetSection("ConnectionStrings:AccountUserEvents")
                                          .Get<UserContextEventSetting>());

            services.AddScoped<IAccountUserBLL, AccountUserBLL>();
            services.AddScoped<IAccountUserDAL, AccountUserDAL>();

            //register automapper
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddControllers();
            services.AddOpenApiDocument();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
