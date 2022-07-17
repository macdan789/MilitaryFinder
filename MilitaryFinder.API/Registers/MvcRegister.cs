using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MilitaryFinder.API.Registers.Abstract;
using MilitaryFinder.API.Services;
using MilitaryFinder.API.Settings;
using System;
using System.Text;

namespace MilitaryFinder.API.Registers
{
    public class MvcRegister : IRegister
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            //For fun
            var secret = configuration["JwtSettings:Secret"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });

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

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                var securityRequirments = new OpenApiSecurityRequirement();
                        securityRequirments.Add(new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>());

                config.AddSecurityRequirement(securityRequirments);
            });

            //services.AddScoped<IFighterAircraftService, FighterAircraftService>();
            //It is recommended to use Singleton for CosmosDB
            services.AddSingleton<IFighterAircraftService, FighterAircraftCosmosDBService>();
        }
    }
}
