using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MilitaryFinder.API.Registers.Abstract;
using System;

namespace MilitaryFinder.API.Registers
{
    public class MvcRegister : IRegister
    {
        public void RegisterServices(IServiceCollection services, IConfiguration Configuration)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Military Finder API",
                    Description = "Simple RESTful API project",
                    Contact = new OpenApiContact
                    {
                        Name = "Bohdan Marko",
                        Email = "markopollo789@gmail.com",
                        Url = new Uri("https://github.com/macdan789"),
                    }
                });
                config.SwaggerDoc("test", new OpenApiInfo
                {
                    Version = "test",
                    Title = "test title",
                    Description = "test description",
                    Contact = new OpenApiContact
                    {
                        Name = "Bohdan Marko",
                        Email = "markopollo789@gmail.com",
                        Url = new Uri("https://github.com/macdan789"),
                    }
                });
            });
        }
    }
}
