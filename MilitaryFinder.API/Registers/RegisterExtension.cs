using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MilitaryFinder.API.Registers.Abstract;
using System;
using System.Linq;

namespace MilitaryFinder.API.Registers
{
    public static class RegisterExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Example of Reflection mechanism usability

            //Getting types of current assembly that implement IRegister interface and register services into application
            //Also here we are creating instace of each type using Activator class and cast these instances to IRegister type
            var typesImplementingServicesRegistration = typeof(Program).Assembly.GetTypes()
                .Where(type => typeof(IRegister).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(type => Activator.CreateInstance(type))
                .Cast<IRegister>()
                .ToList();

            //Call RegisterServices method for each of instances
            typesImplementingServicesRegistration.ForEach(x => x.RegisterServices(services, configuration));
        }
    }
}
