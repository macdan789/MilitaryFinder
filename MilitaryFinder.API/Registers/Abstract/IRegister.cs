using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MilitaryFinder.API.Registers.Abstract
{
    public interface IRegister
    {
        void RegisterServices(IServiceCollection services, IConfiguration Configuration);
    }
}
