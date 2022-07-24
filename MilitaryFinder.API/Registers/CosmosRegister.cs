using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MilitaryFinder.API.Domain;
using MilitaryFinder.API.Registers.Abstract;
using MilitaryFinder.API.Services;

namespace MilitaryFinder.API.Registers
{
    public class CosmosRegister : IRegister
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var settings = new CosmosStoreSettings(configuration["CosmosDBSettings:DatabaseName"],
                configuration["CosmosDBSettings:AccountUri"],
                configuration["CosmosDBSettings:AuthKey"],
                new ConnectionPolicy
                {
                    ConnectionMode = ConnectionMode.Direct,
                    ConnectionProtocol = Protocol.Tcp
                });

            services.AddCosmosStore<CosmosFighterAircraftDto>(settings);
        }
    }
}
